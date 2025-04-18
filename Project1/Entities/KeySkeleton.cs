﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1.Collision;
using Project1.GameObjects.Items;
using Project1.Interfaces;
using Project1.LevelLoading;
using Project1.Projectiles;
using Project1.Sprites;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Project1.Entities
{
    public class KeySkeleton : IEnemy

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

        private IProjectile[] projectiles = new BoomerangProjectile[1];

        private int health = 5;

        private bool alive = true;

        private LootTables lootTables = new LootTables();

        private ISprite boomerang;
        private Vector2 throwDirection;

        public KeySkeleton(Vector2 startPos)
        {
            position = startPos;
            currentState = new KeySkeletonMoveState();
            collider = new CollisionBox((int)startPos.X, (int)startPos.Y);
        }

        public void ChangeState(IEnemyState newState)
        {
            currentState = newState;
        }

        public void createEnemySprites(Texture2D enemyTexture)
        {
            Rectangle[] skelRect = new Rectangle[] { new Rectangle(1, 59, 16, 16) };

            skeletonSprite = new NMoveAnim(enemyTexture, skelRect, 5);
            skeletonSprite.SetColor(Color.Gold);

            boomerang = new NMoveAnim(enemyTexture,
                new Rectangle[]
                {
                    new Rectangle(290, 11, 8, 16),
                    new Rectangle(299, 11, 8, 16),
                    new Rectangle(308, 11, 8, 16)
                }, 5);
            //boomerangThrowable = new BoomerangProjectile(boomerang);
            for (int x = 0; x < projectiles.Length; x++)
            {
                projectiles[x] = new BoomerangProjectile(position, boomerang);
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
                skeletonSprite.SetColor(Color.Magenta);
            }
            else
            {
                skeletonSprite.SetColor(Color.Gold);
            }
            if ((damageFrameCounter / 5) % 2 == 0)
            {
                skeletonSprite.Draw(spriteBatch, position, currentSpriteEffect);
            }

            foreach (var b in projectiles)
            {
                b.Draw(spriteBatch);
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
            throwDirection = new Vector2(0, 1);
            Move(0, 2);
        }

        public void MoveLeft()
        {
            throwDirection = new Vector2(-1, 0);
            Move(-2, 0);
        }

        public void MoveRight()
        {
            throwDirection = new Vector2(1, 0);
            Move(2, 0);
        }

        public void MoveUp()
        {
            throwDirection = new Vector2(0, -1);
            Move(0, -2);
        }

        public void PerformAttack()
        {
            foreach (var b in projectiles)
            {
                b.Throw(position, throwDirection);
            }
            //ChangeState(attacking);

            //Task.Delay(1000).ContinueWith(_ => ChangeState(moving));
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

            if (!frozen)
            {
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

                foreach (var b in projectiles)
                {
                    b.ownerPosition(position);
                    b.Update(gameTime);
                }
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
            return lootTables.getKey();
        }

        public Vector2 getPos()
        {
            return position;
        }
    }
}
