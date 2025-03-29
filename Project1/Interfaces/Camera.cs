using System;
using Microsoft.Xna.Framework.Graphics;

namespace Project1.Interfaces
{ 
	public class Camera
    { 
	    public int row = 0;
        public int column = 0;
        private Viewport view;
        private Matrix CameraMatrix;
            
        public Camera(Viewport view)
        {

            this.view = view;
        }

	}
}