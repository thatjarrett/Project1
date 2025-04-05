using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Collision;
using Project1.Interfaces;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;


namespace Project1.GameObjects.Animations
{
    public class EnemyDeathCloud : IAnimation
    {
        Texture2D texture;
        Rectangle destinationRectangle;
        int frameState = 0;
        Vector2 Pos;

        bool active = true;

        int timer = 0;

        public EnemyDeathCloud(Texture2D texture, Vector2 pos)
        {
            this.texture = texture;
            Pos = pos;
        }

        public void Update(GameTime gameTime)
        {
            frameState = gameTime.TotalGameTime.Milliseconds / 100 % 7;
            timer++;
            if (timer == 20) {
                active = false;
            }

        }
        public void SetColor(Color _color)
        {
        }
        public void SetPosition(Vector2 pos)
        {
            Pos = pos;
        }
        public Vector2 getPosition() { return Pos; }

        public void Draw(SpriteBatch spriteBatch, SpriteEffects spriteEffects)
        {

            Rectangle[] sourceArray = new Rectangle[7];
            sourceArray[0] = new Rectangle(32, 0, 16, 16);
            sourceArray[1] = new Rectangle(48, 0, 16, 16);
            sourceArray[2] = new Rectangle(0, 0, 16, 16);
            sourceArray[3] = new Rectangle(16, 0, 16, 16);
            sourceArray[4] = new Rectangle(0, 0, 16, 16);
            sourceArray[5] = new Rectangle(48, 0, 16, 16);
            sourceArray[6] = new Rectangle(32, 0, 16, 16);
            destinationRectangle = new Rectangle((int)Pos.X, (int)Pos.Y, 2 * 16, 2 * 16);



            spriteBatch.Draw(texture, destinationRectangle, sourceArray[frameState], Color.White);
        }

        public bool isActive()
        {
            return active;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location, SpriteEffects spriteEffects)
        {
            Rectangle[] sourceArray = new Rectangle[7];
            sourceArray[0] = new Rectangle(32, 0, 16, 16);
            sourceArray[1] = new Rectangle(48, 0, 16, 16);
            sourceArray[2] = new Rectangle(0, 0, 16, 16);
            sourceArray[3] = new Rectangle(16, 0, 16, 16);
            sourceArray[4] = new Rectangle(0, 0, 16, 16);
            sourceArray[5] = new Rectangle(48, 0, 16, 16);
            sourceArray[6] = new Rectangle(32, 0, 16, 16);
            destinationRectangle = new Rectangle((int)location.X, (int)location.Y, 2 * 16, 2 * 16);



            spriteBatch.Draw(texture, destinationRectangle, sourceArray[frameState], Color.White);
        }
    }
}