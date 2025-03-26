using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Collision;
using Project1.Entities;
using Project1.Interfaces;
using Microsoft.Xna.Framework.Audio;



public class BombProjectile : IProjectile
{
    private static SoundEffect bombDropSound;
    private static SoundEffect bombExplodeSound;
    private bool exploded = false;

    private Vector2 _position;
    private ISprite bombSprite;
    private ISprite explodingSprite;
    private int bombTimer;
    private CollisionBox collider;
    private Link owner;

    public BombProjectile(Vector2 Position, ISprite bombSprite, ISprite explodingSprite, Link link)
	{
        _position = Position;
        this.bombSprite = bombSprite;
        this.explodingSprite = explodingSprite;
        bombTimer = 0;//300;
        collider = null;//ew CollisionBox((int)_position.X, (int)_position.Y);
        owner = link;

    }
    public static void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
    {
        bombDropSound = content.Load<SoundEffect>("Audio/Bomb Drop");
        bombExplodeSound = content.Load<SoundEffect>("Audio/Bomb Explode");
    }

    public void Update(GameTime gameTime)
    {
        if (bombTimer < 300)
        {
            bombTimer++;
            if (bombTimer == 150 && !exploded)
            {
                bombExplodeSound?.Play();
                exploded = true;
            }

        }
        bombSprite.Update(gameTime);
        explodingSprite.Update(gameTime);
    }
    public void Draw(SpriteBatch spriteBatch)
    {

        if (bombTimer < 150)
        {
            bombSprite.Draw(spriteBatch, _position, SpriteEffects.None);
        }
        else if (bombTimer < 200)
        {
            explodingSprite.Draw(spriteBatch, _position, SpriteEffects.None);
            //if (collider == null) {
                collider = new CollisionBox((int)_position.X, (int)_position.Y);
            //}
            //explodingSprite.Draw(spriteBatch, _position, SpriteEffects.FlipVertically);
            explodingSprite.Draw(spriteBatch, _position, SpriteEffects.None);

        }
        else if (bombTimer < 290) {
            owner.deleteBomb();
        }
    }
    public void placeBomb(Vector2 Position)
    {
        this._position = Position;
        bombTimer = 0;
        exploded = false;

        bombDropSound?.Play();
    }


    public CollisionBox GetCollider()
    {
        return collider;
    }

    public void Destroy()
    {
        //
    }

    public void Throw(Vector2 position, Vector2 direction)
    {
        //does nothing
    }

    public void ownerPosition(Vector2 owner)
    {
        //does nothing
    }
}
