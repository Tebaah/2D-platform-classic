using Godot;
using System;

public partial class LevelButton : TextureButton
{

    private Global _global;
    [Export] public int levelId;
    public override void _Ready()
    {
        _global = GetNode<Global>("/root/Global");

        if(levelId == _global.currentLevel)
        {
            Disabled = false;
        }
        else
        {
            Disabled = true;
        }

    }

    public void OnButtonPressed()
    {
        _global.levelId = levelId;
        GetTree().ChangeSceneToFile("res://Scenes/Levels/game.tscn");
        
    }
}
