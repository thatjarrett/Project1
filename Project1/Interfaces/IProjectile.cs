using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Collision;
using Project1.Projectiles;

namespace Project1.Interfaces
{
    public interface IProjectile
    {
        public void Draw(SpriteBatch spriteBatch);
        public void Destroy();
        public CollisionBox GetCollider();
        public void Update(GameTime gameTime);

        public void Throw(Vector2 position, Vector2 direction);
        public void ownerPosition(Vector2 owner);

    }
}

