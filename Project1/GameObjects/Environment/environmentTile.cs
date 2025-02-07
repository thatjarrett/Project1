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
        private int _tileId;

        public environmentTile(Vector2 pos, bool collision, int tileId)
        {
            _position = pos;
            _collides = collision;
            _tileId = tileId;
        }
        public void Update(GameTime gameTime)
        {
            _sprite.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            _sprite.Draw(spriteBatch, _position, SpriteEffects.None);
        }

        public void setSprite(ISprite sprite)
        {
            _sprite = sprite;
        }

        public int getTileID()
            { return _tileId; }

    }
}
