using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Collision;
using Project1.Interfaces;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;


namespace Project1.GameObjects.Items
{
    public class Rupee : IItem
    {
        Texture2D texture;
        Rectangle destinationRectangle;
        int frameState = 0;
        Vector2 Pos;

        private CollisionBox collider;

        public Rupee(Texture2D texture)
        {
            this.texture = texture;
            this.Pos = new Vector2(200, 300);
            collider = new CollisionBox((int)Pos.X, (int)Pos.Y);
        }
        public void Draw(SpriteBatch spriteBatch, SpriteEffects spriteEffects)
        {

            Rectangle[] sourceArray = new Rectangle[2];
            sourceArray[0] = new Rectangle(72, 0, 8, 16);
            sourceArray[1] = new Rectangle(72, 16, 8, 16);
            destinationRectangle = new Rectangle((int)Pos.X, (int)Pos.Y, 2 * 8, 2 * 16);



            spriteBatch.Draw(texture, destinationRectangle, sourceArray[frameState], Color.White);
        }
        public void Draw(SpriteBatch spriteBatch,Vector2 location, SpriteEffects spriteEffects)
        {

            Rectangle[] sourceArray = new Rectangle[2];
            sourceArray[0] = new Rectangle(72, 0, 8, 16);
            sourceArray[1] = new Rectangle(72, 16, 8, 16);
            destinationRectangle = new Rectangle((int)location.X, (int)location.Y, 2 * 8, 2 * 16);



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