using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Interfaces;
using Project1.Collision;


namespace Project1.Projectiles
{
    public abstract class Projectile : IProjectile
    {
        private Vector2 _position;
        private Vector2 _direction;
        private ISprite _sprite1;
        private ISprite _sprite2;
        private int _magnitude;
        private CollisionBox collider;

        public Projectile(Vector2 pos, Vector2 direction, ISprite sprite1, ISprite sprite2, int magnitude)
        {
            _position = pos;
            _direction = direction;
            _sprite1 = sprite1;
            _sprite2 = sprite2;
            _magnitude = magnitude;
            collider = new CollisionBox((int)pos.X, (int)pos.Y);

        }
        public void Update(GameTime gameTime)
        {
            _sprite1.Update(gameTime);
            _sprite2.Update(gameTime);
            _position = _magnitude * _direction + _position;
            collider.Move((int) (_magnitude*_direction.X), (int)(_magnitude * _direction.Y));
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            SpriteEffects effect = SpriteEffects.None;
            ISprite activeSprite = _sprite1;

            if (_direction.X < 0)
            {
                effect = SpriteEffects.FlipHorizontally;
            }
            if (_direction.Y > 0)
            {
                effect = SpriteEffects.FlipVertically;
            }
            if (_direction.X == 0)
            {
                activeSprite = _sprite2;
            }

            activeSprite.Draw(spriteBatch, _position, effect);


        }

        public void Destroy()
        {
            //Use this to get rid of projectiles
        }

        public CollisionBox GetCollider()
        {
            return collider;
        }
    }
}
