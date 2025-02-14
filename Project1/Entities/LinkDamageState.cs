using Microsoft.Xna.Framework;


using Project1.Interfaces;
namespace Project1.Entities
{
    public class LinkDamageState : ILinkState
    {
        private const double DamageDuration = 1.0; // 1 second of invincibility
        private double elapsedTime; // Tracks time spent in damage state
        private Direction _previousDirection;

        public LinkDamageState(Direction previousDirection)
        {
            _previousDirection = previousDirection;
        }

        public void Enter(Link link)
        {
            link.SetAnimation("Damage"); // Play damage animation
            link.SetInvincible(true);    // Make Link invincible
            elapsedTime = 0;             // Reset timer
        }

        public void MoveLeft(Link link)
        {

        }
        public void MoveRight(Link link)
        {

        }
        public void MoveUp(Link link)
        {

        }
        public void MoveDown(Link link)
        {

        }
        public void Attack(Link link)
        {

        }
        public void Damage(Link link)
        {

        }
        public void Item(Link link, int itemNumber)
        {

        }
        public void Update(Link link, GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;

            if (elapsedTime >= DamageDuration)
            {
                link.SetInvincible(false);   // Allow Link to take damage again
                link.ChangeState(new LinkIdleState(_previousDirection)); // Return to previous idle direction
            }
        }
    }
}
