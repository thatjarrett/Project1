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
        const int MINHEIGHT = -549;
        const int MAXHEIGHT = 0;
        const int SLIDERATE = -MINHEIGHT/60;

        Vector2 HEARTOFFSET = new Vector2(551, 621);
        Vector2 COVEROFFSET = new Vector2(495, 621);
        Link _link;
        int height = MINHEIGHT;
        int linkHealth = 10;
        ISprite hudSprite;
        ISprite heartsSprite;
        ISprite coverSprite;
        SpriteEffects heartEffect = SpriteEffects.None;
        public IHUD(Link link,Texture2D texture, Texture2D hearts, Texture2D cover)
        {
            _link = link;
            hudSprite = new NMoveNAnim(texture, new Rectangle(0, 0, 256, 224));
            heartsSprite = new NMoveNAnim(hearts, new Rectangle(0, 0, 47, 8));
            coverSprite = new NMoveNAnim(cover, new Rectangle(0, 0, 91,8 ));
        }
        public void Update(GameTime gameTime)
        {
            linkHealth = _link.GetHealth();
            if (linkHealth%2 == 0){
                heartEffect = SpriteEffects.FlipHorizontally;
            }
           else
            {
                heartEffect = SpriteEffects.None;
            }

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            hudSprite.Draw(spriteBatch, new Vector2(0,height), SpriteEffects.None);
            heartsSprite.Draw(spriteBatch, new Vector2(24*MathF.Floor(linkHealth/2), height)+HEARTOFFSET, heartEffect);
            coverSprite.Draw(spriteBatch, new Vector2(0, height)+COVEROFFSET, SpriteEffects.None);
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
