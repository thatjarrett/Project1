using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Interfaces;
using Project1.Projectiles;
using Project1.Sprites;

namespace Project1.Entities
{
    public class Goriya : IEnemy

    {
        private SpriteEffects currentSpriteEffect = SpriteEffects.None;


        private IEnemyState attacking;
        private IEnemyState moving;
        private IEnemyState currentState;
        private Vector2 position;

        private const double InvincibilityDuration = 1.0;
        private bool isInvincible = false;
        private double invincibleTime = 0;

        private ISprite goriyaSprite;
        private ISprite goriyaSpriteUp;
        private ISprite goriyaSpriteDown;
        private ISprite goriyaSpriteSide;

        private Direction direction;

        int damageFrameCounter = 0;
        bool hurting = false;

        private double flipTime = 0.1;

        private ISprite boomerang;
        private BoomerangProjectile boomerangThrowable;
        private Vector2 throwDirection;


        public Goriya(Vector2 startPos)
        {
            position = startPos;
            direction = Direction.Right;

            moving = new GoriyaMoveState();
            attacking = new GoriyaAttackState();

            currentState = moving;
        }

        public void ChangeState(IEnemyState newState)
        {
            currentState = newState;
        }

        public void createEnemySprites(Texture2D enemyTexture)
        {
            Rectangle[] gRectUp = new Rectangle[] { new Rectangle(239, 11, 16, 16) };
            Rectangle[] gRectDown = new Rectangle[] { new Rectangle(222, 11, 16, 16) };
            Rectangle[] gRectSide = new Rectangle[] { new Rectangle(256, 11, 16, 16), new Rectangle(273, 11, 16, 16) };

            goriyaSpriteUp = new NMoveAnim(enemyTexture, gRectUp, 5);
            goriyaSpriteDown = new NMoveAnim(enemyTexture, gRectDown, 5);
            goriyaSpriteSide = new NMoveAnim(enemyTexture, gRectSide, 5);

            goriyaSprite = goriyaSpriteSide;

            boomerang = new NMoveAnim(enemyTexture, new Rectangle[] { new Rectangle(290, 11, 8, 16), new Rectangle(299, 11, 8, 16), new Rectangle(308, 11, 8, 16)}, 5);
            boomerangThrowable = new BoomerangProjectile(boomerang);
            throwDirection = new Vector2(0, 1);
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
                goriyaSprite.SetColor(Color.Magenta);
            }
            else
            {

                goriyaSprite.SetColor(Color.White);
            }
            if ((damageFrameCounter / 5) % 2 == 0)
            {
                goriyaSprite.Draw(spriteBatch, position, currentSpriteEffect);
            }
            boomerangThrowable.Draw(spriteBatch);
        }

        public void Move(int dx, int dy)
        {
            position.X += dx;
            position.Y += dy;
        }

        public void MoveDown()
        {
            ChangeState(moving);
            direction = Direction.Down;
            goriyaSprite = goriyaSpriteDown;
            throwDirection = new Vector2(0, 1);
        }

        public void MoveLeft()
        {
            ChangeState(moving);
            direction = Direction.Left;
            goriyaSprite = goriyaSpriteSide;
            throwDirection = new Vector2(-1, 0);
        }

        public void MoveRight()
        {
            ChangeState(moving);
            direction = Direction.Right;
            goriyaSprite = goriyaSpriteSide;
            throwDirection = new Vector2(1, 0);
        }

        public void MoveUp()
        {
            ChangeState(moving);
            direction = Direction.Up;
            goriyaSprite = goriyaSpriteUp;
            throwDirection = new Vector2(0, -1);
        }

        public void PerformAttack()
        {
            boomerangThrowable.Throw(position, throwDirection);
            ChangeState(attacking);
        }

        //public void ResumeMovement()
        //{
            
        //}

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

            double timeStep = gameTime.ElapsedGameTime.TotalSeconds;
            flipTime -= timeStep;


            if (isInvincible)
            {
                invincibleTime -= gameTime.ElapsedGameTime.TotalSeconds;
                if (invincibleTime <= 0)
                    isInvincible = false;
            }
            goriyaSprite.Update(gameTime);

            switch (direction)
            {
                case Direction.Up:
                    if (flipTime <= 0)
                    {
                        currentSpriteEffect = SpriteEffects.None;
                        flipTime = 0.1;
                    }
                    else
                    {
                        currentSpriteEffect = SpriteEffects.FlipHorizontally;
                    }
                    break;
                case Direction.Right:
                    currentSpriteEffect = SpriteEffects.None;
                    break;
                case Direction.Down:
                    if (flipTime <= 0)
                    {
                        currentSpriteEffect = SpriteEffects.None;
                        flipTime = 0.1;
                    }
                    else
                    {
                        currentSpriteEffect = SpriteEffects.FlipHorizontally;
                    }
                    break;
                case Direction.Left:
                    currentSpriteEffect = SpriteEffects.FlipHorizontally;
                    break;
            }
            boomerangThrowable.Update(gameTime, position);
        }
    }
}
