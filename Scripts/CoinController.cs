using Godot;
using System;

public partial class CoinController : Area2D
{
    // variable de animacion y audio
    private Sprite2D _spriteController;
    private AudioStreamPlayer _audioController;

    // variables de puntuacion juego
    public int goldCoinValue = 5;
    public int silverCoinValue = 2;
    public int bronzeCoinValue = 1;

    public override void _Ready()
    {
        // incializa los componentes hijos 
        _spriteController = GetNode<Sprite2D>("Sprite2D");
        _audioController = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
    }

    public async void OnPlayerEntered(Node body)
    {
        if(body.Name == "Player")
        {
            // desactiva el sprite y reproduce el audio
            _spriteController.Visible = false;
            _audioController.Play();

            // TODO vincular con script global para aumentar la puntuacion
            if(IsInGroup("GoldCoin"))
            {
                // aumenta la puntuacion del jugador
                GD.Print($"Gold Coin: {goldCoinValue}");
            }
            else if(IsInGroup("SilverCoin"))
            {
                // aumenta la puntuacion del jugador
                GD.Print($"Gold Coin: {silverCoinValue}");
            }
            else if(IsInGroup("BronzeCoin"))
            {
                // aumenta la puntuacion del jugador
                GD.Print($"Gold Coin: {bronzeCoinValue}");
            }
        }
        // al finalizar la reproduccion del audio, se elimina el nodo
        await ToSignal(_audioController, "finished");
        QueueFree();
    }
}
