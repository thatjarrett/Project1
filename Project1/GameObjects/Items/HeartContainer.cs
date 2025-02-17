using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Interfaces;
using System.Runtime.CompilerServices;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;


namespace Project1.GameObjects.Items
{
    public class HeartContainer : ISprite
    {
        Texture2D texture;
        Rectangle destinationRectangle;
        int frameState = 0;
        Vector2 Pos;

        public HeartContainer(Texture2D texture, Vector2 Pos)
        {
            this.texture = texture;
            this.Pos = Pos;
        }
        public void Draw(SpriteBatch spriteBatch, SpriteEffects spriteEffects)
        {

            Rectangle[] sourceArray = new Rectangle[2];
            sourceArray[0] = new Rectangle(25, 1, 15, 15);

            destinationRectangle = new Rectangle((int)Pos.X, (int)Pos.Y, 2 * 16, 2 * 16);


            spriteBatch.Draw(texture, destinationRectangle, sourceArray[frameState], Color.White);
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