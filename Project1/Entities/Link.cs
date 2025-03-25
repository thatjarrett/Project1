using System.Collections;
using System.Collections.Generic;
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
    public class Link
    {
        private ILinkState currentState;
        private Vector2 position;
        private Vector2 faceDirection;
        private bool isInvincible = false;
        private double invincibleTime = 0;
        private const double InvincibilityDuration = 1.0; // 1 second
        private bool isControlsDisabled = false; // Flag to disable controls
        private bool isVisible = true;
        private ISprite linkSprite;

        private ISprite walkSideSprite;
        private ISprite walkUpSprite;
        private ISprite walkDownSprite;

        private ISprite currentIdleSprite;
        private ISprite idleSideSprite;
        private ISprite idleUpSprite;
        private ISprite idleDownSprite;

        private ISprite currentAttackSprite;
        private ISprite attackSideSprite;
        private ISprite attackSideSprite2;
        private ISprite attackUpSprite;
        private ISprite attackDownSprite;
        private ISprite deathSprite;
        private bool dying = false;
        private int deathFrameCounter = 0;


        private ISprite currentInteractSprite;
        private ISprite interactSideSprite;
        private ISprite interactUpSprite;
        private ISprite interactDownSprite;

        private bool isKnockback = false;
        private Vector2 knockbackDirection;
        private double knockbackTime = 0.2;
        private double knockbackTimer = 0;


        private ISprite swordBeamHorizontal;
        private ISprite swordBeamVertical;
        private ISprite arrowHorizontal;
        private ISprite arrowVertical;
        private ISprite boomerang;
        private ISprite bombSprite;
        private ISprite explodingBombSprite;

        private int bombCount = 0;
        private int health = 10;
        private int ruppeeCount = 0;

        private BoomerangProjectile boomerangThrowable;
        //private BombProjectile bombProjectile;

        private SpriteEffects currentSpriteEffect = SpriteEffects.None;

        int damageFrameCounter = 0;
        bool hurting = false;

        private List<IProjectile> projectilesList = new List<IProjectile>();

        private CollisionBox collider;

        private CollisionBox swordCollision;
        private List<IProjectile> bombs = new List<IProjectile>();

        public Link(Vector2 startPos)
        {
            position = startPos;
            currentState = new LinkIdleState(Direction.Down); // Start in Idle state
            collider = new CollisionBox((int)startPos.X, (int)startPos.Y);

            swordCollision = null;
        }
        public Direction PreviousDirection { get; private set; } = Direction.Down;



        public void ChangeState(ILinkState newState)
        {
            Debug.WriteLine($"Changing state to: {newState.GetType().Name}");
            if (newState is LinkMoveUpState) PreviousDirection = Direction.Up;
            if (newState is LinkMoveDownState) PreviousDirection = Direction.Down;
            if (newState is LinkMoveLeftState) PreviousDirection = Direction.Left;
            if (newState is LinkMoveRightState) PreviousDirection = Direction.Right;

            // Change the current state
            currentState = newState;
            currentState.Enter(this);
        }

        public void SetInvincible(bool value, Vector2? knockbackDir = null)
        {
            isInvincible = value;
            if (value)
            {
                invincibleTime = InvincibilityDuration;

                
                if (knockbackDir.HasValue)
                {
                    knockbackDirection = knockbackDir.Value;
                    isKnockback = true;
                    knockbackTimer = knockbackTime;
                }
            }
        }

        public void clearSword() {
            swordCollision = null;
        }

        public CollisionBox getSword() {
            return swordCollision;
        }

        public void setSword(Direction d) {
            switch (d) {
                case Direction.Down:
                    swordCollision = new CollisionBox((int)position.X, (int)position.Y + 24);
                    break;
                case Direction.Up:
                    swordCollision = new CollisionBox((int)position.X,(int)position.Y - 24);
                    break;
                case Direction.Left:
                    swordCollision = new CollisionBox((int)position.X - 24, (int)position.Y);
                    break;
                case Direction.Right:
                    swordCollision = new CollisionBox((int)position.X + 24, (int) position.Y);
                    break;
            }

        }


        public void MoveLeft()
        {
            if (isControlsDisabled) return;
            currentState.MoveLeft(this);
        }

        public void MoveRight()
        {
            if (isControlsDisabled) return;
            currentState.MoveRight(this);
        }

        public void MoveUp()
        {
            if (isControlsDisabled) return;
            currentState.MoveUp(this);
        }

        public void MoveDown()
        {
            if (isControlsDisabled) return;
            currentState.MoveDown(this);
        }

        public void PerformAttack()
        {
            if (isControlsDisabled) return;
            currentState.Attack(this);
        }

        public void DisableControls()
        {
            isControlsDisabled = true;
            Debug.WriteLine("Controls disabled.");
        }

        public void EnableControls()
        {
            isControlsDisabled = false;
            Debug.WriteLine("Controls enabled.");
        }
        public void TriggerGameOver()
        {
            Debug.WriteLine("Game Over triggered.");
            DisableControls();

           

        }
        public void Hide()
        {
            isVisible = false;
        }

        public void Item(int itemNumber)
        {
            if (isControlsDisabled) return;
            currentState.Item(this, itemNumber);
            Projectile projectile = null;
            IProjectile bomb = null;
            switch (itemNumber)
            {
                case 1:
                    projectile = new StraightProjectile(position, faceDirection, swordBeamHorizontal, swordBeamVertical, 5);
                    break;
                case 2:
                    projectile = new StraightProjectile(position, faceDirection, arrowHorizontal, arrowVertical, 5);
                    break;
                case 3:
                    boomerangThrowable.Throw(position, faceDirection);
                    break;
                case 4:
                    //bombProjectile.placeBomb(position);
                    if (bombCount > 0)
                    {
                        bomb = new BombProjectile(position, bombSprite, explodingBombSprite, this);
                        bombCount--;
                    }
                    //bombs.Add(new BombProjectile(position, bombSprite, explodingBombSprite, this));//b);
                    break;
            }
            if (projectile != null)
            {
                projectilesList.Add(projectile);
            }

            if (bomb != null)
            {
                bombs.Add(bomb);
                //projectilesList.Add(bomb);
            }
        }

        public List<IProjectile> GetProjectiles () {
            return projectilesList;
        }

        public void Update(GameTime gameTime)
        {
            currentState.Update(this, gameTime);
            if (currentState is LinkDeathState) { }
            else
            {
                if (health == 0)
                {
                    //SetAnimation("Death");
                    ChangeState(new LinkDeathState());
                }
                // Handle knockback movement
                if (isKnockback)
                {
                    knockbackTimer -= gameTime.ElapsedGameTime.TotalSeconds;
                    if (knockbackTimer > 0)
                    {
                        Move((int)(knockbackDirection.X * 2), (int)(knockbackDirection.Y * 2)); // Adjust force here
                    }
                    else
                    {
                        isKnockback = false; // Stop knockback when timer ends
                    }
                }

                if (isInvincible)
                {
                    invincibleTime -= gameTime.ElapsedGameTime.TotalSeconds;
                    if (invincibleTime <= 0)
                        isInvincible = false;
                }

                linkSprite.Update(gameTime);
                foreach (var projectile in projectilesList)
                {
                    projectile.Update(gameTime);
                }
                boomerangThrowable.ownerPosition(position);
                boomerangThrowable.Update(gameTime);

                //bombProjectile.Update(gameTime);
                //foreach (var bomb in bombs)
                //{
                //    bomb.Update(gameTime);
                //}
                int x = 0;
                while (x < bombs.Count)
                {
                    bombs[x].Update(gameTime);
                    x++;
                }
            }
        }



        public void Move(int dx, int dy)
        {
            position.X += dx;
            position.Y += dy;
            collider.Move(dx, dy);
        }



        public void Draw(SpriteBatch spriteBatch)
        {
            // If Link is hidden, skip drawing.
            if (!isVisible)
                return;

            if (hurting)
            {
                damageFrameCounter++;
            }
            else
            {
                damageFrameCounter = 0;
            }
            if (damageFrameCounter > 0)
            {
                linkSprite.SetColor(Color.Magenta);
            }
            else
            {
                linkSprite.SetColor(Color.White);
            }
            if ((damageFrameCounter / 5) % 2 == 0)
            {
                linkSprite.Draw(spriteBatch, position, currentSpriteEffect);
            }

            foreach (var projectile in projectilesList)
            {
                projectile.Draw(spriteBatch);
            }
            boomerangThrowable.Draw(spriteBatch);
            //bombProjectile.Draw(spriteBatch);
            int x = 0;
            while(x < bombs.Count) {
                bombs[x].Draw(spriteBatch);
                x++;
            }

            if (dying)
            {
                deathFrameCounter++;
                if (deathFrameCounter % 5 == 3)
                {
                    currentSpriteEffect = SpriteEffects.FlipHorizontally;
                }
                else
                {
                    currentSpriteEffect = SpriteEffects.None;
                }
            }
        }

        public void createLinkSprites(Texture2D linkTexture)
        {
            Rectangle[] walkSide = new Rectangle[] { new Rectangle(2, 29, 15, 16), new Rectangle(18, 30, 16, 16) };
            Rectangle[] walkUp = new Rectangle[] { new Rectangle(69, 11, 16, 16), new Rectangle(86, 11, 16, 16) };
            Rectangle[] walkDown = new Rectangle[] { new Rectangle(1, 11, 16, 16), new Rectangle(18, 11, 16, 16) };

            Rectangle[] attackSide = new Rectangle[] { new Rectangle(270, 217, 27, 16), new Rectangle(270, 234, 27, 16), new Rectangle(270, 252, 27, 16), new Rectangle(270, 271, 27, 16) };
            Rectangle[] attackUp = new Rectangle[] { new Rectangle(1, 97, 16, 28), new Rectangle(18, 97, 16, 28), new Rectangle(35, 97, 16, 28), new Rectangle(52, 97, 16, 28) };
            Rectangle[] attackDown = new Rectangle[] { new Rectangle(1, 47, 16, 16), new Rectangle(18, 47, 16, 27), new Rectangle(35, 47, 16, 23), new Rectangle(53, 47, 16, 19) };

            Rectangle[] death = new Rectangle[] { new Rectangle(1, 11, 16, 16), new Rectangle(2, 29, 15, 16), new Rectangle(69, 11, 16, 16), new Rectangle(2, 29, 15, 16) };
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

            currentIdleSprite = idleDownSprite;
            currentAttackSprite = idleDownSprite;
            currentInteractSprite = interactDownSprite;
            linkSprite = currentIdleSprite;

            deathSprite = new NMoveAnim(linkTexture, death, 5);
            faceDirection = new Vector2(0, 1);

            createProjectileSprites(linkTexture);
        }

        public void SetAnimation(string action)
        {
            if (action.Contains("Idle"))
            {
                linkSprite = currentIdleSprite;
            }
            else if (action.Contains("MoveUp"))
            {
                linkSprite = walkUpSprite;
                currentIdleSprite = idleUpSprite;
                currentAttackSprite = attackUpSprite;
                currentInteractSprite = interactUpSprite;
                faceDirection = new Vector2(0, -1);
                currentSpriteEffect = SpriteEffects.None;
            }
            else if (action.Contains("MoveDown"))
            {
                linkSprite = walkDownSprite;
                currentIdleSprite = idleDownSprite;
                currentAttackSprite = attackDownSprite;
                currentInteractSprite = interactDownSprite;
                faceDirection = new Vector2(0, 1);
                currentSpriteEffect = SpriteEffects.None;
            }
            else if (action.Contains("MoveLeft"))
            {
                linkSprite = walkSideSprite;

                currentIdleSprite = idleSideSprite;
                currentAttackSprite = attackSideSprite2;
                currentInteractSprite = interactSideSprite;
                faceDirection = new Vector2(-1, 0);
                currentSpriteEffect = SpriteEffects.FlipHorizontally;
            }
            else if (action.Contains("MoveRight"))
            {
                linkSprite = walkSideSprite;
                currentIdleSprite = idleSideSprite;
                currentAttackSprite = attackSideSprite;
                currentInteractSprite = interactSideSprite;
                faceDirection = new Vector2(1, 0);
                currentSpriteEffect = SpriteEffects.None;
            }
            else if (action.Contains("Attack"))
            {
                linkSprite = currentAttackSprite;
            }
            else if (action.Contains("Damage"))
            {
                hurting = true;
            }
            else if (action.Contains("Item"))
            {
                linkSprite = currentInteractSprite;

            }
            else if (action.Contains("Death"))
            {
                linkSprite = deathSprite;
                dying = true;

            }
            if (!action.Contains("Damage"))
            {
                hurting = false;
            }
        }
        private void createProjectileSprites(Texture2D texture)
        {
            arrowHorizontal = new NMoveNAnim(texture, new Rectangle(10, 185, 16, 16));
            arrowVertical = new NMoveNAnim(texture, new Rectangle(1, 185, 8, 16));

            swordBeamHorizontal = new NMoveAnim(texture, new Rectangle[] { new Rectangle(45, 154, 16, 16), new Rectangle(115, 154, 16, 16) }, 5);
            swordBeamVertical = new NMoveAnim(texture, new Rectangle[] { new Rectangle(36, 154, 8, 16), new Rectangle(106, 154, 8, 16) }, 5);

            boomerang = new NMoveAnim(texture, new Rectangle[] { new Rectangle(64, 185, 8, 16), new Rectangle(73, 185, 8, 16), new Rectangle(82, 185, 8, 16), new Rectangle(73, 185, 8, 16) }, 5);
            boomerangThrowable = new BoomerangProjectile(position, boomerang);

            projectilesList.Add(boomerangThrowable);

            bombSprite = new NMoveNAnim(texture, new Rectangle(129,184,8,16));
            explodingBombSprite = new NMoveAnim(texture, new Rectangle[] { new Rectangle(137, 184, 16, 16), new Rectangle(154, 184, 16, 16) }, 10);
            //bombProjectile = new BombProjectile(position,bombSprite, explodingBombSprite);
        }

        public void CollisionUpdate(CollisionBox other)
        {
            int intersectionDistance = collider.GetSidePush(other);
            CollisionSide side = collider.side;   
            switch (side)
            {
                case CollisionSide.Top:
                    Move(0, -intersectionDistance);
                    break;
                case CollisionSide.Left:
                    Move(-intersectionDistance, 0);
                    break;
                case CollisionSide.Right:
                    Move(intersectionDistance, 0);
                    break;
                case CollisionSide.Bottom:
                    Move(0, intersectionDistance);
                    break;
                case CollisionSide.None:
                    break;
            }
            //Debug.WriteLine($"Collision: {intersectionDistance}");

        }
        public bool IsInvincible()
        {
            return isInvincible;
        }

        public CollisionBox GetCollider()
        {
            return collider;
        }

        public List<IProjectile> getBombs() {
            return bombs;
        }

        public void deleteBomb() {
            bombs.RemoveAt(0);
        }

        public Vector2 GetPosition() 
        { 
            return position; 
        }

        public void pickup(IItem item) {
            if (item is Bomb)
            {
                bombCount++;
            }
            else if (item is Rupee)
            {
                ruppeeCount++;
            }
            else if (item is Heart)/* AND HEALTH < MAX*/ {
                health++;
            }
            //other item cases go here
        }

        public void takeDamage() { //TODO: change so enemies deal VARIABLE damage?
            if (health > 0) {
                health--;
            }
        }
        public int GetHealth()
        {
            return health;
        }
        public int GetRupeeCount()
        {
            return ruppeeCount;
        }
        public int GetBombCount()
        {
            return bombCount;
        }
    }
}

