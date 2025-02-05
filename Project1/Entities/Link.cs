using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using Project1.Interfaces;
namespace Project1.Entities
{
    public class Link
    {
        private ILinkState currentState;
        private Texture2D spriteSheet;
        private Vector2 position;
        private Rectangle sourceRect;
        private int spriteWidth = 32;
        private int spriteHeight = 32;
        private bool isInvincible = false;
        private double invincibleTime = 0;
        private const double InvincibilityDuration = 1.0; // 1 second

        // Animation-related variables
        private int frameIndex = 0;      // Current animation frame
        private int frameCounter = 0;    // Counts updates for animation delay
        private const int frameDelay = 10; // Change frame every 10 updates

        public Link(Texture2D texture, Vector2 startPos)
        {
            spriteSheet = texture;
            position = startPos;
            currentState = new LinkIdleState(Direction.Down); // Start in Idle state
        }
        public Direction PreviousDirection { get; private set; } = Direction.Down;

        private ILinkState _currentState;

        public void ChangeState(ILinkState newState)
        {
            Debug.WriteLine($"Changing state to: {newState.GetType().Name}");
            if (newState is LinkMoveUpState) PreviousDirection = Direction.Up;
            if (newState is LinkMoveDownState) PreviousDirection = Direction.Down;
            if (newState is LinkMoveLeftState) PreviousDirection = Direction.Left;
            if (newState is LinkMoveRightState) PreviousDirection = Direction.Right;

            // Change the current state
            _currentState = newState;
            _currentState.Enter(this);
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
            UpdateAnimation();

            if (isInvincible)
            {
                invincibleTime -= gameTime.ElapsedGameTime.TotalSeconds;
                if (invincibleTime <= 0)
                    isInvincible = false;
            }
        }

        private void UpdateAnimation()
        {
            frameCounter++;

            if (frameCounter >= frameDelay) // Change frame every `frameDelay` updates
            {
                frameCounter = 0;
                frameIndex++;
            }

            SetAnimation(currentState.ToString()); // Ensure the correct animation
        }

        public void SetAnimation(string action)
        {
            int row = 0;
            int maxFrames = 1;

            if (action.Contains("Idle"))
            {
                row = 0;
                maxFrames = 1;
            }
            else if (action.Contains("MoveUp"))
            {
                row = 1;
                maxFrames = 2;
            }
            else if (action.Contains("MoveDown"))
            {
                row = 2;
                maxFrames = 2;
            }
            else if (action.Contains("MoveLeft"))
            {
                row = 3;
                maxFrames = 2;
            }
            else if (action.Contains("MoveRight"))
            {
                row = 4;
                maxFrames = 2;
            }
            else if (action.Contains("Attack"))
            {
                row = 5;
                maxFrames = 2;
            }
            else if (action.Contains("Damage"))
            {
                row = 6;
                maxFrames = 2;
            }

            frameIndex %= maxFrames;
            sourceRect = new Rectangle(frameIndex * spriteWidth, row * spriteHeight, spriteWidth, spriteHeight);
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

        public bool AnimationFinished()
        {
            return frameIndex == 0; // Reset after completing cycle
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteSheet, position, sourceRect, Color.White);
        }
    }
}
