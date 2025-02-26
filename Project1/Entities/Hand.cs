using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1.Collision;
using Project1.Interfaces;
using Project1.Sprites;

namespace Project1.Entities
{
    public class Hand : IEnemy

    {
        private SpriteEffects currentSpriteEffect = SpriteEffects.None;


        private IEnemyState currentState;
        private Vector2 position;

        private const double InvincibilityDuration = 1.0;
        private bool isInvincible = false;
        private double invincibleTime = 0;
        private CollisionBox collider;


        private ISprite handSprite;

        int damageFrameCounter = 0;
        bool hurting = false;

        public Hand(Vector2 startPos)
        {
            position = startPos;
            currentState = new HandMoveState();
            collider = new CollisionBox((int)startPos.X, (int)startPos.Y);
        }

        public void ChangeState(IEnemyState newState)
        {
            currentState = newState;
        }

        public void createEnemySprites(Texture2D enemyTexture)
        {
            Rectangle[] handRect = new Rectangle[] { new Rectangle(393, 11, 16, 16), new Rectangle(410, 11, 16, 16) };

            handSprite = new NMoveAnim(enemyTexture, handRect, 5);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (hurting)
            {
                damageFrameCounter++;
            }
            else
            {
                damageFrameCounter = 0;
            }

            if (damageFrameCounter > 0)
            {
                handSprite.SetColor(Color.Magenta);
            }
            else
            {

                handSprite.SetColor(Color.White);
            }
            if ((damageFrameCounter / 5) % 2 == 0)
            {
                handSprite.Draw(spriteBatch, position, currentSpriteEffect);
            }
        }

        public void Move(int dx, int dy)
        {
            position.X += dx;
            position.Y += dy;
            collider.Move(dx, dy);
        }

        public void MoveDown()
        {
            //
        }

        public void MoveLeft()
        {
            //
        }

        public void MoveRight()
        {
            //
        }

        public void MoveUp()
        {
            //
        }

        public void PerformAttack()
        {
            //
        }

        public void SetAnimation(string action)
        {
            if (action.Contains("Damage"))
            {
                hurting = true;
            }
            if (!action.Contains("Damage"))
            {
                hurting = false;
            }
        }

        public void SetInvincible(bool value)
        {
            isInvincible = value;
        }

        public void Update(GameTime gameTime)
        {
            currentState.Update(this, gameTime);



            if (isInvincible)
            {
                invincibleTime -= gameTime.ElapsedGameTime.TotalSeconds;
                if (invincibleTime <= 0)
                    isInvincible = false;
            }
            handSprite.Update(gameTime);
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
            Debug.WriteLine($"Collision: {intersectionDistance}");

        }
        public CollisionBox GetCollider()
        {
            return collider;
        }
    }
}
