using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Project1.Interfaces;
using System.Drawing;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;


namespace Project1.GameObjects.Items
{
	public class Key: ISprite
	{
        Texture2D texture;
        Rectangle destinationRectangle;
        

        public Key(Texture2D texture)
        {
            this.texture = texture;

        }
        public void Draw(SpriteBatch spriteBatch, Vector2 location, SpriteEffects spriteEffects)
        {

           Rectangle source = new Rectangle(240, 1, 9, 16);
           destinationRectangle = new Rectangle((int)location.X, (int)location.Y, 2 * 9, 2  * 16);
            
          
            spriteBatch.Draw(texture, destinationRectangle, source,Color.White);
        }

        public void Update(GameTime gameTime)
        {
          

        }
        public void SetColor(Color _color)
        {
        }
    }
}