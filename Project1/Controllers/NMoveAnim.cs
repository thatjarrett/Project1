using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Interfaces;

namespace Project1.Controllers
{
    internal class NMoveAnim : ISprite
    {
        private readonly Texture2D _texture; 
        private readonly Vector2 _position;  
        private readonly int _frameWidth;   
        private readonly int _frameHeight;   
        private readonly int _columns;     
        private readonly int _startFrame;   
        private readonly int _frameCount;   
        private int _currentFrame;           
        private double _frameTimer;         
        private readonly double _frameInterval;

        public NMoveAnim(Texture2D texture, Vector2 position, int frameWidth, int frameHeight, int startFrame, int frameCount, int columns, double frameInterval)
        {
            _texture = texture;
            _position = position;
            _frameWidth = frameWidth;
            _frameHeight = frameHeight;
            _startFrame = startFrame;
            _frameCount = frameCount;
            _columns = columns;
            _currentFrame = 0;
            _frameTimer = 0;
            _frameInterval = frameInterval;
        }

        public void Update(GameTime gameTime)
        {
            // Update the animation timer
            _frameTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if (_frameTimer >= _frameInterval)
            {
                // Move to the next frame within the animation range
                _currentFrame = (_currentFrame + 1) % _frameCount;
                _frameTimer = 0; // Reset the timer
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Calculate the actual frame index in the sprite sheet
            int frameIndex = _startFrame + _currentFrame;

            // Calculate the row and column of the current frame
            int row = frameIndex / _columns;
            int column = frameIndex % _columns;

            // Calculate the source rectangle for the current frame
            Rectangle sourceRectangle = new Rectangle(
                column * _frameWidth,
                row * _frameHeight,
                _frameWidth,
                _frameHeight
            );

            // Draw the sprite with scaling
            spriteBatch.Draw(
                _texture,
                _position,
                sourceRectangle,
                Color.White,
                0f,                
                Vector2.Zero,      
                3f,               
                SpriteEffects.None,
                0f                
            );
        }

    }
}
