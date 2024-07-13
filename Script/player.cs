using Godot;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;

public partial class player : CharacterBody2D
{
	public const float Speed = 100.0f;
	public const float JumpVelocity = -300.0f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	private int max_jump = 2;
	private int jump_count = 0;
	private bool isDoubleJump = false;
	public override void _PhysicsProcess(double delta)
	{
		var player = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		Vector2 velocity = Velocity;
		
		// Add the gravity.
		if (!IsOnFloor()){
			velocity.Y += gravity * (float)delta;
			
		}
		else{
			jump_count = 0;
			isDoubleJump = false;
		}
		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && jump_count < max_jump){
			velocity.Y = JumpVelocity;
			jump_count++;
			if(jump_count == 2){
				isDoubleJump = true;
			}
		}
		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if(direction.X < 0){
			player.FlipH = true;
		}
		if(direction.X > 0){
			player.FlipH = false;
		}
		
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
			player.Play("run");
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			player.Play("idle");
		}
		if(velocity.Y < 0f){
			player.Play("jump");
			
		}
		if(velocity.Y > 0f){
			player.Play("fall");
		}
		if(isDoubleJump){
			player.Play("doublejump");
			isDoubleJump = false;
		}
		Velocity = velocity;
		MoveAndSlide();
	}
}
