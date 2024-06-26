using Godot;
using System;

public partial class EnemyController : CharacterBody2D
{
    // variables de movimiento
    private float _initialPosition;
    private float _endPosition;
    private float _distance;
    private float _speedMovement;

    // variables de animacion y audio
    private AnimatedSprite2D _animationController;
    private AudioStreamPlayer _audioController;
    private bool isDead = false;

    // variables de puntuacion juego
    private int _scoreValue;

    // variables globales
    private Global _global;

    // variables de fisica
    public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

    public override void _Ready()
    {   
        // inicializa los controladores de componentes
        _animationController = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        _audioController = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
        _global = GetNode<Global>("/root/Global");

        // crea un numero random para la velocidad y distancia
        RandomNumberGenerator random = new RandomNumberGenerator();
        random.Randomize();
        
        // asigna valores random a las variables
        _speedMovement = random.RandiRange(75, 150);
        _distance = random.RandiRange(300, 500);
        
        // asigna la puntuacion del enemigo
        _scoreValue = random.RandiRange(5,10);

        // asigna las posiciones iniciales y finales
        _initialPosition = Position.X;
        _endPosition = _initialPosition - _distance;

    }

    public override void _PhysicsProcess(double delta)
    {
        MotionController(delta);
        AnimationController();
    }

    public void MotionController(double delta)
    {
        Vector2 velocity = Velocity;

        // aplica gravedad al enemigo
        if(!IsOnFloor() && IsInGroup("Ground"))
        {
            velocity.Y += gravity * (float)delta;
        }

        // mueve al enemigo de un punto a otro

        if(_initialPosition > _endPosition)
        {
            velocity.X -= _speedMovement * (float)delta;

            if(Position.X <= _endPosition)
            {
                velocity.X = 0;
                _initialPosition = _endPosition;
                _endPosition = _initialPosition + _distance;
            }
        }
        else if(_initialPosition < _endPosition)
        {
            velocity.X += _speedMovement * (float)delta;

            if(Position.X >= _endPosition)
            {
                velocity.X = 0;
                _initialPosition = _endPosition;
                _endPosition = _initialPosition - _distance;
            }
        }
    
        velocity = velocity.Normalized() * _speedMovement;

        Velocity = velocity;
        MoveAndSlide();
    }

    public void AnimationController()
    {
        bool isMoving = Velocity.X != 0;

        // controla la animacion del enemigo
        if(IsInGroup("Ground") && isDead == false)
        {
            if(!isMoving && IsOnFloor())
            {
                _animationController.Play("idle");
            }
            else if(isMoving && IsOnFloor())
            {
                _animationController.Play("walk");
                _animationController.FlipH = Velocity.X > 0;
            }
        }
        else if(IsInGroup("Air") && isDead == false)
        {
            if(!isMoving)
            {
                _animationController.Play("idle");
            }
            else if(isMoving)
            {
                _animationController.Play("walk");
                _animationController.FlipH = Velocity.X > 0;
            }
        }
        else if(isDead == true)
        {
            _animationController.Play("dead");
        }
    }
    public async void Dead()
    {  
        // se modifica el valor de "isDead", se aumenta la puntuacion, se activa el audio y se elimina el nodo al finalizar el audio
        isDead = true;
        _global.score += _scoreValue;
        GD.Print(_scoreValue);
        _audioController.Play();
        await ToSignal(_audioController, "finished");
        QueueFree();
    }
}
