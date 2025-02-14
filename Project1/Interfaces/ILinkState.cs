using Microsoft.Xna.Framework;
using Project1.Entities;

namespace Project1.Interfaces
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
        void Item(Link link, int itemNumber);
        void Update(Link link, GameTime gameTime);
    }
}
