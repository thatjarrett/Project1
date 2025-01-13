using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Interfaces;

namespace Project1.Controllers
{
    internal class MoveAnim : ISprite
    {
        private Texture2D _texture; 
        private Vector2 _position;     
        private Vector2 _velocity;     
        private int _frameWidth;       
        private int _frameHeight;      
        private int _startFrame;        
        private int _endFrame;          
        private int _currentFrame;      
        private double _frameTimer;     
        private double _frameInterval;  
        private float _scale;           

        public MoveAnim(Texture2D texture, Vector2 position, Vector2 velocity, int frameWidth, int frameHeight, int startFrame, int endFrame, double frameInterval, float scale = 1.0f)
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
            _scale = scale; // Initialize scale
        }

        public void Update(GameTime gameTime, Viewport viewport)
        {
            // Update position based on velocity
            _position += _velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Boundary wrapping logic
            if (_position.X > viewport.Width) _position.X = -_frameWidth * _scale;
            if (_position.X + _frameWidth * _scale < 0) _position.X = viewport.Width;
            if (_position.Y > viewport.Height) _position.Y = -_frameHeight * _scale;
            if (_position.Y + _frameHeight * _scale < 0) _position.Y = viewport.Height;

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

        public void Update(GameTime gameTime)
        {
            // overload
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

            // Draw current frame with scaling
            spriteBatch.Draw(
                _texture,
                _position,
                sourceRectangle,
                Color.White,
                0f,               
                Vector2.Zero,     
                _scale,           // Scale factor
                SpriteEffects.None,
                0f                
            );
        }
    }
}
