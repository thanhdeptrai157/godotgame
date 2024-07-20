using Godot;
using System;

public partial class ChangeScene : Area2D
{	
	[Export] public PackedScene targetScene;
	public void on_body_entered(Node body){
		if(body is CharacterBody2D){
			GetTree().ChangeSceneToPacked(targetScene);
		}
	}
}
