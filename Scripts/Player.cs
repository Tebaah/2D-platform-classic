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
        
        CharacterController(delta);

    }

    public void CharacterController(double delta)
    {
        Vector2 velocity = Velocity;

        float direction = Input.GetAxis("left", "right");
        velocity.X = direction * 100;

        if(!IsOnFloor())
        {
            velocity.Y += gravity * (float)delta;
        }

        if(Input.IsActionJustPressed("jump") && IsOnFloor())
        {
            velocity.Y = -500;
            _animationController.Play("jump");
            AudioStreamWav effect = GD.Load<AudioStreamWav>("res://Assets/Audios/Effects/Jump 1.wav");
            audioController.Stream = effect;
            audioController.Play();
        }

        Velocity = velocity;
        MoveAndSlide();

    }
}
