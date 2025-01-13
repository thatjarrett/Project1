using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Interfaces;

namespace Project1.Controllers
{
    internal class MoveAnim : ISprite
    {
        private Texture2D _texture;      // Sprite sheet
        private Vector2 _position;      // Position
        private Vector2 _velocity;      // Movement velocity
        private int _frameWidth;        // Width of each frame
        private int _frameHeight;       // Height of each frame
        private int _startFrame;        // Start frame for animation
        private int _endFrame;          // End frame for animation
        private int _currentFrame;      // Current frame in animation
        private double _frameTimer;     // Time elapsed for animation
        private double _frameInterval;  // Time between frames

        public MoveAnim(Texture2D texture, Vector2 position, Vector2 velocity, int frameWidth, int frameHeight, int startFrame, int endFrame, double frameInterval)
        {
            _texture = texture;
            _position = position;
            _velocity = velocity;
            _frameWidth = frameWidth;
            _frameHeight = frameHeight;
            _startFrame = startFrame;
            _endFrame = endFrame;
            _currentFrame = startFrame;  // Start at the first frame in the range
            _frameTimer = 0;
            _frameInterval = frameInterval;
        }

        public void Update(GameTime gameTime, Viewport viewport)
        {
            // Update position based on velocity
            _position += _velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Boundary wrapping logic
            if (_position.X > viewport.Width) _position.X = -_frameWidth;
            if (_position.X + _frameWidth < 0) _position.X = viewport.Width;
            if (_position.Y > viewport.Height) _position.Y = -_frameHeight;
            if (_position.Y + _frameHeight < 0) _position.Y = viewport.Height;

            // Update animation frame
            _frameTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (_frameTimer >= _frameInterval)
            {
                _currentFrame++;
                if (_currentFrame > _endFrame)
                    _currentFrame = _startFrame; // Loop back to the start frame
                _frameTimer = 0; // Reset timer
            }
        }

        // Implement ISprite's Update(GameTime)
        public void Update(GameTime gameTime)
        {
            // You can pass the viewport dynamically from your Game class
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Calculate source rectangle for the current frame
            Rectangle sourceRectangle = new Rectangle(
                _currentFrame * _frameWidth, // Frame column
                0,                          // Frame row (0 for the first row)
                _frameWidth,
                _frameHeight
            );

            // Draw current frame
            spriteBatch.Draw(_texture, _position, sourceRectangle, Color.White);
        }
    }
}
