using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Project1.Entities;

namespace Project1
{
    public interface ILinkState
    {
        void Enter(Link link);
        void MoveLeft(Link link);
        void MoveRight(Link link);
        void MoveUp(Link link);
        void MoveDown(Link link);
        void Attack(Link link);
        void Damage(Link link);
        void Update(Link link, GameTime gameTime);
    }
}
