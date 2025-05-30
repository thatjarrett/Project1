﻿using System;
using Microsoft.Xna.Framework;
using Project1.Interfaces;

namespace Project1.Entities
{
    public class AquamentusWalkState : IEnemyState
    {
        private Direction movingDirection;
        private double oscillationDuration;
        Random random = new Random();

        public AquamentusWalkState(Direction movingDirection, double oscillationDuration)
        {
            this.movingDirection = movingDirection;
            this.oscillationDuration = oscillationDuration;
        }
        public void Enter(IEnemy aquamentus)
        {
            aquamentus.SetAnimation("Walk");
        }

        public void Update(IEnemy aquamentus, GameTime gameTime)
        {
            oscillationDuration -= gameTime.ElapsedGameTime.TotalSeconds;
            if (oscillationDuration <= 0)
            {
                movingDirection = (movingDirection == Direction.Left) ? Direction.Right : Direction.Left;
                //oscillationDuration = 1.0;
                oscillationDuration = random.NextDouble();
            }

            aquamentus.Move(movingDirection == Direction.Left ? -2 : 2, 0);

        }
        public Direction GetDirection()
        {
            return movingDirection;
        }

        public double GetMovementDuration()
        {
            return oscillationDuration;
        }
    }
}
