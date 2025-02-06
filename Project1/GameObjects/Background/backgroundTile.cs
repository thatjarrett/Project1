using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Project1.Interfaces;

namespace Project1.GameObjects.Background
{
    public interface backgroundTile
    {
        bool isTraversable();
        void update();
        void draw(SpriteBatch sb);
        int xPos();
        int whyPos();
        int getID();
        void setSprite(ISprite s);
    }
}
