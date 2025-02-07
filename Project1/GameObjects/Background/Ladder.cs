using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Project1.Interfaces;

namespace Project1.GameObjects.Background
{
    public class Ladder : backgroundTile
    {
        private int x;
        private int why;
        private ISprite sprite;

        public Ladder(int xVal, int whyVal) {
            x = xVal;
            why = whyVal;
        }
        public void draw(SpriteBatch sb)
        {
            Vector2 v = new Vector2(x, why);
            sprite.Draw(sb, v, SpriteEffects.None);
        }

        public bool isTraversable()
        {
            return true;
        }

        public void update()
        {
            //
        }

        public int whyPos()
        {
            return why;
        }

        public int xPos()
        {
            return x;
        }

        public int getID() {
            return 0;
        }

        public void setSprite(ISprite s) {
            sprite = s;
        }
    }
}
