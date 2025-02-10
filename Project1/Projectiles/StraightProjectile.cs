using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Project1.Interfaces;
using System.Runtime.CompilerServices;

namespace Project1.Projectiles
{
    public class StraightProjectile : Projectile
    {
        private Vector2 _position;
        private Vector2 _velocity;
        private ISprite _sprite;

        public StraightProjectile(Vector2 pos, Vector2 direction, ISprite sprite1, ISprite sprite2, int magnitude):
            base(pos,direction,sprite1,sprite2,magnitude)
        {
            
        }
        
    }
}
