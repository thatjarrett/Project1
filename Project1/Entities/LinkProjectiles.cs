using Microsoft.Xna.Framework;
using Project1.Interfaces;
using Project1.Projectiles;
using Project1.GameObjects.Environment;
using System.Collections.Generic;

namespace Project1.Entities
{
    public partial class Link
    {
        private List<environmentTile> tileList; // This should be assigned when the level loads

        public void SetTileList(List<environmentTile> tiles)
        {
            tileList = tiles;
        }

        public void Item(int itemNumber)
        {
            if (isControlsDisabled) return;

            currentState.Item(this, itemNumber);

            Projectile projectile = null;
            IProjectile bomb = null;

            switch (itemNumber)
            {
                case 1: // Sword Beam
                    if (health >= maxHealth)
                    {
                        projectile = new StraightProjectile(position, faceDirection, swordBeamHorizontal, swordBeamVertical, 5);
                    }
                    break;

                case 2: // Boomerang
                    if (hasBoomerang)
                    {
                        boomerangProjectile.Throw(position, faceDirection);
                    }
                    break;

                case 3: // Bomb
                    if (bombCount > 0)
                    {
                        bomb = new BombProjectile(position, bombSprite, explodingBombSprite, this, tileList);
                        bombCount--;
                    }
                    break;

                case 4: // Arrow
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
