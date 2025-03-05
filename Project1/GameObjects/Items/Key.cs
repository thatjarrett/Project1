using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Collision;
using Project1.Interfaces;
using System.Threading;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;


namespace Project1.GameObjects.Items
{
    public class Key : IItem
    {
        Texture2D texture;
        Rectangle destinationRectangle;
        Vector2 Pos;

        private CollisionBox collider;

        public Key(Texture2D texture)
        {
            this.texture = texture;
            this.Pos = new Vector2(200, 300);
            collider = new CollisionBox((int)Pos.X, (int)Pos.Y);
        }
        public void Draw(SpriteBatch spriteBatch, SpriteEffects spriteEffects)
        {

            Rectangle source = new Rectangle(240, 1, 9, 16);
            destinationRectangle = new Rectangle((int)Pos.X, (int)Pos.Y, 2 * 9, 2 * 16);


            spriteBatch.Draw(texture, destinationRectangle, source, Color.White);
        }
        public void Draw(SpriteBatch spriteBatch,Vector2 location, SpriteEffects spriteEffects)
        {

            Rectangle source = new Rectangle(240, 1, 9, 16);
            destinationRectangle = new Rectangle((int)location.X, (int)location.Y, 2 * 9, 2 * 16);


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

        public CollisionBox GetCollider() {
            return collider;
        }
    }
}