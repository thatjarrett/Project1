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


namespace Project1.LevelLoading
{
    public class TileBuilder
    {
        ISprite statueLeftSprite;
        ISprite statueRightSprite;
        ISprite squareBlockSprite;
        ISprite blueGapSprite;
        ISprite stairsSprite;
        ISprite oldManSprite;

        ISprite topWallSprite;
        ISprite topPlainWallSprite;
        ISprite topOpenDoorSprite;
        ISprite topBombedOpeningSprite;
        ISprite topKeyLockedDoorSprite;
        ISprite topDiamondLockedDoorSprite;

        ISprite bottomWallSprite;
        ISprite bottomPlainWallSprite;
        ISprite bottomOpenDoorSprite;
        ISprite bottomBombedOpeningSprite;
        ISprite bottomKeyLockedDoorSprite;
        ISprite bottomDiamondLockedDoorSprite;

        ISprite leftWallSprite;
        ISprite leftPlainWallSprite;
        ISprite leftOpenDoorSprite;
        ISprite leftBombedOpeningSprite;
        ISprite leftKeyLockedDoorSprite;
        ISprite leftDiamondLockedDoorSprite;

        ISprite rightWallSprite;
        ISprite rightPlainWallSprite;
        ISprite rightOpenDoorSprite;
        ISprite rightBombedOpeningSprite;
        ISprite rightKeyLockedDoorSprite;
        ISprite rightDiamondLockedDoorSprite;

        ISprite fireSprite;

        ISprite ladderSprite;
        ISprite whiteBrickSprite;
        ISprite blueFloorSprite;
        ISprite blueSandSprite;
        

        public TileBuilder(Texture2D environmentTexture, Texture2D npcTexture)
        {
            this.statueLeftSprite = new NMoveNAnim(environmentTexture, new Rectangle(515, 1, 16, 16));
            this.statueRightSprite = new NMoveNAnim(environmentTexture, new Rectangle(498, 1, 16, 16));
            this.squareBlockSprite = new NMoveNAnim(environmentTexture, new Rectangle(481, 1, 16, 16));
            this.blueGapSprite = new NMoveNAnim(environmentTexture, new Rectangle(498, 18, 16, 16));
            this.stairsSprite = new NMoveNAnim(environmentTexture, new Rectangle(515, 18, 16, 16));
            this.oldManSprite = new NMoveNAnim(npcTexture, new Rectangle(1, 1, 16, 16));

            this.topWallSprite = new NMoveNAnim(environmentTexture, new Rectangle(1, 1, 256, 32));
            this.topPlainWallSprite = new NMoveNAnim(environmentTexture, new Rectangle(295, 1, 32, 32));
            this.topOpenDoorSprite = new NMoveNAnim(environmentTexture, new Rectangle(328, 1, 32, 32));
            this.topKeyLockedDoorSprite = new NMoveNAnim(environmentTexture, new Rectangle(361, 1, 32, 32));
            this.topDiamondLockedDoorSprite = new NMoveNAnim(environmentTexture, new Rectangle(394, 1, 32, 32));
            this.topBombedOpeningSprite = new NMoveNAnim(environmentTexture, new Rectangle(427, 1, 32, 32));

            this.bottomWallSprite = new NMoveNAnim(environmentTexture, new Rectangle(1, 145, 256, 32));
            this.bottomPlainWallSprite = new NMoveNAnim(environmentTexture, new Rectangle(295, 100, 32, 32));
            this.bottomOpenDoorSprite = new NMoveNAnim(environmentTexture, new Rectangle(328, 100, 32, 32));
            this.bottomKeyLockedDoorSprite = new NMoveNAnim(environmentTexture, new Rectangle(361, 100, 32, 32));
            this.bottomDiamondLockedDoorSprite = new NMoveNAnim(environmentTexture, new Rectangle(394, 100, 32, 32));
            this.bottomBombedOpeningSprite = new NMoveNAnim(environmentTexture, new Rectangle(427, 100, 32, 32));

            this.leftWallSprite = new NMoveNAnim(environmentTexture, new Rectangle(1, 33, 32, 112));
            this.leftPlainWallSprite = new NMoveNAnim(environmentTexture, new Rectangle(295, 34, 32, 32));
            this.leftOpenDoorSprite = new NMoveNAnim(environmentTexture, new Rectangle(328, 34, 32, 32));
            this.leftKeyLockedDoorSprite = new NMoveNAnim(environmentTexture, new Rectangle(361, 34, 32, 32));
            this.leftDiamondLockedDoorSprite = new NMoveNAnim(environmentTexture, new Rectangle(394, 34, 32, 32));
            this.leftBombedOpeningSprite = new NMoveNAnim(environmentTexture, new Rectangle(427, 34, 32, 32));

            this.rightWallSprite = new NMoveNAnim(environmentTexture, new Rectangle(225, 33, 32, 112));
            this.rightPlainWallSprite = new NMoveNAnim(environmentTexture, new Rectangle(295, 67, 32, 32));
            this.rightOpenDoorSprite = new NMoveNAnim(environmentTexture, new Rectangle(328, 67, 32, 32));
            this.rightKeyLockedDoorSprite = new NMoveNAnim(environmentTexture, new Rectangle(361, 67, 32, 32));
            this.rightDiamondLockedDoorSprite = new NMoveNAnim(environmentTexture, new Rectangle(394, 67, 32, 32));
            this.rightBombedOpeningSprite = new NMoveNAnim(environmentTexture, new Rectangle(427, 67, 32, 32));

            Rectangle[] fireFrames = new Rectangle[] { new Rectangle(52, 1, 16, 16), new Rectangle(69, 1, 16, 16) };
            this.fireSprite = new NMoveAnim(npcTexture, fireFrames, 5);

            this.ladderSprite = new NMoveNAnim(environmentTexture, new Rectangle(481, 35, 16, 16));
            this.whiteBrickSprite = new NMoveNAnim(environmentTexture, new Rectangle(464, 35, 16, 16));
            this.blueFloorSprite = new NMoveNAnim(environmentTexture, new Rectangle(464, 1, 16, 16));
            this.blueSandSprite = new NMoveNAnim(environmentTexture, new Rectangle(481, 18, 16, 16));

        }

        public environmentTile buildTile(int tileID, Vector2 location)
        {
            environmentTile tile;
            switch (tileID)
            {
                case 0:
                    {
                        tile = new statueTileLeft(location);
                        tile.setSprite(this.statueLeftSprite);
                        break;
                    }
                case 1:
                    {
                        tile = new statueTileRight(location);
                        tile.setSprite(this.statueRightSprite);
                        break;
                    }
                case 2:
                    {
                        tile = new blockTile(location);
                        tile.setSprite(this.squareBlockSprite);
                        break;
                    }
                case 3:
                    {
                        tile = new gapTile(location);
                        tile.setSprite(this.blueGapSprite);
                        break;
                    }
                case 4:
                    {
                        tile = new stairsTile(location);
                        tile.setSprite(this.stairsSprite);
                        break;
                    }
                case 5:
                    {
                        tile = new fireTile(location);
                        tile.setSprite(this.fireSprite);
                        break;
                    }
                case 6:
                    {
                        tile = new oldManTile(location);
                        tile.setSprite(this.oldManSprite);
                        break;
                    }
                case 7:
                    {
                        tile = new wallTile(location, 768, 96);       // need to remove ID requirement from wallTile
                        tile.setSprite(this.topWallSprite);
                        break;
                    }
                case 8:
                    {
                        tile = new doorTile(location);        // need to remove ID requirement from doorTile
                        tile.setSprite(this.topPlainWallSprite);
                        break;
                    }
                case 9:
                    {
                        tile = new doorTile(location);        // need to remove ID requirement from doorTile
                        tile.setSprite(this.topOpenDoorSprite);
                        tile.setCollision(false);
                        break;
                    }
                case 10:
                    {
                        tile = new doorTile(location);       // need to remove ID requirement from doorTile
                        tile.setSprite(this.topBombedOpeningSprite);
                        tile.setCollision(false);
                        break;
                    }
                case 11:
                    {
                        tile = new doorTile(location);       // need to remove ID requirement from doorTile
                        tile.setSprite(this.topKeyLockedDoorSprite);
                        break;
                    }
                case 12:
                    {
                        tile = new doorTile(location);       // need to remove ID requirement from doorTile
                        tile.setSprite(this.topDiamondLockedDoorSprite);
                        break;
                    }
                case 13:
                    {
                        tile = new wallTile(location, 768, 96);       // need to remove ID requirement from wallTile
                        tile.setSprite(this.bottomWallSprite);
                        break;
                    }
                case 14:
                    {
                        tile = new doorTile(location);       // need to remove ID requirement from doorTile
                        tile.setSprite(this.bottomPlainWallSprite);
                        break;
                    }
                case 15:
                    {
                        tile = new doorTile(location);       // need to remove ID requirement from doorTile
                        tile.setSprite(this.bottomOpenDoorSprite);
                        tile.setCollision(false);
                        break;
                    }
                case 16:
                    {
                        tile = new doorTile(location);       // need to remove ID requirement from doorTile
                        tile.setSprite(this.bottomBombedOpeningSprite);
                        tile.setCollision(false);
                        break;
                    }
                case 17:
                    {
                        tile = new doorTile(location);       // need to remove ID requirement from doorTile
                        tile.setSprite(this.bottomKeyLockedDoorSprite);
                        break;
                    }
                case 18:
                    {
                        tile = new doorTile(location);       // need to remove ID requirement from doorTile
                        tile.setSprite(this.bottomDiamondLockedDoorSprite);
                        break;
                    }
                case 19:
                    {
                        tile = new wallTile(location, 96, 336, false);       // need to remove ID requirement from doorTile
                        tile.setSprite(this.leftWallSprite);
                        break;
                    }
                case 20:
                    {
                        tile = new doorTile(location);       // need to remove ID requirement from wallTile
                        tile.setSprite(this.leftPlainWallSprite);
                        break;
                    }
                case 21:
                    {
                        tile = new doorTile(location);       // need to remove ID requirement from doorTile
                        tile.setSprite(this.leftOpenDoorSprite);
                        tile.setCollision(false);
                        break;
                    }
                case 22:
                    {
                        tile = new doorTile(location);       // need to remove ID requirement from doorTile
                        tile.setSprite(this.leftBombedOpeningSprite);
                        tile.setCollision(false);
                        break;
                    }
                case 23:
                    {
                        tile = new doorTile(location);       // need to remove ID requirement from doorTile
                        tile.setSprite(this.leftKeyLockedDoorSprite);
                        break;
                    }
                case 24:
                    {
                        tile = new doorTile(location);
                        tile.setSprite(this.leftDiamondLockedDoorSprite);
                        break;
                    }
                case 25:
                    {
                        tile = new wallTile(location, 96, 336, false);
                        tile.setSprite(this.rightWallSprite);
                        break;
                    }
                case 26:
                    {
                        tile = new doorTile(location);
                        tile.setSprite(this.rightPlainWallSprite);
                        break;
                    }
                case 27:
                    {
                        tile = new doorTile(location);
                        tile.setSprite(this.rightOpenDoorSprite);
                        tile.setCollision(false);
                        break;
                    }
                case 28:
                    {
                        tile = new doorTile(location);
                        tile.setSprite(this.rightBombedOpeningSprite);
                        tile.setCollision(false);
                        break;
                    }
                case 29:
                    {
                        tile = new doorTile(location);
                        tile.setSprite(this.rightKeyLockedDoorSprite);
                        break;
                    }
                case 30:
                    {
                        tile = new doorTile(location);
                        tile.setSprite(this.rightDiamondLockedDoorSprite);
                        break;
                    }
                case 31:
                    {
                        tile = new BlueFloor(location);
                        tile.setSprite(this.blueFloorSprite);
                        break;
                    }
                case 32:
                    {
                        tile = new BlueSand(location);
                        tile.setSprite(this.blueSandSprite);
                        break;
                    }
                case 33:
                    {
                        tile = new Ladder(location);
                        tile.setSprite(this.ladderSprite);
                        break;
                    }
                case 34:
                    {
                        tile = new WhiteBrick(location);
                        tile.setSprite(this.whiteBrickSprite);
                        break;
                    }
                case 35:
                    {
                        tile = new pushableBlock(location);
                        tile.setSprite(this.squareBlockSprite);
                        break;
                    }
                default:
                    {
                        tile = new BlueFloor(location);
                        tile.setSprite(this.blueFloorSprite);
                        break;
                    }
            }
            return tile;
        }
    }
}
/*
 *        
    
        environmentTile pushBlock = new pushableBlock(new Vector2(100, 100));    not in dictionary as stands

  spritesIDs = new Dictionary<int, ISprite>
        {
            {0,this.statueLeftSprite},
            { 1,this.statueRightSprite},
            { 2,this.squareBlockSprite},
            { 3,this.blueGapSprite},
            { 4,this.stairsSprite},
            { 5,this.fireSprite},
            { 6,this.oldManSprite},
            { 7,this.topWallSprite},
            { 8,this.topPlainWallSprite},
            { 9,this.topOpenDoorSprite},
            { 10,this.topBombedOpeningSprite},
            { 11,this.topKeyLockedDoorSprite},
            { 12,this.topDiamondLockedDoorSprite},
            { 13,this.bottomWallSprite},
            { 14,this.bottomPlainWallSprite},
            { 15,this.bottomOpenDoorSprite},
            { 16,this.bottomBombedOpeningSprite},
            { 17,this.bottomKeyLockedDoorSprite},
            { 18,this.bottomDiamondLockedDoorSprite},
            { 19,this.leftWallSprite},
            { 20,this.leftPlainWallSprite},
            { 21,this.leftOpenDoorSprite},
            { 22,this.leftBombedOpeningSprite},
            { 23,this.leftKeyLockedDoorSprite},
            { 24,this.leftDiamondLockedDoorSprite},
            { 25,this.rightWallSprite},
            { 26,this.rightPlainWallSprite},
            { 27,this.rightOpenDoorSprite},
            { 28,this.rightBombedOpeningSprite},
            { 29,this.rightKeyLockedDoorSprite},
            { 30,this.rightDiamondLockedDoorSprite},
            { 31,this.blueFloorSprite},
            { 32,this.blueSandSprite },
            { 33, this.ladderSprite},
            { 34, this.whiteBrickSprite}
        };
 */
