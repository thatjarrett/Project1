using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Collision;
using Project1.Interfaces;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Loader;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;


namespace Project1.GameObjects.Items
{
    public class Bow : IItem
    {
        Texture2D texture;
        Rectangle destinationRectangle;
        Vector2 Pos;

        private CollisionBox collider;

        public Bow(Texture2D texture, Vector2 pos)
        {
            this.texture = texture;
            this.Pos = pos;
            collider = new CollisionBox((int)Pos.X, (int)Pos.Y, 16, 32);
        }
        public void Draw(SpriteBatch spriteBatch, SpriteEffects spriteEffects)
        {

            Rectangle source = new Rectangle(144, 0, 8, 16);
            destinationRectangle = new Rectangle((int)Pos.X, (int)Pos.Y, 2 * 8, 2 * 16);


            spriteBatch.Draw(texture, destinationRectangle, source, Color.White);
        }
        public void Draw(SpriteBatch spriteBatch,Vector2 location, SpriteEffects spriteEffects)
        {

            Rectangle source = new Rectangle(144, 0, 8, 16);
            destinationRectangle = new Rectangle((int)location.X, (int)location.Y, 2 * 8, 2 * 16);


            spriteBatch.Draw(texture, destinationRectangle, source, Color.White);
        }

        public void Update(GameTime gameTime)
        {


        }
        public void SetColor(Color _color)
        {
        }
        public void SetPosition(Vector2 Pos)
        {
            this.Pos = Pos;
        }
        public Vector2 getPosition() { return this.Pos; }

        public CollisionBox GetCollider()
        {
            return collider;
        }
    }
}