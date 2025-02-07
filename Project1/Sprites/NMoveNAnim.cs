using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Interfaces;

namespace Project1.Sprites
{
    internal class NMoveNAnim : ISprite
    {
        private Texture2D _texture;
        private Rectangle _sourceRectangle;
        private float _scale; // Scale factor for sprite size

        public NMoveNAnim(Texture2D texture, Rectangle rectangle, float scale = 3.0f)
        {
            _texture = texture;
            _sourceRectangle = rectangle;

            _scale = scale; // Initialize scale
        }

        public void Update(GameTime gameTime)
        {
            // yaaaaaay
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, SpriteEffects spriteEffects)
        {
            spriteBatch.Draw(
                _texture,
                position,
                _sourceRectangle,
                Color.White,
                0f,
                Vector2.Zero,
                _scale,           // Scale factor
                spriteEffects,
                0f
            );
        }
    }
}