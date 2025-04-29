using Project1.GameObjects.Environment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Collision
{
    class BombWallCollisionHandler
    {
        public static void HandleCollision(BombProjectile projectile, CrackedWallTile wall)     //TODO
        {
            if (wall.GetCollider() != null && projectile.GetCollider() != null && projectile.GetCollidingSide(projectile.GetCollider()) != CollisionSide.None)
            {
                // blow up wall
                wall.Break();
            }
        }
    }
}
