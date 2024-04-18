using Godot;
using System;

public partial class Player : CharacterBody2D
{
    // variables
    private AnimatedSprite2D _animationController;
    // private AudioStreamPlayer _audioController;
    public AudioStreamPlayer audioController = new AudioStreamPlayer();
    public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

    // methods
    public override void _Ready()
    {
        _animationController = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        // _audioController = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
        AddChild(audioController);
    }
    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Velocity;

        velocity.Y += gravity * (float)delta;
        
        if(Input.IsActionPressed("right"))
        {            
            _animationController.Play("walk");
            _animationController.FlipH = false;
        }
        else if(Input.IsActionPressed("left"))
        {
            _animationController.Play("walk");
            _animationController.FlipH = true;
        }
        else
        {
            _animationController.Play("idle");
        }

        if (Input.IsActionJustPressed("jump"))
        {
            _animationController.Play("jump");
            AudioStreamWav effect = GD.Load<AudioStreamWav>("res://Assets/Audios/Effects/Jump 1.wav");
            audioController.Stream = effect;
            audioController.Play();
        }

        Velocity = velocity;
        MoveAndSlide();

        GD.Print(Position);
    }
}
