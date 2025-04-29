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
using System.Reflection;


namespace Project1.LevelLoading
{
    public class TileBuilder
    {
        private static int HudOffset = 48;
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
        ISprite topCrackedWallSprite;
        ISprite topKeyLockedDoorSprite;
        ISprite topDiamondLockedDoorSprite;

        ISprite bottomWallSprite;
        ISprite bottomPlainWallSprite;
        ISprite bottomOpenDoorSprite;
        ISprite bottomBombedOpeningSprite;
        ISprite bottomCrackedWallSprite;
        ISprite bottomKeyLockedDoorSprite;
        ISprite bottomDiamondLockedDoorSprite;

        ISprite leftWallSprite;
        ISprite leftPlainWallSprite;
        ISprite leftOpenDoorSprite;
        ISprite leftBombedOpeningSprite;
        ISprite leftCrackedWallSprite;
        ISprite leftKeyLockedDoorSprite;
        ISprite leftDiamondLockedDoorSprite;

        ISprite rightWallSprite;
        ISprite rightPlainWallSprite;
        ISprite rightOpenDoorSprite;
        ISprite rightBombedOpeningSprite;
        ISprite rightCrackedWallSprite;
        ISprite rightKeyLockedDoorSprite;
        ISprite rightDiamondLockedDoorSprite;

        ISprite fireSprite;

        ISprite ladderSprite;
        ISprite whiteBrickSprite;
        ISprite blueFloorSprite;
        ISprite blueSandSprite;

        ISprite voidSprite;


        public TileBuilder(Texture2D environmentTexture, Texture2D npcTexture, Texture2D crackedWallTexture)

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

            this.topCrackedWallSprite = new NMoveNAnim(crackedWallTexture, new Rectangle(427, 1, 32, 32));
            this.bottomCrackedWallSprite = new NMoveNAnim(crackedWallTexture, new Rectangle(427, 100, 32, 32));
            this.leftCrackedWallSprite = new NMoveNAnim(crackedWallTexture, new Rectangle(427, 34, 32, 32));
            this.rightCrackedWallSprite = new NMoveNAnim(crackedWallTexture, new Rectangle(427, 67, 32, 32));



            Rectangle[] fireFrames = new Rectangle[] { new Rectangle(52, 1, 16, 16), new Rectangle(69, 1, 16, 16) };
            this.fireSprite = new NMoveAnim(npcTexture, fireFrames, 5);

            this.ladderSprite = new NMoveNAnim(environmentTexture, new Rectangle(481, 35, 16, 16));
            this.whiteBrickSprite = new NMoveNAnim(environmentTexture, new Rectangle(464, 35, 16, 16));
            this.blueFloorSprite = new NMoveNAnim(environmentTexture, new Rectangle(464, 1, 16, 16));
            this.blueSandSprite = new NMoveNAnim(environmentTexture, new Rectangle(481, 18, 16, 16));

            this.voidSprite = new NMoveNAnim(environmentTexture, new Rectangle(464, 18, 16, 16));
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
                        tile = new wallTile(location, 768, 96);   
                        tile.setSprite(this.topWallSprite);
                        break;
                    }
                case 8:
                    {
                        tile = new doorTile(location); 
                        tile.setSprite(this.topPlainWallSprite);
                        break;
                    }
                case 9:
                    {
                        tile = new doorTile(location);  
                        tile.setSprite(this.topOpenDoorSprite);
                        if(tile is doorTile door)
                        {
                            door.SetOpen(true);
                            door.SetCollider((int)location.X, (int)location.Y, 96, HudOffset);
                        }
                        break;
                    }
                case 10:
                    {
                        tile = new doorTile(location);      
                        tile.setSprite(this.topBombedOpeningSprite);
                        if (tile is doorTile door)
                        {
                            door.SetOpen(true);
                            door.SetCollider((int)location.X, (int)location.Y, 96, HudOffset);
                        }
                        break;
                    }
                case 11:
                    {
                        tile = new LockedDoorTile(location, this.topKeyLockedDoorSprite, this.topOpenDoorSprite, Entities.Direction.Up);
                        break;
                    }
                case 12:
                    {
                        tile = new LockedDoorTile(location, this.topDiamondLockedDoorSprite, this.topOpenDoorSprite, Entities.Direction.Up);
                        break;
                    }
                case 13:
                    {
                        tile = new wallTile(location, 768, 96);   
                        tile.setSprite(this.bottomWallSprite);
                        break;
                    }
                case 14:
                    {
                        tile = new doorTile(location);     
                        tile.setSprite(this.bottomPlainWallSprite);
                        break;
                    }
                case 15:
                    {
                        tile = new doorTile(location);   
                        tile.setSprite(this.bottomOpenDoorSprite);
                        if (tile is doorTile door)
                        {
                            door.SetOpen(true);
                            door.SetCollider((int)location.X, (int)location.Y + HudOffset, 96, HudOffset);
                        }
                        break;
                    }
                case 16:
                    {
                        tile = new doorTile(location);    
                        tile.setSprite(this.bottomBombedOpeningSprite);
                        if (tile is doorTile door)
                        {
                            door.SetOpen(true);
                            door.SetCollider((int)location.X, (int)location.Y + HudOffset, 96, HudOffset);
                        }
                        break;
                    }
                case 17:
                    {
                        tile = new LockedDoorTile(location, this.bottomKeyLockedDoorSprite, this.bottomOpenDoorSprite, Entities.Direction.Down);
                        break;
                    }
                case 18:
                    { 
                        tile = new LockedDoorTile(location, this.bottomDiamondLockedDoorSprite, this.bottomOpenDoorSprite, Entities.Direction.Down);
                        break;
                    }
                case 19:
                    {
                        tile = new wallTile(location, 96, 336, false);     
                        tile.setSprite(this.leftWallSprite);
                        break;
                    }
                case 20:
                    {
                        tile = new doorTile(location);    
                        tile.setSprite(this.leftPlainWallSprite);
                        break;
                    }
                case 21:
                    {
                        tile = new doorTile(location);  
                        tile.setSprite(this.leftOpenDoorSprite);
                        if (tile is doorTile door)
                        {
                            door.SetOpen(true);
                            door.SetCollider((int)location.X, (int)location.Y, HudOffset, 96);
                        }
                        break;
                    }
                case 22:
                    {
                        tile = new doorTile(location);
                        tile.setSprite(this.leftBombedOpeningSprite);
                        if (tile is doorTile door)
                        {
                            door.SetOpen(true);
                            door.SetCollider((int)location.X, (int)location.Y, HudOffset, 96);
                        }
                        break;
                    }
                case 23:
                    {
                        tile = new LockedDoorTile(location, this.leftKeyLockedDoorSprite, this.leftOpenDoorSprite, Entities.Direction.Left);
                        break;
                    }
                case 24:
                    {
                        tile = new LockedDoorTile(location, this.leftDiamondLockedDoorSprite, this.leftOpenDoorSprite, Entities.Direction.Left);
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
                        if (tile is doorTile door)
                        {
                            door.SetOpen(true);
                            door.SetCollider((int)location.X + HudOffset, (int)location.Y, HudOffset, 96);
                        }
                        break;
                    }
                case 28:
                    {
                        tile = new doorTile(location);
                        tile.setSprite(this.rightBombedOpeningSprite);
                        if (tile is doorTile door)
                        {
                            door.SetOpen(true);
                            door.SetCollider((int)location.X + HudOffset, (int)location.Y, HudOffset, 96);
                        }
                        break;
                    }
                case 29:
                    {
                        tile = new LockedDoorTile(location, this.rightKeyLockedDoorSprite, this.rightOpenDoorSprite, Entities.Direction.Right);
                        break;
                    }
                case 30:
                    {
                        tile = new LockedDoorTile(location, this.rightDiamondLockedDoorSprite, this.rightOpenDoorSprite, Entities.Direction.Right);
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
                case 36:
                    {
                        tile = new CrackedWallTile(location, this.topBombedOpeningSprite);
                        tile.setSprite(this.topCrackedWallSprite);
                        break;
                    }
                case 37:
                    {
                        tile = new CrackedWallTile(location, this.bottomBombedOpeningSprite);
                        tile.setSprite(this.bottomCrackedWallSprite);
                        break;
                    }
                case 38:
                    {
                        tile = new CrackedWallTile(location, this.leftBombedOpeningSprite);
                        tile.setSprite(this.leftCrackedWallSprite);
                        break;
                    }
                case 39:
                    {
                        tile = new CrackedWallTile(location, this.rightBombedOpeningSprite);
                        tile.setSprite(this.rightCrackedWallSprite);
                        break;
                    }

                case 40:
                    {
                        tile = new VoidTile(location);
                        tile.setSprite(this.voidSprite);
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
