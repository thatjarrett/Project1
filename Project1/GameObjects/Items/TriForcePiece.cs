using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Collision;
using Project1.Interfaces;
using System.ComponentModel;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;


namespace Project1.GameObjects.Items
{
    public class TriForcePiece : IItem
    {
        Texture2D texture;
        Rectangle destinationRectangle;
        int frameState = 0;
        Vector2 Pos;

        private CollisionBox collider;

        public TriForcePiece(Texture2D texture, Vector2 pos)
        {
            this.texture = texture;
            this.Pos = pos;
            collider = new CollisionBox((int)Pos.X, (int)Pos.Y, 20, 20);
        }
        public void Draw(SpriteBatch spriteBatch, SpriteEffects spriteEffects)
        {

            Rectangle[] sourceArray = new Rectangle[2];
            sourceArray[0] = new Rectangle(275, 3, 12, 12);
            sourceArray[1] = new Rectangle(275, 19, 12, 12);
            destinationRectangle = new Rectangle((int)Pos.X, (int)Pos.Y, 2 * 12, 2 * 12);
            

            spriteBatch.Draw(texture, destinationRectangle, sourceArray[frameState], Color.White);
        }
        public void Draw(SpriteBatch spriteBatch,Vector2 location, SpriteEffects spriteEffects)
        {

            Rectangle[] sourceArray = new Rectangle[2];
            sourceArray[0] = new Rectangle(273, 3, 12, 12);
            sourceArray[1] = new Rectangle(273, 19, 12, 12);
            destinationRectangle = new Rectangle((int)location.X, (int)location.Y, 2 * 12, 2 * 12);


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