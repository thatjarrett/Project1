using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Project1
{
    public class LinkDamageState : ILinkState
    {
        private const double DamageDuration = 1.0; // 1 second of invincibility
        private double elapsedTime; // Tracks time spent in damage state

        public void Enter(Link link)
        {
            link.SetAnimation("Damage"); // Play damage animation
            link.SetInvincible(true);    // Make Link invincible
            elapsedTime = 0;             // Reset timer
        }

        public void HandleInput(Link link, KeyboardState keyboardState)
        {
            // No input allowed while in damage state
        }

        public void Update(Link link, GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;

            if (elapsedTime >= DamageDuration)
            {
                link.SetInvincible(false);   // Allow Link to take damage again
                link.ChangeState(new LinkIdleState()); // Return to idle
            }
        }
    }
}
