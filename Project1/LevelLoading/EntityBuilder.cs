using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Net.Sockets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.GameObjects.Environment;
using Project1.Handlers;
using Project1.Sprites;
using Project1.Interfaces;
using Project1.Entities;
using Project1.GameObjects.Items;
using Project1.GameObjects.Animations;
using System.Reflection.Metadata;
using System.Runtime.ConstrainedExecution;


namespace Project1.LevelLoading
{
    public class EntityBuilder
    {

        Texture2D aquamentusTexture;
        Texture2D enemyTexture;
        Texture2D itemTexture;
        Texture2D deathAnim;

        public EntityBuilder(Texture2D aquamentusTexture, Texture2D enemytexture, Texture2D itemTexture, Texture2D deathAnim)
        {
            this.enemyTexture = enemytexture;
            this.aquamentusTexture = aquamentusTexture;
            this.itemTexture = itemTexture;
            this.deathAnim = deathAnim;
        }

        public IAnimation buildAnimation(int idNum, Vector2 location)
        {
            IAnimation animation;
            switch (idNum) {
                case 1: 
                    {
                        animation = new EnemyDeathCloud(deathAnim, location);
                        break;
                    }

                default:
                    {
                        animation = new EnemyDeathCloud(deathAnim, location);
                        break;
                    }

            }

            return animation;

        }

        public IEnemy buildEnemy(int IdNum, Vector2 Location)
        {
            IEnemy enemy;
            switch (IdNum)
            {
                case 1:
                {
                    enemy = new Aquamentus(Location); 
                    enemy.createEnemySprites(aquamentusTexture);
                    break;
                }
                case 2:
                {
                    enemy = new SpikeTrap(Location);
                    enemy.createEnemySprites(enemyTexture);
                    break;
                }
                case 3:
                {
                    enemy = new Bat(Location);
                    enemy.createEnemySprites(enemyTexture);
                    break;
                }
                case 4:
                {
                    enemy = new Slime(Location);
                    enemy.createEnemySprites(enemyTexture); 
                    break;
                }
                case 5:
                {
                    enemy = new Skeleton(Location);
                    enemy.createEnemySprites(enemyTexture);
                    break;
                }
                case 6:
                {
                    enemy = new Goriya(Location);
                    enemy.createEnemySprites(enemyTexture); 
                    break;
                }
                case 7:
                {
                    enemy = new Hand(Location);
                    enemy.createEnemySprites(enemyTexture);
                    break;
                }
                case 21: //8-20 are for items down below if this were 8 it would also spawn boomerang
                {
                        enemy = new KeySkeleton(Location);
                        enemy.createEnemySprites(enemyTexture);
                        break;    
                }
                case 22:
                    {
                        enemy = new Triceratops(Location);
                        enemy.createEnemySprites(aquamentusTexture);
                        break;
                    }
                case 23:
                    {
                        enemy = new LargeSlime(Location);
                        enemy.createEnemySprites(enemyTexture);
                        break;
                    }
                default:
                {
                    enemy = new Hand(Location);
                    enemy.createEnemySprites(enemyTexture);
                    break;
                }
            }
            return enemy;
        }
        private static int xCenterItemOffset = 16;
        private static int yCenterItemOffset = 13;
        private static int xCenterItemOffsetGeneral = 15;
        private static int yCenterItemOffsetGeneral = 7;
        private static int xCenterItemOffsetSkinny = 11;
        private static int yCenterItemOffsetSkinny = 10;
        private static int xCenterItemOffsetElse = 10;
        private static int yCenterItemOffsetElse = 7;



        public IItem buildItem(int IdNum, Vector2 Location)
        {
            IItem item;
            Vector2 offset = new Vector2(0, 0);

            if (IdNum == 11 || IdNum == 12 || IdNum == 14 || IdNum == 16 || IdNum == 17 || IdNum == 18 || IdNum == 19)
            {
                offset.X = xCenterItemOffsetGeneral;
                offset.Y = yCenterItemOffsetGeneral;
            }
            else if (IdNum == 8 || IdNum == 15)
            {
                offset.X = xCenterItemOffset;
                offset.Y = yCenterItemOffset;
            }
            else if (IdNum == 10 || IdNum == 13)
            {
                offset.X = xCenterItemOffsetSkinny;
                offset.Y = yCenterItemOffsetSkinny;
            } 
            else
            {
                offset.X = xCenterItemOffsetElse;
                offset.Y = yCenterItemOffsetElse;
            }

            Location += offset;

            switch (IdNum)
            {
                case 8:
                    {
                        item = new Boomerang(itemTexture, Location);
                        item.SetPosition(Location);
                        break;
                    }
                case 9:
                    {
                        item = new HeartContainer(itemTexture, Location);
                        item.SetPosition(Location);
                        break;
                    }
                case 10:
                    {
                        item = new Compass(itemTexture, Location);
                        item.SetPosition(Location);
                        break;
                    }
                case 11:
                    {
                        item = new Map(itemTexture, Location);
                        item.SetPosition(Location);
                        break;
                    }
                case 12:
                    {
                        item = new Key(itemTexture, Location);
                        item.SetPosition(Location);
                        break;
                    }
                case 13:
                    {
                        item = new TriForcePiece(itemTexture, Location);
                        item.SetPosition(Location);
                        break;
                    }
                case 14:
                    {
                        item = new Bow(itemTexture, Location);
                        item.SetPosition(Location);
                        break;
                    }
                case 15:
                    {
                        item = new Heart(itemTexture, Location);
                        item.SetPosition(Location);
                        break;
                    }
                case 16:
                    {
                        item = new Rupee(itemTexture, Location);
                        item.SetPosition(Location);
                        break;
                    }
                case 17:
                    {
                        item = new Arrow(itemTexture, Location);
                        item.SetPosition(Location);
                        break;
                    }
                case 18:
                    {
                        item = new Bomb(itemTexture, Location);
                        item.SetPosition(Location);
                        break;
                    }
                case 19:
                    {
                        item = new Fairy(itemTexture, Location);
                        item.SetPosition(Location);
                        break;
                    }
                case 20:
                    {
                        item = new Clock(itemTexture, Location);
                        item.SetPosition(Location);
                        break;
                    }
                default:
                    {
                        item = new Rupee(itemTexture, Location);
                        item.SetPosition(Location);
                        break;
                    }
            }
            return item;
        }


    }
}
