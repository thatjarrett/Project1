using Microsoft.Xna.Framework;
using Project1.Interfaces;

namespace Project1.Entities
{
    public class LinkWinState : ILinkState
    {
        private const double DeathDuration = 2.0;
        private double elapsedTime;
        private bool winTriggered;

        public void Enter(Link link)
        {
            link.SetAnimation("Death");
            link.SetInvincible(true);
            link.DisableControls();
            elapsedTime = 0;
            winTriggered = false;

            // Play death sound immediately upon entering the state
            GameManager.Instance.SetWin();
        }

        public void MoveLeft(Link link) { }
        public void MoveRight(Link link) { }
        public void MoveUp(Link link) { }
        public void MoveDown(Link link) { }
        public void Attack(Link link) { }
        public void Damage(Link link) { }
        public void Item(Link link, int itemNumber) { }

        public void Update(Link link, GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;

            if (elapsedTime >= DeathDuration && !winTriggered)
            {
                link.Hide();
                winTriggered = true;
            }
        }
    }
}
