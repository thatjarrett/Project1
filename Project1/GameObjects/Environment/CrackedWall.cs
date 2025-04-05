using Microsoft.Xna.Framework;
using Project1.Interfaces;
using Project1.GameObjects.Environment;
using System.Collections.Generic;

namespace Project1.GameObjects.Environment
{
    public class CrackedWallTile : environmentTile
    {
        private ISprite _bombedOpeningSprite;
        //private int _index;

        public bool IsBroken { get; private set; }

        public CrackedWallTile(Vector2 location, ISprite bombedOpeningSprite)
            : base(location, true)
        {
            this._bombedOpeningSprite = bombedOpeningSprite;
            //this._index = index;

            this.IsSolid = true;
            this.IsBreakable = true;
            this.IsBroken = false;
        }

        public override void Break()
        {
            if (!IsBroken)
            {
                IsBroken = true;

               
                environmentTile replacement = new doorTile(this._position);
                replacement.setSprite(this._bombedOpeningSprite);
                replacement.SetCollider(); 

               
                //_tileList[_index] = replacement;
            }
        }
    }
}
