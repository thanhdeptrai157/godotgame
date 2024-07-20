using Godot;

public partial class cameracontroller : Camera2D
{
    [Export]
    public NodePath PlayerPath;
    
    private Node2D player;

    public override void _Ready()
    {
        player = GetNode<Node2D>(PlayerPath);
    }
    public override void _Process(double delta)
    {
        if (player != null)
        {
            // Get the current position of the camera
            Vector2 cameraPosition = Position;

            // Update the x position of the camera to match the player's x position
            cameraPosition.X = player.Position.X;

            // Set the camera's position
            Position = cameraPosition;
        }
    }
}
