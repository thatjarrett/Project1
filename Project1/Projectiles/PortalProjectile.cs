using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Project1.Interfaces;
using Project1.Collision;
using Project1.Sprites;

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
        private ISprite _currentSprite;

        private CollisionBox _collider;
        private SpriteEffects _spriteEffect = SpriteEffects.None;

        private bool _isMoving = true;
        private bool _hasCollided = false;
        private bool _isOpen = false;
        private bool _hasPlayedOpenSound = false;

        private float _speed = 5f;

        private SoundEffect _openSound;

        public PortalProjectile(
            Vector2 startPosition,
            Vector2 direction,
            Texture2D texture,
            Rectangle portalSpriteRect,
            Rectangle projectileSpriteRect,
            Rectangle closedPortalRect,
            Rectangle projectileVSpriteRect,
            SoundEffect openSound)
        {
            _position = startPosition;
            _direction = direction;
            _texture = texture;

            _portalSprite = new NMoveNAnim(texture, portalSpriteRect);
            _closedPortalSprite = new NMoveNAnim(texture, closedPortalRect);
            _projectileSprite = new NMoveNAnim(texture, projectileSpriteRect);
            _projectileSpriteV = new NMoveNAnim(texture, projectileVSpriteRect);
            _currentSprite = _projectileSprite;

            _collider = new CollisionBox((int)_position.X, (int)_position.Y, 16, 16);
            _openSound = openSound;
        }

        public void Update(GameTime gameTime)
        {
            if (_isMoving)
            {
                Vector2 velocity = _direction * _speed;
                _position += velocity;
                _collider.Move((int)velocity.X, (int)velocity.Y);

                _currentSprite = (_direction.X == 0) ? _projectileSpriteV : _projectileSprite;
                if (_direction.X < 0)
                    _spriteEffect = SpriteEffects.FlipHorizontally;
                else if (_direction.Y > 0)
                    _spriteEffect = SpriteEffects.FlipVertically;
                else
                    _spriteEffect = SpriteEffects.None;
            }
            else
            {
                _spriteEffect = SpriteEffects.None;
                _currentSprite = _isOpen ? _portalSprite : _closedPortalSprite;

                if (_isOpen && !_hasPlayedOpenSound)
                {
                    _openSound?.Play();
                    _hasPlayedOpenSound = true;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _currentSprite.Draw(spriteBatch, _position, _spriteEffect);
        }

        public void StopMoving()
        {
            _isMoving = false;
            _hasCollided = true;
        }

        public void setOpen()
        {
            _isOpen = true;
        }

        public bool HasCollided() => _hasCollided;

        public bool IsMoving() => _isMoving;

        public Vector2 GetPosition() => _position;

        public CollisionBox GetCollider() => _collider;

        public void Throw(Vector2 start, Vector2 direction)
        {
            _position = start;
            _direction = direction;
            _isMoving = true;
            _hasCollided = false;
            _isOpen = false;
            _hasPlayedOpenSound = false;
            _collider.setPos((int)start.X, (int)start.Y);
        }

        public void Destroy()
        {
            _isMoving = false;
        }

        public void ownerPosition(Vector2 newPosition) { /* Not used */ }
    }
}
