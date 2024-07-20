using Godot;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Transactions;

public partial class player : CharacterBody2D
{
	public const float Speed = 100.0f;
	public const float JumpVelocity = -300.0f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	private int max_jump = 2;
	private int jump_count = 0;
	[Export] public PackedScene Particle;
	public override void _PhysicsProcess(double delta)
	{
		var player = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		Vector2 velocity = Velocity;
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		// Add the gravity.
		if (!IsOnFloor()){
			velocity.Y += gravity * (float)delta;
			if(jump_count == 2){
				player.Play("doublejump");
			}
			else{
				player.Play("jump");
			}
		}
		else
		{
			jump_count = 0;
			if (direction != Vector2.Zero)
			{
				player.Play("run");
			}
			else
			{
				player.Play("idle");
				
			}
			
		}
		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && jump_count < max_jump){
			velocity.Y = JumpVelocity;
			jump_count++;
		}
		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
	
		if(direction.X < 0){
			player.FlipH = true;
		}
		if(direction.X > 0){
			player.FlipH = false;
		}
		if(velocity.Y > 0f){
			player.Play("fall");
		}
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
			run();
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);	
		}
		Velocity = velocity;
		MoveAndSlide();
	}
	public void Jump(){
		Vector2 velocity = Velocity;
		velocity.Y = JumpVelocity;
		Velocity = velocity;
		MoveAndSlide();
	}
	public void hitted(){
		var player = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		player.Play("ishitted");
		//await Task.Delay(1000);
		var position = Position;
		if(!player.FlipH){
			position.X -=10;
			Position = position;
		}
		else{
			position.X +=10;
			Position = position;
		}
		GD.Print("Hit");
	}
	public async void run()
    {
		var player = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        var particleNode = Particle.Instantiate<AnimatedSprite2D>();
		if(!player.FlipH){
			particleNode.Position = Position + new Vector2(-10, 0);
		}
		else{
			particleNode.Position = Position + new Vector2(10, 0);
		}
       
        GetParent().AddChild(particleNode);

        //var timer = GetTree().CreateTimer(0.3f);
        await Task.Delay(100);
        particleNode.QueueFree();
    }
}
