using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Interfaces;

namespace Project1.Controllers
{
    internal class MoveAnim : ISprite
    {
        private Texture2D _texture;      // The sprite sheet texture
        private Vector2 _position;      // The sprite's position
        private Vector2 _velocity;      // The sprite's velocity (direction and speed)
        private int _frameCount;        // Total number of animation frames
        private int _currentFrame;      // The current animation frame
        private int _frameWidth;        // Width of a single frame
        private double _frameTimer;     // Timer for animation
        private double _frameInterval;  // Time between frames (in seconds)

        public MoveAnim(Texture2D texture, Vector2 startPosition, Vector2 velocity, int frameCount, double frameInterval)
        {
            _texture = texture;
            _position = startPosition;
            _velocity = velocity;
            _frameCount = frameCount;
            _currentFrame = 0;
            _frameWidth = texture.Width / frameCount; // Divide texture width by frame count
            _frameTimer = 0;
            _frameInterval = frameInterval;          // Time between each frame
        }

        public void Update(GameTime gameTime)
        {
            // Update the position based on velocity
            _position += _velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Update the animation frame
            _frameTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if (_frameTimer >= _frameInterval)
            {
                _currentFrame = (_currentFrame + 1) % _frameCount; // Loop through frames
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
            // Calculate the source rectangle for the current animation frame
            Rectangle sourceRectangle = new Rectangle(
                _currentFrame * _frameWidth, // X position of the frame
                0,                          // Y position (top of texture)
                _frameWidth,                // Width of the frame
                _texture.Height             // Full height of the texture
            );

            // Draw the current frame
            spriteBatch.Draw(_texture, _position, sourceRectangle, Color.White);
        }
    }
}
