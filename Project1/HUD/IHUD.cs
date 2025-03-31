using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Entities;
using Project1.Interfaces;
using Project1.Sprites;

namespace Project1.HUD
{
    public class IHUD
    {
        private static double lastMoveTime = 0;
        private const double moveCooldown = 0.2;

        private const int MINHEIGHT = -549;
        private const int MAXHEIGHT = -25;
        private const int SLIDERATE = -MINHEIGHT/60;

        private Vector2 selectorPosition = new Vector2(0, 0);
        private Vector2 selectorOffset = new Vector2(384, 141);
        private Vector2 SELECTORMULT = new Vector2(72,48);
        private int inventoryPosition;

        private Vector2 HEARTOFFSET = new Vector2(551, 621);
        private Vector2 COVEROFFSET = new Vector2(495, 621);
        private Vector2 RUPEECOUNTPOS = new Vector2(300, 565);
        private Vector2 KEYCOUNTPOS = new Vector2(315, 600);
        private Vector2 BOMBCOUNTPOS = new Vector2(315, 635);
        private Vector2 DRAWOFFSET = new Vector2(0, 0);

        private Link _link;
        private int height = MINHEIGHT;
        private int linkHealth = 10;
        private ISprite hudSprite;
        private ISprite heartsSprite;
        private ISprite coverSprite;
        private ISprite selectorSprite;
        private SpriteEffects heartEffect;

        private bool canMove = true;
        public bool active = false;

        private TextSprite _rupeeCount;
        private TextSprite _bombCount;
        private TextSprite _keyCount;
        public IHUD(Link link,Texture2D texture, Texture2D hearts, Texture2D cover,Texture2D atlas, SpriteFont font)
        {
            _link = link;
            hudSprite = new NMoveNAnim(texture, new Rectangle(0, 0, 256, 224));
            selectorSprite = new NMoveNAnim(atlas, new Rectangle(128, 47, 16, 16));
            heartsSprite = new NMoveNAnim(hearts, new Rectangle(0, 0, 47, 8));
            coverSprite = new NMoveNAnim(cover, new Rectangle(0, 0, 91,8 ));
            _rupeeCount = new TextSprite("", font, Vector2.Zero);
            _bombCount = new TextSprite("", font, Vector2.Zero);
            _keyCount = new TextSprite("", font, Vector2.Zero);
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
            _rupeeCount._text = _link.GetRupeeCount().ToString();
            _bombCount._text = _link.GetBombCount().ToString();
            _keyCount._text = _link.GetKeyCount().ToString();
            DRAWOFFSET.Y = height;

            double currentTime = GameTimer.TotalGameTime;
            if (currentTime - lastMoveTime >= moveCooldown)
            {
                canMove = true;
                
            }
            else
            {
                canMove = false;    
            }
            inventoryPosition = (int)(selectorPosition.X + 4*selectorPosition.Y);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            hudSprite.Draw(spriteBatch, new Vector2(0,height), SpriteEffects.None);
            heartsSprite.Draw(spriteBatch, new Vector2(24*MathF.Floor(linkHealth/2), height)+HEARTOFFSET, heartEffect);
            coverSprite.Draw(spriteBatch, new Vector2(0, height)+COVEROFFSET, SpriteEffects.None);
            selectorSprite.Draw(spriteBatch,DRAWOFFSET+selectorOffset+(SELECTORMULT*selectorPosition),SpriteEffects.None);
            _rupeeCount.Draw(spriteBatch, new Vector2(0, height) + RUPEECOUNTPOS,SpriteEffects.None);
            _bombCount.Draw(spriteBatch, new Vector2(0, height) + BOMBCOUNTPOS, SpriteEffects.None);
            _keyCount.Draw(spriteBatch, new Vector2(0, height) + KEYCOUNTPOS, SpriteEffects.None);
        }
        public void slideIn()
        {
            active = true;
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
            active = false;
        }
        public void moveSelectorUp()
        {
            if (canMove)
            {
                lastMoveTime = GameTimer.TotalGameTime;
                selectorPosition.Y = (selectorPosition.Y - 1) % 2;
                if(selectorPosition.Y < 0)
                {
                    selectorPosition.Y = 1;
                }
            }
            
        }
        public void moveSelectorDown()
        {
            if (canMove)
            {
                lastMoveTime = GameTimer.TotalGameTime;
                selectorPosition.Y = (selectorPosition.Y + 1) % 2;
            }
        }
        public void moveSelectorLeft()
        {
            if (canMove)
            {
                lastMoveTime = GameTimer.TotalGameTime;
                selectorPosition.X = ((selectorPosition.X - 1) % 4);
                if (selectorPosition.X < 0)
                {
                    selectorPosition.X = 3;
                }
            }
        }
        public void moveSelectorRight()
        {
            if (canMove)
            {
                lastMoveTime = GameTimer.TotalGameTime;
                selectorPosition.X = (selectorPosition.X + 1) % 4;
            }
        }
    }
}
