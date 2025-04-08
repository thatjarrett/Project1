using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Project1.Collision;

namespace Project1.GameObjects.Environment
{
    public class pushableBlock : environmentTile
    {
        private bool isMoving = false;
        private Vector2 targetPosition;
        private float speed = 2f;

        public pushableBlock(Vector2 pos) : base(pos, true)
        {
            SetCollider(); // Initial collider
        }

        public void CollisionUpdate(CollisionBox other, List<CollisionBox> blockingColliders)
        {
            if (isMoving || !collider.Intersects(other)) return;

            int distance = collider.GetSidePush(other);
            CollisionSide side = collider.side;

            if (distance == 0 || side == CollisionSide.None) return;

            Vector2 offset = side switch
            {
                CollisionSide.Top => new Vector2(0, -48),
                CollisionSide.Left => new Vector2(-48, 0),
                CollisionSide.Right => new Vector2(48, 0),
                CollisionSide.Bottom => new Vector2(0, 48),
                _ => Vector2.Zero
            };

            Vector2 potentialTarget = _position + offset;
            Rectangle futureHitbox = new Rectangle((int)potentialTarget.X, (int)potentialTarget.Y, 48, 48);

            foreach (var box in blockingColliders)
            {
                if (futureHitbox.Intersects(box.hitbox))
                    return; // Blocked, can't move
            }

            StartMove(offset);
        }

        public void Update()
        {
            if (!isMoving) return;

            Vector2 direction = targetPosition - _position;

            if (direction.Length() <= speed)
            {
                _position = targetPosition;
                collider.setPos((int)_position.X, (int)_position.Y);
                isMoving = false;
            }
            else
            {
                direction.Normalize();
                Vector2 movement = direction * speed;
                _position += movement;
                collider.Move((int)movement.X, (int)movement.Y);
            }
        }

        private void StartMove(Vector2 offset)
        {
            targetPosition = _position + offset;
            isMoving = true;
        }

        // Optional: override SetCollider so it's only reset once if needed externally
        public override void SetCollider()
        {
            if (!isMoving)
                collider = new CollisionBox((int)_position.X, (int)_position.Y, 48, 48, this);
        }
    }
}
