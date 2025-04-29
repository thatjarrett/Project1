using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Collision;
using Project1.Projectiles;

namespace Project1.Interfaces
{
    public interface IEnemy
    {
        public void ChangeState(IEnemyState newState);

        public void SetInvincible(bool value);

        public void Update(GameTime gameTime, bool frozen);

        public void Move(int dx, int dy);

        public void Draw(SpriteBatch spriteBatch);

        public void createEnemySprites(Texture2D linkTexture);

        public void SetAnimation(string action);

        public void CollisionUpdate(CollisionBox other);
        
        public CollisionBox GetCollider();

        public IProjectile[] GetProjectiles();

        public void takeDamage(); //TODO: set to variable ammount?

        public void die();

        public int getHealth();

        public bool Alive();

        public int getLoot();
        public Vector2 getPos();
    }
}

