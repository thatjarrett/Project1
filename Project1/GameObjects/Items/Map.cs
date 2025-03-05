using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Collision;
using Project1.Interfaces;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;


namespace Project1.GameObjects.Items
{
    public class Map : IItem
    {
        Texture2D texture;
        Rectangle destinationRectangle;
        Vector2 Pos;

        private CollisionBox collider;

        public Map(Texture2D texture)
        {
            this.texture = texture;
            this.Pos = new Vector2(200, 300);
            collider = new CollisionBox((int)Pos.X, (int)Pos.Y);

        }
        public void Draw(SpriteBatch spriteBatch, SpriteEffects spriteEffects)
        {

            Rectangle source = new Rectangle(230, 0, 10, 16);
            destinationRectangle = new Rectangle((int)Pos.X, (int)Pos.Y, 2 * 10, 2 * 16);


            spriteBatch.Draw(texture, destinationRectangle, source, Color.White);
        }
        public void Draw(SpriteBatch spriteBatch,Vector2 location, SpriteEffects spriteEffects)
        {

            Rectangle source = new Rectangle(230, 0, 10, 16);
            destinationRectangle = new Rectangle((int)location.X, (int)location.Y, 2 * 10, 2 * 16);


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

        public CollisionBox GetCollider()
        {
            return collider;
        }
    }
}