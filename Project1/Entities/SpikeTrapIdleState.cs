using Microsoft.Xna.Framework;
using Project1.Interfaces;

namespace Project1.Entities
{
    public class SpikeTrapIdleState : IEnemyState
    {
        private Direction movingDirection = Direction.Up;
        private double movementDuration = 1.0;


        public void Enter(IEnemy spikeTrap)
        {
            //N/A
        }

        public void MoveLeft(IEnemy spikeTrap)
        {
            //N/A
        }
        public void MoveRight(IEnemy spikeTrap)
        {
            //N/A
        }
        public void MoveUp(IEnemy spikeTrap)
        {
            //N/A
        }
        public void MoveDown(IEnemy spikeTrap)
        {
            //N/A
        }
        public void Attack(IEnemy spikeTrap)
        {
            //N/A
        }
        public void Damage(IEnemy spikeTrap)
        {
            //N/A
        }
        public void Update(IEnemy spikeTrap, GameTime gameTime)
        {
            
        }
        public Direction GetDirection()
        {
            return movingDirection;
        }

        public double GetMovementDuration()
        {
            return movementDuration;
        }
    }
}
