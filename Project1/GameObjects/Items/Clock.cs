using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Interfaces;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;


namespace Project1.GameObjects.Items
{
    public class Clock : ISprite
    {
        Texture2D texture;
        Rectangle destinationRectangle;
        int frameState = 0;
        Vector2 Pos;

        public Clock(Texture2D texture)
        {
            this.texture = texture;
            this.Pos = new Vector2(200, 300); 

        }
        public void Draw(SpriteBatch spriteBatch, SpriteEffects spriteEffects)
        {

            Rectangle source = new Rectangle(56, 0, 16, 16);

            destinationRectangle = new Rectangle((int)Pos.X, (int)Pos.Y, 2 * 16, 2 * 16);


            spriteBatch.Draw(texture, destinationRectangle, source, Color.White);
        }
        public void Draw(SpriteBatch spriteBatch,Vector2 location, SpriteEffects spriteEffects)
        {

            Rectangle source = new Rectangle(56, 0, 16, 16);

            destinationRectangle = new Rectangle((int)location.X, (int)location.Y, 2 * 16, 2 * 16);


            spriteBatch.Draw(texture, destinationRectangle, source, Color.White);
        }

        public void Update(GameTime gameTime)
        {

        }
        public void SetColor(Color _color)
        {
        }
        public void SetPosition(Vector2 pos)
        {
            this.Pos = pos;
        }
        public Vector2 getPosition() { return this.Pos; }
    }
}