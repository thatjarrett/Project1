using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Project1.Collision;

namespace Project1.GameObjects.Environment
{
    public class wallTile : environmentTile
    {
        int width;
        int height;
        List<CollisionBox> colliders = new List<CollisionBox>();
        Boolean horizontal;

        public wallTile(Vector2 pos, int width, int height, Boolean horizontal = true) :
            base(pos, true)
        {
            this.width = width;
            this.height = height;
            this.horizontal = horizontal;
        }

        override public void SetCollider()
        {
            if (_collides && horizontal)
            {
                int sideWidth = (this.width / 16) * 7;
                int gapX = (this.width / 16) * 9;

                colliders.Add(new CollisionBox((int)_position.X, (int)_position.Y, sideWidth, this.height));
                colliders.Add(new CollisionBox((int)_position.X + gapX, (int)_position.Y, sideWidth, this.height));

            }
            else if (_collides)
            {
                int halfGapHeight = (this.height - 96) / 2;

                colliders.Add(new CollisionBox((int)_position.X, (int)_position.Y, this.width, halfGapHeight));
                colliders.Add(new CollisionBox((int)_position.X, (int)_position.Y + halfGapHeight + 96, this.width, halfGapHeight));
            }

        }

        public List<CollisionBox> GetColliderList()
        {
            return colliders;
        }
    }
}
