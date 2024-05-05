using Godot;
using System;

public partial class Global : Node
{
    // variables de interfaz
    public int score = 0;
    public int lifes = 3;
    public int levelId; // es igual a 1

    public int currentLevel {get; set;} = 1;

    public PackedScene[] levels = new PackedScene[4];

    public override void _Ready()
    {
        levels[0] = GD.Load<PackedScene>("res://Scenes/Levels/level_00.tscn");
        levels[1] = GD.Load<PackedScene>("res://Scenes/Levels/level_01.tscn");
        levels[2] = GD.Load<PackedScene>("res://Scenes/Levels/level_02.tscn");
        levels[3] = GD.Load<PackedScene>("res://Scenes/Levels/level_03.tscn");

    }
}
