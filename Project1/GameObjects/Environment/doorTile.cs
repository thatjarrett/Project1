using Microsoft.Xna.Framework;
using Project1.Collision;
using System;

namespace Project1.GameObjects.Environment
{
    public class doorTile : environmentTile
    {
        protected bool isOpen = false;
        public doorTile(Vector2 pos) :
            base(pos, true)
        {

        }

        public void SetCollider(int x, int y, int w, int h)
        {
            if(_collides && isOpen)
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
            isOpen = val;
        }

        public bool GetOpen()
        {
            return isOpen;
        }

    }
}
