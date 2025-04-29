using System;
using Microsoft.Xna.Framework;
using Project1.Interfaces;

namespace Project1.Entities
{
    public class SpikeTrapAttackState : IEnemyState
    {
        private Direction movingDirection;
        private double movementDuration;
        private double accelTime = 500;
        int speed = 5;

        public SpikeTrapAttackState(Direction dir)
        {
            this.movingDirection = dir;
        }


        public void Enter(IEnemy spikeTrap)
        {
            //N/A
        }
        public void Update(IEnemy spikeTrap, GameTime gameTime)
        {
            accelTime -= gameTime.ElapsedGameTime.TotalMilliseconds;
            if (accelTime <= 0)
            {
                speed++;
                accelTime = 500;
            }

            if (movingDirection == Direction.Up)
            {
                spikeTrap.Move(0, speed * -1);
            }
            else if (movingDirection == Direction.Down)
            {
                spikeTrap.Move(0, speed);
            }
            else if (movingDirection == Direction.Left)
            {
                spikeTrap.Move(speed*-1, 0);
            }
            else if (movingDirection == Direction.Right)
            {
                spikeTrap.Move(speed, 0);
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
