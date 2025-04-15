using Project1.Collision;
using Project1.Interfaces;
using Microsoft.Xna.Framework;

namespace Project1.Interfaces
{
    public interface ICollidable
    {
        CollisionBox GetCollider();
        void MoveTo(Vector2 newPosition);
        void TeleportTo(Vector2 newPosition);
    }

}

