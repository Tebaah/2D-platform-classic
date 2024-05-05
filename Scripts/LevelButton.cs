using Godot;
using System;

public partial class LevelButton : TextureButton
{
    // variable global
    private Global _global;

    // variable para el id del nivel
    [Export] public int levelId;
    public override void _Ready()
    {
        // inicializa los componentes y verifica si el nivel esta desbloqueado
        _global = GetNode<Global>("/root/Global");

        if(levelId >= _global.currentLevel)
        {
            Disabled = false;
        }
        else
        {
            Disabled = true;
        }

    }

    public void OnButtonPressed()
    {
        // al presionar el boton se cambia a la escena game y se asigna el id del nivel que sera instanciado
        _global.levelId = levelId;
        GetTree().ChangeSceneToFile("res://Scenes/Levels/game.tscn");
        
    }
}
