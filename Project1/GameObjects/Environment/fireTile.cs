using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1.Interfaces;

namespace Project1.GameObjects.Environment
{
    public class fireTile : environmentTile
    {
        public fireTile(Vector2 pos):
            base(pos, true, 5)
        {
            
        }
    }
}
