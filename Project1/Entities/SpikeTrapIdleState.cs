using Microsoft.Xna.Framework;
using Project1.Interfaces;

namespace Project1.Entities
{
    public class SpikeTrapIdleState : IEnemyState
    {
        private Direction movingDirection = Direction.Up;
        private double movementDuration = 1.0;

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
