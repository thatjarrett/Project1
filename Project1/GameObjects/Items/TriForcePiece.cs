using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Interfaces;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;


namespace Project1.GameObjects.Items
{
    public class TriForcePiece : ISprite
    {
        Texture2D texture;
        Rectangle destinationRectangle;
        int frameState = 0;
        Vector2 Pos;

        public TriForcePiece(Texture2D texture, Vector2 Pos)
        {
            this.texture = texture;
            this.Pos = Pos;

        }
        public void Draw(SpriteBatch spriteBatch, SpriteEffects spriteEffects)
        {

            Rectangle[] sourceArray = new Rectangle[2];
            sourceArray[0] = new Rectangle(273, 3, 12, 12);
            sourceArray[1] = new Rectangle(273, 19, 12, 12);
            if (frameState == 0)
            {
                destinationRectangle = new Rectangle((int)Pos.X, (int)Pos.Y, 2 * 12, 2 * 12);
            }
            else if (frameState == 1)
            {

                destinationRectangle = new Rectangle((int)Pos.X, (int)Pos.Y, 2 * 12, 2 * 12);
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