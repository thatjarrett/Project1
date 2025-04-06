using Project1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.LevelLoading
{
    class LootTables
    {
        private Random random = new Random();

        public int getLootA() {
            int x = random.Next(0, 10);
            int lootID = 0;
            if (0 <= x && x <= 6)
            {
                lootID = 15; //heart
            }
            else if (7 <= x && x <= 8)
            {
                lootID = 16; //coin
            }
            else
            {
                lootID = 19; //fairy
            }

            return lootID;
        }

        public int getLootB() {
            int x = random.Next(0, 5);
            int lootID;
            if (x != 3)
            {
                lootID = 17; //arrow
            }
            else
            {
                lootID = 16; //rupee
            }

            return lootID;
        }

        public int getLootC() {
            int x = random.Next(0, 10);
            int lootID = 0;
            if (x == 0)
            {
                lootID = 20; //clock
            }
            else if (x >= 1 && x <= 2)
            {
                lootID = 18; //bomb
            }
            else if (x >= 3 && x <= 5)
            {
                lootID = 15; //heart
            }
            else
            {
                lootID = 16; //coin
            }

            return lootID;
        }

        public int getLootD() {
            int x = random.Next(0, 10);
            int lootID = 0;
            if (x <= 5)
            {
                lootID = 16; //coin. note, sometimes coin is blue??
            }
            else if (6 <= x && x <= 7)
            {
                lootID = 15; //heart
            }
            else if (x == 8)
            {
                lootID = 20; //clock
            }
            else
            {
                x = 17; //arrow. should be blue gem but whatever we dont have that implemented
            }

            return lootID;
        }

        public int getKey() {
            int lootID = 12; //key
            return lootID;
        }

        public int getTriforce() {
            int lootID = 13; //triforce
            return lootID;
        }
    }
}
