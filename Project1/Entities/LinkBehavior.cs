using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1.GameObjects.Items;
using Project1.Projectiles;
using Project1.Sprites;
using System.Collections.Generic;

namespace Project1.Entities
{
    public partial class Link
    {
        public void MoveLeft() => TryMove(() => currentState.MoveLeft(this));
        public void MoveRight() => TryMove(() => currentState.MoveRight(this));
        public void MoveUp() => TryMove(() => currentState.MoveUp(this));
        public void MoveDown() => TryMove(() => currentState.MoveDown(this));
        public void PerformAttack() => TryMove(() => currentState.Attack(this));

        private void TryMove(System.Action action)
        {
            if (!isControlsDisabled)
                action();
        }

        public void SetInvincible(bool value, Vector2? knockbackDir = null)
        {
            isInvincible = value;
            invincibleTime = value ? InvincibilityDuration : 0;

            if (value && knockbackDir.HasValue)
            {
                knockbackDirection = knockbackDir.Value;
                isKnockback = true;
                knockbackTimer = KnockbackDuration;
            }
        }

        public void Update(GameTime gameTime)
        {
        


            currentState.Update(this, gameTime);

            if (!(currentState is LinkDeathState) && health == 0)
            {
                ChangeState(new LinkDeathState());
                return;
            }

            HandleTimers(gameTime);
            UpdateSprites(gameTime);
        }

        private void HandleTimers(GameTime gameTime)
        {
            if (isKnockback)
            {
                knockbackTimer -= gameTime.ElapsedGameTime.TotalSeconds;
                if (knockbackTimer > 0)
                {
                    Move((int)(knockbackDirection.X * KnockbackForce), (int)(knockbackDirection.Y * KnockbackForce));
                }
                else
                {
                    isKnockback = false;
                }
            }

            if (isInvincible)
            {
                invincibleTime -= gameTime.ElapsedGameTime.TotalSeconds;
                if (invincibleTime <= 0)
                    isInvincible = false;
            }

            if (freezeEnemies)
            {
                freezeTimer -= gameTime.ElapsedGameTime.TotalSeconds;
                if (freezeTimer <= 0)
                    freezeEnemies = false;
            }
        }

        private void UpdateSprites(GameTime gameTime)
        {
            if (linkSprite != null)
                linkSprite.Update(gameTime);

            if (boomerangProjectile != null)
            {
                boomerangProjectile.ownerPosition(position);
                boomerangProjectile.Update(gameTime);
            }

            foreach (var proj in projectiles)
                proj.Update(gameTime);

            foreach (var bomb in bombs)
                bomb.Update(gameTime);
        }

        public void Move(int dx, int dy)
        {
            position.X += dx;
            position.Y += dy;
            collider.Move(dx, dy);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!isVisible || linkSprite == null)
                return;

            UpdateDamageFrame();
            linkSprite.SetColor(hurting ? Color.Magenta : Color.White);

            if ((damageFrameCounter / 5) % 2 == 0)
                linkSprite.Draw(spriteBatch, position, currentSpriteEffect);

            foreach (var projectile in projectiles)
                projectile.Draw(spriteBatch);

            boomerangProjectile?.Draw(spriteBatch);

            foreach (var bomb in bombs)
                bomb.Draw(spriteBatch);

            if (dying)
            {
                deathFrameCounter++;
                currentSpriteEffect = (deathFrameCounter % 5 == 3)
                    ? SpriteEffects.FlipHorizontally
                    : SpriteEffects.None;
            }
        }

        private void UpdateDamageFrame()
        {
            if (hurting)
                damageFrameCounter++;
            else
                damageFrameCounter = 0;
        }
    }
}