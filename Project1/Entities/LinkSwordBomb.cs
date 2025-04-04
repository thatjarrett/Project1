using Microsoft.Xna.Framework;
using Project1.Collision;
using Project1.Interfaces;
using Project1.Projectiles;
using System.Collections.Generic;

namespace Project1.Entities
{
    public partial class Link
    {
        public void ClearSword() => swordCollision = null;
        public CollisionBox GetSword() => swordCollision;

        public void SetSword(Direction direction)
        {
            int offset = 24;
            Vector2 offsetVector = direction switch
            {
                Direction.Up => new Vector2(0, -offset),
                Direction.Down => new Vector2(0, offset),
                Direction.Left => new Vector2(-offset, 0),
                Direction.Right => new Vector2(offset, 0),
                _ => Vector2.Zero
            };

            swordCollision = new CollisionBox((int)(position.X + offsetVector.X), (int)(position.Y + offsetVector.Y));
        }

        public List<IProjectile> GetProjectiles() => projectiles;
        public List<IProjectile> GetBombs() => bombs;
        public void DeleteBomb()
        {
            if (bombs.Count > 0)
                bombs.RemoveAt(0);
        }
    }
}
