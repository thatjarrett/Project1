using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using Project1.Interfaces;
using Project1.Sprites;
using System.Reflection.Metadata;
namespace Project1.Entities
{
    public class Aquamentus : IEnemy
    {
        private IEnemyState currentState;
        private Vector2 position;
        private bool isInvincible = false;
        private double invincibleTime = 0;
        private const double InvincibilityDuration = 1.0; // 1 second

        private ISprite aquamentusSprite;
        private ISprite aquamentusWalkSprite;
        private ISprite aquamentusAttackSprite;

        private SpriteEffects currentSpriteEffect = SpriteEffects.None;

        int damageFrameCounter = 0;
        bool hurting = false;

        public Aquamentus(Vector2 startPos)
        {
            position = startPos;
            currentState = new AquamentusWalkState();
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
            isInvincible = value;
            if (value)
                invincibleTime = InvincibilityDuration;
        }

        public void MoveLeft()
        {
            currentState.MoveLeft(this);
        }
        public void MoveRight()
        {
            currentState.MoveRight(this);
        }
        public void MoveUp()
        {
            currentState.MoveUp(this);
        }
        public void MoveDown()
        {
            currentState.MoveDown(this);
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
            aquamentusSprite.Update(gameTime);

        }

        public void Move(int dx, int dy)
        {
            position.X += dx;
            position.Y += dy;
        }

        public void PerformAttack()
        {
            // Logic to trigger attack
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
                aquamentusSprite.SetColor(Color.Magenta);
            }
            else
            {
                aquamentusSprite.SetColor(Color.White);
            }
            if ((damageFrameCounter/5)%2==0)
            {
                aquamentusSprite.Draw(spriteBatch, position, currentSpriteEffect);
            }
            
        }
        public void createEnemySprites(Texture2D aquamentusTexture)
        {
            Rectangle[] aquamentusWalk = new Rectangle[] { new Rectangle(51, 11, 24, 32), new Rectangle(76, 11, 24, 32) };

            Rectangle[] aquamentusAttack = new Rectangle[] { new Rectangle(1, 11, 24, 32), new Rectangle(26, 11, 24, 32) };


            aquamentusWalkSprite = new NMoveAnim(aquamentusTexture, aquamentusWalk, 5);

            aquamentusAttackSprite = new NMoveAnim(aquamentusTexture, aquamentusAttack, 5);

            aquamentusSprite = aquamentusWalkSprite;

            
        }

        public void SetAnimation(string action)
        {
            if (action.Contains("Walk"))
            {
                aquamentusSprite = aquamentusWalkSprite;
            }
            else if (action.Contains("Attack"))
            {
                aquamentusSprite = aquamentusAttackSprite;

            }
            else if (action.Contains("Damage"))
            {
                hurting = true;
            }
            if (!action.Contains("Damage")){
                hurting = false;
            }
        }
    }
}
   
