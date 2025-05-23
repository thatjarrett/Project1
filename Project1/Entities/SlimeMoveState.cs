﻿using Microsoft.Xna.Framework;
using Project1.Interfaces;
using System;
using System.Diagnostics;


namespace Project1.Entities
{
    public class SlimeMoveState : IEnemyState
    {
        private Direction movingDirection = Direction.Down;
        private double movementDuration = 0.25;
        Random random = new Random();

        public void Update(IEnemy slime, GameTime gameTime)
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
                        movementDuration /= 2;
                        break;
                }

                movementDuration = 1.0;
            }

            switch (movingDirection)
            {
                case Direction.Up:
                    slime.Move(0, -1);
                    break;
                case Direction.Right:
                    slime.Move(1, 0);
                    break;
                case Direction.Down:
                    slime.Move(0, 1);
                    break;
                case Direction.Left:
                    slime.Move(-1, 0);
                    break;
                case Direction.None:
                    slime.Move(0, 0);
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
