using Godot;
using System;

public partial class Menu : Node
{
       public void OnButtonPressed()
    {
        GetTree().ChangeSceneToFile("res://Scenes/Levels/game.tscn");
    }
}
