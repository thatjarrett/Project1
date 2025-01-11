using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Interfaces;

namespace Project1.Controllers
{
    internal class MoveNAnim : ISprite
    {
        private Texture2D _texture;      // The sprite sheet
        private Vector2 _position;      // The sprite's position
        private Vector2 _velocity;      // The velocity (movement direction and speed)
        private int _frameCount;        // Total number of frames
        private int _currentFrame;      // The current frame being displayed
        private int _frameWidth;        // The width of a single frame
        private double _frameTimer;     // Timer for animation
        private double _frameInterval;  // Time between frames

        public MoveNAnim(Texture2D texture, Vector2 startPosition, Vector2 velocity, int frameCount, double frameInterval)
        {
            _texture = texture;
            _position = startPosition;
            _velocity = velocity;           // Movement velocity
            _frameCount = frameCount;       // Total animation frames
            _currentFrame = 0;              // Start at the first frame
            _frameWidth = texture.Width / _frameCount;
            _frameTimer = 0;
            _frameInterval = frameInterval; // Time in seconds between frames (e.g., 0.2 for 5 FPS)
        }

        public void Update(GameTime gameTime)
        {
            // Update position based on velocity
            _position += _velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Update the animation timer
            _frameTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if (_frameTimer >= _frameInterval)
            {
                // Move to the next frame
                _currentFrame = (_currentFrame + 1) % _frameCount;
                _frameTimer = 0; // Reset the timer
            }
        }
        public void Update()
        {
            // Call the Update method with GameTime parameter
            Update(new GameTime());
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            // Calculate the source rectangle for the current frame
            Rectangle sourceRectangle = new Rectangle(_currentFrame * _frameWidth, 0, _frameWidth, _texture.Height);

            // Draw the current frame at the current position
            spriteBatch.Draw(_texture, _position, sourceRectangle, Color.White);
        }
    }
}
