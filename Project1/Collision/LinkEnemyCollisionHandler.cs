using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Project1.Collision;
using Project1.Entities;
using Project1.Interfaces;
using Project1.Projectiles;

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
                    Debug.WriteLine("Link collided with an enemy! Taking damage and knockback.");

                    // Get knockback direction (opposite of enemy)
                    Vector2 knockbackDir = link.GetCollider().GetCenter() - enemy.GetCollider().GetCenter();
                    knockbackDir.Normalize(); // Make it unit length

                    // Change Link's state to damage
                    link.ChangeState(new LinkDamageState(link.PreviousDirection));
                    link.takeDamage();

                    // Set Link to invincible and apply knockback
                    link.SetInvincible(true, knockbackDir);
                }
            }
        }

        public static void HandleCollision(Link link, IProjectile projectile)
        {
            if (projectile.GetCollider() != null && link.GetCollider().Intersects(projectile.GetCollider()) &&projectile != null)
            {
                if (!link.IsInvincible())
                {
                    Debug.WriteLine("Link collided with a projectile! Taking damage and knockback.");

                    // Get knockback direction (opposite of enemy)
                    Vector2 knockbackDir = link.GetCollider().GetCenter() - projectile.GetCollider().GetCenter();
                    knockbackDir.Normalize(); // Make it unit length

                    // Change Link's state to damage
                    link.ChangeState(new LinkDamageState(link.PreviousDirection));
                    link.takeDamage();

                    // Set Link to invincible and apply knockback
                    link.SetInvincible(true, knockbackDir);
                }
            }
        }

        public static void HandleCollision(IProjectile projectile, IEnemy enemy)
        {
            if (enemy.GetCollider() != null && projectile.GetCollider() != null && projectile.GetCollider().Intersects(enemy.GetCollider()))
            {
                
                Debug.WriteLine("Link shot an enemy! Dealing damage.");


                    // Change enemy's state to damage
                enemy.SetAnimation("Damage");
                enemy.takeDamage();
                if (enemy.getHealth() == 0) {
                    enemy.die();
                }

                    // Set enemy to invincible and apply knockback
                enemy.SetInvincible(true);
            }
        }

        public static void HandleCollision(IItem item, Link Link)
        {
            if (item.GetCollider() != null && Link.GetCollider().Intersects(item.GetCollider()))
            {
                Debug.WriteLine("⚠️ Link picked up an item!");
                Link.pickup(item);
                item.pickup();
                //delete item
            }
        }

        public static void HandleCollision(CollisionBox sword, IEnemy enemy)
        {
            if (enemy.GetCollider() != null && sword != null && sword.Intersects(enemy.GetCollider()))
            {
                Debug.WriteLine("Link stabbed an enemy! Dealing damage.");


                // Change enemy's state to damage
                enemy.SetAnimation("Damage");
                enemy.takeDamage();
                if (enemy.getHealth() == 0)
                {
                    enemy.die();
                }

                // Set enemy to invincible and apply knockback
                enemy.SetInvincible(true);
            }
        }
    }
}
