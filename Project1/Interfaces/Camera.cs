using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using NVorbis.Contracts;

namespace Project1.Interfaces
{
    public class Camera
    {
        public int row = 0;
        public int column = 0;
        private Viewport view;
        private Matrix CameraMatrix;
        private float zoom = 1.0f;
        private int CameraPosX=0;
        private int CameraPosY=120;
        private int CurrPlayerPosX;
        private int CurrPlayerPosY;
        private int cameraSpeed = 24;// must be a common factor of 528 and 768
        private int targetPosX=0;
        private int targetPosY=120;

        public Matrix GetTransformation(Vector2 playerPosition, ref bool isTransitioning)
        {
            this.CurrPlayerPosX = (int)playerPosition.X;
            this.CurrPlayerPosY = (int)playerPosition.Y;
            if ((CameraPosX - CurrPlayerPosX) < -384)
            {
                targetPosX = CameraPosX + 768;//row++
                row++;
            } else if (CameraPosX - CurrPlayerPosX > 384)
            {
                targetPosX = CameraPosX - 768;//row--
                row--;
            } else if ((CameraPosY - (CurrPlayerPosY+100))<-264)
            {
                targetPosY = CameraPosY + 528;//column++
                column++;
            } else if ((CameraPosY - (CurrPlayerPosY + 100)) > 264)
            {
                targetPosY = CameraPosY - 528;//column--
                column--;
            }
            
            MoveTowardsTarget(ref isTransitioning);
            return
                Matrix.CreateTranslation(new Vector3(
                    (CameraPosX) *-1,
                    (CameraPosY - 120)*-1,
                    0f));
        }

        public void MoveTowardsTarget(ref bool isTransitioning)
        {

            if (CameraPosY < targetPosY)
            {
                isTransitioning = true;
                CameraPosY += cameraSpeed;
            }
            else if (CameraPosY > targetPosY)
            {
                isTransitioning = true;
                CameraPosY -= cameraSpeed;
            }
            else if (CameraPosX < targetPosX)
            {
                isTransitioning = true;
                CameraPosX += cameraSpeed;
            }
            else if (CameraPosX > targetPosX)
            {
                isTransitioning = true;
                CameraPosX -= cameraSpeed;
            }
            else
            {
                isTransitioning = false;
            }
        }
        public Camera(Viewport view)
        {

            this.view = view;
        }
        public Vector2 getCameraPos()
        {
            return (new Vector2(row, column));
        }
        public void setCameraPos(Vector2 newLocation)
        {
            CameraPosX = newLocation.x*768;
            CameraPosY = newLocation.y*528+120;
            targetPosX = newLocation.x * 768;
            targetPosY = newLocation.y * 528 + 120;
            row = newLocation.x;
            column = newLocation.y; 
        }
    }
}