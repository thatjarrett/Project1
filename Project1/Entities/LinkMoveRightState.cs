using System.Diagnostics;
using Microsoft.Xna.Framework;


using Project1.Interfaces;
namespace Project1.Entities
{
    public class LinkMoveRightState : ILinkState
    {
        private Direction _facingDirection = Direction.Right;
        public void Enter(Link link)
        {
            link.SetAnimation("MoveRight");
        }

        public void MoveLeft(Link link)
        {
            link.ChangeState(new LinkMoveLeftState());
        }
        public void MoveRight(Link link)
        {
            Debug.WriteLine("move right state");
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
        public void Item(Link link, int itemNumber)
        {
            link.ChangeState(new LinkUseItemState(_facingDirection));
        }
        public void Update(Link link, GameTime gameTime)
        {
            link.Move(2, 0); // Move right
        }
    }
}
