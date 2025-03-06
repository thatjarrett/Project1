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


namespace Project1.LevelLoading
{
    public class Level
    {

        int[,] levelTiles = new int[7, 12];
        int[,] levelEntities = new int[7, 12];
        TileBuilder tileBuilder;

        public Level(string tileFile, string entityFile)
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

        }
        public void loadTileSprites(Texture2D environmentTexture, Texture2D npcTexture)
        {
            tileBuilder = new TileBuilder(environmentTexture, npcTexture);
        }

        public List<environmentTile> buildTiles()
        {
            List<environmentTile> tileList = new List<environmentTile>();
            int x = 16 * 3;
            int y = 16 * 3;
            for (int i = 0; i < 7; i++)
            {
                for(int j = 0; j < 12; j++)
                {
                    int tileNum = levelTiles[i, j];    
                    int destinationx =(3*16) + (x * j);
                    int destinationy = (3*16) + (y * i);
                    tileList.Add(tileBuilder.buildTile(tileNum,new Vector2(destinationx, destinationy)));
                      
                }
            }
            return tileList;
        }
    }
}



/*        public void drawBG(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Rectangle(16 * 3, 16 * 3, 16 * 12 * 3, 16 * 7 * 3), new Rectangle(1, 24 * 8, 16 * 12, 16 * 7), Color.White);
        }
*/
