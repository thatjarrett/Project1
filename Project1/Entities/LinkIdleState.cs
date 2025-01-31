using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Project1.Entities
{
    public class LinkIdleState : ILinkState
    {
        private Direction _facingDirection;

        public LinkIdleState(Direction facingDirection)
        {
            _facingDirection = facingDirection;
        }

        public void Enter(Link link)
        {
            link.SetAnimation(GetIdleAnimationName());
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
        public void Update(Link link, GameTime gameTime) { }

        private string GetIdleAnimationName()
        {
            return _facingDirection switch
            {
                Direction.Up => "Idle_Up",
                Direction.Down => "Idle_Down",
                Direction.Left => "Idle_Left",
                Direction.Right => "Idle_Right",
                _ => "Idle_Down" // Default idle facing down
            };
        }
    }
}
