using System.Collections.Generic;
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


namespace Project1.Controllers
{
    public class Level
    {
        int[,] level = new int[7,12];
        Texture2D background;

        public Level(string filename, Texture2D background)
        {
            string[] lines = File.ReadAllLines(filename);
            int i = 0;
            foreach (string line in lines)
            {
                int j = 0;
                string[] splitLine = line.Split(',');
                foreach (string number in splitLine)
                {
                    int x = Int32.Parse(number);
                    level[i, j] = x; 
                    j++;
                }
                i++;
            }
            this.background = background;

        }
        public void drawBG(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Rectangle (16*3,16*3,16*12*3, 16 * 7 * 3), new Rectangle (1, 24 * 8, 16*12, 16 * 7), Color.White);
        }
    }
}
