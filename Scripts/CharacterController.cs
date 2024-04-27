using Godot;
using System;

public partial class CharacterController : CharacterBody2D
{
    // variables de movimiento
    [Export] public float speedWalk;
    [Export] public float speedJump;
    private bool _isHit = false;
 

    // variables de animacion y audio
    private AnimatedSprite2D _animationController;
    public AudioStreamPlayer audioController = new AudioStreamPlayer();
    [Export]public AudioStreamWav[] effects;

    // variables de colision
    private bool _isOverTheEnemy = false;

    // variable global
    private Global _global;

    // variables de fisica
    public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

    // methods
    public override void _Ready()
    {
        _animationController = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        AddChild(audioController);
        _global = GetNode<Global>("/root/Global");
    }
    public override void _PhysicsProcess(double delta)
    {        
        MotionController(delta);
        AnimationController(); 
        CollisionWithElement();
    }

    public void MotionController(double delta)
    {
        Vector2 velocity = Velocity;


        float directionHor = Input.GetAxis("left", "right");
        velocity.X = directionHor * speedWalk;
        

        // aplicar gravedad
        if(!IsOnFloor())
        {
            velocity.Y += gravity * (float)delta;
        }

        // salto y doble salto
        if(Input.IsActionJustPressed("jump") && IsOnFloor())
        {
            velocity.Y = -speedJump;
            audioController.Stream = effects[0];
            audioController.Play();
        }
        else if(_isOverTheEnemy == true)
        {
            velocity.Y = -speedJump;
            audioController.Stream = effects[0];
            audioController.Play();
            _isOverTheEnemy = false;
        }

        // TODO hit contra enemigos desde lado derecho

        if(_isHit == true)
        {
            velocity.X = -7500;
            _isHit = false;
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
        else if(_isHit == true)
        {
            _animationController.Play("hit");
        }

        //TODO: animacion hit contra enemigos
    }

    public void CollisionWithElement()
    {
        for (int i = 0; i < GetSlideCollisionCount(); i++)
        {
            KinematicCollision2D collision = GetSlideCollision(i);
            
            if(((Node)collision.GetCollider()).IsInGroup("Enemy"))
            {
                if(Position.Y <= collision.GetPosition().Y)
                {
                    _isOverTheEnemy = true;
                    EnemyController enemy = (EnemyController)collision.GetCollider();
                    enemy.Dead();
                }
                else if(collision.GetPosition().X > Position.X)
                {
                    _isHit = true;
                    _global.lifes -= 1;
                }
                else if(collision.GetPosition().X < Position.X)
                {
                    _isHit = true;
                    _global.lifes -= 1;
                }
                
            }
        }
    }
}
