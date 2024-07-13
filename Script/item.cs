using Godot;
using System;


public partial class item : Area2D
{
	public void on_body_entered(Node body){
		this.QueueFree();
	}	
}
