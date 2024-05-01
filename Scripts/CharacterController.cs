using Godot;
using System;

public partial class CharacterController : CharacterBody2D
{
    // variables de movimiento
    [Export] public float speedWalk;
    [Export] public float speedJump;
    private bool _isHitLeft = false;
    private bool _isHitRight = false; 

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

    // metodos
    public override void _Ready()
    {
        // inicializar variables
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

        // movimiento horizontal del personaje
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

        // impulso al ser golpeado por un enemigo
        if(_isHitLeft == true)
        {
            velocity.Y = -200;
            velocity.X = -10000;
            audioController.Stream = effects[1];
            audioController.Play();
            _isHitLeft = false;
        }
        else if(_isHitRight == true)
        {
            velocity.Y = -200;
            velocity.X = 10000;
            audioController.Stream = effects[1];
            audioController.Play();
            _isHitRight = false;
        }

        Velocity = velocity;
        MoveAndSlide();


    }    

    public void AnimationController()
    {
        bool isMoving = Velocity.X != 0;

        // si esta quieto, en el suelo y sin tocar un enemigo 
        if(!isMoving && IsOnFloor() &&_isHitLeft == false && _isHitRight == false)
        {
            _animationController.Play("idle");
        }
        // si esta en movimiento, en el suelo y sin tocar un emeigo
        else if(isMoving && IsOnFloor() && _isHitLeft == false && _isHitRight == false)
        {
            _animationController.Play("walk");
            _animationController.FlipH = Velocity.X < 0;
        }
        // si no esta en el suelo, dependiendo de tocar o no un enemigo
        else if(!IsOnFloor() && _isHitLeft == false && _isHitRight == false)
        {
            _animationController.Play("jump");
        }
    }

    public void CollisionWithElement()
    {
        // detecta la colision 
        for (int i = 0; i < GetSlideCollisionCount(); i++)
        {
            KinematicCollision2D collision = GetSlideCollision(i);
            
            // si colisiona con un enemigo
            if(((Node)collision.GetCollider()).IsInGroup("Enemy"))
            {
                while(GetSlideCollisionCount() == 1)
                {
                    // establece los siguientes valores para los eventos de colision en el eje "y" y "x"
                    // sobre el eje "y"
                    if(Position.Y <= collision.GetPosition().Y)
                    {
                        _isOverTheEnemy = true;
                        EnemyController enemy = (EnemyController)collision.GetCollider();
                        enemy.Dead();
                    }
                    // sobre el eje "x"
                    else if(collision.GetPosition().X > Position.X)
                    {
                        _isHitLeft = true;
                        _global.lifes -= 1;
                    }
                    else if(collision.GetPosition().X < Position.X)
                    {
                        _isHitRight = true;
                        _global.lifes -= 1;
                    }

                    break;
                }
                
            }
        }
    }

}
