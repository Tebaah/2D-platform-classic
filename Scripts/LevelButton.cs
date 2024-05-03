using Godot;
using System;

public partial class LevelButton : TextureButton
{

    private Global _global;
    public Label labelButton;

    public override void _Ready()
    {
        labelButton = GetNode<Label>("Label");
        _global = GetNode<Global>("/root/Global");
    }
    public void OnButtonPressed()
    {
        _global.levelId = int.Parse(labelButton.Text);
        GetTree().ChangeSceneToFile("res://Scenes/Levels/game.tscn");
    }
}
