using Microsoft.Xna.Framework;
using Project1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Entities
{
    public class BatFlyState : IEnemyState
    {
        private Direction movingDirection;
        private double movementDuration;
        Random random = new Random();

        public BatFlyState(Direction d, double duration) {
            movementDuration = duration;
            movingDirection = d;
        }

        public Direction GetDirection()
        {
            return movingDirection;
        }

        public double GetMovementDuration()
        {
            return movementDuration;
        }

        public void Update(IEnemy enemy, GameTime gameTime)
        {
            movementDuration -= gameTime.ElapsedGameTime.TotalSeconds;
            if (movementDuration <= 0) {
                int x = random.Next(3);
                if (x == 0)
                {
                    movingDirection = Direction.Left;
                }
                else if (x == 1)
                {
                    movingDirection = Direction.Right;
                }
                else if (x == 2)
                {
                    movingDirection = Direction.Up;
                }
                else {
                    movingDirection = Direction.Up;
                }
                movementDuration = random.NextDouble()/10;
                enemy.Move(movingDirection == Direction.Left ? -4 : 4, movingDirection == Direction.Up ? -4 : 4);
            }
        }
    }
}
