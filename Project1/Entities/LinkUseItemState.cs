using System.Diagnostics;
using Microsoft.Xna.Framework;


using Project1.Interfaces;
namespace Project1.Entities
{
    public class LinkUseItemState : ILinkState
    {
        private const double ItemDuration = 1.0; // 1 second of item use
        private double elapsedTime; // Tracks time spent in item state
        private Direction _previousDirection;

        public LinkUseItemState(Direction previousDirection)
        {
            _previousDirection = previousDirection;
        }

        public void Enter(Link link)
        {
            link.SetAnimation("Item"); // Play item animation

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
            Debug.WriteLine("use item " + itemNumber);

        }
        public void Update(Link link, GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;

            if (elapsedTime >= ItemDuration)
            {

                link.ChangeState(new LinkIdleState(_previousDirection)); // Return to previous idle direction
            }
        }
    }
}
