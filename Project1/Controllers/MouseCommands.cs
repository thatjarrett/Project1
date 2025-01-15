using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Project1.Controllers;
using Project1.Interfaces;
using Project1;

public class TopLeftCommand : ICommand
{
    private readonly Game1 _game;

    public TopLeftCommand(Game1 game)
    {
        _game = game;
    }

    public void Execute()
    {
        _game.ActiveSprite = new NMoveNAnim(
            _game.Content.Load<Texture2D>("Images/Link"),
            new Vector2(350, 170), 3.0f
        );
    }
}

public class TopRightCommand : ICommand
{
    private readonly Game1 _game;

    public TopRightCommand(Game1 game)
    {
        _game = game;
    }

    public void Execute()
    {
        _game.ActiveSprite = new NMoveAnim(
            _game.Content.Load<Texture2D>("Images/Link Spritesheet"),
            new Vector2(300, 200),
            20, 24, 60, 4, 16, 0.15
        );
    }
}

public class BottomLeftCommand : ICommand
{
    private readonly Game1 _game;

    public BottomLeftCommand(Game1 game)
    {
        _game = game;
    }

    public void Execute()
    {
        _game.ActiveSprite = new MoveNAnim(
            _game.Content.Load<Texture2D>("Images/LinkJump"),
            new Vector2(-300, 200),
            new Vector2(0, 100), 50, 3.0f
        );
    }
}

public class BottomRightCommand : ICommand
{
    private readonly Game1 _game;

    public BottomRightCommand(Game1 game)
    {
        _game = game;
    }

    public void Execute()
    {
        _game.ActiveSprite = new MoveAnim(
            _game.Content.Load<Texture2D>("Images/Link Spritesheet"),
            new Vector2(300, 200),
            new Vector2(100, 0), 16, 27, 2, 3, 0.2, 3.0f
        );
    }
}
