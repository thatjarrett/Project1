using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Project1.Interfaces;

namespace Project1.Entities
{
    public class AquamentusWalkState : IEnemyState
    {
        private Direction movingDirection = Direction.Left;
        private double oscillationDuration;
        Random random = new Random();

        public AquamentusWalkState()
        {
            this.oscillationDuration = 1.0;
        }
        public void Enter(IEnemy aquamentus)
        {
            aquamentus.SetAnimation("Walk");
        }

        public void MoveLeft(IEnemy aquamentus)
        {
           //N/A
        }
        public void MoveRight(IEnemy aquamentus)
        {
            //N/A
        }
        public void MoveUp(IEnemy aquamentus)
        {
            //N/A
        }
        public void MoveDown(IEnemy aquamentus)
        {
            //N/A
        }
        public void Attack(IEnemy aquamentus)
        {
            //aquamentus.ChangeState(new aquamentusAttackState());
        }
        public void Damage(IEnemy aquamentus)
        {
            //aquamentus.ChangeState(new aquamentusDamageState());
        }
        public void Update(IEnemy aquamentus, GameTime gameTime)
        {
            oscillationDuration -= gameTime.ElapsedGameTime.TotalSeconds;
            if (oscillationDuration <= 0)
            {
                if(movingDirection == Direction.Left)
                {
                    movingDirection = Direction.Right;
                } else
                {
                    movingDirection = Direction.Left;
                }
                oscillationDuration = random.NextDouble();
            }
            if (movingDirection == Direction.Left)
            {
                aquamentus.Move(-2, 0);
            } else
            {
                aquamentus.Move(2, 0);
            }
            
        }
    }
}
