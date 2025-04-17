using Microsoft.Xna.Framework;
using Project1.Interfaces;
using System;

namespace Project1.Entities
{
    public class TriceratopsMoveState : IEnemyState
    {
        private Direction movingDirection = Direction.Down;
        private double movementDuration = 0.25;
        private Random random = new Random();
        private bool tired = false;

        public TriceratopsMoveState(bool b) {
            tired = b;
        }

        public void Enter(IEnemy dino)
        {
           
        }

        public void MoveLeft(IEnemy dino) { }
        public void MoveRight(IEnemy dino) { }
        public void MoveUp(IEnemy dino) { }
        public void MoveDown(IEnemy dino) { }
        public void Attack(IEnemy dino) { }
        public void Damage(IEnemy dino) { }

        public void Update(IEnemy dino, GameTime gameTime)
        {
            double timeStep = gameTime.ElapsedGameTime.TotalSeconds;
            movementDuration -= timeStep;

            if (movementDuration <= 0)
            {
                movementDuration = 1.0;
                int x = random.Next(4); 

                
                    tired = false;
                    switch (x)
                    {
                        case 0:
                            movingDirection = Direction.Right;
                            dino.MoveRight();
                            break;
                        case 1:
                            movingDirection = Direction.Down;
                            dino.MoveDown();
                            break;
                        case 2:
                            movingDirection = Direction.Left;
                            dino.MoveLeft();
                            break;
                        case 3:
                            movingDirection = Direction.Up;
                            dino.MoveUp();
                            break;
                    
                }
            }

            if (!tired) 
            {
                switch (movingDirection)
                {
                    case Direction.Up:
                        dino.Move(0, -1);
                        break;
                    case Direction.Right:
                        dino.Move(1, 0);
                        break;
                    case Direction.Down:
                        dino.Move(0, 1);
                        break;
                    case Direction.Left:
                        dino.Move(-1, 0);
                        break;
                    case Direction.None:
                      
                        break;
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
