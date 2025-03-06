using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Net.Sockets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1.Collision;
using Project1.Commands;
using Project1.Controllers;
using Project1.Entities;
//using Project1.GameObjects.Background;
using Project1.GameObjects.Environment;
using Project1.GameObjects.Items;
using Project1.Handlers;
using Project1.Interfaces;
using Project1.Projectiles;
using Project1.Sprites;
using Project1.LevelLoading;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private KeyboardController keyboardController;
    private GamepadController gamepadController;
    private Link link;
    private Aquamentus aquamentus;
    private SpikeTrap trap;

    private Bat bat;
    private Slime slime;
    private Skeleton skeleton;
    private Goriya goriya;
    private Hand hand;

    private List<environmentTile> tiles = new List<environmentTile>();
    private List<IEnemy> enemies = new List<IEnemy>();

    //private List<Enemy> enemies = new List<Enemy>();


    Texture2D linkTexture;
    Texture2D environmentTexture;
    Texture2D npcTexture;
    Texture2D itemTexture;
    Texture2D aquamentusTexture;
    Texture2D enemyTexture;
    Texture2D enemyDeathTexture;
    Texture2D enemySpawnTexture;

    IItem boomerang;
    IItem HeartContainer;
    IItem compass;
    IItem Map;
    IItem Key;
    IItem TriForcePiece;
    IItem Bow;
    IItem Heart;
    IItem Rupee;
    IItem Arrow;
    IItem Bomb;
    IItem Fairy;
    IItem Clock;

    ISprite enemyDeathCloud;
    ISprite enemySpawnCloud;

    private int currentBlockIndex = 0;
    private int currentItemIndex = 0;
    private int currentNPCIndex = 0;

    private int currentEnemyIndex = 0;

    private List<IItem> itemsList = new List<IItem>();

    Level leveltest;


    //Debug Variables
    Texture2D pixelTexture;
    bool debugDraw = true;
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void LoadContent()
    {
        

        pixelTexture = new Texture2D(GraphicsDevice, 1, 1);
        pixelTexture.SetData(new[] { Color.White });

        link = new Link(new Vector2(350, 170));
        aquamentus = new Aquamentus(new Vector2(500, 170));
        trap = new SpikeTrap(new Vector2(500, 170));
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        


        bat = new Bat(new Vector2(500, 170));
        slime = new Slime(new Vector2(500, 170));
        skeleton = new Skeleton(new Vector2(500, 170));
        goriya = new Goriya(new Vector2(500, 170));
        hand = new Hand(new Vector2(500, 170));

        createSprites();



        environmentTile pushBlock = new pushableBlock(new Vector2(100, 100));

        leveltest = new Level("Content/Level Data/BlockLevel.txt", Content.Load<Texture2D>("Images/DungeonRooms"));
        environmentTexture = Content.Load<Texture2D>("Images/dungeonTiles");
        npcTexture = Content.Load<Texture2D>("Images/oldMan");
        leveltest.loadTileSprites(environmentTexture, npcTexture);
        tiles.AddRange(leveltest.buildTiles());

        
        //tiles.Add(pushBlock);

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

        //itemsList.Add(enemyDeathCloud);
        //itemsList.Add(enemySpawnCloud);

        enemies.Add(aquamentus);
        enemies.Add(trap);

        enemies.Add(bat);
        enemies.Add(slime);
        enemies.Add(skeleton);
        enemies.Add(goriya);
        enemies.Add(hand);

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
{ Keys.D4, new UseItemCommand(link, 4) },
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
        var gamepadCommands = new Dictionary<Buttons, ICommand>
{
    { Buttons.DPadUp, new MoveUpCommand(link) },
    { Buttons.LeftThumbstickUp, new MoveUpCommand(link) },
    { Buttons.DPadDown, new MoveDownCommand(link) },
    { Buttons.LeftThumbstickDown, new MoveDownCommand(link) },
    { Buttons.DPadLeft, new MoveLeftCommand(link) },
    { Buttons.LeftThumbstickLeft, new MoveLeftCommand(link) },
    { Buttons.DPadRight, new MoveRightCommand(link) },
    { Buttons.LeftThumbstickRight, new MoveRightCommand(link) },
    { Buttons.A, new AttackCommand(link) },
    { Buttons.X, new UseItemCommand(link, 1) },
    { Buttons.Y, new UseItemCommand(link, 2) },
    { Buttons.LeftShoulder, new UseItemCommand(link, 3) }, // LB = Item 3
    { Buttons.RightShoulder, new UseItemCommand(link, 4) }, // RB = Item 4
    { Buttons.B, new DamageCommand(link) },
    { Buttons.Back, new QuitCommand(this) },
    { Buttons.Start, new ResetCommand(this) }
};


        gamepadController = new GamepadController(gamepadCommands, new IdleCommand(link));

        keyboardController = new KeyboardController(commands, new IdleCommand(link));
    }

    protected override void Update(GameTime gameTime)
    {
        keyboardController.Update(gameTime);
        gamepadController.Update(gameTime);  // Gamepad input added

        link.Update(gameTime);
        foreach (var tile in tiles)
        {
            tile.Update(gameTime);
        }

        base.Update(gameTime);

        int itemNum = 0;
        foreach (var item in itemsList)
        {
            item.Update(gameTime);
            if (currentItemIndex == itemNum)
            {
                item.Update(gameTime);
                LinkEnemyCollisionHandler.HandleCollision(item, link);

                itemNum++;
                if (itemNum >= enemies.Count)
                {
                    itemNum = 0;
                }
            }
        }

        int enemyNum = 0;
        foreach (var enemy in enemies)
        {
            if (currentEnemyIndex == enemyNum)
            {
                enemy.Update(gameTime);
                LinkEnemyCollisionHandler.HandleCollision(link, enemy);
                IProjectile[] p = enemy.GetProjectiles();
                if (p != null)
                {
                    for (int x = 0; x < p.Length; x++)
                    {
                        if (p[x] != null)
                        {
                            LinkEnemyCollisionHandler.HandleCollision(link, p[x]);
                        }
                    }
                }
                List<IProjectile> lp = link.GetProjectiles();
                foreach (var pp in lp)
                {
                    LinkEnemyCollisionHandler.HandleCollision(pp, enemy);
                }
                CollisionBox sword = link.getSword();
                LinkEnemyCollisionHandler.HandleCollision(sword, enemy);
            }
            enemyNum++;
            if (enemyNum >= enemies.Count)
            {
                enemyNum = 0;
            }
        }
        UpdateCollisions();
    }


    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp);


        int tileNum = 0;
        int enemyNum = 0;
        int itemNum =0;
        foreach (var tile in tiles)
        {
            tile.SetCollider();
            if (currentBlockIndex == tileNum)
            {
                tile.Draw(_spriteBatch);
                CollisionBox collider = tile.GetCollider();
                if (collider != null && debugDraw)
                {
                    tile.GetCollider().DebugDraw(_spriteBatch, pixelTexture,collider.hitbox,Color.Red);
                }
            }
            tileNum++;
            if (tileNum >= tiles.Count)
            {
                tileNum = 0;
            }
        }
        foreach(var tile in tiles)
        {
            tile.Draw(_spriteBatch );
        }
     
        foreach (var item in itemsList)
        {
            if (currentItemIndex == itemNum)
            {
                item.Draw(_spriteBatch, new Vector2(200, 300), SpriteEffects.None);
            }
            itemNum++;
            if (itemNum >= itemsList.Count)
            {
                itemNum = 0;
            }
        }

        foreach (var enemy in enemies)
        {
            //enemy.Draw(_spriteBatch);
            if (currentEnemyIndex == enemyNum)
            {
                enemy.Draw(_spriteBatch);
                enemy.GetCollider().DebugDraw(_spriteBatch, pixelTexture, enemy.GetCollider().hitbox, Color.Green);
            }
            enemyNum++;
            if (enemyNum >= enemies.Count)
            {
                enemyNum = 0;
            }
        }

        //Keep link below the tiles so he's drawn above them
       

        link.Draw(_spriteBatch);
        if (debugDraw)
        {
            link.GetCollider().DebugDraw(_spriteBatch, pixelTexture, link.GetCollider().hitbox, Color.Blue);
        }
        _spriteBatch.End();



        base.Draw(gameTime);
    }

    protected void createSprites()
    {
        linkTexture = Content.Load<Texture2D>("Images/Link Spritesheet");
        createItemSprites();
        link.createLinkSprites(linkTexture);
        createEnemySprites();
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

        enemyDeathTexture = Content.Load<Texture2D>("Images/EnemyDeathCloud");
        enemySpawnTexture = Content.Load<Texture2D>("Images/EnemyCloud");
        enemyDeathCloud = new EnemyDeathCloud(enemyDeathTexture);
        enemySpawnCloud = new EnemySpawnCloud(enemySpawnTexture);
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
        hand.createEnemySprites(enemyTexture);

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

    private void UpdateCollisions()
    {
        
        foreach (var tile in tiles)
        {
            CollisionBox collider = tile.GetCollider();
            if(collider != null)
            {
                link.CollisionUpdate(collider);
            }
            foreach (var enemy in enemies)
            {
                if (collider != null)
                {
                    enemy.CollisionUpdate(collider);
                }
            }
        }
        CollisionBox linkCollider = link.GetCollider();
        foreach (var enemy in enemies)
        {
            if (linkCollider != null)
            {
                //if enemy calls update with link, link can push it around but it can't push link
                enemy.CollisionUpdate(linkCollider);
                //vice versa if link calls update with enemy
            }
        }

        
        
    }
}
