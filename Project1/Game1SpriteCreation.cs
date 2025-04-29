using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;



public partial class Game1 : Game
{
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

    }

}
