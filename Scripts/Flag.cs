using Godot;
using System;

public partial class Flag : Area2D
{
    // variable de animacion
    private AnimatedSprite2D _animationController;

    public override void _Ready()
    {
        // inicializa la variable de animacion
        _animationController = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    }

    public void OnPlayerEntered(Node body)
    {
        if(body.Name == "Player")
        {
            // reproduce la animacion
            _animationController.Play("move");
        }
    }
}
