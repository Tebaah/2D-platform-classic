using Godot;
using System;

public partial class GameManager : Node
{
    // variable global
    private Global _global;
    public PackedScene player = GD.Load<PackedScene>("res://Scenes/Characters/Player/player.tscn");
    private Marker2D spawnPlayer;
    public override void _Ready()
    {
        _global = GetNode<Global>("/root/Global");
        spawnPlayer = GetNode<Marker2D>("SpawnPlayer");
        AddChild(_global.levels[_global.levelCurrent].Instantiate());

        CharacterBody2D newPlayer = (CharacterBody2D)player.Instantiate();
        newPlayer.Position = spawnPlayer.Position;
        GetParent().AddChild(newPlayer);
    }
}
