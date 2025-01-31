using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Project1.Controllers;
using Project1.Entities;
using Project1.Interfaces;
using System.Collections.Generic;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private KeyboardController keyboardController;
    private Link link;
    
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        Texture2D linkTexture = Content.Load<Texture2D>("Images/Link Spritesheet");
        link = new Link(linkTexture, new Vector2(350, 170));
    }
    protected override void Initialize()
    {
        base.Initialize();

        var commands = new Dictionary<Keys, ICommand>
    {
        { Keys.W, new MoveUpCommand(link) },
        { Keys.Up, new MoveUpCommand(link) },    
        { Keys.S, new MoveDownCommand(link) },
        { Keys.Down, new MoveDownCommand(link) }, 
        { Keys.A, new MoveLeftCommand(link) },
        { Keys.Left, new MoveLeftCommand(link) },  
        { Keys.D, new MoveRightCommand(link) },
        { Keys.Right, new MoveRightCommand(link) }, 
        { Keys.Z, new AttackCommand(link) },
        { Keys.E, new DamageCommand(link) }         
    };

        keyboardController = new KeyboardController(commands);
    }

    protected override void Update(GameTime gameTime)
    {
        var keyboardState = Keyboard.GetState();
        
        if (keyboardState.IsKeyDown(Keys.D0))
        {
            Exit();
        }

        keyboardController.Update(gameTime);
        link.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        link.Draw(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
