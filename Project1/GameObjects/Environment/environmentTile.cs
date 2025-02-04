using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1.Interfaces;

namespace Project1.GameObjects.Environment
{
    public abstract class environmentTile
    {
        private Vector2 _position;
        private bool _collides;
        private ISprite _sprite;

        public environmentTile(Vector2 pos, bool collision, ISprite sprite)
        {
            _position = pos;
            _collides = collision;
            _sprite = sprite;
        }
        public void Update(GameTime gameTime) {
            
        }
        public void Draw(SpriteBatch spriteBatch) { 
            _sprite.Draw(spriteBatch, _position);
        }
    }
    
   }
