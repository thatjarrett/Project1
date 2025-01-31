using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Project1
{
    public class LinkMoveRightState : ILinkState
    {
        public void Enter(Link link)
        {
            link.SetAnimation("MoveRight");
        }

        public void HandleInput(Link link, KeyboardState keyboardState)
        {
            if (!keyboardState.IsKeyDown(Keys.D) && !keyboardState.IsKeyDown(Keys.Right))
                link.ChangeState(new LinkIdleState());
        }

        public void Update(Link link, GameTime gameTime)
        {
            link.Move(2, 0); // Move right
        }
    }
}
