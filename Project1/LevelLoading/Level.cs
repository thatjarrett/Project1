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
            int x = 16 * 3;
            int y = 16 * 3;
            for (int i = 0; i < 7; i++)
            {
                for(int j = 0; j < 12; j++)
                {
                    int tileNum = levelTiles[i, j];    
                    int destinationx = roomOffsetX+(3*32) + (x * j);
                    int destinationy = roomOffsetY +(3*32)+120 + (y * i);
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
            tileList.Add(tileBuilder.buildTile(levelTiles[7, 1], new Vector2(roomOffsetX + 0, roomOffsetY + (120 +(32+(16*7))*3))));
            tileList.Add(tileBuilder.buildTile(levelTiles[7, 2], new Vector2(roomOffsetX + 0, roomOffsetY + (120 +32*3))));
            tileList.Add(tileBuilder.buildTile(levelTiles[7, 3], new Vector2(roomOffsetX + (3 * 32) + (12 * 48), roomOffsetY + (3 * 32) + 120)));

            //Door slots
            tileList.Add(tileBuilder.buildTile(levelTiles[7, 4], new Vector2(roomOffsetX + (112 *3), roomOffsetY + 120)));     //top
            tileList.Add(tileBuilder.buildTile(levelTiles[7, 5], new Vector2(roomOffsetX + (112 *3), roomOffsetY + (120 +(32 + (16 * 7)) * 3))));       //bottom
            tileList.Add(tileBuilder.buildTile(levelTiles[7, 6], new Vector2(roomOffsetX + 0, roomOffsetY + (120 +(72)*3))));       //left
            tileList.Add(tileBuilder.buildTile(levelTiles[7, 7], new Vector2(roomOffsetX + ((32 + (16 * 12)) * 3), roomOffsetY + (120 +(72) * 3))));        //right
            return tileList;
        }
        public (List<IItem>, List<IEnemy>) buildEntities(EntityBuilder EntityBuilder)
        {
            List<IItem> itemList = new List<IItem>();
            List<IEnemy> enemyList = new List<IEnemy>();
            int x = 16 * 3;
            int y = 16 * 3;
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    int entityNum = levelEntities[i, j];
                    int destinationx = roomOffsetX + (3 * 32) + (x * j);
                    int destinationy = roomOffsetY + 120 + (3 * 32) + (y * i);
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



/*        public void drawBG(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Rectangle(16 * 3, 16 * 3, 16 * 12 * 3, 16 * 7 * 3), new Rectangle(1, 24 * 8, 16 * 12, 16 * 7), Color.White);
        }
public void drawBG(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.TileTex, new Rectangle(0, 0, 16 * 16 * 3, 16 * 10 * 3), new Rectangle(1, 1, 256, 160), Color.White);
            
        }

*/
