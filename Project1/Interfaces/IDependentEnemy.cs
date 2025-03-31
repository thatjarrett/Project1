using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Collision;
using Project1.Entities;
using Project1.GameObjects.Environment;
using Project1.Projectiles;

namespace Project1.Interfaces
{
    public interface IDependentEnemy : IEnemy
    {
        public void Update(GameTime gameTime, Link link, bool frozen);
    }
}

