using Microsoft.Xna.Framework;

namespace Project1.GameObjects.Environment
{
    public class wallTile : environmentTile
    {
        public wallTile(Vector2 pos, int id) :
            base(pos, true, id)
        {

        }
    }
}
