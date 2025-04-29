using Microsoft.Xna.Framework;
using Project1.Interfaces;
using System;

namespace Project1.Entities
{
    public class GoriyaMoveState : IEnemyState
    {
        private Direction movingDirection = Direction.Down;
        private double movementDuration = 0.25;
        private Random random = new Random();
        private bool isAttacking = false;

        public void Enter(IEnemy goriya)
        {
           
        }

        public void Update(IEnemy enemy, GameTime gameTime)
        {
            if (enemy is Goriya goriya)
            {
                double timeStep = gameTime.ElapsedGameTime.TotalSeconds;
                movementDuration -= timeStep;

                if (movementDuration <= 0)
                {
                    movementDuration = 1.0;
                    int x = random.Next(5);

                    if (x == 4)
                    {
                        goriya.PerformAttack();
                        isAttacking = true;
                        movingDirection = Direction.None;
                    }
                    else
                    {
                        isAttacking = false;
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
                        }
                    }
                }

                if (!isAttacking)
                {
                    switch (movingDirection)
                    {
                        case Direction.Up:
                            goriya.Move(0, -1);
                            break;
                        case Direction.Right:
                            goriya.Move(1, 0);
                            break;
                        case Direction.Down:
                            goriya.Move(0, 1);
                            break;
                        case Direction.Left:
                            goriya.Move(-1, 0);
                            break;
                        case Direction.None:

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
