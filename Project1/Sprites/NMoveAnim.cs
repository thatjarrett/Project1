using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Interfaces;

namespace Project1.Sprites
{
    internal class NMoveAnim : ISprite
    {
        private Texture2D _texture;
        private Rectangle _sourceRectangle;
        private float _scale; // Scale factor for sprite size
        private Rectangle[] frames;
        private int fps;
        private int frameCounter = 0;
        Color color = Color.White;
        private Vector2 _offset = Vector2.Zero;

        public NMoveAnim(Texture2D texture, Rectangle[] framesList, int frameRate, float scale = 3.0f, Vector2 offset = default)
        {
            _texture = texture;
            frames = framesList;
            fps = frameRate;
            _scale = scale; // Initialize scale
            _offset = offset;
        }

        public void Update(GameTime gameTime)
        {

            // _sourceRectangle = frames[(gameTime.TotalGameTime.Seconds)/fps)%frame]; I think this does everything below but based on game time and better
            frameCounter++;
            int frame = frameCounter / fps;
            if (frame > frames.Length - 1)
            {
                frameCounter = 0;
                frame = 0;
            }
            _sourceRectangle = frames[frame];

        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, SpriteEffects spriteEffects)
        {
            spriteBatch.Draw(
                _texture,
                (position - _scale * _offset),
                _sourceRectangle,
                color,
                0f,
                Vector2.Zero,
                _scale,           // Scale factor
                spriteEffects,
                0f
            );
        }
        public void SetColor(Color _color)
        {
            color = _color;
        }
    }
}

