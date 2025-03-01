using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Collision;
using Project1.Interfaces;

namespace Project1.Projectiles
{
    public class EnemyBoomerangProjectile : IProjectile
    {
        private Vector2 _position;
        private Vector2 _direction;
        private ISprite _sprite;
        private float _magnitude;
        private int frameCount = 0;
        private bool active = false;
        private bool peakReached = false;

        private float speedDecay = 0.1f;

        private CollisionBox collider;


        public EnemyBoomerangProjectile(Vector2 pos, ISprite sprite)
        {
            _sprite = sprite;
            collider = new CollisionBox((int)pos.X, (int)pos.Y);
        }
        public void Update(GameTime gameTime)//, Vector2 linkPos)
        {
            _sprite.Update(gameTime);
            _position = _magnitude * _direction + _position;
            if (active)
            {
                frameCount++;
                if (frameCount > 30)
                {
                    peakReached = true;
                }
            }
            if (peakReached)
            {
                if (_magnitude > -5)
                {
                    _magnitude -= speedDecay;
                }
                //ReturnToLink(linkPos);
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            SpriteEffects effect = SpriteEffects.None;
            int rotation = (frameCount / 5) % 4;
            switch (rotation)
            {
                case 0:
                    effect = SpriteEffects.None;
                    break;
                case 1:
                    effect = SpriteEffects.FlipHorizontally;
                    break;
                case 2:
                    effect = SpriteEffects.FlipVertically;
                    break;
                case 3:
                    effect = SpriteEffects.None;
                    break;
            }
            if (active)
            {
                _sprite.Draw(spriteBatch, _position, effect);
            }
        }
        public void Throw(Vector2 position, Vector2 direction)
        {
            if (active)
            {
                return;
            }
            _position = position;
            active = true;
            peakReached = false;
            _direction = direction;
            _magnitude = 5;
        }
        private void ReturnToLink(Vector2 linkPos)
        {
            _direction = (_position - linkPos);
            _direction.Normalize();
            if ((_position - linkPos).Length() < 10)
            {
                active = false;
                frameCount = 0;
                _magnitude = 0;
                peakReached = false;
            }
        }

        public void Destroy()
        {
            //
        }

        public CollisionBox GetCollider()
        {
            return collider;
        }
    }
}
