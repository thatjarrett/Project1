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
    public class Link
    {
        private ILinkState currentState;
        private Vector2 position;
        private bool isInvincible = false;
        private double invincibleTime = 0;
        private const double InvincibilityDuration = 1.0; // 1 second

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
        private ISprite attackUpSprite;
        private ISprite attackDownSprite;

        private ISprite currentInteractSprite;
        private ISprite interactSideSprite;
        private ISprite interactUpSprite;
        private ISprite interactDownSprite;

        private SpriteEffects currentSpriteEffect = SpriteEffects.None;

        int damageFrameCounter = 0;
        bool hurting = false;

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
        public void Item(int itemNumber)
        {
            //currentState.Item(this, itemNumber);
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
                linkSprite.SetColor(Color.Magenta);
            }
            else
            {
                linkSprite.SetColor(Color.White);
            }
            if ((damageFrameCounter/5)%2==0)
            {
                linkSprite.Draw(spriteBatch, position, currentSpriteEffect);
            }
            
        }
        public void createLinkSprites(Texture2D linkTexture)
        {
            Rectangle[] walkSide = new Rectangle[] { new Rectangle(2, 29, 15, 16), new Rectangle(18, 30, 16, 16) };
            Rectangle[] walkUp = new Rectangle[] { new Rectangle(69, 11, 16, 16), new Rectangle(86, 11, 16, 16) };
            Rectangle[] walkDown = new Rectangle[] { new Rectangle(1, 11, 16, 16), new Rectangle(18, 11, 16, 16) };

            Rectangle[] attackSide = new Rectangle[] { new Rectangle(1, 77, 16, 16), new Rectangle(18, 77, 27, 17), new Rectangle(46, 77, 23, 17), new Rectangle(70, 77, 19, 17) };
            Rectangle[] attackUp = new Rectangle[] { new Rectangle(1, 109, 16, 16), new Rectangle(18, 97, 16, 28), new Rectangle(35, 98, 16, 27), new Rectangle(52, 106, 16, 19) };
            Rectangle[] attackDown = new Rectangle[] { new Rectangle(1, 47, 16, 16), new Rectangle(18, 47, 16, 27), new Rectangle(35, 47, 16, 23), new Rectangle(53, 47, 16, 19) };

            ISprite walkLeftSprite = new NMoveAnim(linkTexture, walkSide, 5);

            walkSideSprite = new NMoveAnim(linkTexture, walkSide, 5);
            walkUpSprite = new NMoveAnim(linkTexture, walkUp, 5);
            walkDownSprite = new NMoveAnim(linkTexture, walkDown, 5);

            idleSideSprite = new NMoveNAnim(linkTexture,new Rectangle(2, 29, 15, 16)); 
            idleUpSprite = new NMoveNAnim(linkTexture, new Rectangle(69, 11, 16, 16));
            idleDownSprite = new NMoveNAnim(linkTexture, new Rectangle(1, 11, 16, 16));

            attackSideSprite = new NMoveAnim(linkTexture, attackSide, 5);
            attackUpSprite = new NMoveAnim(linkTexture, attackUp, 5);
            attackDownSprite = new NMoveAnim(linkTexture, attackDown, 5);

            interactSideSprite = new NMoveNAnim(linkTexture, new Rectangle(124, 11, 15, 16));
            interactUpSprite = new NMoveNAnim(linkTexture, new Rectangle(141, 11, 15, 16));
            interactDownSprite = new NMoveNAnim(linkTexture, new Rectangle(107, 11, 16, 16));

            currentIdleSprite = idleDownSprite;
            currentAttackSprite = idleDownSprite;
            currentInteractSprite = interactDownSprite;
            linkSprite = currentIdleSprite;
            
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

                currentSpriteEffect = SpriteEffects.None;
            }
            else if (action.Contains("MoveDown"))
            {
                linkSprite = walkDownSprite;
                currentIdleSprite = idleDownSprite;
                currentAttackSprite = attackDownSprite;
                currentInteractSprite = interactDownSprite;

                currentSpriteEffect = SpriteEffects.None;
            }
            else if (action.Contains("MoveLeft"))
            {
                linkSprite = walkSideSprite;

                currentIdleSprite = idleSideSprite;
                currentAttackSprite = attackSideSprite;
                currentInteractSprite = interactSideSprite;

                currentSpriteEffect = SpriteEffects.FlipHorizontally;
            }
            else if (action.Contains("MoveRight"))
            {
                linkSprite = walkSideSprite;
                currentIdleSprite = idleSideSprite;
                currentAttackSprite = attackSideSprite;
                currentInteractSprite = interactSideSprite;

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
            else if (action.Contains("Interact"))
            {
                linkSprite = currentInteractSprite;
            }
            if (!action.Contains("Damage")){
                hurting = false;
            }
        }
    }
}
   
