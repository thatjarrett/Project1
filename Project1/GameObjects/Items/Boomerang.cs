using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Interfaces;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;


namespace Project1.GameObjects.Items
{
    public class Boomerang : ISprite
    {
        Texture2D texture;
        Rectangle destinationRectangle;
        int frameState = 0;
        Vector2 Pos;


        public Boomerang(Texture2D texture, Vector2 Pos)
        {
            this.texture = texture;
            this.Pos = Pos;

        }
        public void Draw(SpriteBatch spriteBatch, SpriteEffects spriteEffects)
        {

            Rectangle[] sourceArray = new Rectangle[2];
            sourceArray[0] = new Rectangle(129, 3, 5, 8);
            sourceArray[1] = new Rectangle(129, 19, 5, 8);
            if (frameState == 0)
            {
                destinationRectangle = new Rectangle((int)Pos.X, (int)Pos.Y, 2 * 5, 2 * 8);
            }
            else if (frameState == 1)
            {

                destinationRectangle = new Rectangle((int)Pos.X, (int)Pos.Y, 2 * 5, 2 * 8);
            }

            spriteBatch.Draw(texture, destinationRectangle, sourceArray[frameState], Color.White);
        }

        public void Update(GameTime gameTime)
        {
            frameState = (gameTime.TotalGameTime.Seconds) % 2;

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