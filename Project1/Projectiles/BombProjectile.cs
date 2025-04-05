using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Collision;
using Project1.Entities;
using Project1.Interfaces;
using Microsoft.Xna.Framework.Audio;
using Project1.GameObjects.Environment;
using System.Collections.Generic;



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
    private List<environmentTile> tileList;

    public BombProjectile(Vector2 Position, ISprite bombSprite, ISprite explodingSprite, Link link, List<environmentTile> tileList)
    {
        _position = Position;
        this.bombSprite = bombSprite;
        this.explodingSprite = explodingSprite;
        bombTimer = 0;
        this.tileList = tileList;
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
                BreakNearbyWalls();
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
            owner.DeleteBomb();
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
    private void BreakNearbyWalls()
    {
        const int explosionRadius = 48;

        foreach (var tile in tileList)
        {
            if (tile.IsBreakable && tile.IsSolid)
            {
                CollisionBox box = tile.GetCollider();
                if (box != null)
                {
                    Rectangle hitbox = box.hitbox;
                    Vector2 tileCenter = new Vector2(hitbox.X + hitbox.Width / 2f, hitbox.Y + hitbox.Height / 2f);

                    if (Vector2.Distance(tileCenter, _position) <= explosionRadius)
                    {
                        tile.Break();
                    }
                }
            }
        }
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
