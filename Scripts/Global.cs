using Godot;
using System;

public partial class Global : Node
{
    // variables de interfaz
    public int score = 0;
    public int lifes = 3;
    public int levelId;

    public int levelCurrent;

    public PackedScene[] levels = new PackedScene[3];

    public override void _Ready()
    {
        levels[0] = GD.Load<PackedScene>("res://Scenes/Levels/level_01.tscn");
        levels[1] = GD.Load<PackedScene>("res://Scenes/Levels/level_02.tscn");
        levels[2] = GD.Load<PackedScene>("res://Scenes/Levels/level_03.tscn");
    }

    public override void _Process(double delta)
    {
        if(levelCurrent < levelId)
        {

        }
        levelCurrent = levelId;
        GD.Print($"levelCurrent {levelCurrent}");
    }
}
