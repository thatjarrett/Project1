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

        string EntityZeroZero = "Content/Level Data/EntityZeroZero.txt";
        string EntityNegOneNegOne = "Content/Level Data/EntityNegOneNegOne.txt";
        string EntityNegOneNegThree = "Content/Level Data/EntityNegOneNegThree.txt";
        string EntityNegOneTwo = "Content/Level Data/EntityNegOneTwo.txt";
        string EntityNegOneZero = "Content/Level Data/EntityNegOneZero.txt";
        string EntityNegTwoZero = "Content/Level Data/EntityNegTwoZero.txt";
        string EntityOneNegOne = "Content/Level Data/EntityOneNegOne.txt";
        string EntityOneNegThree = "Content/Level Data/EntityOneNegThree.txt";
        string EntityOneZero = "Content/Level Data/EntityOneZero.txt";
        string EntityThreeOne = "Content/Level Data/EntityThreeOne.txt";
        string EntityTwoOne = "Content/Level Data/EntityTwoOne.txt";
        string EntityTwoZero = "Content/Level Data/EntityTwoZero.txt";
        string EntityZeroNegOne = "Content/Level Data/EntityZeroNegOne.txt";
        string EntityZeroNegThree = "Content/Level Data/EntityZeroNegThree.txt";
        string EntityZeroNegTwo = "Content/Level Data/EntityZeroNegTwo.txt";
        string EntityZeroOne = "Content/Level Data/EntityZeroOne.txt";
        string EntityZeroTwo = "Content/Level Data/EntityZeroTwo.txt";



        TileBuilder tileBuilder;
        EntityBuilder EntityBuilder;
        Texture2D TileTex;
        List<Level> levels = new List<Level>();
        int pageSizeX = 768;
        int pageSizeY = -528;

        public levelManager(Texture2D environmentTexture, Texture2D npcTexture, Texture2D aquamentusTexture, Texture2D enemytexture, Texture2D itemSprites, Texture2D crackedWallTexture, Texture2D deathAnim)

        {
            tileBuilder = new TileBuilder(environmentTexture, npcTexture, crackedWallTexture);
            this.TileTex = environmentTexture;
            EntityBuilder = new EntityBuilder(aquamentusTexture, enemytexture, itemSprites, deathAnim);

            levels.Add(new Level(levelZeroZero,EntityZeroZero,0,0));
            levels.Add(new Level(levelZeroNegOne,EntityZeroNegOne,0,(-1*pageSizeY)));
            levels.Add(new Level(levelNegOneNegOne, EntityNegOneNegOne, -1 * pageSizeX, -1 * pageSizeY));
            levels.Add(new Level(levelNegOneNegThree, EntityNegOneNegThree, -1 * pageSizeX, -3 * pageSizeY));
            levels.Add(new Level(levelNegOneTwo, EntityNegOneTwo, -1 * pageSizeX, 2 *pageSizeY));
            levels.Add(new Level(levelNegOneZero, EntityNegOneZero, -1*pageSizeX,0));
            levels.Add(new Level(levelNegTwoZero, EntityNegTwoZero, -2 * pageSizeX, 0));
            levels.Add(new Level(levelOneNegOne, EntityOneNegOne, pageSizeX, -1 * pageSizeY));
            levels.Add(new Level(levelOneNegThree, EntityOneNegThree, pageSizeX, -3 * pageSizeY));
            levels.Add(new Level(levelOneZero, EntityOneZero,1*pageSizeX, 0));
            levels.Add(new Level(levelThreeOne, EntityThreeOne, 3 * pageSizeX, 1*pageSizeY));
            levels.Add(new Level(levelTwoOne,EntityTwoOne, 2 * pageSizeX, 1* pageSizeY));
            levels.Add(new Level(levelTwoZero,EntityTwoZero,2*pageSizeX, 0));
            levels.Add(new Level(levelZeroNegThree, EntityZeroNegThree, 0 * pageSizeX, -3*pageSizeY));
            levels.Add(new Level(levelZeroNegTwo, EntityZeroNegTwo, 0 * pageSizeX, -2 * pageSizeY));
            levels.Add(new Level(levelZeroOne, EntityZeroOne, 0 * pageSizeX, 1 * pageSizeY));
            levels.Add(new Level(levelZeroTwo, EntityZeroTwo, 0 * pageSizeX, 2 * pageSizeY));

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