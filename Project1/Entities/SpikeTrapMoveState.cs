using Microsoft.Xna.Framework;
using Project1.Interfaces;

namespace Project1.Entities
{
    public class SpikeTrapMoveState : IEnemyState
    {
        private Direction movingDirection = Direction.Up;
        private double movementDuration = 1.0;


        public void Enter(IEnemy spikeTrap)
        {
            //N/A
        }

        public void MoveLeft(IEnemy spikeTrap)
        {
            //N/A
        }
        public void MoveRight(IEnemy spikeTrap)
        {
            //N/A
        }
        public void MoveUp(IEnemy spikeTrap)
        {
            //N/A
        }
        public void MoveDown(IEnemy spikeTrap)
        {
            //N/A
        }
        public void Attack(IEnemy spikeTrap)
        {
            //N/A
        }
        public void Damage(IEnemy spikeTrap)
        {
            //N/A
        }
        public void Update(IEnemy spikeTrap, GameTime gameTime)
        {
            double timeStep = gameTime.ElapsedGameTime.TotalSeconds;
            movementDuration -= timeStep;

            if (movementDuration <= 0)
            {
                switch (movingDirection)
                {
                    case Direction.Up:
                        movingDirection = Direction.Right;
                        break;
                    case Direction.Right:
                        movingDirection = Direction.Down;
                        break;
                    case Direction.Down:
                        movingDirection = Direction.Left;
                        break;
                    case Direction.Left:
                        movingDirection = Direction.Up;
                        break;
                }
                movementDuration = 1.0;
            }

            switch (movingDirection)
            {
                case Direction.Up:
                    spikeTrap.Move(0, -1);
                    break;
                case Direction.Right:
                    spikeTrap.Move(1, 0);
                    break;
                case Direction.Down:
                    spikeTrap.Move(0, 1);
                    break;
                case Direction.Left:
                    spikeTrap.Move(-1, 0);
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
