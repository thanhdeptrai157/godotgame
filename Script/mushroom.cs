using Godot;
using System;
using System.Threading.Tasks;

public partial class mushroom : CharacterBody2D
{
	public const float Speed = -60.0f;
	
	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;
	
		Velocity = velocity;
		MoveAndSlide();
	}
	public async void _body_entered(Node body){
		if(body is CharacterBody2D){
			var y_delta = Position.Y - ((CharacterBody2D)body).Position.Y;
			if(y_delta > 0){
				var Mushroom = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
				Mushroom.Play("hitted");
				((player)body).Jump();
				await Task.Delay(100);
				this.QueueFree();
				
			}
			else if(y_delta < 0){
				((player)body).hitted();
			}
		}	
	}
}
