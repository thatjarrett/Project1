using Microsoft.Xna.Framework;
using Project1.Interfaces;
using System;
using System.Diagnostics;


namespace Project1.Entities
{
    public class HandMoveState : IEnemyState
    {
        private Direction movingDirection = Direction.Down;
        private double movementDuration = 0.25;
        Random random = new Random();


        public void Enter(IEnemy enemy)
        {
            //N/A
        }

        public void MoveLeft(IEnemy enemy)
        {
            //N/A
        }
        public void MoveRight(IEnemy enemy)
        {
            //N/A
        }
        public void MoveUp(IEnemy enemy)
        {
            //N/A
        }
        public void MoveDown(IEnemy enemy)
        {
            //N/A
        }
        public void Attack(IEnemy enemy)
        {
            //N/A
        }
        public void Damage(IEnemy enemy)
        {
            //N/A
        }
        public void Update(IEnemy enemy, GameTime gameTime)
        {
            double timeStep = gameTime.ElapsedGameTime.TotalSeconds;
            movementDuration -= timeStep;

            if (movementDuration <= 0)
            {
                int x = random.Next(4);

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
                }

                movementDuration = 1.0;
            }

            switch (movingDirection)
            {
                case Direction.Up:
                    enemy.Move(0, -3);
                    break;
                case Direction.Right:
                    enemy.Move(3, 0);
                    break;
                case Direction.Down:
                    enemy.Move(0, 3);
                    break;
                case Direction.Left:
                    enemy.Move(-3, 0);
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
