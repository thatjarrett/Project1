using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Project1
{
    public interface ILinkState
    {
        void Enter(Link link);
        void HandleInput(Link link, KeyboardState keyboardState);
        void Update(Link link, GameTime gameTime);
    }
}
