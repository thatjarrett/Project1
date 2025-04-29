using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Project1.Interfaces;

namespace Project1.Entities
{
    public class GoriyaAttackState : IEnemyState
    {
        private Direction movingDirection;
        private double movementDuration;
        Random random = new Random();

        public GoriyaAttackState()
        {
             movingDirection = Direction.Down;
             movementDuration = 0.25;
        }

        public void Update(IEnemy enemy, GameTime gameTime)
        {
            if(enemy is Goriya goriya)
            {
                double timeStep = gameTime.ElapsedGameTime.TotalSeconds;
                movementDuration -= timeStep;

                //Trace.WriteLine(movementDuration + ", boomerang throwing in progress");

                if (movementDuration <= 0)
                {
                    movementDuration = 1.0;
                    //goriya.MoveUp();
                    int x = random.Next(5);
                    switch (x)
                    {
                        case 0:
                            movingDirection = Direction.Right;
                            goriya.MoveRight();
                            break;
                        case 1:
                            movingDirection = Direction.Down;
                            goriya.MoveDown();
                            break;
                        case 2:
                            movingDirection = Direction.Left;
                            goriya.MoveLeft();
                            break;
                        case 3:
                            movingDirection = Direction.Up;
                            goriya.MoveUp();
                            break;
                        case 4:
                            goriya.PerformAttack();
                            break;
                    }
                }
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
