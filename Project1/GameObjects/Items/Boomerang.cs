using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Project1.Interfaces;


namespace Project1.GameObjects.Items
{
	public class Boomerang: ISprite
	{
        Texture2D texture;
        Rectangle destinationRectangle;
        int frameState = 0;

        public Boomerang(Texture2D texture)
        {
            this.texture = texture;

        }
        public void Draw(SpriteBatch spriteBatch, Vector2 location, SpriteEffects spriteEffects)
        {

            Rectangle[] sourceArray = new Rectangle[2];
            sourceArray[0] = new Rectangle(129, 3, 5, 8);
            sourceArray[1] = new Rectangle(129, 19, 5, 8);
            if (frameState == 0)
            {
                destinationRectangle = new Rectangle((int)location.X, (int)location.Y, 2*5, 2*8);
            }
            else if (frameState == 1)
            {

                destinationRectangle = new Rectangle((int)location.X, (int)location.Y, 2 * 5, 2 * 8);
            }
          
            spriteBatch.Draw(texture, destinationRectangle, sourceArray[frameState], Color.White);
        }

        public void Update(GameTime gameTime)
        {
            frameState = (gameTime.TotalGameTime.Seconds) % 2;

        }
    }
}