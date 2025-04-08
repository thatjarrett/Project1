using Microsoft.Xna.Framework;
using Project1.Collision;
using System;

namespace Project1.GameObjects.Environment
{
    public class doorTile : environmentTile
    {
        private bool open = false;
        public doorTile(Vector2 pos) :
            base(pos, true)
        {

        }

        public void SetCollider(int x, int y, int w, int h)
        {
            if(_collides && open)
            {
                collider = new CollisionBox(x, y, w, h);
            } else if (_collides)
            {
                
            }
            
        }

        public override void SetCollider()
        {
            if(_collides)
            {
                collider = new CollisionBox((int)_position.X, (int)_position.Y, 96, 96);
            }
        }

        public void SetOpen(bool val)
        {
            open = val;
        }

        public bool GetOpen()
        {
            return open;
        }

    }
}
