using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Project1.Interfaces;
using System.Drawing;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;


namespace Project1.GameObjects.Items
{
	public class Fairy: ISprite
	{
        Texture2D texture;
        Rectangle destinationRectangle;
        int frameState = 0;
        

        public Fairy(Texture2D texture)
        {
            this.texture = texture;

        }
        public void Draw(SpriteBatch spriteBatch, Vector2 location, SpriteEffects spriteEffects)
        {

            Rectangle[] sourceArray = new Rectangle[2];
            sourceArray[0] = new Rectangle(39, 0, 9, 16);
            sourceArray[1] = new Rectangle(47, 0, 9,16);
            destinationRectangle = new Rectangle((int)location.X, (int)location.Y, 2*9, 2*16);
            
          
          
            spriteBatch.Draw(texture, destinationRectangle, sourceArray[frameState],Color.White);
        }

        public void Update(GameTime gameTime)
        {
            frameState = (gameTime.TotalGameTime.Seconds) % 2;

        }
        public void SetColor(Color _color)
        {
        }
    }
}