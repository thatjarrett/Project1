using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Project1.Collision;
using Project1.GameObjects.Environment;
using Project1.Handlers;
using Project1.Interfaces;

public partial class Game1 : Game
{
    private void UpdateCollisions(GameTime gameTime)
    {

        foreach (var tile in tiles)
        {
            if (tile is wallTile wall)
            {
                List<CollisionBox> colliders = wall.GetColliderList();
                if (colliders != null)
                {
                    foreach (var collider in colliders)
                    {
                        link.CollisionUpdate(collider);
                        foreach (var enemy in enemies)
                            enemy.CollisionUpdate(collider);

                        if (portalManager?.GetBlueCollider()?.Intersects(collider) == true)
                        {
                            portalManager.StopBluePortal();
                        }

                        if (portalManager?.GetOrangeCollider()?.Intersects(collider) == true)
                        {
                            portalManager.StopOrangePortal();
                        }
                    }
                }


            }
            else if (tile is stairsTile stairs)
            {
                if (stairs.GetCollider().Intersects(link.GetCollider()))
                {
                    //TODO: alter this later i guess...
                    link.Move(0, 300);
                }
            }
            else
            {
                CollisionBox collider = tile.GetCollider();
                if (tile is pushableBlock block)
                {
                    List<CollisionBox> blocking = new List<CollisionBox>();

                    foreach (var otherTile in tiles)
                    {
                        if (otherTile == tile) continue;

                        if (otherTile is wallTile wall2)
                        {
                            var wallBoxes = wall2.GetColliderList();
                            if (wallBoxes != null)
                            {
                                blocking.AddRange(wallBoxes);
                            }
                        }
                        else
                        {
                            CollisionBox otherCollider = otherTile.GetCollider();
                            if (otherCollider != null)
                            {
                                blocking.Add(otherCollider);
                            }
                        }
                    }
                    block.CollisionUpdate(link.GetCollider(), blocking);
                    link.CollisionUpdate(collider);
                }
                else if (collider != null)
                {
                    if (tile is LockedDoorTile keyDoor && link.GetCollidingSide(collider) != CollisionSide.None && !keyDoor.GetOpen())
                    {
                        keyDoor.TryOpen(link);
                        if (keyDoor.IsSolid)
                        {

                            link.CollisionUpdate(collider);
                        }
                    }
                    else if (tile is doorTile door2 && door2.GetOpen())
                    {
                        if (link.GetCollidingSide(collider) == CollisionSide.Bottom)
                        {
                            link.Move(0, -144);
                        }
                        else if (link.GetCollidingSide(collider) == CollisionSide.Top)
                        {
                            link.Move(0, 144);
                        }
                        else if (link.GetCollidingSide(collider) == CollisionSide.Right)
                        {
                            link.Move(-144, 0);
                        }
                        else if (link.GetCollidingSide(collider) == CollisionSide.Left)
                        {
                            link.Move(144, 0);
                        }
                    }
                    else if (!(tile is doorTile door && door.GetOpen()))
                    {
                        link.CollisionUpdate(collider);
                    }
                }
                foreach (var enemy in enemies)
                {
                    if (collider != null)
                    {
                        enemy.CollisionUpdate(collider);
                    }
                }
            }

        }

        CollisionBox blue = portalManager?.GetBlueCollider();
        CollisionBox orange = portalManager?.GetOrangeCollider();

        foreach (var tile in tiles)
        {
            var wallCollider = tile.GetCollider();
            if (wallCollider != null)
            {
                if (blue != null && blue.Intersects(wallCollider))
                {
                    portalManager.StopBluePortal();
                }
                if (orange != null && orange.Intersects(wallCollider))
                {
                    portalManager.StopOrangePortal();
                }
            }
        }

        foreach (var item in itemsList)
        {


            item.Update(gameTime);
            LinkItemCollisionHandler.HandleCollision(item, link);
        }

        foreach (var anim in animationsList)
        {
            anim.Update(gameTime);
        }

        List<IProjectile> linkBombs = link.GetBombs();
        int tileCount = tiles.Count - 1;
        foreach (BombProjectile b in linkBombs)
        {
            LinkEnemyCollisionHandler.HandleCollision(link, b);
            while (tileCount >= 0)
            {
                environmentTile t = tiles[tileCount];
                if (t is CrackedWallTile c)
                {
                    BombWallCollisionHandler.HandleCollision(b, c);
                }
                tileCount--;
            }
        }

        foreach (var enemy in enemies)
        {
            CollisionBox collider = enemy.GetCollider();
            foreach (var enemy2 in enemies)
            {
                if (enemy != enemy2)
                {
                    enemy2.CollisionUpdate(collider);
                }
            }

            if (enemy is IDependentEnemy spikeTrap)
            {
                spikeTrap.Update(gameTime, link, link.IsFrozen());
            }
            else
            {
                enemy.Update(gameTime, link.IsFrozen());
            }

            LinkEnemyCollisionHandler.HandleCollision(link, enemy);
            IProjectile[] p = enemy.GetProjectiles();
            if (p != null)
            {
                for (int x = 0; x < p.Length; x++)
                {
                    if (p[x] != null)
                    {
                        LinkEnemyCollisionHandler.HandleCollision(link, p[x]);
                    }
                }
            }
            List<IProjectile> lp = link.GetProjectiles();
            foreach (var pp in lp)
            {
                LinkEnemyCollisionHandler.HandleCollision(pp, enemy);
            }

            foreach (var b in linkBombs)
            {
                LinkEnemyCollisionHandler.HandleCollision(b, enemy);
            }

            CollisionBox sword = link.GetSword();
            LinkEnemyCollisionHandler.HandleCollision(sword, enemy);
        }
    }

}
