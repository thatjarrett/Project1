using System;
using Microsoft.Xna.Framework;
using Project1.Interfaces;

namespace Project1.Entities
{
    public class TriceratopsAttackState : IEnemyState
    {
        private Direction movingDirection;
        private double movementDuration;
        private double accelTime = 500;
        int speed = 5;

        public TriceratopsAttackState(Direction dir)
        {
            this.movingDirection = dir;
        }

        public void Update(IEnemy dino, GameTime gameTime)
        {
            accelTime -= gameTime.ElapsedGameTime.TotalMilliseconds;
            if (accelTime <= 0)
            {
                speed++;
                accelTime = 500;
            }

            if (movingDirection == Direction.Up)
            {
                dino.Move(0, speed * -1);
            }
            else if (movingDirection == Direction.Down)
            {
                dino.Move(0, speed);
            }
            else if (movingDirection == Direction.Left)
            {
                dino.Move(speed*-1, 0);
            }
            else if (movingDirection == Direction.Right)
            {
                dino.Move(speed, 0);
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
