using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1.Collision;
using Project1.Interfaces;
using Project1.Projectiles;
using Project1.Sprites;
using System;
using System.Diagnostics;

namespace Project1.Entities
{
    public class Skeleton : IEnemy

    {
        private SpriteEffects currentSpriteEffect = SpriteEffects.None;


        private IEnemyState currentState;
        private Vector2 position;

        //private const double InvincibilityDuration = 1.0;
        private bool isInvincible = false;
        private double invincibleTime = .25;
        private double flipTime = 0.2;
        private CollisionBox collider;


        private ISprite skeletonSprite;

        int damageFrameCounter = 0;
        bool hurting = false;

        private IProjectile[] projectiles = null;

        private int health = 3;

        private bool alive = true;

        Random random = new Random();
        public Skeleton(Vector2 startPos)
        {
            position = startPos;
            currentState = new SkeletonMoveState(Direction.Left, 1.0);
            collider = new CollisionBox((int)startPos.X, (int)startPos.Y);
        }

        public void ChangeState(IEnemyState newState)
        {
            currentState = newState;
        }

        public void createEnemySprites(Texture2D enemyTexture)
        {
            Rectangle[] skelRect = new Rectangle[] {new Rectangle(1, 59, 16, 16) };

            skeletonSprite = new NMoveAnim(enemyTexture, skelRect, 5);
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
                skeletonSprite.SetColor(Color.Magenta);
            }
            else
            {
                skeletonSprite.SetColor(Color.White);
            }
            if ((damageFrameCounter / 5) % 2 == 0)
            {
                skeletonSprite.Draw(spriteBatch, position, currentSpriteEffect);
            }
        }

        public void Move(int dx, int dy)
        {
            position.X += dx;
            position.Y += dy;
            collider.Move(dx, dy);
        }

        public void MoveDown()
        {
            //
        }

        public void MoveLeft()
        {
            //
        }

        public void MoveRight()
        {
            //
        }

        public void MoveUp()
        {
            //
        }

        public void PerformAttack()
        {
            //
        }

        public void SetAnimation(string action)
        {
            if (action.Contains("Damage"))
            {
                hurting = true;
            }
            if (!action.Contains("Damage"))
            {
                hurting = false;
            }
        }

        public void SetInvincible(bool value)
        {
            isInvincible = value;
        }

        public void Update(GameTime gameTime, bool frozen)
        {
            if (isInvincible)
            {
                invincibleTime -= gameTime.ElapsedGameTime.TotalSeconds;
                if (invincibleTime <= 0)
                    isInvincible = false;
                    SetAnimation("");
            }

            if (!frozen) {
                double timeStep = gameTime.ElapsedGameTime.TotalSeconds;

                if ((int)(gameTime.TotalGameTime.TotalMilliseconds / 150) % 2 == 0)
                {
                    currentSpriteEffect = SpriteEffects.None;
                }
                else
                {
                    currentSpriteEffect = SpriteEffects.FlipHorizontally;
                }

                currentState.Update(this, gameTime);

                skeletonSprite.Update(gameTime);
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
            return projectiles;
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

        public bool Alive()
        {
            return alive;
        }

        public int getHealth()
        {
            return health;
        }

        public int getLoot()
        {
            int x = random.Next(0, 10);
            int lootID = 0;
            if (x <= 5)
            {
                lootID = 16; //coin. note, sometimes coin is blue??
            }
            else if (6 <= x && x <= 7)
            {
                lootID = 15; //heart
            }
            else if (x == 8)
            {
                lootID = 20; //clock
            }
            else
            {
                x = 17; //arrow. should be blue gem but whatever we dont have that implemented
            }

            return lootID;
        }

        public Vector2 getPos()
        {
            return position;
        }
    }
}
