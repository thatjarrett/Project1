using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Project1.Interfaces;

namespace Project1.Entities
{
    public class AquamentusWalkState : IEnemyState
    {
        private Direction _facingDirection = Direction.Left;
        public void Enter(IEnemy aquamentus)
        {
            aquamentus.SetAnimation("MoveLeft");
        }

        public void MoveLeft(IEnemy aquamentus)
        {
           
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
            //aquamentus.ChangeState(new LinkAttackState(_facingDirection));
        }
        public void Damage(IEnemy aquamentus)
        {
            //aquamentus.ChangeState(new LinkDamageState(_facingDirection));
        }
        public void Update(IEnemy aquamentus, GameTime gameTime)
        {
            aquamentus.Move(-2, 0); // Move left
        }
    }
}
