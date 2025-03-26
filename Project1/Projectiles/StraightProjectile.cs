using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Project1.Interfaces;
using Microsoft.Xna.Framework.Content;

namespace Project1.Projectiles
{
    public class StraightProjectile : Projectile
    {
        private Vector2 _position;
        private Vector2 _velocity;
        private ISprite _sprite;

        private static SoundEffect arrowSound;

        public static void LoadContent(ContentManager content)
        {
            arrowSound = content.Load<SoundEffect>("Audio/Arrow");
        }

        public StraightProjectile(Vector2 pos, Vector2 direction, ISprite sprite1, ISprite sprite2, int magnitude) :
            base(pos, direction, sprite1, sprite2, magnitude)
        {
            arrowSound?.Play();
        }
    }
}
