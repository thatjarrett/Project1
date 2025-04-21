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
    public class Triceratops : IDependentEnemy
    {
        private IEnemyState currentState;
        private IEnemyState walk;
        private IEnemyState attack;

        private Vector2 position;
        private Vector2 originalPos;
        private ISprite dinoSprite;
        private SpriteEffects currentSpriteEffect = SpriteEffects.None;
        
        private CollisionBox collider;
        private CollisionBox frontBack;
        private CollisionBox leftRight;

        private ISprite side;
        private ISprite forward;
        private ISprite back;

        int damageFrameCounter = 0;
        bool hurting = false;

        private IProjectile[] projectiles = null;

        int health = 10;

        Direction currentDirection = Direction.Down;

        private bool alive = true;

        LootTables loot = new LootTables();

        private bool isInvincible = false;
        private double invincibleTime = .25;


        public Triceratops(Vector2 startPos)
        {
            position = startPos;
            originalPos = startPos;

            //walk = new TriceratopsMoveState(false);
            //attack = 
            currentState = new TriceratopsMoveState(false);

            //frontBack = new CollisionBox((int)startPos.X, (int)startPos.Y);
            //leftRight = new CollisionBox((int)startPos.X, (int)startPos.Y, 84, 48);
            collider = new CollisionBox((int)startPos.X, (int)startPos.Y);
        }

        public void ChangeState(IEnemyState newState)
        {
            //Debug.WriteLine($"Changing state to: {newState.GetType().Name}");

            // Change the current state
            currentState = newState;
            currentState.Enter(this);
        }

        public void SetInvincible(bool value)
        {

        }

        public void MoveLeft()
        {
            currentDirection = Direction.Left;
            dinoSprite = side;
            currentSpriteEffect = SpriteEffects.FlipHorizontally;
            collider = new CollisionBox((int)position.X, (int)position.Y, 84, 48);
        }
        public void MoveRight()
        {
            currentDirection = Direction.Right;
            dinoSprite = side;
            currentSpriteEffect = SpriteEffects.None;
            collider = new CollisionBox((int)position.X, (int)position.Y, 84, 48);
        }
        public void MoveUp()
        {
            currentDirection = Direction.Up;
            dinoSprite = back;
            currentSpriteEffect = SpriteEffects.None;
            collider = new CollisionBox((int)position.X, (int)position.Y);
        }
        public void MoveDown()
        {
            currentDirection = Direction.Down;
            dinoSprite = forward;
            currentSpriteEffect = SpriteEffects.None;
            collider = new CollisionBox((int)position.X, (int)position.Y); ;
        }
        public void Update(GameTime gameTime, bool frozen)
        {

        }
        public void Update(GameTime gameTime, Link link, bool frozen)
        {
            if (isInvincible)
            {
                invincibleTime -= gameTime.ElapsedGameTime.TotalSeconds; ;
                if (invincibleTime <= 0) {
                    isInvincible = false;
                }
                    

            }

            if (!frozen) {
                Vector2 linkPos = link.GetPosition();

                if (linkPos.X == this.position.X && linkPos.Y < this.position.Y && currentState is TriceratopsMoveState)
                {
                    //attack up
                    MoveUp();
                    currentState = new TriceratopsAttackState(Direction.Up);
                }
                else if (linkPos.X == this.position.X && linkPos.Y > this.position.Y && currentState is TriceratopsMoveState)
                {
                    //attack down
                    MoveDown();
                    currentState = new TriceratopsAttackState(Direction.Down);
                }
                else if (linkPos.Y == this.position.Y && linkPos.X < this.position.X && currentState is TriceratopsMoveState)
                {
                    //attack left
                    MoveLeft();
                    currentState = new TriceratopsAttackState(Direction.Left);
                }
                else if (linkPos.Y == this.position.Y && linkPos.X > this.position.X && currentState is TriceratopsMoveState)
                {
                    //attack right
                    MoveRight();
                    currentState = new TriceratopsAttackState(Direction.Right);
                }

                currentState.Update(this, gameTime);

                if (currentDirection == Direction.Up || currentDirection == Direction.Down) {
                    if ((int)(gameTime.TotalGameTime.TotalMilliseconds / 150) % 2 == 0)
                    {
                        currentSpriteEffect = SpriteEffects.None;
                    }
                    else
                    {
                        currentSpriteEffect = SpriteEffects.FlipHorizontally;
                    } 
                }
                dinoSprite.Update(gameTime);
                
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
                dinoSprite.SetColor(Color.Magenta);
            }
            else
            {
                dinoSprite.SetColor(Color.White);
            }

            dinoSprite.Draw(spriteBatch, position, currentSpriteEffect);

        }
        public void createEnemySprites(Texture2D bossTexture)
        {

            Rectangle[] frontRect = new Rectangle[] { new Rectangle(1, 58, 16, 16) };
            Rectangle[] backRect = new Rectangle[] {new Rectangle(35, 58, 16, 16)};
            Rectangle[] sideRect = new Rectangle[] {new Rectangle(69, 58, 28, 16), new Rectangle(102, 58, 28, 16)};

            forward = new NMoveAnim(bossTexture, frontRect, 5);
            back = new NMoveAnim(bossTexture, backRect, 5);
            side = new NMoveAnim(bossTexture, sideRect, 5);

            dinoSprite = forward;
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
                    if (currentState is TriceratopsAttackState)
                    {
                        currentState = new TriceratopsMoveState(true);
                    }
                    break;
                case CollisionSide.Left:
                    Move(-intersectionDistance, 0);
                    if (currentState is TriceratopsAttackState)
                    {
                        currentState = new TriceratopsMoveState(true);
                    }
                    break;
                case CollisionSide.Right:
                    Move(intersectionDistance, 0);
                    if (currentState is TriceratopsAttackState)
                    {
                        currentState = new TriceratopsMoveState(true);
                    }
                    break;
                case CollisionSide.Bottom:
                    Move(0, intersectionDistance);
                    if (currentState is TriceratopsAttackState)
                    {
                        currentState = new TriceratopsMoveState(true);
                    }
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
    }
}

