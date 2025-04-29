using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Collision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Interfaces
{
    public interface IAnimation
    {
        public void Update(GameTime gametime);
        void Draw(SpriteBatch spriteBatch, SpriteEffects spriteEffects);

        public void SetColor(Color _color);
        public void SetPosition(Vector2 pos);
        public Vector2 getPosition();
        public bool isActive();
    }
}
