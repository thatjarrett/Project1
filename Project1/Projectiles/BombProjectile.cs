using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Collision;
using Project1.Interfaces;

public class BombProjectile
{

    private Vector2 _position;
    private ISprite bombSprite;
    private ISprite explodingSprite;
    private int bombTimer;
    private CollisionBox collider;

    public BombProjectile(Vector2 Position, ISprite bombSprite, ISprite explodingSprite)
	{
        _position = Position;
        this.bombSprite = bombSprite;
        this.explodingSprite = explodingSprite;
        bombTimer = 300;
        collider = new CollisionBox((int)_position.X, (int)_position.Y);

    }
    public void Update(GameTime gameTime)
    {
        if (bombTimer < 300)
        {
            bombTimer++;
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
            //explodingSprite.Draw(spriteBatch, _position, SpriteEffects.FlipVertically);
        }
    }
    public void placeBomb(Vector2 Position)
    {
        this._position = Position;
        bombTimer = 0;

    }

    public CollisionBox GetCollider()
    {
        return collider;
    }
}
