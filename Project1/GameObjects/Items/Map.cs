using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Interfaces;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;


namespace Project1.GameObjects.Items
{
    public class Map : ISprite
    {
        Texture2D texture;
        Rectangle destinationRectangle;
        Vector2 Pos;


        public Map(Texture2D texture, Vector2 Pos)
        {
            this.texture = texture;
            this.Pos = Pos;

        }
        public void Draw(SpriteBatch spriteBatch, SpriteEffects spriteEffects)
        {

            Rectangle source = new Rectangle(230, 0, 10, 16);
            destinationRectangle = new Rectangle((int)Pos.X, (int)Pos.Y, 2 * 10, 2 * 16);


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