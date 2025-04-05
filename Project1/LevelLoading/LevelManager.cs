using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Project1.Interfaces;
using System.IO;
using System;
using Microsoft.Xna.Framework.Graphics;
using System.Threading;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Project1.LevelLoading;
using Project1.GameObjects.Environment;
using Project1.Entities;

namespace Project1.LevelLoading
{
    public class levelManager
    {
        string levelZeroZero = "Content/Level Data/BlockLevel.txt";
        string levelNegOneNegOne = "Content/Level Data/NegOneNegOne.txt";
        string levelNegOneNegThree = "Content/Level Data/NegOneNegThree.txt";
        string levelNegOneTwo = "Content/Level Data/NegOneTwo.txt";
        string levelNegOneZero = "Content/Level Data/negOneZero.txt";
        string levelNegTwoZero = "Content/Level Data/negTwoZero.txt";
        string levelOneNegOne = "Content/Level Data/OneNegOne.txt";
        string levelOneNegThree = "Content/Level Data/OneNegThree.txt";
        string levelOneZero = "Content/Level Data/OneZero.txt";
        string levelThreeOne = "Content/Level Data/ThreeOne.txt";
        string levelTwoOne = "Content/Level Data/TwoOne.txt";
        string levelTwoZero = "Content/Level Data/TwoZero.txt";
        string levelZeroNegOne = "Content/Level Data/ZeroNegOne.txt";
        string levelZeroNegThree = "Content/Level Data/ZeroNegThree.txt";
        string levelZeroNegTwo = "Content/Level Data/ZeroNegTwo.txt";
        string levelZeroOne = "Content/Level Data/ZeroOne.txt";
        string levelZeroTwo = "Content/Level Data/ZeroTwo.txt";

        string entityZeroZero = "Content/Level Data/EntityLevel.txt";



        TileBuilder tileBuilder;
        EntityBuilder EntityBuilder;
        Texture2D TileTex;
        List<Level> levels = new List<Level>();
        int pageSizeX = 768;
        int pageSizeY = -528;

        public levelManager(Texture2D environmentTexture, Texture2D npcTexture, Texture2D aquamentusTexture, Texture2D enemytexture, Texture2D itemSprites, Texture2D crackedWallTexture)

        {
            tileBuilder = new TileBuilder(environmentTexture, npcTexture, crackedWallTexture);
            this.TileTex = environmentTexture;
            EntityBuilder = new EntityBuilder(aquamentusTexture, enemytexture, itemSprites);

            levels.Add(new Level(levelZeroZero,entityZeroZero,0,0));
            levels.Add(new Level(levelZeroNegOne,entityZeroZero,0,(-1*pageSizeY)));
            levels.Add(new Level(levelNegOneNegOne, entityZeroZero, -1 * pageSizeX, -1 * pageSizeY));
            levels.Add(new Level(levelNegOneNegThree, entityZeroZero, -1 * pageSizeX, -3 * pageSizeY));
            levels.Add(new Level(levelNegOneTwo,entityZeroZero, -1 * pageSizeX, 2 *pageSizeY));
            levels.Add(new Level(levelNegOneZero,entityZeroZero,-1*pageSizeX,0));
            levels.Add(new Level(levelNegTwoZero, entityZeroZero, -2 * pageSizeX, 0));
            levels.Add(new Level(levelOneNegOne, entityZeroZero, pageSizeX, -1 * pageSizeY));
            levels.Add(new Level(levelOneNegThree, entityZeroZero, pageSizeX, -3 * pageSizeY));
            levels.Add(new Level(levelOneZero, entityZeroZero,1*pageSizeX, 0));
            levels.Add(new Level(levelThreeOne, entityZeroZero, 3 * pageSizeX, 1*pageSizeY));
            levels.Add(new Level(levelTwoOne,entityZeroZero, 2 * pageSizeX, 1* pageSizeY));
            levels.Add(new Level(levelTwoZero,entityZeroZero,2*pageSizeX, 0));
            levels.Add(new Level(levelZeroNegThree, entityZeroZero, 0 * pageSizeX, -3*pageSizeY));
            levels.Add(new Level(levelZeroNegTwo, entityZeroZero, 0 * pageSizeX, -2 * pageSizeY));
            levels.Add(new Level(levelZeroOne, entityZeroZero, 0 * pageSizeX, 1 * pageSizeY));
            levels.Add(new Level(levelZeroTwo, entityZeroZero, 0 * pageSizeX, 2 * pageSizeY));

        }


        public List<environmentTile> buildTiles()
        {
            List<environmentTile> tileList = new List<environmentTile>();
            
            foreach (var level in levels)
            {
                tileList.AddRange(level.buildTiles(tileBuilder));
            }
            return tileList;
        }
        public (List<IItem>, List<IEnemy>) buildEntities()
        {
            List<IItem> itemList = new List<IItem>();
            List<IEnemy> enemyList = new List<IEnemy>();
            List<IItem> tempitemlist = new List<IItem>();
            List<IEnemy> tempenemylist = new List<IEnemy>();
            foreach (var level in levels)
            {
                (tempitemlist, tempenemylist) = level.buildEntities(EntityBuilder);
                itemList.AddRange(tempitemlist);
                enemyList.AddRange(tempenemylist);
            }
            return (itemList, enemyList);
        }
    }
}