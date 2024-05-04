using Godot;
using System;

public partial class ChangeToSelector : Button
{

    public void OnButtonPressed()
    {
        GetTree().ChangeSceneToFile("res://Scenes/Gui/level_selector.tscn");
        
    }
}
