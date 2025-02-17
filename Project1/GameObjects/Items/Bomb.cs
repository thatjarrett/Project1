using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Interfaces;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;


namespace Project1.GameObjects.Items
{
    public class Bomb : ISprite
    {
        Texture2D texture;
        Rectangle destinationRectangle;
        Vector2 Pos;


        public Bomb(Texture2D texture, Vector2 pos)
        {
            this.texture = texture;
            this.Pos = pos; 

        }
        public void Draw(SpriteBatch spriteBatch, SpriteEffects spriteEffects)
        {

            Rectangle source = new Rectangle(136, 0, 8, 16);
            destinationRectangle = new Rectangle((int)Pos.X, (int)Pos.Y, 2 * 8, 2 * 16);


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
            this.pos = pos;
        }
        public Vector2 getPosition() { return this.pos; }
    }
}