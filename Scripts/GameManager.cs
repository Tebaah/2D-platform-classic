using Godot;
using System;

public partial class GameManager : Node
{
    // variable global
    private Global _global;
    public PackedScene player = GD.Load<PackedScene>("res://Scenes/Characters/Player/player.tscn");
    private Marker2D spawnPlayer;
    private Button _button;
    private CharacterBody2D _playerCharacter;
    public override void _Ready()
    {
        _global = GetNode<Global>("/root/Global");
        _button = GetNode<Button>("Node/Control/Button");


        spawnPlayer = GetNode<Marker2D>("SpawnPlayer");
        AddChild(_global.levels[_global.currentLevel].Instantiate());

        _global.currentLevel += 1;

        CharacterBody2D newPlayer = (CharacterBody2D)player.Instantiate();
        newPlayer.Position = spawnPlayer.Position;
        GetParent().AddChild(newPlayer);
    }

    public override void _Process(double delta)
    {
        if(_button.ButtonPressed == true)
        {
            _playerCharacter = GetNode<CharacterBody2D>("/root/Player");
            _playerCharacter.QueueFree();
        }
    }
}
