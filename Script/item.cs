using Godot;
using System;
using System.Threading.Tasks;
public partial class item : Area2D
{
	[Export] public PackedScene Particle;
	public void on_body_entered(Node body){
		this.QueueFree();
		collected();
	}
	public async void collected()
    {
        var particleNode = Particle.Instantiate<AnimatedSprite2D>();
        particleNode.Position = Position;
        GetParent().AddChild(particleNode);

        //var timer = GetTree().CreateTimer(0.3f);
        await Task.Delay(500);
        particleNode.QueueFree();
    }
	
}
