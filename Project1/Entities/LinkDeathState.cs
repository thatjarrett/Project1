using Microsoft.Xna.Framework;
using Project1.Interfaces;

namespace Project1.Entities
{
    public class LinkDeathState : ILinkState
    {
        private const double DeathDuration = 2.0; // 2 seconds of death animation
        private double elapsedTime; // Tracks time spent in death state

        public void Enter(Link link)
        {
            link.SetAnimation("Death"); // Play death animation
            link.SetInvincible(true);   // Prevent further damage
            link.DisableControls();     // Disable player input (assuming such a method exists)
            elapsedTime = 0;            // Reset timer
        }

        public void MoveLeft(Link link) { }
        public void MoveRight(Link link) { }
        public void MoveUp(Link link) { }
        public void MoveDown(Link link) { }
        public void Attack(Link link) { }
        public void Damage(Link link) { }
        public void Item(Link link, int itemNumber) { }

        public void Update(Link link, GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;

            if (elapsedTime >= DeathDuration)
            {
                link.TriggerGameOver(); // Call game-over logic (or respawn)
            }
        }
    }
}
