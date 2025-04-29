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

        public void Update(IEnemy enemy, GameTime gameTime)
        {
            if (enemy is not Goriya goriya) return;

            UpdateMovementTimer(gameTime);

            if (movementDuration <= 0)
            {
                DecideNextAction(goriya);
            }

            if (!isAttacking)
            {
                MoveInCurrentDirection(goriya);
            }
        }

        private void UpdateMovementTimer(GameTime gameTime)
        {
            double timeStep = gameTime.ElapsedGameTime.TotalSeconds;
            movementDuration -= timeStep;
        }

        private void DecideNextAction(Goriya goriya)
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
                movingDirection = (Direction)x;
                MoveGoriya(goriya, movingDirection);
            }
        }

        private void MoveGoriya(Goriya goriya, Direction direction)
        {
            switch (direction)
            {
                case Direction.Right:
                    goriya.MoveRight();
                    break;
                case Direction.Down:
                    goriya.MoveDown();
                    break;
                case Direction.Left:
                    goriya.MoveLeft();
                    break;
                case Direction.Up:
                    goriya.MoveUp();
                    break;
            }
        }

        private void MoveInCurrentDirection(Goriya goriya)
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
                    // No movement
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
