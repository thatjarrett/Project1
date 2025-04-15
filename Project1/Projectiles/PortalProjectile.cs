using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Interfaces;
using Project1.Collision;

namespace Project1.Projectiles
{
    public class PortalProjectile : IProjectile
    {
        private Vector2 _position;
        private Vector2 _direction;
        private Texture2D _texture;
        private Rectangle _portalSprite;
        private Rectangle _projectileSprite;
        private CollisionBox _collider;

        private bool _isMoving = true;
        private bool _collided = false;
        private float _speed = 5f;

        public PortalProjectile(Vector2 startPosition, Vector2 direction, Texture2D texture, Rectangle portalSprite, Rectangle projectileSprite)
        {
            _position = startPosition;
            _direction = direction;
            _texture = texture;
            _portalSprite = portalSprite;
            _projectileSprite = projectileSprite;
            _collider = new CollisionBox((int)_position.X, (int)_position.Y, _projectileSprite.Width, _projectileSprite.Height);
        }

        public void Update(GameTime gameTime)
        {
            if (_isMoving)
            {
                Vector2 velocity = _direction * _speed;
                _position += velocity;
                _collider.Move((int)velocity.X, (int)velocity.Y);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle source = _isMoving ? _projectileSprite : _portalSprite;
            spriteBatch.Draw(_texture, _position, source, Color.White);
        }

        public void StopMoving()
        {
            _isMoving = false;
            _collided = true;
        }

        public bool HasCollided()
        {
            return _collided;
        }

        public void Throw(Vector2 start, Vector2 direction)
        {
            _position = start;
            _direction = direction;
            _isMoving = true;
            _collided = false;
            _collider.setPos((int)start.X, (int)start.Y);
        }

        public void Destroy()
        {
            _isMoving = false;
        }

        public CollisionBox GetCollider()
        {
            return _collider;
        }

        public void ownerPosition(Vector2 newPosition)
        {
            // Not used for portals, but required by interface
        }

        public Vector2 GetPosition()
        {
            return _position;
        }

        public bool IsMoving()
        {
            return _isMoving;
        }

        public void SetColor(Color color) { /* optional visual feedback */ }
    }
}