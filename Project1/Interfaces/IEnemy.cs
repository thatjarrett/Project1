using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project1.Interfaces
{
    public interface IEnemy
    {
        public void ChangeState(IEnemyState newState);

        public void SetInvincible(bool value);

        public void MoveLeft();

        public void MoveRight();

        public void MoveUp();

        public void MoveDown();

        public void Update(GameTime gameTime);

        public void Move(int dx, int dy);

        public void PerformAttack();

        public void Draw(SpriteBatch spriteBatch);

        public void createEnemySprites(Texture2D linkTexture);

        public void SetAnimation(string action);
    }
}

