using Microsoft.Xna.Framework;
using Project1.Interfaces;
using System;

namespace Project1.Entities
{
    public class SkeletonMoveState : IEnemyState
    {
        private Direction movingDirection = Direction.Down;
        private double movementDuration;
        Random random = new Random();

        public SkeletonMoveState(Direction d, double duration)
        {
            movementDuration = duration;
            movingDirection = d;
        }

        public void Update(IEnemy skeleton, GameTime gameTime)
        {
            double timeStep = gameTime.ElapsedGameTime.TotalSeconds;
            movementDuration -= timeStep;

            
                movementDuration -= gameTime.ElapsedGameTime.TotalSeconds;
                if (movementDuration <= 0)
                {
                    int x = random.Next(4);
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
                    else
                    {
                        movingDirection = Direction.Down;
                    }
                    movementDuration = random.NextDouble() / 20;

                    movementDuration = 1.0;
                }

                switch (movingDirection)
                {
                    case Direction.Up:
                        skeleton.Move(0, -1);
                        break;
                    case Direction.Right:
                        skeleton.Move(1, 0);
                        break;
                    case Direction.Down:
                        skeleton.Move(0, 1);
                        break;
                    case Direction.Left:
                        skeleton.Move(-1, 0);
                        break;
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
