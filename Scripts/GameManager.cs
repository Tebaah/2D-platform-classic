using Godot;
using System;

public partial class GameManager : Node
{
    // variable global
    private Global _global;

    // variable para instanciar al jugador
    public PackedScene player = GD.Load<PackedScene>("res://Scenes/Characters/Player/player.tscn");
    private Marker2D spawnPlayer;

    // variable para el boton
    private TextureButton _button;
    public override void _Ready()
    {
        // inicializa los componentes
        _global = GetNode<Global>("/root/Global");
        _button = GetNode<TextureButton>("Gui/CanvasLayer/Control/TextureButton");

        // instancia el nivel y lo agrega al nodo padre
        spawnPlayer = GetNode<Marker2D>("SpawnPlayer");
        AddChild(_global.levels[_global.currentLevel].Instantiate());

        // aumenta el nivel en la variable global
        _global.currentLevel += 1;

        // instancia al jugador y lo agrega al nodo padre
        CharacterBody2D newPlayer = (CharacterBody2D)player.Instantiate();
        newPlayer.Position = spawnPlayer.Position;
        GetParent().AddChild(newPlayer);
    }
}
