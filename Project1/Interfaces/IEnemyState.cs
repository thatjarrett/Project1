using Microsoft.Xna.Framework;
using Project1.Entities;

namespace Project1.Interfaces
{
    public interface IEnemyState
    {
        void Enter(IEnemy enemy);
        void Update(IEnemy enemy, GameTime gameTime);
        Direction GetDirection();
        double GetMovementDuration();
    }
}
