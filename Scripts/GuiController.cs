using Godot;
using System;

public partial class GuiController : Node
{
    // variables Globales
    private Global _global;

    // variables de nodos hijos
    private Label _scoreLabel;

    public override void _Ready()
    {
        // inicializa el controlador global
        _global = GetNode<Global>("/root/Global");
        _scoreLabel = GetNode<Label>("CanvasLayer/PanelContainer/HBoxContainer/Score");
    }    

    public override void _Process(double delta)
    {
        int score = _global.score;
        _scoreLabel.Text = $"Score: {score}";
    }
}
