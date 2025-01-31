using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Project1
{
    public class LinkMoveDownState : ILinkState
    {
        public void Enter(Link link)
        {
            link.SetAnimation("MoveDown");
        }

        public void HandleInput(Link link, KeyboardState keyboardState)
        {
            if (!keyboardState.IsKeyDown(Keys.S) && !keyboardState.IsKeyDown(Keys.Down))
                link.ChangeState(new LinkIdleState());
        }

        public void Update(Link link, GameTime gameTime)
        {
            link.Move(0, 2); // Moves Link down
        }
    }
}
