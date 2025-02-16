using Microsoft.Xna.Framework;
using Project1.Interfaces;
using System;
using System.Diagnostics;


namespace Project1.Entities
{
    public class GoriyaMoveState : IEnemyState
    {
        private Direction movingDirection = Direction.Down;
        private double movementDuration = 0.25;
        Random random = new Random();


        public void Enter(IEnemy slime)
        {
            //N/A
        }

        public void MoveLeft(IEnemy slime)
        {
            //N/A
        }
        public void MoveRight(IEnemy slime)
        {
            //N/A
        }
        public void MoveUp(IEnemy slime)
        {
            //N/A
        }
        public void MoveDown(IEnemy slime)
        {
            //N/A
        }
        public void Attack(IEnemy slime)
        {
            //N/A
        }
        public void Damage(IEnemy slime)
        {
            //N/A
        }
        public void Update(IEnemy goriya, GameTime gameTime)
        {
            double timeStep = gameTime.ElapsedGameTime.TotalSeconds;
            movementDuration -= timeStep;

            if (movementDuration <= 0)
            {
                movementDuration = 1.0;
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
                    goriya.Move(0, 0);
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
