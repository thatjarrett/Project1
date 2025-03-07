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


namespace Project1.LevelLoading
{
    public class EntityBuilder
    {

        Texture2D aquamentusTexture;
        Texture2D enemyTexture;
        Texture2D itemTexture;

        public EntityBuilder(Texture2D aquamentusTexture, Texture2D enemytexture, Texture2D itemTexture)
        {
            this.enemyTexture = enemytexture;
            this.aquamentusTexture = aquamentusTexture;
            this.itemTexture = itemTexture;
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
                default:
                {
                    enemy = new Hand(Location);
                    enemy.createEnemySprites(enemyTexture);
                    break;
                }
            }
            return enemy;
        }

        public IItem buildItem(int IdNum, Vector2 Location)
        {
            IItem item;
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
                case 15:
                    {
                        item = new Bow(itemTexture, Location);
                        item.SetPosition(Location);
                        break;
                    }
                case 16:
                    {
                        item = new Heart(itemTexture, Location);
                        item.SetPosition(Location);
                        break;
                    }
                case 17:
                    {
                        item = new Rupee(itemTexture, Location);
                        item.SetPosition(Location);
                        break;
                    }
                case 18:
                    {
                        item = new Arrow(itemTexture, Location);
                        item.SetPosition(Location);
                        break;
                    }
                case 19:
                    {
                        item = new Bomb(itemTexture, Location);
                        item.SetPosition(Location);
                        break;
                    }
                case 20:
                    {
                        item = new Fairy(itemTexture, Location);
                        item.SetPosition(Location);
                        break;
                    }
                case 21:
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


/* 
 *      boomerang = new Boomerang(itemTexture);
        HeartContainer = new HeartContainer(itemTexture);
        compass = new Compass(itemTexture);
        Map = new Map(itemTexture);
        Key = new Key(itemTexture);
        TriForcePiece = new TriForcePiece(itemTexture);
        Bow = new Bow(itemTexture);
        Heart = new Heart(itemTexture);
        Rupee = new Rupee(itemTexture);
        Arrow = new Arrow(itemTexture);
        Bomb = new Bomb(itemTexture);
        Fairy = new Fairy(itemTexture);
        Clock = new Clock(itemTexture);

 *        itemsList.Add(boomerang);
        itemsList.Add(HeartContainer);
        itemsList.Add(compass);
        itemsList.Add(Map);
        itemsList.Add(Key);
        itemsList.Add(TriForcePiece);
        itemsList.Add(Bow);
        itemsList.Add(Heart);
        itemsList.Add(Rupee);
        itemsList.Add(Arrow);
        itemsList.Add(Bomb);
        itemsList.Add(Fairy);
        itemsList.Add(Clock);
 * 
 * 
 * 
 * 
 * 
 * aquamentus.createEnemySprites(aquamentusTexture);
        trap.createEnemySprites(enemyTexture);


        bat.createEnemySprites(enemyTexture);
        slime.createEnemySprites(enemyTexture);
        skeleton.createEnemySprites(enemyTexture);
        goriya.createEnemySprites(enemyTexture);
        hand.createEnemySprites(enemyTexture);

 
 aquamentus = new Aquamentus(new Vector2(500, 170));
        trap = new SpikeTrap(new Vector2(500, 170));
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        


        bat = new Bat(new Vector2(500, 170));
        slime = new Slime(new Vector2(500, 170));
        skeleton = new Skeleton(new Vector2(500, 170));
        goriya = new Goriya(new Vector2(500, 170));
        hand = new Hand(new Vector2(500, 170));

 
 
        enemies.Add(aquamentus);
        enemies.Add(trap);

        enemies.Add(bat);
        enemies.Add(slime);
        enemies.Add(skeleton);
        enemies.Add(goriya);
        enemies.Add(hand);
 
 
 
 */
