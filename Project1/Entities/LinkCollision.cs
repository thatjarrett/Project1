using Project1.Collision;
using Microsoft.Xna.Framework;
namespace Project1.Entities
{
    public partial class Link
    {
        public void CollisionUpdate(CollisionBox other)
        {
            int distance = collider.GetSidePush(other);
            switch (collider.side)
            {
                case CollisionSide.Top:
                    Move(0, -distance);
                    break;
                case CollisionSide.Bottom:
                    Move(0, distance);
                    break;
                case CollisionSide.Left:
                    Move(-distance, 0);
                    break;
                case CollisionSide.Right:
                    Move(distance, 0);
                    break;
            }
        }

        public CollisionBox GetCollider() => collider;
        public bool IsInvincible() => isInvincible;
        public Vector2 GetPosition() => position;
    }
}
