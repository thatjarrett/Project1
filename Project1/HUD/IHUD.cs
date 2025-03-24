using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Entities;
using Project1.Interfaces;
using Project1.Sprites;

namespace Project1.HUD
{
    public class IHUD
    {
        const int MINHEIGHT = -183;
        const int MAXHEIGHT = 0;
        const int SLIDERATE = 15;
        Link _link;
        int height = 0;

        ISprite hudSprite;
        public IHUD(Link link,Texture2D texture)
        {
            _link = link;
            hudSprite = new NMoveNAnim(texture, new Rectangle(0, 0, 256, 224));
        }
        public void Update(GameTime gameTime)
        {

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            hudSprite.Draw(spriteBatch, new Vector2(0,height), SpriteEffects.None);
        }
        public void slideIn()
        {
            if (height < MAXHEIGHT)
            {
                height += SLIDERATE;
            }
            else
            {
                height = MAXHEIGHT;
            }

        }
        public void slideOut()
        {
            if (height > MINHEIGHT)
            {
                height -= SLIDERATE;
            }
            else
            {
                height = MINHEIGHT;
            }
        }
    }
}
