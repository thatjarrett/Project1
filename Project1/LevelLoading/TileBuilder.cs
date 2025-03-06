using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Net.Sockets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1.Collision;
using Project1.Commands;
using Project1.Controllers;
using Project1.Entities;
using Project1.GameObjects.Background;
using Project1.GameObjects.Environment;
using Project1.GameObjects.Items;
using Project1.Handlers;
using Project1.Interfaces;
using Project1.Projectiles;
using Project1.Sprites;
using Project1.LevelLoading;

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

    Dictionary<int, ISprite> spritesIDs;


    public TileBuilder(Texture2D environmentTexture, Texture2D npcTexture)
	{
        createEnvironmentSprites(environmentTexture, npcTexture);
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

    }

    public environmentTile buildTile(int tileID,Vector2 location)
    {

    }

    protected void createEnvironmentSprites(Texture2D environmentTexture, Texture2D npcTexture)
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

        this.leftWallSprite = new NMoveNAnim(environmentTexture, new Rectangle(1, 33, 32, 96));
        this.leftPlainWallSprite = new NMoveNAnim(environmentTexture, new Rectangle(295, 34, 32, 32));
        this.leftOpenDoorSprite = new NMoveNAnim(environmentTexture, new Rectangle(328, 34, 32, 32));
        this.leftKeyLockedDoorSprite = new NMoveNAnim(environmentTexture, new Rectangle(361, 34, 32, 32));
        this.leftDiamondLockedDoorSprite = new NMoveNAnim(environmentTexture, new Rectangle(394, 34, 32, 32));
        this.leftBombedOpeningSprite = new NMoveNAnim(environmentTexture, new Rectangle(427, 34, 32, 32));

        this.rightWallSprite = new NMoveNAnim(environmentTexture, new Rectangle(225, 33, 32, 96));
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
}
/*
 *         environmentTile statueTile = new statueTileLeft(new Vector2(100, 100));
        environmentTile statueTile2 = new statueTileRight(new Vector2(100, 100));
        environmentTile squareBlock = new blockTile(new Vector2(100, 100));
        environmentTile blueGap = new gapTile(new Vector2(100, 100));
        environmentTile stairs = new stairsTile(new Vector2(100, 100));
        environmentTile fire = new fireTile(new Vector2(100, 100));
        environmentTile oldMan = new oldManTile(new Vector2(100, 100));
        environmentTile pushBlock = new pushableBlock(new Vector2(100, 100));

        environmentTile topWall = new wallTile(new Vector2(100, 100), 7);
        environmentTile topPlainWall = new doorTile(new Vector2(100, 100), 8);
        environmentTile topOpenDoor = new doorTile(new Vector2(100, 100), 9);
        environmentTile topKeyLockedDoor = new doorTile(new Vector2(100, 100), 11);
        environmentTile topDiamondLockedDoor = new doorTile(new Vector2(100, 100), 12);
        environmentTile topBombedOpening = new doorTile(new Vector2(100, 100), 10);

        environmentTile bottomWall = new wallTile(new Vector2(100, 100), 13);
        environmentTile bottomPlainWall = new doorTile(new Vector2(100, 100), 14);
        environmentTile bottomOpenDoor = new doorTile(new Vector2(100, 100), 15);
        environmentTile bottomKeyLockedDoor = new doorTile(new Vector2(100, 100), 17);
        environmentTile bottomDiamondLockedDoor = new doorTile(new Vector2(100, 100), 18);
        environmentTile bottomBombedOpening = new doorTile(new Vector2(100, 100), 16);

        environmentTile leftWall = new wallTile(new Vector2(100, 100), 19);
        environmentTile leftPlainWall = new doorTile(new Vector2(100, 100), 20);
        environmentTile leftOpenDoor = new doorTile(new Vector2(100, 100), 21);
        environmentTile leftKeyLockedDoor = new doorTile(new Vector2(100, 100), 23);
        environmentTile leftDiamondLockedDoor = new doorTile(new Vector2(100, 100), 24);
        environmentTile leftBombedOpening = new doorTile(new Vector2(100, 100), 22);

        environmentTile rightWall = new wallTile(new Vector2(100, 100), 25);
        environmentTile rightPlainWall = new doorTile(new Vector2(100, 100), 26);
        environmentTile rightOpenDoor = new doorTile(new Vector2(100, 100), 27);
        environmentTile rightKeyLockedDoor = new doorTile(new Vector2(100, 100), 29);
        environmentTile rightDiamondLockedDoor = new doorTile(new Vector2(100, 100), 30);
        environmentTile rightBombedOpening = new doorTile(new Vector2(100, 100), 28);


        environmentTile blueFloor = new BlueFloor(new Vector2(100, 100), 31);
        environmentTile blueSand = new BlueSand(new Vector2(100, 100), 32);
        environmentTile ladder = new Ladder(new Vector2(100, 100), 33);
        environmentTile whiteBrick = new WhiteBrick(new Vector2(100, 100), 34);*/
