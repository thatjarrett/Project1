using Microsoft.Xna.Framework;
using Project1.Interfaces;
using Project1.GameObjects.Environment;
using Project1.Entities;

namespace Project1.GameObjects.Environment
{
    public class LockedDoorTile : doorTile
    {
        private ISprite openSprite;
        private bool isOpen = false;

        public LockedDoorTile(Vector2 position, ISprite closedSprite, ISprite openSprite)
            : base(position)
        {
            this.setSprite(closedSprite);
            this.openSprite = openSprite;
            this.IsSolid = true;
            this.IsBreakable = false;
            this._collides = true;
            SetCollider();
        }

        public void TryOpen(Link link)
        {
            if (!isOpen && link.keyCount > 0)
            {
                link.keyCount--;
                Open();
            }
        }

        public void Open()
        {
            this.setSprite(openSprite);
            this.isOpen = true;
            this.IsSolid = false;
            this._collides = false;
            this.collider = null;
        }
    }
}
