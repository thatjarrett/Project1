using System.Diagnostics;
using Microsoft.Xna.Framework;
using Project1.Entities;
using Project1.Interfaces;

namespace Project1.Handlers
{
    public static class LinkEnemyCollisionHandler
    {
        public static void HandleCollision(Link link, IEnemy enemy)
        {
            if (enemy.GetCollider() != null && link.GetCollider().Intersects(enemy.GetCollider()))
            {
                if (!link.IsInvincible())
                {
                    Debug.WriteLine("⚠️ Link collided with an enemy! Taking damage and knockback.");

                    // Get knockback direction (opposite of enemy)
                    Vector2 knockbackDir = link.GetCollider().GetCenter() - enemy.GetCollider().GetCenter();
                    knockbackDir.Normalize(); // Make it unit length

                    // Change Link's state to damage
                    link.ChangeState(new LinkDamageState(link.PreviousDirection));

                    // Set Link to invincible and apply knockback
                    link.SetInvincible(true, knockbackDir);
                }
            }
        }
    }
}
