using Microsoft.Xna.Framework;
using Project1.Interfaces;
using System;
using System.Diagnostics;


namespace Project1.Entities
{
    public class KeySkeletonMoveState : IEnemyState
    {
        private Direction movingDirection = Direction.Down;
        private double movementDuration = 0.25;
        Random random = new Random();


        public void Enter(IEnemy skeleton)
        {
            //N/A
        }

        public void MoveLeft(IEnemy skeleton)
        {
            //N/A
        }
        public void MoveRight(IEnemy skeleton)
        {
            //N/A
        }
        public void MoveUp(IEnemy skeleton)
        {
            //N/A
        }
        public void MoveDown(IEnemy skeleton)
        {
            //N/A
        }
        public void Attack(IEnemy skeleton)
        {
            //N/A
        }
        public void Damage(IEnemy skeleton)
        {
            //N/A
        }
        public void Update(IEnemy skeleton, GameTime gameTime)
        {
            double timeStep = gameTime.ElapsedGameTime.TotalSeconds;
            movementDuration -= timeStep;

            if (movementDuration <= 0)
            {
                int x = random.Next(5);

                //Trace.WriteLine(x);

                switch (x)
                {
                    case 0:
                        movingDirection = Direction.Right;
                        break;
                    case 1:
                        movingDirection = Direction.Down;
                        break;
                    case 2:
                        movingDirection = Direction.Left;
                        break;
                    case 3:
                        movingDirection = Direction.Up;
                        break;
                    case 4:
                        movingDirection = Direction.None;
                        movementDuration /= 4;
                        break;
                }

                movementDuration = 1.0;
            }

            switch (movingDirection)
            {
                case Direction.Up:
                    skeleton.MoveUp();
                    break;
                case Direction.Right:
                    skeleton.MoveRight();
                    break;
                case Direction.Down:
                    skeleton.MoveDown();
                    break;
                case Direction.Left:
                    skeleton.MoveLeft();
                    break;
                case Direction.None:
                    skeleton.PerformAttack();
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
