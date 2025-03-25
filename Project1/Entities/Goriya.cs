using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1.Collision;
using Project1.Interfaces;
using Project1.Projectiles;
using Project1.Sprites;

namespace Project1.Entities
{
    public class Goriya : IEnemy
    {
        private SpriteEffects currentSpriteEffect = SpriteEffects.None;

        private IEnemyState attacking;
        private IEnemyState moving;
        private IEnemyState currentState;
        private Vector2 position;

        private const double InvincibilityDuration = 1.0;
        private bool isInvincible = false;
        private double invincibleTime = 1.0;

        private ISprite goriyaSprite;
        private ISprite goriyaSpriteUp;
        private ISprite goriyaSpriteDown;
        private ISprite goriyaSpriteSide;
        private CollisionBox collider;

        private Direction direction;

        int damageFrameCounter = 0;
        bool hurting = false;

        private ISprite boomerang;
        //private BoomerangProjectile boomerangThrowable;
        private Vector2 throwDirection;

        private IProjectile[] boomerangs = new BoomerangProjectile[1]; //TODO: make boomerang a projectile

        private int health = 4;

        private bool alive = true;
        public Goriya(Vector2 startPos)
        {
            position = startPos;
            direction = Direction.Right;

            moving = new GoriyaMoveState();
            attacking = new GoriyaAttackState();

            currentState = moving;
            collider = new CollisionBox((int)startPos.X, (int)startPos.Y);
        }

        public void ChangeState(IEnemyState newState)
        {
            currentState = newState;
        }

        public void createEnemySprites(Texture2D enemyTexture)
        {
            Rectangle[] gRectUp = new Rectangle[] { new Rectangle(239, 11, 16, 16) };
            Rectangle[] gRectDown = new Rectangle[] { new Rectangle(222, 11, 16, 16) };
            Rectangle[] gRectSide = new Rectangle[] { new Rectangle(256, 11, 16, 16), new Rectangle(273, 11, 16, 16) };

            goriyaSpriteUp = new NMoveAnim(enemyTexture, gRectUp, 5);
            goriyaSpriteDown = new NMoveAnim(enemyTexture, gRectDown, 5);
            goriyaSpriteSide = new NMoveAnim(enemyTexture, gRectSide, 5);

           
            goriyaSprite = goriyaSpriteSide;

            boomerang = new NMoveAnim(enemyTexture,
                new Rectangle[]
                {
                    new Rectangle(290, 11, 8, 16),
                    new Rectangle(299, 11, 8, 16),
                    new Rectangle(308, 11, 8, 16)
                }, 5);
            //boomerangThrowable = new BoomerangProjectile(boomerang);
            for(int x = 0; x < boomerangs.Length; x ++) {
                boomerangs[x] = new BoomerangProjectile(position, boomerang);
            }
            //boomerangs[0] = new BoomerangProjectile(position, boomerang);
            throwDirection = new Vector2(0, 1);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
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
                goriyaSprite.SetColor(Color.Magenta);
            }
            else
            {
                goriyaSprite.SetColor(Color.White);
            }

            if ((damageFrameCounter / 5) % 2 == 0)
            {
                goriyaSprite.Draw(spriteBatch, position, currentSpriteEffect);
            }
            //boomerangThrowable.Draw(spriteBatch);
            foreach (var b in boomerangs) {
                b.Draw(spriteBatch);
                //b.GetCollider().DebugDraw(spriteBatch, new[] { Color.White}, b.GetCollider().hitbox, Color.Green);
                //TODO: HITBOX OUTLINE FOR PROJECTILES
            }
        }

       
        public void Move(int dx, int dy)
        {
            position.X += dx;
            position.Y += dy;
            collider.Move(dx, dy);
        }

        // Movement methods update state, direction, sprite, throw direction, and apply movement.
        public void MoveDown()
        {
            ChangeState(moving);
            direction = Direction.Down;
            goriyaSprite = goriyaSpriteDown;
            throwDirection = new Vector2(0, 1);
            Move(0, 2);

           
            currentSpriteEffect = SpriteEffects.None;
        }

        public void MoveLeft()
        {
            ChangeState(moving);
            direction = Direction.Left;
            goriyaSprite = goriyaSpriteSide;
            throwDirection = new Vector2(-1, 0);
            Move(-2, 0); 
        }

        public void MoveRight()
        {
            ChangeState(moving);
            direction = Direction.Right;
            goriyaSprite = goriyaSpriteSide;
            throwDirection = new Vector2(1, 0);
            Move(2, 0); 
        }

        public void MoveUp()
        {
            ChangeState(moving);
            direction = Direction.Up;
            goriyaSprite = goriyaSpriteUp;
            throwDirection = new Vector2(0, -1);
            Move(0, -2);


            currentSpriteEffect = SpriteEffects.FlipVertically;
        }

        public void PerformAttack()
        {
            //boomerangThrowable.Throw(position, throwDirection);
            foreach (var b in boomerangs) {
                b.Throw(position, throwDirection);
            }
            ChangeState(attacking);
           
            Task.Delay(1000).ContinueWith(_ => ChangeState(moving));
        }

        public void SetAnimation(string action)
        {
            if (action.Contains("Damage"))
            {
                hurting = true;
                Task.Delay(500).ContinueWith(_ => hurting = false);
            }
            else
            {
                hurting = false;
            }
        }

        public void SetInvincible(bool value)
        {
            isInvincible = value;
        }

        public void Update(GameTime gameTime)
        {
            currentState.Update(this, gameTime);
            double timeStep = gameTime.ElapsedGameTime.TotalSeconds;

            if (isInvincible)
            {
                invincibleTime -= timeStep;
                if (invincibleTime <= 0)
                    isInvincible = false;
                    SetAnimation("");

            }

            goriyaSprite.Update(gameTime);

          
            if (direction == Direction.Up || direction == Direction.Down)
            {
                
                if ((int)(gameTime.TotalGameTime.TotalMilliseconds / 150) % 2 == 0)
                {
                    currentSpriteEffect = SpriteEffects.None;
                }
                else
                {
                    currentSpriteEffect = SpriteEffects.FlipHorizontally;
                }
            }
            else if (direction == Direction.Left)
            {
                currentSpriteEffect = SpriteEffects.FlipHorizontally;
            }
            else
            {
                currentSpriteEffect = SpriteEffects.None;
            }

            //boomerangThrowable.Update(gameTime, position);
            foreach (var b in boomerangs) {
                b.ownerPosition(position);
                b.Update(gameTime);
            }
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
        public CollisionBox GetCollider()
        {
            return collider;
        }

        public IProjectile[] GetProjectiles()
        {
            return boomerangs;
        }

        public void takeDamage()
        {
            if (health > 0 && !isInvincible)
            {
                health--;
            }
        }

        public void die()
        {
            alive = false;
        }

        public bool Alive() {
            return alive;
        }

        public int getHealth()
        {
            return health;
        }
    }
}
