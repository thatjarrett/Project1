using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Interfaces;

namespace Project1.Controllers
{
    internal class NMoveNAnim : ISprite
    {
        private Texture2D _texture; 
        private Vector2 _position;

        
        public NMoveNAnim(Texture2D texture, Vector2 position)
        {
            _texture = texture;
            _position = position;
        }

        
        public void Update(GameTime gameTime)
        {
           //yaaaaaay
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, Color.White);
        }
    }
}
