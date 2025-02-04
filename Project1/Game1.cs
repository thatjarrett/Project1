using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Project1.Controllers;
<<<<<<< HEAD
using Project1.Entities;
using Project1.Interfaces;
using System.Collections.Generic;
=======
using Project1;
using Project1.Sprites;
using System.Collections.Generic;
using Project1.GameObjects.Environment;
using Project1.Interfaces;
>>>>>>> 0895e9e4577d78924758881193204d83699c710b

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private KeyboardController keyboardController;
    private Link link;
<<<<<<< HEAD
    
=======

    private List<environmentTile> tiles = new List<environmentTile>();

>>>>>>> 0895e9e4577d78924758881193204d83699c710b
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
        Texture2D environmentTexture = Content.Load<Texture2D>("Images/dungeonTiles");
        Texture2D npcTexture = Content.Load<Texture2D>("Images/oldMan");

        ISprite statueLeftSprite = new NMoveNAnim(environmentTexture,new Rectangle(515,1,16,16));
        ISprite statueRightSprite = new NMoveNAnim(environmentTexture, new Rectangle(498, 1, 16, 16));
        ISprite squareBlockSprite = new NMoveNAnim(environmentTexture, new Rectangle(481, 1, 16, 16));
        ISprite blueGapSprite = new NMoveNAnim(environmentTexture, new Rectangle(498, 18, 16, 16));
        ISprite stairsSprite = new NMoveNAnim(environmentTexture, new Rectangle(515, 18, 16, 16));

        statueTile statueTile = new statueTile(new Vector2(100,100),true,statueLeftSprite);
        statueTile statueTile2 = new statueTile(new Vector2(148, 100), true, statueRightSprite);
        statueTile squareBlock = new statueTile(new Vector2(196, 100), true, squareBlockSprite);
        statueTile blueGap = new statueTile(new Vector2(244, 100), true, blueGapSprite);
        statueTile stairs = new statueTile(new Vector2(292, 100), false, stairsSprite);

        tiles.Add(statueTile);
        tiles.Add(statueTile2);
        tiles.Add(squareBlock);
        tiles.Add(blueGap);
        tiles.Add(stairs);

        //When adding other tiles remember to add them to "tiles" list and delete this comment! - Bren
        //Add bomb to list of items and delete this comment when items are implemented! -Bren
        //Add old man to list of characters and delete this comment when enemies are implemented! -Bren
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
        
        foreach (var tile in tiles)
        {
            tile.Draw(_spriteBatch);
        }
        //Keep link below the tiles so he's drawn above them
        link.Draw(_spriteBatch);
        _spriteBatch.End();

       

        base.Draw(gameTime);
    }
}
