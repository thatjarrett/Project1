using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Project1
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
            currentState = new LinkIdleState(); // Start in Idle state
        }

        public void ChangeState(ILinkState newState)
        {
            currentState = newState;
            currentState.Enter(this);
        }

        public void SetInvincible(bool value)
        {
            isInvincible = value;
            if (value)
                invincibleTime = InvincibilityDuration;
        }

        public void HandleInput(KeyboardState keyboardState)
        {
            currentState.HandleInput(this, keyboardState);
        }

        public void Update(GameTime gameTime) // ✅ Keeping `Update(GameTime gameTime)`
        {
            currentState.Update(this, gameTime);
            UpdateAnimation(); // ✅ Frame-based animation updates here

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
