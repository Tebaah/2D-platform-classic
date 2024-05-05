using Godot;
using System;

public partial class ChangeToSelector : TextureButton
{
    // varaiables 
    public CharacterBody2D player;

    public override void _Process(double delta)
    {
        // inicializa los componentes
        player = GetNode<CharacterBody2D>("/root/Player");
    }
    public void OnButtonPressed()
    {
        // al presionar el boton se cambia de escena y se elimiana la instancia del jugador
        GetTree().ChangeSceneToFile("res://Scenes/Gui/level_selector.tscn");
        player.QueueFree();
    }
}
