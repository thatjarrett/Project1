using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Project1
{
    public class Link
    {
        private ILinkState currentState;  // Manages state transitions
        private Texture2D spriteSheet;   // Link's spritesheet
        private Vector2 position;        // Link's position in the game world
        private Rectangle sourceRect;    // Section of the spritesheet to draw
        private int spriteWidth = 32;    // Adjust as per your sprite size
        private int spriteHeight = 32;

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

        public void HandleInput(KeyboardState keyboardState)
        {
            currentState.HandleInput(this, keyboardState);
        }

        public void Update(GameTime gameTime)
        {
            currentState.Update(this, gameTime);
        }

        public void SetAnimation(string action)
        {
            if (action == "Idle")
                sourceRect = new Rectangle(0, 0, spriteWidth, spriteHeight); // Example: Idle position in sprite sheet
            else if (action == "MoveUp")
                sourceRect = new Rectangle(spriteWidth, 0, spriteWidth, spriteHeight);
            else if (action == "MoveDown")
                sourceRect = new Rectangle(spriteWidth * 2, 0, spriteWidth, spriteHeight);
            else if (action == "MoveLeft")
                sourceRect = new Rectangle(spriteWidth * 3, 0, spriteWidth, spriteHeight);
            else if (action == "MoveRight")
                sourceRect = new Rectangle(spriteWidth * 4, 0, spriteWidth, spriteHeight);
            else if (action == "Attack")
                sourceRect = new Rectangle(spriteWidth * 5, 0, spriteWidth, spriteHeight);
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
            return true; // Replace with actual logic if needed
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteSheet, position, sourceRect, Color.White);
        }
    }
}
