using Microsoft.Xna.Framework;
using Project1.Interfaces;
using Project1.GameObjects.Environment;
using Project1.Entities;

namespace Project1.GameObjects.Environment
{
    public class LockedDoorTile : doorTile
    {
        private ISprite openSprite;
        private Direction direction;
        private bool isOpen = false;

        public LockedDoorTile(Vector2 position, ISprite closedSprite, ISprite openSprite, Direction direction)
            : base(position)
        {
            this.IsSolid = true;

            this.setSprite(closedSprite);
            this.openSprite = openSprite;
            //this.IsSolid = true;
            //this.IsBreakable = false;
            this._collides = true;
            this.direction = direction;
            SetCollider();
        }

        public void TryOpen(Link link)
        {
            if (!this.isOpen && link.keyCount > 0)
            {
                link.keyCount--;
                Open();
            }
        }

        public void Open()
        {
            this.isOpen = true;
            this.setSprite(openSprite);
            if (this.direction == Direction.Left)
            {
                this.SetCollider((int)_position.X, (int)_position.Y, 48, 96);
            }
            else if (this.direction == Direction.Right)
            {
                this.SetCollider((int)_position.X + 48, (int)_position.Y, 48, 96);
            }
            else if (this.direction == Direction.Up)
            {
                this.SetCollider((int)_position.X, (int)_position.Y, 96, 48);
            }
            else if (this.direction == Direction.Down)
            {
                this.SetCollider((int)_position.X, (int)_position.Y + 48, 96, 48);
            }

            this.IsSolid = false;
            
            
        }
    }
}
