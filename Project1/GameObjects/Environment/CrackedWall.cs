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
            SetCollider();
        }

        public override void Break()
        {
            environmentTile replacement = null;
            if (!IsBroken)
            {
                IsBroken = true;


                //replacement = new doorTile(this._position);
                setSprite(this._bombedOpeningSprite);
                collider = null;
                //replacement.SetCollider();


                //_tileList[_index] = replacement;
            }
                        
        }
    }
}
