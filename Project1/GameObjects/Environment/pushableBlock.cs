using Microsoft.Xna.Framework;
using Project1.Collision;

namespace Project1.GameObjects.Environment
{
    public class pushableBlock : environmentTile
    {

        public pushableBlock(Vector2 pos) :
            base(pos, true)
        {
            SetCollider();
        }

        public void CollisionUpdate(CollisionBox other)
        {
            int intersectionDistance = collider.GetSidePush(other);
            CollisionSide side = collider.side;
            switch (side)
            {
                case CollisionSide.Top:
                    Move(0, -intersectionDistance);
                    break;
                case CollisionSide.Left:
                    Move(-intersectionDistance, 0);
                    break;
                case CollisionSide.Right:
                    Move(intersectionDistance, 0);
                    break;
                case CollisionSide.Bottom:
                    Move(0, intersectionDistance);
                    break;
                case CollisionSide.None:
                    break;
            }
            //Debug.WriteLine($"Collision: {intersectionDistance}");

        }

        public void Move(int dx, int dy)
        {
            _position.X += dx;
            _position.Y += dy;
            collider.Move(dx, dy);
        }
    }
}
