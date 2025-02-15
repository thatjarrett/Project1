using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Interfaces;
using Project1.Projectiles;
using Project1.Sprites;
namespace Project1.Entities
{
    public class Link
    {
        private ILinkState currentState;
        private Vector2 position;
        private Vector2 faceDirection;
        private bool isInvincible = false;
        private double invincibleTime = 0;
        private const double InvincibilityDuration = 1.0; // 1 second
        private bool isControlsDisabled = false; // Flag to disable controls
        private bool isVisible = true;
        private ISprite linkSprite;

        private ISprite walkSideSprite;
        private ISprite walkUpSprite;
        private ISprite walkDownSprite;

        private ISprite currentIdleSprite;
        private ISprite idleSideSprite;
        private ISprite idleUpSprite;
        private ISprite idleDownSprite;

        private ISprite currentAttackSprite;
        private ISprite attackSideSprite;
        private ISprite attackSideSprite2;
        private ISprite attackUpSprite;
        private ISprite attackDownSprite;
        private ISprite deathSprite;
        private bool dying = false;
        private int deathFrameCounter = 0;


        private ISprite currentInteractSprite;
        private ISprite interactSideSprite;
        private ISprite interactUpSprite;
        private ISprite interactDownSprite;

        private ISprite swordBeamHorizontal;
        private ISprite swordBeamVertical;
        private ISprite arrowHorizontal;
        private ISprite arrowVertical;
        private ISprite boomerang;

        private BoomerangProjectile boomerangThrowable;

        private SpriteEffects currentSpriteEffect = SpriteEffects.None;

        int damageFrameCounter = 0;
        bool hurting = false;

        private List<Projectile> projectilesList = new List<Projectile>();

        public Link(Vector2 startPos)
        {
            position = startPos;
            currentState = new LinkIdleState(Direction.Down); // Start in Idle state
        }
        public Direction PreviousDirection { get; private set; } = Direction.Down;



        public void ChangeState(ILinkState newState)
        {
            Debug.WriteLine($"Changing state to: {newState.GetType().Name}");
            if (newState is LinkMoveUpState) PreviousDirection = Direction.Up;
            if (newState is LinkMoveDownState) PreviousDirection = Direction.Down;
            if (newState is LinkMoveLeftState) PreviousDirection = Direction.Left;
            if (newState is LinkMoveRightState) PreviousDirection = Direction.Right;

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
            if (isControlsDisabled) return;
            currentState.MoveLeft(this);
        }

        public void MoveRight()
        {
            if (isControlsDisabled) return;
            currentState.MoveRight(this);
        }

        public void MoveUp()
        {
            if (isControlsDisabled) return;
            currentState.MoveUp(this);
        }

        public void MoveDown()
        {
            if (isControlsDisabled) return;
            currentState.MoveDown(this);
        }

        public void PerformAttack()
        {
            if (isControlsDisabled) return;
            currentState.Attack(this);
        }

        public void DisableControls()
        {
            isControlsDisabled = true;
            Debug.WriteLine("Controls disabled.");
        }

        public void EnableControls()
        {
            isControlsDisabled = false;
            Debug.WriteLine("Controls enabled.");
        }
        public void TriggerGameOver()
        {
            Debug.WriteLine("Game Over triggered.");
            DisableControls();

            GameManager.Instance.SetGameOver();

        }
        public void Hide()
        {
            isVisible = false;
        }

        public void Item(int itemNumber)
        {
            if (isControlsDisabled) return;
            currentState.Item(this, itemNumber);
            Projectile projectile = null;
            switch (itemNumber)
            {
                case 1:
                    projectile = new StraightProjectile(position, faceDirection, swordBeamHorizontal, swordBeamVertical, 5);
                    break;
                case 2:
                    projectile = new StraightProjectile(position, faceDirection, arrowHorizontal, arrowVertical, 5);
                    break;
                case 3:
                    boomerangThrowable.Throw(position, faceDirection);
                    break;
            }
            if (projectile != null)
            {
                projectilesList.Add(projectile);
            }
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
            linkSprite.Update(gameTime);
            foreach (var projectile in projectilesList)
            {
                projectile.Update(gameTime);
            }
            boomerangThrowable.Update(gameTime, position);
        }


        public void Move(int dx, int dy)
        {
            position.X += dx;
            position.Y += dy;
        }



        public void Draw(SpriteBatch spriteBatch)
        {
            // If Link is hidden, skip drawing.
            if (!isVisible)
                return;

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
                linkSprite.SetColor(Color.Magenta);
            }
            else
            {
                linkSprite.SetColor(Color.White);
            }
            if ((damageFrameCounter / 5) % 2 == 0)
            {
                linkSprite.Draw(spriteBatch, position, currentSpriteEffect);
            }

            foreach (var projectile in projectilesList)
            {
                projectile.Draw(spriteBatch);
            }
            boomerangThrowable.Draw(spriteBatch);
            if (dying)
            {
                deathFrameCounter++;
                if (deathFrameCounter % 5 == 3)
                {
                    currentSpriteEffect = SpriteEffects.FlipHorizontally;
                }
                else
                {
                    currentSpriteEffect = SpriteEffects.None;
                }
            }
        }

        public void createLinkSprites(Texture2D linkTexture)
        {
            Rectangle[] walkSide = new Rectangle[] { new Rectangle(2, 29, 15, 16), new Rectangle(18, 30, 16, 16) };
            Rectangle[] walkUp = new Rectangle[] { new Rectangle(69, 11, 16, 16), new Rectangle(86, 11, 16, 16) };
            Rectangle[] walkDown = new Rectangle[] { new Rectangle(1, 11, 16, 16), new Rectangle(18, 11, 16, 16) };

            Rectangle[] attackSide = new Rectangle[] { new Rectangle(270, 217, 27, 16), new Rectangle(270, 234, 27, 16), new Rectangle(270, 252, 27, 16), new Rectangle(270, 271, 27, 16) };
            Rectangle[] attackUp = new Rectangle[] { new Rectangle(1, 97, 16, 28), new Rectangle(18, 97, 16, 28), new Rectangle(35, 97, 16, 28), new Rectangle(52, 97, 16, 28) };
            Rectangle[] attackDown = new Rectangle[] { new Rectangle(1, 47, 16, 16), new Rectangle(18, 47, 16, 27), new Rectangle(35, 47, 16, 23), new Rectangle(53, 47, 16, 19) };

            Rectangle[] death = new Rectangle[] { new Rectangle(1, 11, 16, 16), new Rectangle(2, 29, 15, 16), new Rectangle(69, 11, 16, 16), new Rectangle(2, 29, 15, 16) };
            walkSideSprite = new NMoveAnim(linkTexture, walkSide, 5);
            walkUpSprite = new NMoveAnim(linkTexture, walkUp, 5);
            walkDownSprite = new NMoveAnim(linkTexture, walkDown, 5);

            idleSideSprite = new NMoveNAnim(linkTexture, new Rectangle(2, 29, 15, 16));
            idleUpSprite = new NMoveNAnim(linkTexture, new Rectangle(69, 11, 16, 16));
            idleDownSprite = new NMoveNAnim(linkTexture, new Rectangle(1, 11, 16, 16));

            attackSideSprite = new NMoveAnim(linkTexture, attackSide, 5);
            attackSideSprite2 = new NMoveAnim(linkTexture, attackSide, 5, 3, new Vector2(12, 0));
            attackUpSprite = new NMoveAnim(linkTexture, attackUp, 5, 3, new Vector2(0, 12));
            attackDownSprite = new NMoveAnim(linkTexture, attackDown, 5);

            interactSideSprite = new NMoveNAnim(linkTexture, new Rectangle(124, 11, 15, 16));
            interactUpSprite = new NMoveNAnim(linkTexture, new Rectangle(141, 11, 15, 16));
            interactDownSprite = new NMoveNAnim(linkTexture, new Rectangle(107, 11, 16, 16));

            currentIdleSprite = idleDownSprite;
            currentAttackSprite = idleDownSprite;
            currentInteractSprite = interactDownSprite;
            linkSprite = currentIdleSprite;

            deathSprite = new NMoveAnim(linkTexture, death, 5);
            faceDirection = new Vector2(0, 1);

            createProjectileSprites(linkTexture);
        }

        public void SetAnimation(string action)
        {
            if (action.Contains("Idle"))
            {
                linkSprite = currentIdleSprite;
            }
            else if (action.Contains("MoveUp"))
            {
                linkSprite = walkUpSprite;
                currentIdleSprite = idleUpSprite;
                currentAttackSprite = attackUpSprite;
                currentInteractSprite = interactUpSprite;
                faceDirection = new Vector2(0, -1);
                currentSpriteEffect = SpriteEffects.None;
            }
            else if (action.Contains("MoveDown"))
            {
                linkSprite = walkDownSprite;
                currentIdleSprite = idleDownSprite;
                currentAttackSprite = attackDownSprite;
                currentInteractSprite = interactDownSprite;
                faceDirection = new Vector2(0, 1);
                currentSpriteEffect = SpriteEffects.None;
            }
            else if (action.Contains("MoveLeft"))
            {
                linkSprite = walkSideSprite;

                currentIdleSprite = idleSideSprite;
                currentAttackSprite = attackSideSprite2;
                currentInteractSprite = interactSideSprite;
                faceDirection = new Vector2(-1, 0);
                currentSpriteEffect = SpriteEffects.FlipHorizontally;
            }
            else if (action.Contains("MoveRight"))
            {
                linkSprite = walkSideSprite;
                currentIdleSprite = idleSideSprite;
                currentAttackSprite = attackSideSprite;
                currentInteractSprite = interactSideSprite;
                faceDirection = new Vector2(1, 0);
                currentSpriteEffect = SpriteEffects.None;
            }
            else if (action.Contains("Attack"))
            {
                linkSprite = currentAttackSprite;
            }
            else if (action.Contains("Damage"))
            {
                hurting = true;
            }
            else if (action.Contains("Item"))
            {
                linkSprite = currentInteractSprite;

            }
            else if (action.Contains("Death"))
            {
                linkSprite = deathSprite;
                dying = true;

            }
            if (!action.Contains("Damage"))
            {
                hurting = false;
            }
        }
        private void createProjectileSprites(Texture2D texture)
        {
            arrowHorizontal = new NMoveNAnim(texture, new Rectangle(10, 185, 16, 16));
            arrowVertical = new NMoveNAnim(texture, new Rectangle(1, 185, 8, 16));

            swordBeamHorizontal = new NMoveAnim(texture, new Rectangle[] { new Rectangle(45, 154, 16, 16), new Rectangle(115, 154, 16, 16) }, 5);
            swordBeamVertical = new NMoveAnim(texture, new Rectangle[] { new Rectangle(36, 154, 8, 16), new Rectangle(106, 154, 8, 16) }, 5);

            boomerang = new NMoveAnim(texture, new Rectangle[] { new Rectangle(64, 185, 8, 16), new Rectangle(73, 185, 8, 16), new Rectangle(82, 185, 8, 16), new Rectangle(73, 185, 8, 16) }, 5);
            boomerangThrowable = new BoomerangProjectile(boomerang);
        }
    }
}

