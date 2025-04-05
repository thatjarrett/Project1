using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Collision;
using Project1.Interfaces;

namespace Project1.GameObjects.Environment
{
    public abstract class environmentTile
    {
        protected Vector2 _position;
        protected bool _collides;
        private ISprite _sprite;
        protected CollisionBox collider;

        public environmentTile(Vector2 pos, bool collision)
        {
            _position = pos;
            _collides = collision;
        }
        public void Update(GameTime gameTime)
        {
            if (_sprite != null)
            {
                _sprite.Update(gameTime);
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            _sprite.Draw(spriteBatch, _position, SpriteEffects.None);
        }

        public void setSprite(ISprite sprite)
        {
            _sprite = sprite;
        }
        public void setCollision(bool collision)
        {
            _collides = collision;
        }

        public CollisionBox GetCollider()
        { return collider; }

        public virtual void SetCollider()
        {
            if (_collides)
            {
                collider = new CollisionBox((int)_position.X, (int)_position.Y);
            }
            
        }
    }
}
