using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Interfaces;
using Project1.Sprites;

namespace Project1.Entities
{
    public class Slime : IEnemy

    {
        private SpriteEffects currentSpriteEffect = SpriteEffects.None;


        private IEnemyState currentState;
        private Vector2 position;

        private const double InvincibilityDuration = 1.0;
        private bool isInvincible = false;
        private double invincibleTime = 0;


        private ISprite slimeSprite;

        int damageFrameCounter = 0;
        bool hurting = false;

        public Slime(Vector2 startPos)
        {
            position = startPos;
            currentState = new SlimeMoveState();
        }

        public void ChangeState(IEnemyState newState)
        {
            currentState = newState;
        }

        public void createEnemySprites(Texture2D enemyTexture)
        {
            Rectangle[] slimeRect = new Rectangle[] { new Rectangle(1, 11, 8, 16), new Rectangle(10, 11, 8, 16) };

            slimeSprite = new NMoveAnim(enemyTexture, slimeRect, 5);
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
                slimeSprite.SetColor(Color.Magenta);
            }
            else
            {
                
                slimeSprite.SetColor(Color.White);
            }
            if ((damageFrameCounter / 5) % 2 == 0)
            {
                slimeSprite.Draw(spriteBatch, position, currentSpriteEffect);
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
            currentState.Update(this, gameTime);



            if (isInvincible)
            {
                invincibleTime -= gameTime.ElapsedGameTime.TotalSeconds;
                if (invincibleTime <= 0)
                    isInvincible = false;
            }
            slimeSprite.Update(gameTime);
        }
    }
}
