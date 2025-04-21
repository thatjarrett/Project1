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
using static Project1.Entities.Link;
using Project1;
using Microsoft.Xna.Framework.Audio;



public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private KeyboardController keyboardController;
    private GamepadController gamepadController;
    private Link link;
    private KeyboardState previousKeyboard;

    private List<environmentTile> tiles = new List<environmentTile>();
    private List<IEnemy> enemies = new List<IEnemy>();

    private DungeonMusicPlayer dungeonMusicPlayer;

    private Camera Camera;
    private DevConsole devConsole;
    private KeyboardState prevKeyboardState;


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
    private PortalManager portalManager;

    //ISprite enemyDeathCloud;
    //ISprite enemySpawnCloud;
    SpriteFont font1;
    
    private int currentBlockIndex = 0;
    private int currentItemIndex = 0;
    //private int currentNPCIndex = 0;
    
    private int currentEnemyIndex = 0;

    private List<IItem> itemsList = new List<IItem>();

    private List<IAnimation> animationsList = new List<IAnimation>();

    levelManager levels;
    IHUD hud;

    private bool paused = false;
    private bool IsTransitioning = false;



    //Debug Variables
    Texture2D pixelTexture;
    bool debugDraw = false;

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
        link = new Link(new Vector2(350, 250));
        linkTexture = Content.Load<Texture2D>("Images/Link Spritesheet");
        Texture2D crackedWallTexture = Content.Load<Texture2D>("Images/crackedWall");
        Texture2D portalSheet = Content.Load<Texture2D>("Images/portalSprites");
        SoundEffect bluePortalSound = Content.Load<SoundEffect>("portal1");
        SoundEffect orangePortalSound = Content.Load<SoundEffect>("portal2");

        // Define source rectangles from the sprite sheet
        Rectangle bluePortalRect = new Rectangle(16, 0, 16, 16);         // example coords
        Rectangle blueProjectileRect = new Rectangle(32, 0, 16, 16);    // projectile
        Rectangle bluePortalClosedRect = new Rectangle(0, 0, 16, 16);         // example coords
        Rectangle blueProjectileVRect = new Rectangle(48, 0, 16, 16);    // projectile
        Rectangle orangePortalRect = new Rectangle(16, 16, 16, 16);      // example coords
        Rectangle orangeProjectileRect = new Rectangle(32, 16, 16, 16); // projectile
        Rectangle orangePortalClosedRect = new Rectangle(0, 16, 16, 16);      // example coords
        Rectangle orangeProjectileVRect = new Rectangle(48, 16, 16, 16); // projectile

        // Create the portal manager with all required arguments
        portalManager = new PortalManager(
     portalSheet,
     bluePortalRect,
     blueProjectileRect,
     bluePortalClosedRect,
     blueProjectileVRect,
     orangePortalRect,
     orangeProjectileRect,
     orangePortalClosedRect,
     orangeProjectileVRect,
     bluePortalSound,
     orangePortalSound
 );


        // Attach to Link
        link.SetPortalManager(portalManager);




        createSprites();
        
        environmentTile pushBlock = new pushableBlock(new Vector2(100, 100));

        _spriteBatch = new SpriteBatch(GraphicsDevice);


        
        environmentTexture = Content.Load<Texture2D>("Images/dungeonTiles");
        npcTexture = Content.Load<Texture2D>("Images/oldMan");
        aquamentusTexture = Content.Load<Texture2D>("Images/bosses");
        enemyTexture = Content.Load<Texture2D>("Images/enemies");
        itemTexture = Content.Load<Texture2D>("NES - The Legend of Zelda - Items & Weapons");


        levels = new levelManager(environmentTexture, npcTexture, aquamentusTexture, enemyTexture, itemTexture, crackedWallTexture, enemyDeathTexture);
        tiles.AddRange(levels.buildTiles());
        foreach (var tile in tiles)
        {
            if (!(tile is doorTile door && door.GetOpen()))
            {
                tile.SetCollider(); // Do this once
            }
            
        }
        

      

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

        entityBuilder = new EntityBuilder(aquamentusTexture, enemyTexture, itemTexture, enemyDeathTexture);

        //IEnemy g = entityBuilder.buildEnemy(6, new Vector2(200, 200));
        //enemies.Add(g);

    }
    protected override void Initialize()
    {
        base.Initialize();

        Camera = new Camera(new Viewport());
        hud = new IHUD(link, hudTexture, heartsTexture, coverTexture, atlasTexture, font1, Camera);
        devConsole = new DevConsole(font1, GraphicsDevice, link);


        var commands = new Dictionary<Keys, ICommand>
    {{ Keys.C, new StartBluePortalCommand(link) },     
    { Keys.V, new StartOrangePortalCommand(link) },
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
        var releaseCommands = new Dictionary<Keys, ICommand>
{
    { Keys.C, new EndBluePortalCommand(link) },
    { Keys.V, new EndOrangePortalCommand(link) }
};

        keyboardController = new KeyboardController(commands, new IdleCommand(link), releaseCommands);

        var movementCommands = new Dictionary<Project1.Controllers.Direction, ICommand>
{
    { Project1.Controllers.Direction.Up, new MoveUpCommand(link, hud) },
    { Project1.Controllers.Direction.Down, new MoveDownCommand(link, hud) },
    { Project1.Controllers.Direction.Left, new MoveLeftCommand(link, hud) },
    { Project1.Controllers.Direction.Right, new MoveRightCommand(link, hud) },

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
    { Buttons.B, new UseItemCommand(link) },
    { Buttons.LeftShoulder, new StartBluePortalCommand(link) },
{ Buttons.RightShoulder, new StartOrangePortalCommand(link) },

    { Buttons.Back, new QuitCommand(this) },
    { Buttons.Start, new ResetCommand(this) },
    { Buttons.BigButton, new ResetCommand(this) }
};
        var gamepadReleaseCommands = new Dictionary<Buttons, ICommand>
{
    { Buttons.LeftShoulder, new EndBluePortalCommand(link) },
    { Buttons.RightShoulder, new EndOrangePortalCommand(link) }
};
        gamepadController = new GamepadController(gamepadCommands, movementCommands, new IdleCommand(link), gamepadReleaseCommands);

        
        _graphics.PreferredBackBufferWidth = 768;
        _graphics.PreferredBackBufferHeight = 648;
        _graphics.ApplyChanges();
   

    }

    protected override void Update(GameTime gameTime)
    {
        if (!devConsole.IsOpen)
        {
            keyboardController.Update(gameTime);
            gamepadController.Update(gameTime);
        }

        hud.Update(gameTime);
        portalManager?.Update(gameTime);
        GameTimer.Update(gameTime);// Gamepad input added
        if (!paused)
        { 
            hud.slideOut();
            DungeonMusicPlayer.Instance.PlayDungeonMusic();
            GameManager.Instance.Update(gameTime);
            
            if(!IsTransitioning)
            {
                //freezing link during transition breaks only the downward transition for some reason
            }
            link.Update(gameTime);
            foreach (var tile in tiles)
            {
                tile.Update(gameTime);
                    if(tile is pushableBlock block)
                    {
                        block.Update();
                    }
            }
                KeyboardState currentKeyboard = Keyboard.GetState();
                if (currentKeyboard.IsKeyDown(Keys.M) && previousKeyboard.IsKeyUp(Keys.M))
                {
                    DungeonMusicPlayer.Instance.ToggleMusic();
                }
                previousKeyboard = currentKeyboard;

                base.Update(gameTime);
            CollisionBox portalBox = portalManager.GetBlueCollider(); // or similar
            foreach (var tile in tiles)
            {
                var wallCollider = tile.GetCollider();
                if (wallCollider != null && portalBox != null && portalBox.Intersects(wallCollider))
                {
                    portalManager.StopBluePortal(); // or: portal.StopMoving()
                }
            }


            UpdateCollisions(gameTime);

            removeInactive();
        }
        else
        {
            hud.slideIn();
        }
        KeyboardState currentKeyboardState = Keyboard.GetState();

        if (currentKeyboardState.IsKeyDown(Keys.OemTilde) && prevKeyboardState.IsKeyUp(Keys.OemTilde))
        {
            devConsole.Toggle();
        }

        devConsole.Update(gameTime);

        if (devConsole.IsOpen)
        {
            
            prevKeyboardState = currentKeyboardState;
            return;
        }

        prevKeyboardState = currentKeyboardState;


    }

    public void removeInactive() {
        int x = enemies.Count - 1;

        while (x >= 0) {
            if (!enemies[x].Alive()) {
                IItem i = entityBuilder.buildItem(enemies[x].getLoot(), enemies[x].getPos());
                itemsList.Add(i);

                IAnimation death = entityBuilder.buildAnimation(1, enemies[x].getPos());
                animationsList.Add(death);

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

        int z = animationsList.Count - 1;
        while (z >= 0) {
            if (!animationsList[z].isActive())
            {
                animationsList.RemoveAt(z);
            }
            z--;
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
            Camera.GetTransformation(link.GetCenterPos(), ref IsTransitioning)
        );
        
        // Draw regular tiles first
        foreach (var tile in tiles)
        {
            if (tile is not pushableBlock/* && tile is not doorTile*/)
                tile.Draw(_spriteBatch);
        }

        // Then draw pushable blocks on top
        foreach (var tile in tiles)
        {
            if (tile is pushableBlock)
                tile.Draw(_spriteBatch);
        }

        foreach (var tile in tiles)
        {
            if (tile is doorTile)
            {
                CollisionBox collider = tile.GetCollider();
                if(collider != null)
                {
                    //tile.GetCollider().DebugDraw(_spriteBatch, pixelTexture, collider.hitbox, Color.White);
                }
                
            }
        }


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
        }*/



        foreach (var anim in animationsList) {
            anim.Draw(_spriteBatch, SpriteEffects.None);
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

            if (tile is pushableBlock)
            {
                tile.Draw(_spriteBatch);
            }
        }
        portalManager?.Draw(_spriteBatch);
        if (!paused && !IsTransitioning)
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

        _spriteBatch.Begin();
        devConsole.Draw(_spriteBatch);
        _spriteBatch.End();
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
        //enemyDeathCloud = new EnemyDeathCloud(enemyDeathTexture, new Vector2(0,0));
        //enemySpawnCloud = new EnemySpawnCloud(enemySpawnTexture, new Vector2(0, 0));
        
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
        animationsList.Clear();

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
                if (colliders != null)
                {
                    foreach (var collider in colliders)
                    {
                        link.CollisionUpdate(collider);
                        foreach (var enemy in enemies)
                            enemy.CollisionUpdate(collider);

                        if (portalManager?.GetBlueCollider()?.Intersects(collider) == true)
                        {
                            portalManager.StopBluePortal();
                        }

                        if (portalManager?.GetOrangeCollider()?.Intersects(collider) == true)
                        {
                            portalManager.StopOrangePortal();
                        }
                    }
                }


            }
            else if (tile is stairsTile stairs) {
                if (stairs.GetCollider().Intersects(link.GetCollider())) {
                    //TODO: alter this later i guess...
                    link.Move(0, 300);
                }
            }
            else
            {
                CollisionBox collider = tile.GetCollider();
                if (tile is pushableBlock block)
                {
                    List<CollisionBox> blocking = new List<CollisionBox>();

                    foreach (var otherTile in tiles)
                    {
                        if (otherTile == tile) continue;

                        if (otherTile is wallTile wall2)
                        {
                            var wallBoxes = wall2.GetColliderList();
                            if (wallBoxes != null)
                            {
                                blocking.AddRange(wallBoxes);
                            }
                        }
                        else
                        {
                            CollisionBox otherCollider = otherTile.GetCollider();
                            if (otherCollider != null)
                            {
                                blocking.Add(otherCollider);
                            }
                        }
                    }
                    block.CollisionUpdate(link.GetCollider(), blocking);
                    link.CollisionUpdate(collider);
                }
                else if (collider != null)
                {
                    if (tile is LockedDoorTile keyDoor && link.GetCollidingSide(collider) != CollisionSide.None && !keyDoor.GetOpen())
                    {
                        keyDoor.TryOpen(link);
                        if (keyDoor.IsSolid) {

                            link.CollisionUpdate(collider);
                        }
                    }
                    else if (tile is doorTile door2 && door2.GetOpen())
                    {
                        if (link.GetCollidingSide(collider) == CollisionSide.Bottom)
                        {
                            link.Move(0, -144);
                        }
                        else if (link.GetCollidingSide(collider) == CollisionSide.Top)
                        {
                            link.Move(0, 144);
                        }
                        else if (link.GetCollidingSide(collider) == CollisionSide.Right)
                        {
                            link.Move(-144, 0);
                        }
                        else if (link.GetCollidingSide(collider) == CollisionSide.Left)
                        {
                            link.Move(144, 0);
                        }
                    }
                    else if (!(tile is doorTile door && door.GetOpen()))
                    {
                        link.CollisionUpdate(collider);
                    }
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
        
        CollisionBox blue = portalManager?.GetBlueCollider();
        CollisionBox orange = portalManager?.GetOrangeCollider();

        foreach (var tile in tiles)
        {
            var wallCollider = tile.GetCollider();
            if (wallCollider != null)
            {
                if (blue != null && blue.Intersects(wallCollider))
                {
                    portalManager.StopBluePortal();
                }
                if (orange != null && orange.Intersects(wallCollider))
                {
                    portalManager.StopOrangePortal();
                }
            }
        }

        foreach (var item in itemsList)
        {
            

            item.Update(gameTime);
            LinkEnemyCollisionHandler.HandleCollision(item, link);
        }

        foreach (var anim in animationsList) {
            anim.Update(gameTime);
        }

        List<IProjectile> linkBombs = link.GetBombs();
        int tileCount = tiles.Count - 1;
        foreach (BombProjectile b in linkBombs)
        {
            LinkEnemyCollisionHandler.HandleCollision(link, b);
            while (tileCount >= 0) {
                environmentTile t = tiles[tileCount];
                if (t is CrackedWallTile c) {
                    LinkEnemyCollisionHandler.HandleCollision(b, c);
                }
                tileCount --;
            }
        }

        foreach (var enemy in enemies)
        {
            CollisionBox collider = enemy.GetCollider();
            foreach (var enemy2 in enemies)
            {
                if (enemy != enemy2)
                {
                    enemy2.CollisionUpdate(collider);
                }
            }

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
        }
    }
    public void PauseGame()
    {
        paused = !paused;
    }
}
