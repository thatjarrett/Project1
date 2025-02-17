using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1.Commands;
using Project1.Controllers;
using Project1.Entities;
using Project1.GameObjects.Background;
using Project1.GameObjects.Environment;
using Project1.GameObjects.Items;
using Project1.Interfaces;
using Project1.Sprites;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private KeyboardController keyboardController;
    private Link link;
    private Aquamentus aquamentus;
    private SpikeTrap trap;

    private Bat bat;
    private Slime slime;
    private Skeleton skeleton;
    private Goriya goriya;

    private List<environmentTile> tiles = new List<environmentTile>();
    private List<IEnemy> enemies = new List<IEnemy>();

    //private List<Enemy> enemies = new List<Enemy>();


    Texture2D linkTexture;
    Texture2D environmentTexture;
    Texture2D npcTexture;
    Texture2D itemTexture;
    Texture2D aquamentusTexture;
    Texture2D enemyTexture;

    ISprite statueLeftSprite;
    ISprite statueRightSprite;
    ISprite squareBlockSprite;
    ISprite blueGapSprite;
    ISprite stairsSprite;
    ISprite oldManSprite;

    ISprite topWallSprite;
    ISprite topPlainWallSprite;
    ISprite topOpenDoorSprite;
    ISprite topBombedOpeningSprite;
    ISprite topKeyLockedDoorSprite;
    ISprite topDiamondLockedDoorSprite;

    ISprite bottomWallSprite;
    ISprite bottomPlainWallSprite;
    ISprite bottomOpenDoorSprite;
    ISprite bottomBombedOpeningSprite;
    ISprite bottomKeyLockedDoorSprite;
    ISprite bottomDiamondLockedDoorSprite;

    ISprite leftWallSprite;
    ISprite leftPlainWallSprite;
    ISprite leftOpenDoorSprite;
    ISprite leftBombedOpeningSprite;
    ISprite leftKeyLockedDoorSprite;
    ISprite leftDiamondLockedDoorSprite;

    ISprite rightWallSprite;
    ISprite rightPlainWallSprite;
    ISprite rightOpenDoorSprite;
    ISprite rightBombedOpeningSprite;
    ISprite rightKeyLockedDoorSprite;
    ISprite rightDiamondLockedDoorSprite;

    ISprite fireSprite;

    ISprite ladderSprite;
    ISprite whiteBrickSprite;
    ISprite blueFloorSprite;
    ISprite blueSandSprite;

    ISprite boomerang;
    ISprite HeartContainer;
    ISprite compass;
    ISprite Map;
    ISprite Key;
    ISprite TriForcePiece;
    ISprite Bow;
    ISprite Heart;
    ISprite Rupee;
    ISprite Arrow;
    ISprite Bomb;
    ISprite Fairy;
    ISprite Clock;

    private int currentBlockIndex = 0;
    private int currentItemIndex = 0;
    private int currentNPCIndex = 0;

    private int currentEnemyIndex = 0;


    Dictionary<int, ISprite> spritesIDs;

    private List<ISprite> itemsList = new List<ISprite>();

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void LoadContent()
    {
        link = new Link(new Vector2(350, 170));
        aquamentus = new Aquamentus(new Vector2(500, 170));
        trap = new SpikeTrap(new Vector2(500, 170));
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        bat = new Bat(new Vector2(500, 170));
        slime = new Slime(new Vector2(500, 170));
        skeleton = new Skeleton(new Vector2(500, 170));
        goriya = new Goriya(new Vector2(500, 170));

        createSprites();

        environmentTile statueTile = new statueTileLeft(new Vector2(100, 100));
        environmentTile statueTile2 = new statueTileRight(new Vector2(100, 100));
        environmentTile squareBlock = new blockTile(new Vector2(100, 100));
        environmentTile blueGap = new gapTile(new Vector2(100, 100));
        environmentTile stairs = new stairsTile(new Vector2(100, 100));
        environmentTile fire = new fireTile(new Vector2(100, 100));
        environmentTile oldMan = new oldManTile(new Vector2(100, 100));
        environmentTile pushBlock = new pushableBlock(new Vector2(100, 100));

        environmentTile topWall = new wallTile(new Vector2(100, 100), 7);
        environmentTile topPlainWall = new doorTile(new Vector2(100, 100), 8);
        environmentTile topOpenDoor = new doorTile(new Vector2(100, 100), 9);
        environmentTile topKeyLockedDoor = new doorTile(new Vector2(100, 100), 11);
        environmentTile topDiamondLockedDoor = new doorTile(new Vector2(100, 100), 12);
        environmentTile topBombedOpening = new doorTile(new Vector2(100, 100), 10);

        environmentTile bottomWall = new wallTile(new Vector2(100, 100), 13);
        environmentTile bottomPlainWall = new doorTile(new Vector2(100, 100), 14);
        environmentTile bottomOpenDoor = new doorTile(new Vector2(100, 100), 15);
        environmentTile bottomKeyLockedDoor = new doorTile(new Vector2(100, 100), 17);
        environmentTile bottomDiamondLockedDoor = new doorTile(new Vector2(100, 100), 18);
        environmentTile bottomBombedOpening = new doorTile(new Vector2(100, 100), 16);

        environmentTile leftWall = new wallTile(new Vector2(100, 100), 19);
        environmentTile leftPlainWall = new doorTile(new Vector2(100, 100), 20);
        environmentTile leftOpenDoor = new doorTile(new Vector2(100, 100), 21);
        environmentTile leftKeyLockedDoor = new doorTile(new Vector2(100, 100), 23);
        environmentTile leftDiamondLockedDoor = new doorTile(new Vector2(100, 100), 24);
        environmentTile leftBombedOpening = new doorTile(new Vector2(100, 100), 22);

        environmentTile rightWall = new wallTile(new Vector2(100, 100), 25);
        environmentTile rightPlainWall = new doorTile(new Vector2(100, 100), 26);
        environmentTile rightOpenDoor = new doorTile(new Vector2(100, 100), 27);
        environmentTile rightKeyLockedDoor = new doorTile(new Vector2(100, 100), 29);
        environmentTile rightDiamondLockedDoor = new doorTile(new Vector2(100, 100), 30);
        environmentTile rightBombedOpening = new doorTile(new Vector2(100, 100), 28);


        environmentTile blueFloor = new BlueFloor(new Vector2(100, 100), 31);
        environmentTile blueSand = new BlueSand(new Vector2(100, 100), 32);
        environmentTile ladder = new Ladder(new Vector2(100, 100), 33);
        environmentTile whiteBrick = new WhiteBrick(new Vector2(100, 100), 34);


        tiles.Add(statueTile);
        tiles.Add(statueTile2);
        tiles.Add(squareBlock);
        tiles.Add(blueGap);
        tiles.Add(stairs);
        tiles.Add(fire);
        tiles.Add(oldMan);
        tiles.Add(pushBlock);

        tiles.Add(blueFloor);
        tiles.Add(blueSand);
        tiles.Add(whiteBrick);
        tiles.Add(ladder);

        /*tiles.Add(topWall);
        tiles.Add(topPlainWall);
        tiles.Add(topOpenDoor);
        tiles.Add(topKeyLockedDoor);
        tiles.Add(topDiamondLockedDoor);
        tiles.Add(topBombedOpening);

        tiles.Add(bottomWall);
        tiles.Add(bottomPlainWall);
        tiles.Add(bottomOpenDoor);
        tiles.Add(bottomKeyLockedDoor);
        tiles.Add(bottomDiamondLockedDoor);
        tiles.Add(bottomBombedOpening);

        tiles.Add(leftWall);
        tiles.Add(leftPlainWall);
        tiles.Add(leftOpenDoor);
        tiles.Add(leftKeyLockedDoor);
        tiles.Add(leftDiamondLockedDoor);
        tiles.Add(leftBombedOpening);

        tiles.Add(rightWall);
        tiles.Add(rightPlainWall);
        tiles.Add(rightOpenDoor);
        tiles.Add(rightKeyLockedDoor);
        tiles.Add(rightDiamondLockedDoor);
        tiles.Add(rightBombedOpening);*/





        itemsList.Add(boomerang);
        itemsList.Add(HeartContainer);
        itemsList.Add(compass);
        itemsList.Add(Map);
        itemsList.Add(Key);
        itemsList.Add(TriForcePiece);
        itemsList.Add(Bow);
        itemsList.Add(Heart);
        itemsList.Add(Rupee);
        itemsList.Add(Arrow);
        itemsList.Add(Bomb);
        itemsList.Add(Fairy);
        itemsList.Add(Clock);

        enemies.Add(aquamentus);
        enemies.Add(trap);

        enemies.Add(bat);
        enemies.Add(slime);
        enemies.Add(skeleton);
        enemies.Add(goriya);

        //When adding other tiles remember to add them to "tiles" list and delete this comment! - Bren
        //Add bomb to list of items and delete this comment when items are implemented! -Bren
        //Add old man to list of characters and delete this comment when enemies are implemented! -Bren

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
{ Keys.N, new AttackCommand(link) },
{ Keys.E, new DamageCommand(link) },
{ Keys.D1, new UseItemCommand(link, 1) },
{ Keys.D2, new UseItemCommand(link, 2) },
{ Keys.D3, new UseItemCommand(link, 3) },
{ Keys.G, new DeathCommand(link) },
{ Keys.Q, new QuitCommand(this) },
{ Keys.R, new ResetCommand(this) },
{ Keys.T, new CycleBlockCommand(this, false) }, // Previous block
{ Keys.Y, new CycleBlockCommand(this, true) },  // Next block
{ Keys.U, new CycleItemCommand(this, false) }, // Previous item
{ Keys.I, new CycleItemCommand(this, true) },  // Next item
{ Keys.O, new CycleNPCCommand(this, false) }, // Previous NPC
{ Keys.P, new CycleNPCCommand(this, true) }   // Next NPC

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
            {7,topWallSprite},
            {8,topPlainWallSprite},
            {9,topOpenDoorSprite},
            {10,topBombedOpeningSprite},
            {11,topKeyLockedDoorSprite},
            {12,topDiamondLockedDoorSprite},
            {13,bottomWallSprite},
            {14,bottomPlainWallSprite},
            {15,bottomOpenDoorSprite},
            {16,bottomBombedOpeningSprite},
            {17,bottomKeyLockedDoorSprite},
            {18,bottomDiamondLockedDoorSprite},
            {19,leftWallSprite},
            {20,leftPlainWallSprite},
            {21,leftOpenDoorSprite},
            {22,leftBombedOpeningSprite},
            {23,leftKeyLockedDoorSprite},
            {24,leftDiamondLockedDoorSprite},
            {25,rightWallSprite},
            {26,rightPlainWallSprite},
            {27,rightOpenDoorSprite},
            {28,rightBombedOpeningSprite},
            {29,rightKeyLockedDoorSprite},
            {30,rightDiamondLockedDoorSprite},
            {31, blueFloorSprite},
            {32, blueSandSprite },
            {33, ladderSprite},
            {34, whiteBrickSprite}
        };

        setTileSprites();
        keyboardController = new KeyboardController(commands, new IdleCommand(link));
    }

    protected override void Update(GameTime gameTime)
    {
        var keyboardState = Keyboard.GetState();


        keyboardController.Update(gameTime);
        link.Update(gameTime);
        foreach (var tile in tiles)
        {
            tile.Update(gameTime);
        }
        base.Update(gameTime);

        foreach (var item in itemsList)
        {
            item.Update(gameTime);
        }

        int enemyNum = 0;
        foreach (var enemy in enemies)
        {
            //enemy.Update(gameTime);
            if (currentEnemyIndex == enemyNum)
            {
                enemy.Update(gameTime);
            }
            enemyNum++;
            if (enemyNum >= enemies.Count)
            {
                enemyNum = 0;
            }
        }

    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp);



        int tileNum = 0;
        int enemyNum = 0;
        foreach (var tile in tiles)
        {

            if (currentBlockIndex == tileNum)
            {
                tile.Draw(_spriteBatch);
            }
            tileNum++;
            if (tileNum >= tiles.Count)
            {
                tileNum = 0;
            }
        }
        int x = 200;
        foreach (var item in itemsList)
        {
            item.Draw(_spriteBatch, new Vector2(x, 300), SpriteEffects.None);
            x += 40;
        }

        foreach (var enemy in enemies)
        {
            //enemy.Draw(_spriteBatch);
            if (currentEnemyIndex == enemyNum)
            {
                enemy.Draw(_spriteBatch);
            }
            enemyNum++;
            if (enemyNum >= enemies.Count)
            {
                enemyNum = 0;
            }
        }

        //Keep link below the tiles so he's drawn above them
       

        link.Draw(_spriteBatch);

        _spriteBatch.End();



        base.Draw(gameTime);
    }

    protected void createSprites()
    {
        linkTexture = Content.Load<Texture2D>("Images/Link Spritesheet");
        createItemSprites();
        link.createLinkSprites(linkTexture);
        createEnemySprites();
        createEnvironmentSprites();
        createItemSprites();
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
        oldManSprite = new NMoveNAnim(npcTexture, new Rectangle(1, 1, 16, 16));

        topWallSprite = new NMoveNAnim(environmentTexture, new Rectangle(1, 1, 256, 32));
        topPlainWallSprite = new NMoveNAnim(environmentTexture, new Rectangle(295, 1, 32, 32));
        topOpenDoorSprite = new NMoveNAnim(environmentTexture, new Rectangle(328, 1, 32, 32));
        topKeyLockedDoorSprite = new NMoveNAnim(environmentTexture, new Rectangle(361, 1, 32, 32));
        topDiamondLockedDoorSprite = new NMoveNAnim(environmentTexture, new Rectangle(394, 1, 32, 32));
        topBombedOpeningSprite = new NMoveNAnim(environmentTexture, new Rectangle(427, 1, 32, 32));

        bottomWallSprite = new NMoveNAnim(environmentTexture, new Rectangle(1, 145, 256, 32));
        bottomPlainWallSprite = new NMoveNAnim(environmentTexture, new Rectangle(295, 100, 32, 32));
        bottomOpenDoorSprite = new NMoveNAnim(environmentTexture, new Rectangle(328, 100, 32, 32));
        bottomKeyLockedDoorSprite = new NMoveNAnim(environmentTexture, new Rectangle(361, 100, 32, 32));
        bottomDiamondLockedDoorSprite = new NMoveNAnim(environmentTexture, new Rectangle(394, 100, 32, 32));
        bottomBombedOpeningSprite = new NMoveNAnim(environmentTexture, new Rectangle(427, 100, 32, 32));

        leftWallSprite = new NMoveNAnim(environmentTexture, new Rectangle(1, 33, 32, 96));
        leftPlainWallSprite = new NMoveNAnim(environmentTexture, new Rectangle(295, 34, 32, 32));
        leftOpenDoorSprite = new NMoveNAnim(environmentTexture, new Rectangle(328, 34, 32, 32));
        leftKeyLockedDoorSprite = new NMoveNAnim(environmentTexture, new Rectangle(361, 34, 32, 32));
        leftDiamondLockedDoorSprite = new NMoveNAnim(environmentTexture, new Rectangle(394, 34, 32, 32));
        leftBombedOpeningSprite = new NMoveNAnim(environmentTexture, new Rectangle(427, 34, 32, 32));

        rightWallSprite = new NMoveNAnim(environmentTexture, new Rectangle(225, 33, 32, 96));
        rightPlainWallSprite = new NMoveNAnim(environmentTexture, new Rectangle(295, 67, 32, 32));
        rightOpenDoorSprite = new NMoveNAnim(environmentTexture, new Rectangle(328, 67, 32, 32));
        rightKeyLockedDoorSprite = new NMoveNAnim(environmentTexture, new Rectangle(361, 67, 32, 32));
        rightDiamondLockedDoorSprite = new NMoveNAnim(environmentTexture, new Rectangle(394, 67, 32, 32));
        rightBombedOpeningSprite = new NMoveNAnim(environmentTexture, new Rectangle(427, 67, 32, 32));

        Rectangle[] fireFrames = new Rectangle[] { new Rectangle(52, 1, 16, 16), new Rectangle(69, 1, 16, 16) };
        fireSprite = new NMoveAnim(npcTexture, fireFrames, 5);

        ladderSprite = new NMoveNAnim(environmentTexture, new Rectangle(481, 35, 16, 16));
        whiteBrickSprite = new NMoveNAnim(environmentTexture, new Rectangle(464, 35, 16, 16));
        blueFloorSprite = new NMoveNAnim(environmentTexture, new Rectangle(464, 1, 16, 16));
        blueSandSprite = new NMoveNAnim(environmentTexture, new Rectangle(481, 18, 16, 16));

    }

    protected void createItemSprites()
    {
        itemTexture = Content.Load<Texture2D>("NES - The Legend of Zelda - Items & Weapons");
        boomerang = new Boomerang(itemTexture);
        HeartContainer = new HeartContainer(itemTexture);
        compass = new Compass(itemTexture);
        Map = new Map(itemTexture);
        Key = new Key(itemTexture);
        TriForcePiece = new TriForcePiece(itemTexture);
        Bow = new Bow(itemTexture);
        Heart = new Heart(itemTexture);
        Rupee = new Rupee(itemTexture);
        Arrow = new Arrow(itemTexture);
        Bomb = new Bomb(itemTexture);
        Fairy = new Fairy(itemTexture);
        Clock = new Clock(itemTexture);
    }
    public void CycleBlock(bool forward)
    {
        if (tiles.Count == 0) return;
        currentBlockIndex = (currentBlockIndex + (forward ? 1 : tiles.Count - 1)) % tiles.Count;
    }

    public void CycleItem(bool forward)
    {
        if (itemsList.Count == 0) return;
        currentItemIndex = (currentItemIndex + (forward ? 1 : itemsList.Count - 1)) % itemsList.Count;
    }

    public void CycleNPC(bool forward)
    {
        if (enemies.Count == 0) return;
        currentEnemyIndex = (currentEnemyIndex + (forward ? 1 : itemsList.Count - 1)) % enemies.Count;
    }

    protected void createEnemySprites()
    {
        //Create sprites for enemies here
        aquamentusTexture = Content.Load<Texture2D>("Images/bosses");
        enemyTexture = Content.Load<Texture2D>("Images/enemies");
        aquamentus.createEnemySprites(aquamentusTexture);
        trap.createEnemySprites(enemyTexture);


        bat.createEnemySprites(enemyTexture);
        slime.createEnemySprites(enemyTexture);
        skeleton.createEnemySprites(enemyTexture);
        goriya.createEnemySprites(enemyTexture);

    }
    public void RestartGame()
    {
        // Clear all game objects
        tiles.Clear();
        itemsList.Clear();
        enemies.Clear();

        // Reinitialize the game
        Initialize();
    }


    protected void setTileSprites()
    {
        foreach (var tile in tiles)
        {
            tile.setSprite(spritesIDs[tile.getTileID()]);
        }

    }
}
