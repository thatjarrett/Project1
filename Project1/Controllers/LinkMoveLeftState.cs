using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Project1
{
    public class LinkMoveLeftState : ILinkState
    {
        public void Enter(Link link)
        {
            link.SetAnimation("MoveLeft");
        }

        public void HandleInput(Link link, KeyboardState keyboardState)
        {
            if (!keyboardState.IsKeyDown(Keys.A) && !keyboardState.IsKeyDown(Keys.Left))
                link.ChangeState(new LinkIdleState());
        }

        public void Update(Link link, GameTime gameTime)
        {
            link.Move(-2, 0); // Move right
        }
    }
}
