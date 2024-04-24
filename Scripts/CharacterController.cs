using Godot;
using System;

public partial class CharacterController : CharacterBody2D
{
    // variables de movimiento
    [Export] public float speedWalk;
    [Export] public float speedJump;
 

    // variables de animacion y audio
    private AnimatedSprite2D _animationController;
    public AudioStreamPlayer audioController = new AudioStreamPlayer();
    [Export]public AudioStreamWav[] effects;

    // variables de colision
    public bool collisionWithMobs;
    public float collisionMobsY;
    public float collisionMobsX;
    public bool isNextToMobs = true;

    // variables de fisica
    public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

    // methods
    public override void _Ready()
    {
        _animationController = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        AddChild(audioController);
    }
    public override void _PhysicsProcess(double delta)
    {        
        MotionController(delta);
        AnimationController(); 

        for (int i = 0; i < GetSlideCollisionCount(); i++)
        {
            KinematicCollision2D collision = GetSlideCollision(i);
            collisionWithMobs = ((Node)collision.GetCollider()).IsInGroup("Enemy");
            collisionMobsY = collision.GetPosition().Y;
            collisionMobsX = collision.GetPosition().X;
        }
    }

    public void MotionController(double delta)
    {
        Vector2 velocity = Velocity;

        float direction = Input.GetAxis("left", "right");
        velocity.X = direction * speedWalk;

        // aplicar gravedad
        if(!IsOnFloor())
        {
            velocity.Y += gravity * (float)delta;
        }

        // 
        if(Input.IsActionJustPressed("jump") && IsOnFloor())
        {
            velocity.Y = -speedJump;
            audioController.Stream = effects[0];
            audioController.Play();
        }
        else if(collisionWithMobs && Position.Y < collisionMobsY)
        {
            velocity.Y = -speedJump;
            audioController.Stream = effects[0];
            audioController.Play();
            collisionWithMobs = false;
        }

        // TODO hit contra enemigos
        if(collisionWithMobs)
        {
            if(Position.X < collisionMobsX && isNextToMobs == true)
            {   
  
            }
            else if(Position.X > collisionMobsX && isNextToMobs == true)
            {

            }      
            collisionWithMobs = false;    
        }

        Velocity = velocity;
        MoveAndSlide();

    }    

    public void AnimationController()
    {
        bool isMoving = Velocity.X != 0;

        if(!isMoving && IsOnFloor())
        {
            _animationController.Play("idle");
        }
        else if(isMoving && IsOnFloor())
        {
            _animationController.Play("walk");
            _animationController.FlipH = Velocity.X < 0;
        }
        else if(!IsOnFloor())
        {
            _animationController.Play("jump");
        }

        //TODO: animacion hit contra enemigos
    }
}
