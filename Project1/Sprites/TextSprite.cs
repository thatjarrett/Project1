using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Interfaces;

namespace Project1.Sprites
{
    internal class TextSprite : ISprite
    {
        private readonly string _text;
        private readonly SpriteFont _font;
        private readonly Vector2 _position;

        public TextSprite(string text, SpriteFont font, Vector2 position)
        {
            _text = "Credits\n Made by Jarrett Reeves\n Sprites from: https://www.spriters-resource.com/fullview/8366/";
            _font = font;
            _position = position;
        }

        public void Update(GameTime gameTime)
        {
            // No update logic needed for static text
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw the text
            spriteBatch.DrawString(_font, _text, _position, Color.White);
        }
    }
}
