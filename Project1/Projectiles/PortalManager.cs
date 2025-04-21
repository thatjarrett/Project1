using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Project1.Collision;
using Project1.Projectiles;

namespace Project1
{
    public class PortalManager
    {
        private PortalProjectile bluePortal;
        private PortalProjectile orangePortal;

        private readonly Texture2D texture;
        private readonly Rectangle bluePortalRect, blueProjectileRect, bluePortalClosedRect, blueProjectileVRect;
        private readonly Rectangle orangePortalRect, orangeProjectileRect, orangePortalClosedRect, orangeProjectileVRect;

        private readonly SoundEffect bluePortalSound;
        private readonly SoundEffect orangePortalSound;

        public PortalManager(Texture2D texture,
                             Rectangle bluePortalRect, Rectangle blueProjectileRect, Rectangle bluePortalClosedRect, Rectangle blueProjectileVRect,
                             Rectangle orangePortalRect, Rectangle orangeProjectileRect, Rectangle orangePortalClosedRect, Rectangle orangeProjectileVRect,
                             SoundEffect bluePortalSound, SoundEffect orangePortalSound)
        {
            this.texture = texture;

            this.bluePortalRect = bluePortalRect;
            this.blueProjectileRect = blueProjectileRect;
            this.bluePortalClosedRect = bluePortalClosedRect;
            this.blueProjectileVRect = blueProjectileVRect;

            this.orangePortalRect = orangePortalRect;
            this.orangeProjectileRect = orangeProjectileRect;
            this.orangePortalClosedRect = orangePortalClosedRect;
            this.orangeProjectileVRect = orangeProjectileVRect;

            this.bluePortalSound = bluePortalSound;
            this.orangePortalSound = orangePortalSound;
        }

        public void FireBlue(Vector2 start, Vector2 direction)
        {
            bluePortal = new PortalProjectile(start, direction, texture, bluePortalRect, blueProjectileRect, bluePortalClosedRect, blueProjectileVRect, bluePortalSound);
        }

        public void FireOrange(Vector2 start, Vector2 direction)
        {
            orangePortal = new PortalProjectile(start, direction, texture, orangePortalRect, orangeProjectileRect, orangePortalClosedRect, orangeProjectileVRect, orangePortalSound);
        }

        public void StopBluePortal() => bluePortal?.StopMoving();
        public void StopOrangePortal() => orangePortal?.StopMoving();

        public CollisionBox GetBlueCollider() => bluePortal?.GetCollider();
        public CollisionBox GetOrangeCollider() => orangePortal?.GetCollider();

        public Vector2? GetBluePosition() => bluePortal?.GetPosition();
        public Vector2? GetOrangePosition() => orangePortal?.GetPosition();

        public bool HasValidPortals() => bluePortal != null && orangePortal != null;

        public void Update(GameTime gameTime)
        {
            bluePortal?.Update(gameTime);
            orangePortal?.Update(gameTime);

            if (bluePortal?.HasCollided() == true)
                bluePortal.StopMoving();

            if (orangePortal?.HasCollided() == true)
                orangePortal.StopMoving();

            if (HasValidPortals())
            {
                bluePortal.setOpen();
                orangePortal.setOpen();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            bluePortal?.Draw(spriteBatch);
            orangePortal?.Draw(spriteBatch);
        }
    }
}
