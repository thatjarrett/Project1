using Project1.Entities;
using Project1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Collision
{
    class LinkItemCollisionHandler
    {
        public static void HandleCollision(IItem item, Link Link)       //TODO
        {
            if (item.GetCollider() != null && Link.GetCollider().Intersects(item.GetCollider()))
            {
                //Debug.WriteLine("⚠️ Link picked up an item!");
                Link.Pickup(item);
                item.pickup();
                //delete item
            }
        }
    }
}
