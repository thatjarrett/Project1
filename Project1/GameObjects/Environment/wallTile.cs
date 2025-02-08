using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1.Interfaces;

namespace Project1.GameObjects.Environment
{
    public class wallTile : environmentTile
    {
        public wallTile(Vector2 pos, int id):
            base(pos, true, id)
        {
            
        }
    }
}
