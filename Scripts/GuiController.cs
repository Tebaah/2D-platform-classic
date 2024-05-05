using Godot;
using System;

public partial class GuiController : Node
{
    // variables Globales
    private Global _global;

    // variables de nodos hijos
    private Label _scoreLabel;
    private Label _livesLabel;
    private Label _timeLabel;
    public int time = 300;
    public bool loop = true;
    public float lapso;
    public int contador = 0;

    public override void _Ready()
    {
        // inicializa el controlador global
        _global = GetNode<Global>("/root/Global");
        _scoreLabel = GetNode<Label>("CanvasLayer/Control/PanelContainer/HBoxContainer/Score");
        _livesLabel = GetNode<Label>("CanvasLayer/Control/PanelContainer/HBoxContainer/Life");
        _timeLabel = GetNode<Label>("CanvasLayer/Control/PanelContainer/HBoxContainer/Time");

        lapso = 1.0f;
    }    

    public override void _Process(double delta)
    {
        // actualiza los valores de la interfaz
        int score = _global.score;
        _scoreLabel.Text = $"Score: {score}";

        int life = _global.lifes;
        _livesLabel.Text = $"Lifes: {life}";

        
        UpdateTimer(delta);



    }

    public void UpdateTimer(double delta)
    {    
        _timeLabel.Text = $"Time: {time}";
        
        lapso -= (float)delta;

        if(lapso <= 0)
        {
            if(loop)
            {   
                lapso = 1.0f;
                _timeLabel.Text = $"Time: {time}";
                time -= 1;
            }
        }
  
    }
}
