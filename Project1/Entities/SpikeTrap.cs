using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1.Collision;
using Project1.GameObjects.Environment;
using Project1.Interfaces;
using Project1.Projectiles;
using Project1.Sprites;
namespace Project1.Entities
{
    public class SpikeTrap : IDependentEnemy
    {
        private IEnemyState currentState;
        private Vector2 position;
        private Vector2 originalPos;
        private ISprite spikeTrapSprite;
        private SpriteEffects currentSpriteEffect = SpriteEffects.None;
        private CollisionBox collider;

        int damageFrameCounter = 0;
        bool hurting = false;

        private IProjectile[] projectiles = null;

        int health = 100;

        public SpikeTrap(Vector2 startPos)
        {
            position = startPos;
            originalPos = startPos;
            currentState = new SpikeTrapIdleState();
            collider = new CollisionBox((int)startPos.X, (int)startPos.Y);
        }

        public void ChangeState(IEnemyState newState)
        {

        }

        public void SetInvincible(bool value)
        {

        }

        public void Update(GameTime gameTime, bool frozen)
        {

        }
        public void Update(GameTime gameTime, Link link, bool frozen)
        {
            if (!frozen) {
                Vector2 linkPos = link.GetPosition();

                if (linkPos.X == this.position.X && linkPos.Y < this.position.Y && currentState is SpikeTrapIdleState)
                {
                    //attack up
                    currentState = new SpikeTrapAttackState(Direction.Up);
                }
                else if (linkPos.X == this.position.X && linkPos.Y > this.position.Y && currentState is SpikeTrapIdleState)
                {
                    //attack down
                    currentState = new SpikeTrapAttackState(Direction.Down);
                }
                else if (linkPos.Y == this.position.Y && linkPos.X < this.position.X && currentState is SpikeTrapIdleState)
                {
                    //attack left
                    currentState = new SpikeTrapAttackState(Direction.Left);
                }
                else if (linkPos.Y == this.position.Y && linkPos.X > this.position.X && currentState is SpikeTrapIdleState)
                {
                    //attack right
                    currentState = new SpikeTrapAttackState(Direction.Right);
                }

                currentState.Update(this, gameTime);
                spikeTrapSprite.Update(gameTime);
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

            spikeTrapSprite.Draw(spriteBatch, position, currentSpriteEffect);

        }
        public void createEnemySprites(Texture2D enemyTexture)
        {

            Rectangle spikeTrap = new Rectangle(164, 59, 16, 16);

            spikeTrapSprite = new NMoveNAnim(enemyTexture, spikeTrap);
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
                    if (currentState is SpikeTrapAttackState)
                    {
                        currentState = new SpikeTrapReturnState(Direction.Up);
                    }
                    else if (currentState is SpikeTrapReturnState)
                    {
                        currentState = new SpikeTrapIdleState();
                    }
                    break;
                case CollisionSide.Left:
                    Move(-intersectionDistance, 0);
                    if (currentState is SpikeTrapAttackState)
                    {
                        currentState = new SpikeTrapReturnState(Direction.Left);
                    }
                    else if (currentState is SpikeTrapReturnState)
                    {
                        currentState = new SpikeTrapIdleState();
                    }
                    break;
                case CollisionSide.Right:
                    Move(intersectionDistance, 0);
                    if (currentState is SpikeTrapAttackState)
                    {
                        currentState = new SpikeTrapReturnState(Direction.Right);
                    }
                    else if (currentState is SpikeTrapReturnState)
                    {
                        currentState = new SpikeTrapIdleState();
                    }
                    break;
                case CollisionSide.Bottom:
                    Move(0, intersectionDistance);
                    if (currentState is SpikeTrapAttackState)
                    {
                        currentState = new SpikeTrapReturnState(Direction.Down);
                    }
                    else if (currentState is SpikeTrapReturnState)
                    {
                        currentState = new SpikeTrapIdleState();
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
            //
        }

        public void die()
        {
            //
        }

        public int getHealth()
        {
            return health;
        }

        public bool Alive()
        {
            return true;
        }

        public int getLoot()
        {
            int lootID = 0;
            return lootID;  //spike trap wont die so who cares about its loot
        }

        public Vector2 getPos()
        {
            return position;
        }
    }
}

