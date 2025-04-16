using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Interfaces;
using Project1.Collision;
using Project1.Sprites;
using Project1.Entities;

namespace Project1.Projectiles
{
    public class PortalProjectile : IProjectile
    {
        private Vector2 _position;
        private Vector2 _direction;
        private Texture2D _texture;
        private ISprite _portalSprite;
        private ISprite _closedPortalSprite;
        private ISprite _projectileSprite;
        private ISprite _projectileSpriteV;
        private ISprite _currentProjectileSprite;
        private CollisionBox _collider;

        private bool _isMoving = true;
        private bool _collided = false;
        private float _speed = 5f;
        private SpriteEffects _spriteEffect = SpriteEffects.None;
        private bool isOpen;

        public PortalProjectile(Vector2 startPosition, Vector2 direction, Texture2D texture, Rectangle portalSprite, Rectangle projectileSprite, Rectangle closedPortalSprite, Rectangle vProjectileSprite)
        {
            _position = startPosition;
            _direction = direction;
            _texture = texture;
            _portalSprite = new NMoveNAnim(texture,portalSprite);
            _closedPortalSprite = new NMoveNAnim(texture, closedPortalSprite);
            _projectileSprite = new NMoveNAnim(texture, projectileSprite);
            _projectileSpriteV = new NMoveNAnim(texture, vProjectileSprite);
            _collider = new CollisionBox((int)_position.X, (int)_position.Y, 16, 16);
            _currentProjectileSprite = _projectileSprite; 

        }

        public void Update(GameTime gameTime)
        {
            if (_isMoving)
            {
                Vector2 velocity = _direction * _speed;
                _position += velocity;
                _collider.Move((int)velocity.X, (int)velocity.Y);
                if (velocity.X == 0)
                {
                    _currentProjectileSprite = _projectileSpriteV;
                    if (velocity.Y > 0)
                    {
                        _spriteEffect = SpriteEffects.FlipVertically;
                    }
                }
                else
                {
                    _currentProjectileSprite = _projectileSprite;
                    if (velocity.X < 0)
                    {
                        _spriteEffect = SpriteEffects.FlipHorizontally;
                    }
                    
                }
                
            }
            else
            {
                _spriteEffect = SpriteEffects.None;
                if (isOpen)
                {
                    _currentProjectileSprite = _portalSprite;
                }
                else
                {
                    _currentProjectileSprite = _closedPortalSprite;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _currentProjectileSprite.Draw(spriteBatch, _position,_spriteEffect);
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
        public void setOpen()
        {
            isOpen = true;
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