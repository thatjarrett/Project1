using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Interfaces;
using Project1.Sprites;
using System;
using System.Diagnostics;

namespace Project1.Entities
{
    public class Skeleton : IEnemy

    {
        private SpriteEffects currentSpriteEffect = SpriteEffects.None;


        private IEnemyState currentState;
        private Vector2 position;

        private const double InvincibilityDuration = 1.0;
        private bool isInvincible = false;
        private double invincibleTime = 0;
        private double flipTime = 0.1;


        private ISprite skeletonSprite;

        int damageFrameCounter = 0;
        bool hurting = false;

        public Skeleton(Vector2 startPos)
        {
            position = startPos;
            currentState = new SkeletonMoveState(Direction.Left, 1.0);
        }

        public void ChangeState(IEnemyState newState)
        {
            currentState = newState;
        }

        public void createEnemySprites(Texture2D enemyTexture)
        {
            Rectangle[] skelRect = new Rectangle[] {new Rectangle(1, 59, 16, 16) };

            skeletonSprite = new NMoveAnim(enemyTexture, skelRect, 5);
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
                skeletonSprite.SetColor(Color.Magenta);
            }
            else
            {
                skeletonSprite.SetColor(Color.White);
            }
            if ((damageFrameCounter / 5) % 2 == 0)
            {
                skeletonSprite.Draw(spriteBatch, position, currentSpriteEffect);
            }
        }

        public void Move(int dx, int dy)
        {
            position.X += dx;
            position.Y += dy;
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
            double timeStep = gameTime.ElapsedGameTime.TotalSeconds;
            flipTime -= timeStep;
            if (flipTime <= 0)
            {
                currentSpriteEffect = SpriteEffects.None;
                flipTime = 0.1;
            }
            else {
                currentSpriteEffect = SpriteEffects.FlipHorizontally;
            }
            //Trace.WriteLine(timeStep);

            currentState.Update(this, gameTime);



            if (isInvincible)
            {
                invincibleTime -= gameTime.ElapsedGameTime.TotalSeconds;
                if (invincibleTime <= 0)
                    isInvincible = false;
            }
            skeletonSprite.Update(gameTime);
        }
    }
}
