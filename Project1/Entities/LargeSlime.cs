using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1.Collision;
using Project1.GameObjects.Environment;
using Project1.Interfaces;
using Project1.LevelLoading;
using Project1.Projectiles;
using Project1.Sprites;
namespace Project1.Entities
{
    public class LargeSlime : IDependentEnemy
    {
        private LargeSlimeMoveState currentState;
        private IEnemyState walk;
        private IEnemyState attack;

        private Vector2 position;
        private Vector2 originalPos;
        private ISprite sprite;
        private SpriteEffects currentSpriteEffect = SpriteEffects.None;
        
        private CollisionBox collider;

        private ISprite side;
        private ISprite forward;
        private ISprite back;

        int damageFrameCounter = 0;
        bool hurting = false;

        private IProjectile[] projectiles = new StraightProjectile[5];
        private int currentProjectile = 0;

        private ISprite projectileSprite;

        int health = 10;

        private bool alive = true;

        LootTables loot = new LootTables();

        private bool isInvincible = false;
        private double invincibleTime = .25;

        public LargeSlime(Vector2 startPos)
        {
            position = startPos;
            originalPos = startPos;

            //walk = new TriceratopsMoveState(false);
            //attack = 
            currentState = new LargeSlimeMoveState();

            //frontBack = new CollisionBox((int)startPos.X, (int)startPos.Y);
            //leftRight = new CollisionBox((int)startPos.X, (int)startPos.Y, 84, 48);
            collider = new CollisionBox((int)startPos.X, (int)startPos.Y);
        }

        public void ChangeState(IEnemyState newState)
        {
            //Debug.WriteLine($"Changing state to: {newState.GetType().Name}");

            // Change the current state
            //currentState = newState;
            //currentState.Enter(this);
        }

        public void SetInvincible(bool value)
        {

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
        public void MoveDown()
        {
            //
        }
        public void Update(GameTime gameTime, bool frozen)
        {

        }
        public void Update(GameTime gameTime, Link link, bool frozen)
        {
            if (isInvincible)
            {
                invincibleTime -= gameTime.ElapsedGameTime.TotalSeconds; ;
                if (invincibleTime <= 0)
                    isInvincible = false;
                SetAnimation("");

            }
            if (!frozen) {
                Vector2 linkPos = link.GetPosition();

                if (linkPos.X == this.position.X && linkPos.Y < this.position.Y && !currentState.waiting())
                {
                    //attack up
                    shoot(Direction.Up);
                    currentState.wait();
                    
                }
                else if (linkPos.X == this.position.X && linkPos.Y > this.position.Y && !currentState.waiting())
                {
                    //attack down
                    shoot(Direction.Down);
                    currentState.wait();
                }
                else if (linkPos.Y == this.position.Y && linkPos.X < this.position.X && !currentState.waiting())
                {
                    //attack left
                    shoot(Direction.Left);
                    currentState.wait();
                }
                else if (linkPos.Y == this.position.Y && linkPos.X > this.position.X && !currentState.waiting())
                {
                    //attack right
                    shoot(Direction.Right);
                    currentState.wait();
                }

                currentState.Update(this, gameTime);

                for (int i = 0; i < 5; i++)
                {
                    if (projectiles[i] != null)
                    {
                        projectiles[i].Update(gameTime);
                    }
                }


                sprite.Update(gameTime);
                
            }
        }

        public void Move(int dx, int dy)
        {
            position.X += dx;
            position.Y += dy;
            collider.Move(dx, dy);
        }

        public void PerformAttack()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isInvincible)
            {
                damageFrameCounter++;
            }
            else
            {
                damageFrameCounter = 0;
            }
            if (damageFrameCounter > 0)
            {
                sprite.SetColor(Color.Magenta);
            }
            else
            {
                sprite.SetColor(Color.White);
            }

            for (int i = 0; i < 5; i++)
                {
                    if (projectiles[i] != null)
                    {
                        projectiles[i].Draw(spriteBatch);
                    }
                }
            
            sprite.Draw(spriteBatch, position, currentSpriteEffect);

        }
        public void createEnemySprites(Texture2D enemyTexture)
        {
            Rectangle[] projRectangle = new Rectangle[] { new Rectangle(351, 59, 8, 16), new Rectangle(369, 59, 8, 16) };
           
            Rectangle[] goopRect = new Rectangle[] {new Rectangle(77, 11, 16, 16), new Rectangle(94, 11, 16, 16)};

            sprite = new NMoveAnim(enemyTexture, goopRect, 5);
            projectileSprite = new NMoveAnim(enemyTexture, projRectangle, 5);
        }

        public void SetAnimation(string action)
        {

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
                isInvincible = true;
                health--;
                invincibleTime = .25;
            }
        }

        public void die()
        {
            alive = false;
        }

        public int getHealth()
        {
            return health;
        }

        public bool Alive()
        {
            return alive;
        }

        public int getLoot()
        {
            return loot.getTriforce();
        }

        public Vector2 getPos()
        {
            return position;
        }

        public void shoot(Direction d) {
            switch (d) {
                case Direction.Up:
                    projectiles[currentProjectile] = new StraightProjectile(position, new Vector2(0, -2), projectileSprite, projectileSprite, 2);
                    break;
                case Direction.Down:
                    projectiles[currentProjectile] = new StraightProjectile(position, new Vector2(0, 2), projectileSprite, projectileSprite, 2);
                    break;
                case Direction.Left:
                    projectiles[currentProjectile] = new StraightProjectile(position, new Vector2(-2, 0), projectileSprite, projectileSprite, 2);
                    break;
                case Direction.Right:
                    projectiles[currentProjectile] = new StraightProjectile(position, new Vector2(2, 0), projectileSprite, projectileSprite, 2);
                    break;
            }
            //set to do nothing state;
            if (currentProjectile >= 5)
            {
                currentProjectile = 0;
            }
            else {
                currentProjectile++;
            }
        }
    }
}

