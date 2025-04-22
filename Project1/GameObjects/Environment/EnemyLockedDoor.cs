using Microsoft.Xna.Framework;
using Project1.Interfaces;
using Project1.GameObjects.Environment;
using Project1.Entities;
using System;

namespace Project1.GameObjects.Environment
{
    public class EnemyLockedDoor : doorTile
    {
        private ISprite openSprite;
        private Direction direction;
        private bool isOpen = false;

        public EnemyLockedDoor(Vector2 position, ISprite closedSprite, ISprite openSprite, Direction direction)
            : base(position)
        {
            this.IsSolid = true;

            this.setSprite(closedSprite);
            this.openSprite = openSprite;
            //this.IsSolid = true;
            //this.IsBreakable = false;
            this._collides = true;
            this.direction = direction;
            SetCollider();
        }

      /*
       * public void TryOpen(List<Ienemy> enemies, Vector2 room) // camera.getPos() = room // import IEnemy
        {
            bool open = false;
            int camPosX = room.x*768+384;
            int camPosY = (room.y * 528) + 120 + 264;
            int enemiesInRoomCount = 0;
            foreach (var enemy in enemies){
                if (Math.abs(enemy.getPos().x+camPosX)<384 && Math.abs(enemy.getPos().y + camPosY) < 264) 
                {
                    enemiesInRoomCount++;
                }

            }
            if (enemiesInRoomCount = 0)
            {
                this.Open();
            }
        }
      */
        public void Open()
        {
            this.isOpen = true;
            this.setSprite(openSprite);
            if (this.direction == Direction.Left)
            {
                this.SetCollider((int)_position.X, (int)_position.Y, 48, 96);
            }
            else if (this.direction == Direction.Right)
            {
                this.SetCollider((int)_position.X + 48, (int)_position.Y, 48, 96);
            }
            else if (this.direction == Direction.Up)
            {
                this.SetCollider((int)_position.X, (int)_position.Y, 96, 48);
            }
            else if (this.direction == Direction.Down)
            {
                this.SetCollider((int)_position.X, (int)_position.Y + 48, 96, 48);
            }

            this.IsSolid = false;
            
            
        }
    }
}
