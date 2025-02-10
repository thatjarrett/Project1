using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using Project1.Interfaces;
using Project1.Sprites;
using System.Reflection.Metadata;
using Project1.Projectiles;
namespace Project1.Entities
{
    public class Aquamentus : IEnemy
    {
        private IEnemyState currentState;
        private Vector2 position;
        private bool isInvincible = false;
        private double invincibleTime = 0;
        private double attackInterval = 1.0;
        private const double InvincibilityDuration = 1.0;

        private ISprite aquamentusSprite;
        private ISprite aquamentusWalkSprite;
        private ISprite aquamentusAttackSprite;
        private ISprite aquamentusFireball;

        private Projectile[] fireballs = new StraightProjectile[3];

        private SpriteEffects currentSpriteEffect = SpriteEffects.None;

        int damageFrameCounter = 0;
        bool hurting = false;

        public Aquamentus(Vector2 startPos)
        {
            position = startPos;
            currentState = new AquamentusWalkState(Direction.Left, 1.0);
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

            if (isInvincible)
            {
                invincibleTime -= gameTime.ElapsedGameTime.TotalSeconds;
                if (invincibleTime <= 0)
                    isInvincible = false;
            }
            aquamentusSprite.Update(gameTime);

            if (currentState is AquamentusAttackState)
            {
                attackInterval -= gameTime.ElapsedGameTime.TotalSeconds;
                if ((attackInterval < 0))
                {
                    this.ChangeState(new AquamentusWalkState(currentState.GetDirection(), currentState.GetMovementDuration()));
                    attackInterval = 1.0;
                }
                
            }
            else if (currentState is AquamentusWalkState)
            {
                attackInterval -= gameTime.ElapsedGameTime.TotalSeconds;
                if ((attackInterval < 0)) {
                    this.PerformAttack();
                    attackInterval = 1.0;
                }
            }
            if (fireballs[0] != null)
            {
                for (int i = 0; i < 3; i++)
                {
                    fireballs[i].Update(gameTime);
                }
            }
            

        }

        public void Move(int dx, int dy)
        {
            position.X += dx;
            position.Y += dy;
        }

        public void PerformAttack()
        {
            fireballs[0] = new StraightProjectile(position, new Vector2(-2, -1), aquamentusFireball, aquamentusFireball, 2);
            fireballs[1] = new StraightProjectile(position, new Vector2(-2, 0), aquamentusFireball, aquamentusFireball, 2);
            fireballs[2] = new StraightProjectile(position, new Vector2(-2, 1), aquamentusFireball, aquamentusFireball, 2);
            this.ChangeState(new AquamentusAttackState(currentState.GetDirection(), currentState.GetMovementDuration()));
            
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
            if (fireballs[0] != null)
            {
                for (int i = 0; i < 3; i++)
                {
                    fireballs[i].Draw(spriteBatch);
                }
            }

        }
        public void createEnemySprites(Texture2D aquamentusTexture)
        {
            createFireballSprites(aquamentusTexture);

            Rectangle[] aquamentusWalk = new Rectangle[] { new Rectangle(51, 11, 24, 32), new Rectangle(76, 11, 24, 32) };
            Rectangle[] aquamentusAttack = new Rectangle[] { new Rectangle(1, 11, 24, 32), new Rectangle(26, 11, 24, 32) };

            aquamentusWalkSprite = new NMoveAnim(aquamentusTexture, aquamentusWalk, 5);
            aquamentusAttackSprite = new NMoveAnim(aquamentusTexture, aquamentusAttack, 5);

            aquamentusSprite = aquamentusWalkSprite;
        }

        public void createFireballSprites(Texture2D aquamentusTexture)
        {
            aquamentusFireball = new NMoveAnim(aquamentusTexture, new Rectangle[] { new Rectangle(119, 11, 8, 16), new Rectangle(128, 11, 8, 16) }, 30);
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
   
