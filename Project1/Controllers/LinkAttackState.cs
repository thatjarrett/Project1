using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Project1
{
    public class LinkAttackState : ILinkState
    {
        public void Enter(Link link)
        {
            link.SetAnimation("Attack");
            link.PerformAttack();
        }

        public void HandleInput(Link link, KeyboardState keyboardState) { }

        public void Update(Link link, GameTime gameTime)
        {
            if (link.AnimationFinished())
                link.ChangeState(new LinkIdleState());
        }
    }
}
