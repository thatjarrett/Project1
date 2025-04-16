using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1.Collision;
using Project1.GameObjects.Items;
using Project1.Interfaces;

using Project1.Projectiles;
using Project1.Sprites;

namespace Project1.Entities
{
    public partial class Link
    {
        private const double InvincibilityDuration = 1.0;
        private const double KnockbackDuration = 0.2;
        private const int KnockbackForce = 2;

        private ILinkState currentState;
        private Vector2 position;
        private Vector2 faceDirection;

        private bool isInvincible;
        private double invincibleTime;

        private bool isControlsDisabled;
        private bool isVisible = true;

        private bool isKnockback;
        private Vector2 knockbackDirection;
        private double knockbackTimer;

        private bool dying;
        private int deathFrameCounter;

        private bool hurting;
        private int damageFrameCounter;

        private bool freezeEnemies;
        private double freezeTimer;

        private int health;
        private int maxHealth = 10;
        private int bombCount;
        private int rupeeCount;
        public int keyCount;
        private int arrowCount;

        private int currentItemIndex = 2;

        private bool hasBow;
        private bool hasBoomerang;
        private bool hasCompass;
        private bool hasMap;
        private bool hasTriforce;

        private ISprite linkSprite;

        private ISprite walkSideSprite, walkUpSprite, walkDownSprite;
        private ISprite idleSideSprite, idleUpSprite, idleDownSprite, currentIdleSprite;
        private ISprite attackSideSprite, attackSideSprite2, attackUpSprite, attackDownSprite, currentAttackSprite;
        private ISprite interactSideSprite, interactUpSprite, interactDownSprite, currentInteractSprite;
        private ISprite deathSprite;
        private SpriteEffects currentSpriteEffect;

        private ISprite swordBeamHorizontal, swordBeamVertical;
        private ISprite arrowHorizontal, arrowVertical;
        private ISprite boomerangSprite, bombSprite, explodingBombSprite;

        private BoomerangProjectile boomerangProjectile;
        private List<IProjectile> projectiles = new();
        private List<IProjectile> bombs = new();

        private CollisionBox collider;
        private CollisionBox swordCollision;

        private Collection<IItem> inventory = new();

        public Direction PreviousDirection { get; private set; } = Direction.Down;

        public Link(Vector2 startPosition)
        {
            position = startPosition;
            currentState = new LinkIdleState(Direction.Down);
            collider = new CollisionBox((int)startPosition.X, (int)startPosition.Y);
            swordCollision = null;
            health = maxHealth;
        }
        private bool firingBluePortal = false;
        private bool firingOrangePortal = false;
        private PortalManager portalManager;

        public void SetPortalManager(PortalManager manager)
        {
            this.portalManager = manager;
        }
 
        public SpriteEffects GetSpriteEffect()
        {
            return currentSpriteEffect;
        }
        public void ChangeState(ILinkState newState)
        {
            //Debug.WriteLine($"Changing state to: {newState.GetType().Name}");
            if (newState is LinkMoveUpState) PreviousDirection = Direction.Up;
            if (newState is LinkMoveDownState) PreviousDirection = Direction.Down;
            if (newState is LinkMoveLeftState) PreviousDirection = Direction.Left;
            if (newState is LinkMoveRightState) PreviousDirection = Direction.Right;

            currentState = newState;
            currentState.Enter(this);
        }
        public void TeleportTo(Vector2 newPosition)
        {
            position = newPosition;
            collider.setPos((int)position.X, (int)position.Y);
        }

        public Vector2 GetCenterPos()
        {
            //set offset to half of resolution
            return position + new Vector2(-384, -324);
        }

        public class StartBluePortalCommand : ICommand
        {
            private Link link;
            public StartBluePortalCommand(Link l) => link = l;
            public void Execute() => link.BeginFireBluePortal();
        }

        public class EndBluePortalCommand : ICommand
        {
            private Link link;
            public EndBluePortalCommand(Link l) => link = l;
            public void Execute() => link.EndFireBluePortal();
        }
        public class StartOrangePortalCommand : ICommand
        {
            private Link link;
            public StartOrangePortalCommand(Link l) => link = l;
            public void Execute() => link.BeginFireOrangePortal();
        }

        public class EndOrangePortalCommand : ICommand
        {
            private Link link;
            public EndOrangePortalCommand(Link l) => link = l;
            public void Execute() => link.EndFireOrangePortal();
        }
        private bool isFiringBlue = false;
        private bool isFiringOrange = false;
        private Vector2 fireDirection;

       

        public void BeginFireBluePortal()
        {
            isFiringBlue = true;
            fireDirection = faceDirection; // or whatever direction Link is facing
        }

        public void EndFireBluePortal()
        {
            Debug.WriteLine("Firing blue portal!");
            if (isFiringBlue)
            {
                Vector2 spawnPos = position + fireDirection * 32; // offset from Link
                portalManager.FireBlue(spawnPos, fireDirection);
                isFiringBlue = false;
            }
        }

        public void BeginFireOrangePortal()
        {
            isFiringOrange = true;
            fireDirection = faceDirection;
        }

        public void EndFireOrangePortal()
        {
            Debug.WriteLine("Firing orange portal!");
            if (isFiringOrange)
            {
                Vector2 spawnPos = position + fireDirection * 32;
                portalManager.FireOrange(spawnPos, fireDirection);
                isFiringOrange = false;
            }
        }

        public void Hide()
        {
            isVisible = false;
        }

        public void DisableControls()
        {
            isControlsDisabled = true;
            Debug.WriteLine("Controls disabled.");
        }

        public void CreateLinkSprites(Texture2D linkTexture)
        {
            Rectangle[] walkSide = { new Rectangle(2, 29, 15, 16), new Rectangle(18, 30, 16, 16) };
            Rectangle[] walkUp = { new Rectangle(69, 11, 16, 16), new Rectangle(86, 11, 16, 16) };
            Rectangle[] walkDown = { new Rectangle(1, 11, 16, 16), new Rectangle(18, 11, 16, 16) };

            Rectangle[] attackSide = {
        new Rectangle(270, 217, 27, 16), new Rectangle(270, 234, 27, 16),
        new Rectangle(270, 252, 27, 16), new Rectangle(270, 271, 27, 16)
    };
            Rectangle[] attackUp = {
        new Rectangle(1, 97, 16, 28), new Rectangle(18, 97, 16, 28),
        new Rectangle(35, 97, 16, 28), new Rectangle(52, 97, 16, 28)
    };
            Rectangle[] attackDown = {
        new Rectangle(1, 47, 16, 16), new Rectangle(18, 47, 16, 27),
        new Rectangle(35, 47, 16, 23), new Rectangle(53, 47, 16, 19)
    };

            Rectangle[] swordBeamV = {
        new Rectangle(0, 154, 9, 16), new Rectangle(105, 154, 9, 16)
    };

            Rectangle[] swordBeamH = {
        new Rectangle(10, 158, 16, 9), new Rectangle(115, 158, 16, 9)
    };

            Rectangle[] boomerang = {
        new Rectangle(65, 185, 5, 16), new Rectangle(73, 185, 8, 16),
        new Rectangle(82, 185, 8, 16)
    };

            Rectangle[] bombExplode = {
        new Rectangle(138, 185, 16, 16), new Rectangle(155, 185, 16, 16),
        new Rectangle(172, 185, 16, 16)
    };


            walkSideSprite = new NMoveAnim(linkTexture, walkSide, 5);
            walkUpSprite = new NMoveAnim(linkTexture, walkUp, 5);
            walkDownSprite = new NMoveAnim(linkTexture, walkDown, 5);

            idleSideSprite = new NMoveNAnim(linkTexture, new Rectangle(2, 29, 15, 16));
            idleUpSprite = new NMoveNAnim(linkTexture, new Rectangle(69, 11, 16, 16));
            idleDownSprite = new NMoveNAnim(linkTexture, new Rectangle(1, 11, 16, 16));

            attackSideSprite = new NMoveAnim(linkTexture, attackSide, 5);
            attackSideSprite2 = new NMoveAnim(linkTexture, attackSide, 5, 3, new Vector2(12, 0));
            attackUpSprite = new NMoveAnim(linkTexture, attackUp, 5, 3, new Vector2(0, 12));
            attackDownSprite = new NMoveAnim(linkTexture, attackDown, 5);

            interactSideSprite = new NMoveNAnim(linkTexture, new Rectangle(124, 11, 15, 16));
            interactUpSprite = new NMoveNAnim(linkTexture, new Rectangle(141, 11, 15, 16));
            interactDownSprite = new NMoveNAnim(linkTexture, new Rectangle(107, 11, 16, 16));

            swordBeamHorizontal = new NMoveAnim(linkTexture, swordBeamH, 5);
            swordBeamVertical = new NMoveAnim(linkTexture, swordBeamV, 5);

            arrowVertical = new NMoveNAnim(linkTexture, new Rectangle(0, 185, 10, 16));
            arrowHorizontal = new NMoveNAnim(linkTexture, new Rectangle(10, 185, 16, 15));

            boomerangSprite = new NMoveAnim(linkTexture, boomerang, 5);

            bombSprite = new NMoveNAnim(linkTexture, new Rectangle(128, 184, 10, 16));

            explodingBombSprite = new NMoveAnim(linkTexture, bombExplode, 5);

            currentIdleSprite = idleDownSprite;
            currentAttackSprite = attackDownSprite;
            currentInteractSprite = interactDownSprite;

            linkSprite = currentIdleSprite;
            faceDirection = new Vector2(0, 1);

            
        }

    }
}
