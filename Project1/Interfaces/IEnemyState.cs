using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Project1.Entities;

namespace Project1.Interfaces
{
    public interface IEnemyState
    {
        void Enter(IEnemy enemy);
        void MoveLeft(IEnemy enemy);
        void MoveRight(IEnemy enemy);
        void MoveUp(IEnemy enemy);
        void MoveDown(IEnemy enemy);
        void Attack(IEnemy enemy);
        void Damage(IEnemy enemy);
        void Update(IEnemy enemy, GameTime gameTime);
        Direction GetDirection();
        double GetMovementDuration();
    }
}
