using Microsoft.Xna.Framework;
using Project1.GameObjects.Environment;

namespace Project1.GameObjects.Background
{
    public class WhiteBrick : environmentTile
    {

        public WhiteBrick(Vector2 pos, int id) :
            base(pos, true, id)
        { }
    }
}
