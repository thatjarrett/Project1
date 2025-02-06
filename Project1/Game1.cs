using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Project1.Controllers;
using Project1.Sprites;
using System.Collections.Generic;
using Project1.GameObjects.Environment;
using Project1.Interfaces;
using Project1.Entities;
using System;
using Project1.Commands;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private KeyboardController keyboardController;
    private Link link;

    private List<environmentTile> tiles = new List<environmentTile>();

    Texture2D linkTexture;
    Texture2D environmentTexture;
    Texture2D npcTexture;

    ISprite statueLeftSprite;
    ISprite statueRightSprite;
    ISprite squareBlockSprite;
    ISprite blueGapSprite;
    ISprite stairsSprite;
    ISprite oldManSprite;

    ISprite fireSprite;

    Dictionary<int, ISprite> spritesIDs;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        createSprites();

        environmentTile statueTile = new statueTileLeft(new Vector2(100, 100));
        environmentTile statueTile2 = new statueTileRight(new Vector2(148, 100));
        environmentTile squareBlock = new blockTile(new Vector2(196, 100));
        environmentTile blueGap = new gapTile(new Vector2(244, 100));
        environmentTile stairs = new stairsTile(new Vector2(292, 100));
        environmentTile fire = new fireTile(new Vector2(340, 100));
        environmentTile oldMan = new oldManTile(new Vector2(388, 100));

        tiles.Add(statueTile);
        tiles.Add(statueTile2);
        tiles.Add(squareBlock);
        tiles.Add(blueGap);
        tiles.Add(stairs);
        tiles.Add(fire);
        tiles.Add(oldMan);

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
        spritesIDs = new Dictionary<int, ISprite>
        {
            {0,statueLeftSprite},
            {1,statueRightSprite},
            {2,squareBlockSprite},
            {3,blueGapSprite},
            {4,stairsSprite},
            {5,fireSprite},
            {6,oldManSprite},
        };

        setTileSprites();
        keyboardController = new KeyboardController(commands, new IdleCommand(link));
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
        foreach (var tile in tiles)
        {
            tile.Update(gameTime);
        }
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

    protected void createSprites()
    {
        createItemSprites();
        createLinkSprites();
        createEnemySprites();
        createEnvironmentSprites();
    }

    protected void createLinkSprites()
    {
        linkTexture = Content.Load<Texture2D>("Images/Link Spritesheet");
    }

    protected void createEnvironmentSprites()
    {
        environmentTexture = Content.Load<Texture2D>("Images/dungeonTiles");
        npcTexture = Content.Load<Texture2D>("Images/oldMan");

        statueLeftSprite = new NMoveNAnim(environmentTexture, new Rectangle(515, 1, 16, 16));
        statueRightSprite = new NMoveNAnim(environmentTexture, new Rectangle(498, 1, 16, 16));
        squareBlockSprite = new NMoveNAnim(environmentTexture, new Rectangle(481, 1, 16, 16));
        blueGapSprite = new NMoveNAnim(environmentTexture, new Rectangle(498, 18, 16, 16));
        stairsSprite = new NMoveNAnim(environmentTexture, new Rectangle(515, 18, 16, 16));
        oldManSprite = new NMoveNAnim(npcTexture,new Rectangle(1,1, 16, 16));

        Rectangle[] fireFrames = new Rectangle[] { new Rectangle(52, 1, 16, 16), new Rectangle(69, 1, 16, 16) };
        fireSprite = new NMoveAnim(npcTexture, fireFrames, 5);
    }

    protected void createItemSprites()
    {
        //Create sprites for items here
    }

    protected void createEnemySprites()
    {
        //Create sprites for enemies here
    }

    protected void setTileSprites()
    {
        foreach (var tile in tiles)
        {
            tile.setSprite(spritesIDs[tile.getTileID()]);
        }

    }
}
