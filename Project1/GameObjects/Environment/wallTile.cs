using Microsoft.Xna.Framework;
using Project1.Collision;

namespace Project1.GameObjects.Environment
{
    public class wallTile : environmentTile
    {
        int width;
        int height;

        public wallTile(Vector2 pos, int width, int height) :
            base(pos, true)
        {
            this.width = width;
            this.height = height;
        }

        override public void SetCollider()
        {
            if (_collides)
            {
                collider = new CollisionBox((int)_position.X, (int)_position.Y, this.width, this.height);
            }

        }
    }
}
