using Microsoft.Xna.Framework;
using Project1.Interfaces;
using Project1.Projectiles;

namespace Project1.Entities
{
    public partial class Link
    {
        public void Item(int itemNumber)
        {
            if (isControlsDisabled) return;
            currentState.Item(this, itemNumber);

            Projectile projectile = null;
            IProjectile bomb = null;

            switch (itemNumber)
            {
                case 1:
                    if (health >= maxHealth)
                        projectile = new StraightProjectile(position, faceDirection, swordBeamHorizontal, swordBeamVertical, 5);
                    break;
                case 2:
                    if (hasBoomerang)
                        boomerangProjectile.Throw(position, faceDirection);
                    break;
                case 3:
                    if (bombCount > 0)
                    {
                        bomb = new BombProjectile(position, bombSprite, explodingBombSprite, this);
                        bombCount--;
                    }
                    break;
                case 4:
                    if (hasBow && arrowCount > 0)
                    {
                        projectile = new StraightProjectile(position, faceDirection, arrowHorizontal, arrowVertical, 5);
                        arrowCount--;
                    }
                    break;
            }

            if (projectile != null)
                projectiles.Add(projectile);

            if (bomb != null)
                bombs.Add(bomb);
        }
    }
}