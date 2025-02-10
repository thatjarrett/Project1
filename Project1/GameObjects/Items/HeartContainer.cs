using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Project1.Interfaces;
using System.Drawing;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;


namespace Project1.GameObjects.Items
{
	public class HeartContainer: ISprite
	{
        Texture2D texture;
        Rectangle destinationRectangle;
        int frameState = 0;

        public HeartContainer(Texture2D texture)
        {
            this.texture = texture;

        }
        public void Draw(SpriteBatch spriteBatch, Vector2 location, SpriteEffects spriteEffects)
        {

            Rectangle[] sourceArray = new Rectangle[2];
            sourceArray[0] = new Rectangle(25, 1, 15, 15);
     
            destinationRectangle = new Rectangle((int)location.X, (int)location.Y, 2 * 16, 2  * 16);
            
          
            spriteBatch.Draw(texture, destinationRectangle, sourceArray[frameState],Color.White);
        }

        public void Update(GameTime gameTime)
        {

        }
        public void SetColor(Color _color)
        {
        }
    }
}