using System;
using Microsoft.Xna.Framework;
using Project1.Interfaces;

namespace Project1.Entities
{
    public class SpikeTrapReturnState : IEnemyState
    {
        private Direction movingDirection;
        private double movementDuration;
        private double accelTime = 500;
        int speed = 2;


        public SpikeTrapReturnState(Direction dir)
        {
            movingDirection = dir;
        }


        public void Enter(IEnemy spikeTrap)
        {
            //N/A
        }
        public void Update(IEnemy spikeTrap, GameTime gameTime)
        {
            if (movingDirection == Direction.Left)
            {
                spikeTrap.Move(speed * -1, 0);
            }
            else if (movingDirection == Direction.Right)
            {
                spikeTrap.Move(speed, 0);
            }
            else if (movingDirection == Direction.Down)
            {
                spikeTrap.Move(0, speed);
            }
            else if (movingDirection == Direction.Up)
            {
                spikeTrap.Move(0, speed);
            }

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
