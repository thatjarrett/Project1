using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1.Interfaces;

namespace Project1.GameObjects.Environment
{
    public class statueTile : environmentTile
    {
        public statueTile(Vector2 pos, bool collides, ISprite sprite):
            base(pos, collides = true, sprite)
        {
            
        }
    }
}
