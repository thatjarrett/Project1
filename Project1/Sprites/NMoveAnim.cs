using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Project1.Interfaces;
using System.Diagnostics;

namespace Project1.Sprites
{
    internal class NMoveAnim:ISprite
    {
        private Texture2D _texture;
        private Rectangle _sourceRectangle;
        private float _scale; // Scale factor for sprite size
        private Rectangle [] frames;
        private int fps;
        private int frameCounter = 0;

        public NMoveAnim(Texture2D texture, Rectangle[] framesList, int frameRate, float scale = 3.0f)
        {
            _texture = texture;
            frames = framesList;
            fps = frameRate;
            _scale = scale; // Initialize scale
        }

        public void Update(GameTime gameTime)
        {
            frameCounter++;
            int frame = frameCounter / fps;
            if (frame > frames.Length-1) {
                frameCounter = 0;
                frame = 0;
            }
            _sourceRectangle = frames[frame];
            
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(
                _texture,
                position,
                _sourceRectangle,
                Color.White,
                0f,
                Vector2.Zero,
                _scale,           // Scale factor
                SpriteEffects.None,
                0f
            );
        }
    }
}

