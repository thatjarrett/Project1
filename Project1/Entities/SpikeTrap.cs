using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1.Collision;
using Project1.Interfaces;
using Project1.Sprites;
namespace Project1.Entities
{
    public class SpikeTrap : IEnemy
    {
        private IEnemyState currentState;
        private Vector2 position;

        private ISprite spikeTrapSprite;
        private SpriteEffects currentSpriteEffect = SpriteEffects.None;
        private CollisionBox collider;

        int damageFrameCounter = 0;
        bool hurting = false;

        public SpikeTrap(Vector2 startPos)
        {
            position = startPos;
            currentState = new SpikeTrapMoveState();
            collider = new CollisionBox((int)startPos.X, (int)startPos.Y);
        }

        public void ChangeState(IEnemyState newState)
        {
            //Debug.WriteLine($"Changing state to: {newState.GetType().Name}");

            // Change the current state
            currentState = newState;
            currentState.Enter(this);
        }

        public void SetInvincible(bool value)
        {

        }

        public void MoveLeft()
        {
            //N/A
        }
        public void MoveRight()
        {
            //N/A
        }
        public void MoveUp()
        {
            //N/A
        }
        public void MoveDown()
        {
            //N/A
        }
        public void Update(GameTime gameTime)
        {
            currentState.Update(this, gameTime);


            spikeTrapSprite.Update(gameTime);
        }

        public void Move(int dx, int dy)
        {
            position.X += dx;
            position.Y += dy;
            collider.Move(dx, dy);
        }

        public void PerformAttack()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spikeTrapSprite.Draw(spriteBatch, position, currentSpriteEffect);

        }
        public void createEnemySprites(Texture2D enemyTexture)
        {

            Rectangle spikeTrap = new Rectangle(164, 59, 16, 16);

            spikeTrapSprite = new NMoveNAnim(enemyTexture, spikeTrap);
        }

        public void SetAnimation(string action)
        {

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
        public CollisionBox GetCollider()
        {
            return collider;
        }
    }
}

