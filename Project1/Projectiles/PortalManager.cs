using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Collision;
using Project1.Projectiles;

namespace Project1
{
    public class PortalManager
    {
        private PortalProjectile bluePortal;
        private PortalProjectile orangePortal;

        private Texture2D portalTexture;
        private Rectangle bluePortalRect;
        private Rectangle blueProjectileRect;
        private Rectangle bluePortalClosed;
        private Rectangle blueProjectileRectV;
        private Rectangle orangePortalRect;
        private Rectangle orangeProjectileRect;
        private Rectangle orangePortalClosed;
        private Rectangle orangeProjectileRectV;

        public PortalManager(Texture2D portalTexture,
                             Rectangle bluePortalRect, Rectangle blueProjectileRect, Rectangle bluePortalClosed, Rectangle blueProjectileRectV,
                             Rectangle orangePortalRect, Rectangle orangeProjectileRect, Rectangle orangePortalClosed, Rectangle orangeProjectileRectV)
        {
            this.portalTexture = portalTexture;
            this.bluePortalRect = bluePortalRect;
            this.blueProjectileRect = blueProjectileRect;
            this.blueProjectileRectV = blueProjectileRectV;
            this.orangePortalRect = orangePortalRect;
            this.orangeProjectileRect = orangeProjectileRect;
            this.orangeProjectileRectV = orangeProjectileRectV;
        }

        public void FireBlue(Vector2 start, Vector2 direction)
        {
            bluePortal = new PortalProjectile(start, direction, portalTexture, bluePortalRect, blueProjectileRect,bluePortalClosed,blueProjectileRectV);
            Vector2.Normalize(direction);

        }
        public CollisionBox GetBlueCollider() => bluePortal?.GetCollider();
        public void StopBluePortal() => bluePortal?.StopMoving();

        public void FireOrange(Vector2 start, Vector2 direction)
        {
            orangePortal = new PortalProjectile(start, direction, portalTexture, orangePortalRect, orangeProjectileRect,orangePortalClosed, orangeProjectileRectV);
            Vector2.Normalize(direction);
        }
        public CollisionBox GetOrangeCollider() => orangePortal?.GetCollider();
        public void StopOrangePortal() => orangePortal?.StopMoving();

        public void Update(GameTime gameTime)
        {
            bluePortal?.Update(gameTime);
            orangePortal?.Update(gameTime);

            if (bluePortal?.HasCollided() == true)
                bluePortal.StopMoving();
            if (orangePortal?.HasCollided() == true)
                orangePortal.StopMoving();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            bluePortal?.Draw(spriteBatch);
            orangePortal?.Draw(spriteBatch);
        }

        public bool HasValidPortals() => bluePortal != null && orangePortal != null;

        public Vector2? GetBluePosition() => bluePortal?.GetPosition();
        public Vector2? GetOrangePosition() => orangePortal?.GetPosition();
    }
}
