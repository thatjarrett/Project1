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
using Microsoft.Xna.Framework.Media;
using Project1.Audio;
using Project1.HUD;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private KeyboardController keyboardController;
    private GamepadController gamepadController;
    private Link link;

    private List<environmentTile> tiles = new List<environmentTile>();
    private List<IEnemy> enemies = new List<IEnemy>();

    private DungeonMusicPlayer dungeonMusicPlayer;

    private Camera Camera;

    Texture2D linkTexture;
    Texture2D hudTexture;
    Texture2D heartsTexture;
    Texture2D coverTexture;
    Texture2D environmentTexture;
    Texture2D npcTexture;
    Texture2D itemTexture;
    Texture2D aquamentusTexture;
    Texture2D enemyTexture;
    Texture2D enemyDeathTexture;
    Texture2D enemySpawnTexture;
    Texture2D atlasTexture;

    ISprite enemyDeathCloud;
    ISprite enemySpawnCloud;
    SpriteFont font1;
    
    private int currentBlockIndex = 0;
    private int currentItemIndex = 0;
    private int currentNPCIndex = 0;
    
    private int currentEnemyIndex = 0;

    private List<IItem> itemsList = new List<IItem>();

    levelManager levels;
    IHUD hud;

    private bool paused = false;
    

    //Debug Variables
    Texture2D pixelTexture;
    bool debugDraw = true;

    private EntityBuilder entityBuilder;// = new EntityBuilder(aquamentusTexture, enemytexture, );
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void LoadContent()
    {
        font1 = Content.Load<SpriteFont>("Images/File");
        pixelTexture = new Texture2D(GraphicsDevice, 1, 1);
        pixelTexture.SetData(new[] { Color.White });
        DungeonMusicPlayer.Instance.LoadContent(Content);
        MusicManager.Instance.LoadContent(Content);
        StraightProjectile.LoadContent(Content);
        BoomerangProjectile.LoadContent(Content);
        DungeonMusicPlayer.Instance.PlayDungeonMusic();
        GameManager.Instance.LoadContent(Content);
        AttackCommand.LoadContent(Content);
        BombProjectile.LoadContent(Content);
        link = new Link(new Vector2(350, 170));
        linkTexture = Content.Load<Texture2D>("Images/Link Spritesheet");


        createSprites();
        hud = new IHUD(link, hudTexture,heartsTexture,coverTexture,atlasTexture,font1);
        environmentTile pushBlock = new pushableBlock(new Vector2(100, 100));

        _spriteBatch = new SpriteBatch(GraphicsDevice);


        
        environmentTexture = Content.Load<Texture2D>("Images/dungeonTiles");
        npcTexture = Content.Load<Texture2D>("Images/oldMan");
        aquamentusTexture = Content.Load<Texture2D>("Images/bosses");
        enemyTexture = Content.Load<Texture2D>("Images/enemies");
        itemTexture = Content.Load<Texture2D>("NES - The Legend of Zelda - Items & Weapons");

        
        levels = new levelManager(environmentTexture, npcTexture, aquamentusTexture, enemyTexture, itemTexture);


        tiles.AddRange(levels.buildTiles());

        List<IItem> tempitemlist = new List<IItem>();
        List<IEnemy> tempenemylist = new List<IEnemy>();
        (tempitemlist,tempenemylist) = levels.buildEntities(); // potentially referenceing issues here? but if it works I wont think too hard about it
        // can return empty lists, im pretty sure draw and update break if there are no enemies or no items on the map
        itemsList.AddRange(tempitemlist);
        enemies.AddRange(tempenemylist);



        //itemsList.Add(enemyDeathCloud);
        //itemsList.Add(enemySpawnCloud);


         //When adding other tiles remember to add them to "tiles" list and delete this comment! - Bren
         //Add bomb to list of items and delete this comment when items are implemented! -Bren
         //Add old man to list of characters and delete this comment when enemies are implemented! -Bren

         entityBuilder = new EntityBuilder(aquamentusTexture, enemyTexture, itemTexture);

    }
    protected override void Initialize()
    {
        base.Initialize();

        Camera = new Camera(new Viewport());

        var commands = new Dictionary<Keys, ICommand>
    {
{ Keys.W, new MoveUpCommand(link,hud) },
{ Keys.Up, new MoveUpCommand(link,hud) },
{ Keys.S, new MoveDownCommand(link,hud) },
{ Keys.Down, new MoveDownCommand(link,hud) },
{ Keys.A, new MoveLeftCommand(link,hud) },
{ Keys.Left, new MoveLeftCommand(link,hud) },
{ Keys.D, new MoveRightCommand(link,hud) },
{ Keys.Right, new MoveRightCommand(link,hud) },
{ Keys.Z, new AttackCommand(link) },
{ Keys.N, new AttackCommand(link) },
{ Keys.E, new DamageCommand(link) },
{ Keys.X, new UseItemCommand(link)},
{ Keys.G, new DeathCommand(link) },
{ Keys.Q, new QuitCommand(this) },
{ Keys.R, new ResetCommand(this) },
{ Keys.Escape, new PauseCommand(this) },
/*{ Keys.T, new CycleBlockCommand(this, false) }, // Previous block
{ Keys.Y, new CycleBlockCommand(this, true) },  // Next block
{ Keys.U, new CycleItemCommand(this, false) }, // Previous item
{ Keys.I, new CycleItemCommand(this, true) },  // Next item
{ Keys.O, new CycleNPCCommand(this, false) }, // Previous NPC
{ Keys.P, new CycleNPCCommand(this, true) }   // Next NPC
*/
    };
        var gamepadCommands = new Dictionary<Buttons, ICommand>
{
    { Buttons.DPadUp, new MoveUpCommand(link,hud) },
    { Buttons.LeftThumbstickUp, new MoveUpCommand(link,hud) },
    { Buttons.DPadDown, new MoveDownCommand(link,hud) },
    { Buttons.LeftThumbstickDown, new MoveDownCommand(link,hud) },
    { Buttons.DPadLeft, new MoveLeftCommand(link,hud) },
    { Buttons.LeftThumbstickLeft, new MoveLeftCommand(link,hud) },
    { Buttons.DPadRight, new MoveRightCommand(link,hud) },
    { Buttons.LeftThumbstickRight, new MoveRightCommand(link,hud) },
    { Buttons.A, new AttackCommand(link) },
    { Buttons.X, new UseItemCommand(link) },
    { Buttons.B, new DamageCommand(link) },
    { Buttons.Back, new QuitCommand(this) },
    { Buttons.Start, new ResetCommand(this) },
    { Buttons.BigButton, new ResetCommand(this) }
};
        
        gamepadController = new GamepadController(gamepadCommands, new IdleCommand(link));

        keyboardController = new KeyboardController(commands, new IdleCommand(link));
        _graphics.PreferredBackBufferWidth = 768;
        _graphics.PreferredBackBufferHeight = 648;
        _graphics.ApplyChanges();
    }

    protected override void Update(GameTime gameTime)
    {
        keyboardController.Update(gameTime);
        gamepadController.Update(gameTime);
        hud.Update(gameTime);
        GameTimer.Update(gameTime);// Gamepad input added
        if (!paused)
        { 
            hud.slideOut();
            DungeonMusicPlayer.Instance.PlayDungeonMusic();
            GameManager.Instance.Update(gameTime);
            
        link.Update(gameTime);
        foreach (var tile in tiles)
        {
            tile.Update(gameTime);
        }

        base.Update(gameTime);

        
        UpdateCollisions(gameTime);

        removeInactive();
        }
        else
        {
            hud.slideIn();
        }

    }

    public void removeInactive() {
        int x = enemies.Count - 1;

        while (x >= 0) {
            if (!enemies[x].Alive()) {
                IItem i = entityBuilder.buildItem(enemies[x].getLoot(), enemies[x].getPos());
                itemsList.Add(i);
                enemies.RemoveAt(x);
                //TODO: play death animation also
            }
            x--;
        }

        int why = itemsList.Count - 1;
        while (why >= 0)
        {
            if (!itemsList[why].isActive())
            {
                itemsList.RemoveAt(why);
            }
            why--;
        }
    }
    

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin(
            SpriteSortMode.Deferred,
            BlendState.NonPremultiplied,
            SamplerState.PointClamp,
            null,
            null,
            null,
            Camera.GetTransformation(link.GetCenterPos())
        );




        int tileNum = 0;
        int enemyNum = 0;
        int itemNum =0;
        /*foreach (var tile in tiles)
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
        }*/
        foreach(var tile in tiles)
        {
            
            if (tile is not doorTile)
            {
                tile.SetCollider();
                tile.Draw(_spriteBatch);
            } 
            else
            {
                //some door logic
                //tile.Draw(_spriteBatch);
            }
        }
     
        foreach (var item in itemsList)
        {
           item.Draw(_spriteBatch, SpriteEffects.None);
            if (debugDraw)
            {
                item.GetCollider().DebugDraw(_spriteBatch, pixelTexture, item.GetCollider().hitbox, Color.White);
            }
        }

        foreach (var enemy in enemies)
        { 
                enemy.Draw(_spriteBatch);
            if (debugDraw)
            {
                enemy.GetCollider().DebugDraw(_spriteBatch, pixelTexture, enemy.GetCollider().hitbox, Color.Red);
            }
        }

        //Keep link below the tiles so he's drawn above them

        foreach (var tile in tiles)
        {
            if(tile is pushableBlock)
            {
                tile.Draw(_spriteBatch);
            }
        }

        if (!paused)
        {
            link.Draw(_spriteBatch);
        }
        
        if (debugDraw)
        {
            link.GetCollider().DebugDraw(_spriteBatch, pixelTexture, link.GetCollider().hitbox, Color.Blue);
        }
        
        

        _spriteBatch.End();

        _spriteBatch.Begin(
            SpriteSortMode.Deferred,
            BlendState.NonPremultiplied,
            SamplerState.PointClamp
        );

        hud.Draw(_spriteBatch);
        GameManager.Instance.Draw(_spriteBatch, GraphicsDevice);

        _spriteBatch.End();

        base.Draw(gameTime);
    }

    protected void createSprites()
    {
        linkTexture = Content.Load<Texture2D>("Images/Link Spritesheet");
        hudTexture = Content.Load<Texture2D>("Images/blankUI");
        atlasTexture = Content.Load<Texture2D>("Images/fullUi");
        heartsTexture = Content.Load<Texture2D>("Images/HealthSprite");
        coverTexture = Content.Load<Texture2D>("Images/coverSprite");
        createItemSprites();
        link.CreateLinkSprites(linkTexture);
    }

    protected void createItemSprites()
    {
        itemTexture = Content.Load<Texture2D>("NES - The Legend of Zelda - Items & Weapons");
        enemyDeathTexture = Content.Load<Texture2D>("Images/EnemyDeathCloud");
        enemySpawnTexture = Content.Load<Texture2D>("Images/EnemyCloud");
        enemyDeathCloud = new EnemyDeathCloud(enemyDeathTexture, new Vector2(0,0));
        enemySpawnCloud = new EnemySpawnCloud(enemySpawnTexture, new Vector2(0, 0));
        
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

  
    public void RestartGame()
    {
        // Clear all game objects
        tiles.Clear();
        itemsList.Clear();
        enemies.Clear();

        // Reinitialize the game
        Initialize();
    }

    private void UpdateCollisions(GameTime gameTime)
    {
        
        foreach (var tile in tiles)
        {
            if (tile is wallTile wall)
            {
                List<CollisionBox> colliders = wall.GetColliderList();
                if(colliders != null)
                {
                    foreach(var collider in colliders)
                    {
                        link.CollisionUpdate(collider);
                    }
                }

            } else
            {
                CollisionBox collider = tile.GetCollider();
                if (tile is pushableBlock block)
                {
                    CollisionBox linkPush = link.GetCollider();
                    block.CollisionUpdate(linkPush);



                }
                else if (collider != null)
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

        int itemNum = 0;
        foreach (var item in itemsList)
        {
            item.Update(gameTime);

            item.Update(gameTime);
            LinkEnemyCollisionHandler.HandleCollision(item, link);

            itemNum++;
            if (itemNum >= enemies.Count)
            {
                itemNum = 0;
            }
        }

        List<IProjectile> linkBombs = link.GetBombs();
        foreach (var b in linkBombs)
        {
            LinkEnemyCollisionHandler.HandleCollision(link, b);
        }

        int enemyNum = 0;
        foreach (var enemy in enemies)
        {
            if (enemy is IDependentEnemy spikeTrap)
            {
                spikeTrap.Update(gameTime, link, link.IsFrozen());
            } else
            {
                enemy.Update(gameTime, link.IsFrozen());
            }

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

            foreach (var b in linkBombs)
            {
                LinkEnemyCollisionHandler.HandleCollision(b, enemy);
            }

            CollisionBox sword = link.GetSword();
            LinkEnemyCollisionHandler.HandleCollision(sword, enemy);
            enemyNum++;
            if (enemyNum >= enemies.Count)
            {
                enemyNum = 0;
            }
        }
    }
    public void PauseGame()
    {
        paused = !paused;
    }
}
