using Godot;
using System;

public partial class Global : Node
{
    // variables de interfaz
    public int score;
    public int lives;

    public override void _Process(double delta)
    {
        GD.Print(score);
    }

}
