using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Interfaces;
using Color = Microsoft.Xna.Framework.Color;

namespace Project1.Sprites
{
    internal class TextSprite : ISprite
    {
        public string _text;
        private readonly SpriteFont _font;
        private Vector2 _position;
        Color color = Color.White;

        public TextSprite(string text, SpriteFont font, Vector2 position)
        {
            _text = text;
            _font = font;
            _position = position;
        }

        public void Update(GameTime gameTime)
        {
            // No update logic needed for static text
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, SpriteEffects spriteEffect)
        {
            // Draw the text
            spriteBatch.DrawString(_font, _text, position, Color.White,0f,Vector2.Zero,2.5f,spriteEffect,1f);
        }
        public void SetColor(Color _color)
        {
            color = _color;
        }
    }
}
