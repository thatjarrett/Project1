using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Project1
{
    public class LinkMoveUpState : ILinkState
    {
        public void Enter(Link link)
        {
            link.SetAnimation("MoveUp");
        }

        public void HandleInput(Link link, KeyboardState keyboardState)
        {
            if (!keyboardState.IsKeyDown(Keys.W) && !keyboardState.IsKeyDown(Keys.Up))
                link.ChangeState(new LinkIdleState());
        }

        public void Update(Link link, GameTime gameTime)
        {
            link.Move(0, -2); // Moves Link up
        }
    }
}
