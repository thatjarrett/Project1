using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Project1.Interfaces
{
    public class Camera
    {
        public int row = 0;
        public int column = 0;
        private Viewport view;
        private Matrix CameraMatrix;
        private float zoom = 1.0f;

        public Matrix GetTransformation(Vector2 playerPosition)
        {
            return
                Matrix.CreateScale(zoom) *
                Matrix.CreateTranslation(new Vector3(
                    view.Width / 2f - playerPosition.X * zoom,
                    view.Height / 2f - playerPosition.Y * zoom,
                    0f));
        }


        public Camera(Viewport view)
        {

            this.view = view;
        }

    }
}