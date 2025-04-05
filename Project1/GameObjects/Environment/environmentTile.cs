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
        protected bool _isSolid = false;
        protected bool _isBreakable = false;

        private ISprite _sprite;
        protected CollisionBox collider;

        public environmentTile(Vector2 pos, bool collision)
        {
            _position = pos;
            _collides = collision;
        }

        public bool IsSolid
        {
            get => _isSolid;
            protected set => _isSolid = value;
        }

        public bool IsBreakable
        {
            get => _isBreakable;
            protected set => _isBreakable = value;
        }


        public virtual void Break()
        {
            // By default, nothing happens. Override in breakable tiles.
        }

        public void Update(GameTime gameTime)
        {
            _sprite?.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _sprite?.Draw(spriteBatch, _position, SpriteEffects.None);
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
        {
            return collider;
        }

        public virtual void SetCollider()
        {
            if (_collides)
            {
                collider = new CollisionBox((int)_position.X, (int)_position.Y);
            }
        }
    }
}
