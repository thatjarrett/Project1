using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Entities;
using Project1.GameObjects.Environment;
using Project1.Interfaces;
using Project1.Projectiles;
using Project1.LevelLoading;
using Project1.Audio;
using Project1;
using Microsoft.Xna.Framework.Audio;



public partial class Game1 : Game
{
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
        (tempitemlist, tempenemylist) = levels.buildEntities(); // potentially referenceing issues here? but if it works I wont think too hard about it
        // can return empty lists, im pretty sure draw and update break if there are no enemies or no items on the map
        itemsList.AddRange(tempitemlist);
        enemies.AddRange(tempenemylist);


        entityBuilder = new EntityBuilder(aquamentusTexture, enemyTexture, itemTexture, enemyDeathTexture);



    }

}
