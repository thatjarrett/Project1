using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Interfaces;

namespace Project1.Controllers
{
    internal class MoveNAnim : ISprite
    {
        private Texture2D _texture;
        private Vector2 _position;
        private Vector2 _velocity;  // Movement velocity (X and Y speed)
        private float _upperLimit;  // Upper boundary for vertical movement
        private float _lowerLimit;  // Lower boundary for vertical movement

        public MoveNAnim(Texture2D texture, Vector2 position, Vector2 velocity, float movementRange)
        {
            _texture = texture;
            _position = position;
            _velocity = velocity;

            // Define boundaries for vertical movement
            _upperLimit = position.Y - movementRange;
            _lowerLimit = position.Y + movementRange;
        }

        public void Update(GameTime gameTime)
        {
            // Update the position based on velocity
            _position += _velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Reverse direction if the sprite reaches boundaries
            if (_position.Y <= _upperLimit || _position.Y >= _lowerLimit)
            {
                _velocity.Y *= -1; // Reverse the vertical direction
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, Color.White);
        }
    }
}
