using Microsoft.Xna.Framework;
using Project1.Interfaces;

namespace Project1.Entities
{
    public class LinkDeathState : ILinkState
    {
        private const double DeathDuration = 2.0; 
        private double elapsedTime;          
        private bool gameOverTriggered;         

        public void Enter(Link link)
        {
            link.SetAnimation("Death");    
            link.SetInvincible(true);     
            link.DisableControls();       
            elapsedTime = 0;               
            gameOverTriggered = false;   
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

            if (elapsedTime >= DeathDuration && !gameOverTriggered)
            {
                link.Hide();               
                link.TriggerGameOver();   
                gameOverTriggered = true;
            }
        }
    }
}
