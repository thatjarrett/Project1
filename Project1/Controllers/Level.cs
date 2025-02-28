using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Project1.Interfaces;
using System.IO;
using System;



namespace Project1.Controllers
{
    public class Level
    {
        int[,] level = new int[7,12];
        public Level(string filename)
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
            system.out.print(level);

        }
    }
}
