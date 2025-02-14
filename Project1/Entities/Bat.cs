using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Interfaces;
using Project1.Sprites;

namespace Project1.Entities
{
    public class Bat : IEnemy

    {
        private SpriteEffects currentSpriteEffect = SpriteEffects.None;


        private IEnemyState currentState;
        private Vector2 position;

        private const double InvincibilityDuration = 1.0;
        private bool isInvincible = false;
        private double invincibleTime = 0;


        private ISprite batSprite;

        int damageFrameCounter = 0;
        bool hurting = false;

        public Bat(Vector2 startPos)
        {
            position = startPos;
            currentState = new AquamentusWalkState(Direction.Left, 1.0);
        }

        public void ChangeState(IEnemyState newState)
        {
            currentState = newState;
        }

        public void createEnemySprites(Texture2D enemyTexture)
        {
            Rectangle[] batRect = new Rectangle[] { new Rectangle(183, 11, 15, 15), new Rectangle(200, 11, 15, 15) };

            batSprite = new NMoveAnim(enemyTexture, batRect, 5);
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
                batSprite.SetColor(Color.Magenta);
            }
            else
            {
                batSprite.SetColor(Color.White);
            }
            if ((damageFrameCounter / 5) % 2 == 0)
            {
                batSprite.Draw(spriteBatch, position, currentSpriteEffect);
            }

            //TODO: why the fuck is the code like that????????????????
        }

        public void Move(int dx, int dwhy)
        {
            position.X += dx;
            position.Y += dwhy;
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
            //the bat just flies around i think
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
            if (isInvincible)
            {
                invincibleTime -= gameTime.ElapsedGameTime.TotalSeconds;
                if (invincibleTime <= 0)
                    isInvincible = false;
            }
            batSprite.Update(gameTime);
        }
    }
}
