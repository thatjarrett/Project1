using System.Diagnostics;
using Microsoft.Xna.Framework;
using Project1.Interfaces;
namespace Project1.Entities
{
    public class LinkMoveDownState : ILinkState
    {
        private Direction _facingDirection = Direction.Down;
        public void Enter(Link link)
        {
            Debug.WriteLine("move down state");
            link.SetAnimation("MoveDown");
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
            link.ChangeState(new LinkMoveUpState());
        }
        public void MoveDown(Link link)
        {

        }
        public void Attack(Link link)
        {
            link.ChangeState(new LinkAttackState(_facingDirection));
        }
        public void Damage(Link link)
        {
            link.ChangeState(new LinkDamageState(_facingDirection));
        }
        public void Item(Link link, int itemNumber)
        {
            link.ChangeState(new LinkUseItemState(_facingDirection));
        }
        public void Update(Link link, GameTime gameTime)
        {
            link.Move(0, 2); // Moves Link down
        }
    }
}
