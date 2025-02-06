using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1.Interfaces;

namespace Project1.GameObjects.Environment
{
    public class stairsTile : environmentTile
    {
        public stairsTile(Vector2 pos):
            base(pos, false, 4)
        {
            
        }
    }
}
