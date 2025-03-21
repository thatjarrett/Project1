﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Interfaces;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Project1.Collision;



namespace Project1.GameObjects.Items
{
    public abstract class abstractItem
    {
        ISprite sprite;
        Rectangle destinationRectangle;
        int frameState = 0;
        Vector2 pos;

        private CollisionBox collider;

        public abstractItem(Texture2D texture)
        {
            // this.texture = texture;
            this.pos = new Vector2(200, 300);
            collider = new CollisionBox((int)pos.X, (int)pos.Y);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //    sprite.Draw(spriteBatch, _position, SpriteEffects.None);
        }

        public void Update(GameTime gameTime)
        {
            if (sprite != null)
            {
                sprite.Update(gameTime);
            }
        }
        public void SetColor(Color _color)
        {
        }
        public void SetPosition(Vector2 pos)
        {
            this.pos = pos;
        }
        public Vector2 getPosition() { return this.pos; }
        public void SetSprite(ISprite sprite) { this.sprite = sprite; }


        public CollisionBox GetCollider()
        {
            return collider;
        }


    }
}

