using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Project1.Interfaces;

namespace Project1.Entities
{
    public class LinkMoveLeftState : ILinkState
    {
        private Direction _facingDirection = Direction.Left;
        public void Enter(Link link)
        {
            link.SetAnimation("MoveLeft");
        }

        public void MoveLeft(Link link)
        {
           
        }
        public void MoveRight(Link link)
        {
            link.ChangeState(new LinkMoveRightState());
        }
        public void MoveUp(Link link)
        {
            link.ChangeState(new LinkMoveUpState());
        }
        public void MoveDown(Link link)
        {
            link.ChangeState(new LinkMoveDownState());
        }
        public void Attack(Link link)
        {
            link.ChangeState(new LinkAttackState(_facingDirection));
        }
        public void Damage(Link link)
        {
            link.ChangeState(new LinkDamageState(_facingDirection));
        }
        public void Update(Link link, GameTime gameTime)
        {
            link.Move(-2, 0); // Move left
        }
    }
}
