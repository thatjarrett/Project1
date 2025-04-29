using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Project1.Interfaces;
using System.IO;
using System;
using Microsoft.Xna.Framework.Graphics;
using Project1.Interfaces;
using System.Threading;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Project1.LevelLoading;
using Project1.GameObjects.Environment;
using Project1.Entities;
using Project1.LevelLoading;


namespace Project1.LevelLoading
{
    public class Level
    {

        int[,] levelTiles = new int[8, 12];
        int[,] levelEntities = new int[7, 12];
        int roomOffsetX=0;
        int roomOffsetY=0;
        private static int tileSize = 16;
        private static int ScaleMult = 3;
        private static int ThickTileSize = 32;


        public Level(string tileFile, string entityFile,int X,int Y)
        {
            string[] lines = File.ReadAllLines(tileFile);
            int i = 0;
            foreach (string line in lines)
            {
                int j = 0;
                string[] splitLine = line.Split(',');
                foreach (string number in splitLine)
                {
                     int x = Int32.Parse(number);
                    levelTiles[i, j] = x;
                    j++;
                }
                i++;
            }
            lines = File.ReadAllLines(entityFile);
            i = 0;
            foreach (string line in lines)
            {
                int j = 0;
                string[] splitLine = line.Split(',');
                foreach (string number in splitLine)
                {
                    int x = Int32.Parse(number);
                    levelEntities[i, j] = x;
                    j++;
                }
                i++;
            }

            this.roomOffsetX = X;
            this.roomOffsetY = Y;
        }  
        


        public List<environmentTile> buildTiles(TileBuilder tileBuilder)
        {
            List<environmentTile> tileList = new List<environmentTile>();
            int x = tileSize * ScaleMult;
            int y = tileSize * ScaleMult;
            for (int i = 0; i < 7; i++)
            {
                for(int j = 0; j < 12; j++)
                {
                    int tileNum = levelTiles[i, j];    
                    int destinationx = roomOffsetX+(ScaleMult * ThickTileSize) + (x * j);
                    int destinationy = roomOffsetY +(ScaleMult * ThickTileSize) +120 + (y * i);
                    int index = tileList.Count;
                    tileList.Add(tileBuilder.buildTile(tileNum, new Vector2(destinationx, destinationy)));
                    if(tileNum == 35)
                    {
                        tileList.Add(tileBuilder.buildTile(31, new Vector2(destinationx, destinationy)));
                    }


                }
            }
            //Big walls
            tileList.Add(tileBuilder.buildTile(levelTiles[7, 0], new Vector2(roomOffsetX+0, roomOffsetY + 120)));
            tileList.Add(tileBuilder.buildTile(levelTiles[7, 1], new Vector2(roomOffsetX + 0, roomOffsetY + (120 +(ThickTileSize + (tileSize*7))* ScaleMult))));
            tileList.Add(tileBuilder.buildTile(levelTiles[7, 2], new Vector2(roomOffsetX + 0, roomOffsetY + (120 + ThickTileSize * ScaleMult))));
            tileList.Add(tileBuilder.buildTile(levelTiles[7, 3], new Vector2(roomOffsetX + (ScaleMult * ThickTileSize) + (12 * 48), roomOffsetY + (ScaleMult * ThickTileSize) + 120)));

            //Door slots
            tileList.Add(tileBuilder.buildTile(levelTiles[7, 4], new Vector2(roomOffsetX + (112 * ScaleMult), roomOffsetY + 120)));     //top
            tileList.Add(tileBuilder.buildTile(levelTiles[7, 5], new Vector2(roomOffsetX + (112 * ScaleMult), roomOffsetY + (120 +(ThickTileSize + (tileSize * 7)) * ScaleMult))));       //bottom
            tileList.Add(tileBuilder.buildTile(levelTiles[7, 6], new Vector2(roomOffsetX + 0, roomOffsetY + (120 +(72)* ScaleMult))));       //left
            tileList.Add(tileBuilder.buildTile(levelTiles[7, 7], new Vector2(roomOffsetX + ((ThickTileSize + (tileSize * 12)) * ScaleMult), roomOffsetY + (120 +(72) * ScaleMult))));        //right
            return tileList;
        }
        public (List<IItem>, List<IEnemy>) buildEntities(EntityBuilder EntityBuilder)
        {
            List<IItem> itemList = new List<IItem>();
            List<IEnemy> enemyList = new List<IEnemy>();
            int x = tileSize * ScaleMult;
            int y = tileSize * ScaleMult;
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    int entityNum = levelEntities[i, j];
                    int destinationx = roomOffsetX + (ScaleMult * ThickTileSize) + (x * j);
                    int destinationy = roomOffsetY + 120 + (ScaleMult * ThickTileSize) + (y * i);
                    if (entityNum >=1 && entityNum <= 7 || entityNum > 20)
                    {
                        enemyList.Add(EntityBuilder.buildEnemy(entityNum, new Vector2(destinationx, destinationy)));
                    }else if (entityNum >=8 && entityNum <= 20)
                    {
                        itemList.Add(EntityBuilder.buildItem(entityNum, new Vector2(destinationx, destinationy)));
                    }


                }
            }
            return (itemList,enemyList);
        }

        public void setRoomOffset(int x, int y)
        {
            this.roomOffsetX = x;
            this.roomOffsetY = y;
        }

    }
}



