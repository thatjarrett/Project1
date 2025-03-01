using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project1.Collision
{
    public class CollisionBox
    {
        public CollisionSide side = CollisionSide.None;
        public Rectangle hitbox { get; private set; }
        public CollisionBox(int x, int y, int width = 48, int height = 48)
        {
            hitbox = new Rectangle(x,y,width, height);
        }

        public void Move(int dx,int dy)
        {

            hitbox = new Rectangle(hitbox.X+dx,hitbox.Y+dy,hitbox.Width,hitbox.Height);
        }

        public void setPos(int dx, int dwhy) {
            hitbox = new Rectangle(dx,dwhy, hitbox.Width, hitbox.Height);
        }

        public bool Intersects(CollisionBox collider)
        {
            return hitbox.Intersects(collider.hitbox);
        }

        public int GetSidePush(CollisionBox collider) {
            side = CollisionSide.None;
            int minimumCollision = 0;
            if (hitbox.Intersects(collider.hitbox))
            {
                int topCollide = hitbox.Bottom - collider.hitbox.Top;
                int leftCollide = hitbox.Right-collider.hitbox.Left;
                int rightCollide = collider.hitbox.Right-hitbox.Left;
                int bottomCollide = collider.hitbox.Bottom - hitbox.Top;

                minimumCollision = Math.Min(Math.Min(leftCollide,rightCollide),Math.Min(topCollide,bottomCollide));

                if (minimumCollision == topCollide)
                {
                    side = CollisionSide.Top;
                }
                else if (minimumCollision == leftCollide)
                {
                    side = CollisionSide.Left;
                }
                else if (minimumCollision == rightCollide)
                {
                    side = CollisionSide.Right;
                }
                else if (minimumCollision == bottomCollide)
                {
                    side = CollisionSide.Bottom;
                }

            }

            return minimumCollision; 
        }
        public Vector2 GetCenter()
        {
            return new Vector2(hitbox.X + hitbox.Width / 2, hitbox.Y + hitbox.Height / 2);
        }

        public void DebugDraw(SpriteBatch spriteBatch, Texture2D pixel, Rectangle rect, Color color)
        {
            spriteBatch.Draw(pixel, new Rectangle(rect.X, rect.Y, rect.Width, 1), color);
            
            spriteBatch.Draw(pixel, new Rectangle(rect.X, rect.Y + rect.Height, rect.Width, 1), color);
            
            spriteBatch.Draw(pixel, new Rectangle(rect.X, rect.Y, 1, rect.Height), color);
            
            spriteBatch.Draw(pixel, new Rectangle(rect.X + rect.Width, rect.Y, 1, rect.Height), color);
        }
    }   
}
