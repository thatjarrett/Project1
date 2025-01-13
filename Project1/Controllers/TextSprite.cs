using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Interfaces;

namespace Project1.Controllers
{
    internal class TextSprite : ISprite
    {
        private readonly string _text;         // The text to display
        private readonly SpriteFont _font;    // The font used to render the text
        private readonly Vector2 _position;  // The position of the text on the screen

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
