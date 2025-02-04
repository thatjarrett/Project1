using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Project1
{
    public class LinkIdleState : ILinkState
    {
        public void Enter(Link link)
        {
            link.SetAnimation("Idle");
        }

        public void HandleInput(Link link, KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up))
                link.ChangeState(new LinkMoveUpState());
            else if (keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Down))
                link.ChangeState(new LinkMoveDownState());
            else if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left))
                link.ChangeState(new LinkMoveLeftState());
            else if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
                link.ChangeState(new LinkMoveRightState());
            else if (keyboardState.IsKeyDown(Keys.Z))
                link.ChangeState(new LinkAttackState());
            else if (keyboardState.IsKeyDown(Keys.E))  
                link.ChangeState(new LinkDamageState());
        }

        public void Update(Link link, GameTime gameTime) { }
    }
}
