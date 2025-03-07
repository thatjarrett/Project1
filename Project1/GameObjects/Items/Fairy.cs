using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Collision;
using Project1.Interfaces;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;


namespace Project1.GameObjects.Items
{
    public class Fairy : IItem
    {
        Texture2D texture;
        Rectangle destinationRectangle;
        int frameState = 0;
        Vector2 Pos;

        private CollisionBox collider;


        public Fairy(Texture2D texture, Vector2 pos)
        {
            this.texture = texture;
            this.Pos = pos;
            collider = new CollisionBox((int)Pos.X, (int)Pos.Y, 18, 32);
        }
        public void Draw(SpriteBatch spriteBatch, SpriteEffects spriteEffects)
        {

            Rectangle[] sourceArray = new Rectangle[2];
            sourceArray[0] = new Rectangle(39, 0, 9, 16);
            sourceArray[1] = new Rectangle(47, 0, 9, 16);
            destinationRectangle = new Rectangle((int)Pos.X, (int)Pos.Y, 2 * 9, 2 * 16);

            spriteBatch.Draw(texture, destinationRectangle, sourceArray[frameState], Color.White);
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 location, SpriteEffects spriteEffects)
        {

            Rectangle[] sourceArray = new Rectangle[2];
            sourceArray[0] = new Rectangle(39, 0, 9, 16);
            sourceArray[1] = new Rectangle(47, 0, 9, 16);
            destinationRectangle = new Rectangle((int)location.X, (int)location.Y, 2 * 9, 2 * 16);

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
            this.Pos = pos;
        }
        public Vector2 getPosition() { return this.Pos; }

        public CollisionBox GetCollider()
        {
            return collider;
        }
    }
}