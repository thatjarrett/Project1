using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1.GameObjects.Environment;
using Project1.Interfaces;

namespace Project1.GameObjects.Background
{
    public class WhiteBrick : environmentTile
    {

        public WhiteBrick(Vector2 pos, int id) :
            base(pos, true, id)
        { }
    }
}
