using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


using Project1.Interfaces;
namespace Project1.Entities
{
    public class LinkMoveUpState : ILinkState
    {
        private Direction _facingDirection = Direction.Up;
        public void Enter(Link link)
        {
            link.SetAnimation("MoveUp");
        }

        public void MoveLeft(Link link)
        {
            link.ChangeState(new LinkMoveLeftState());
        }
        public void MoveRight(Link link)
        {
            link.ChangeState(new LinkMoveRightState());
        }
        public void MoveUp(Link link)
        {
            
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
            link.Move(0, -2); // Moves Link up
        }
    }
}
