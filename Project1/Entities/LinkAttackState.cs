﻿using System.Diagnostics;
using Microsoft.Xna.Framework;


using Project1.Interfaces;
namespace Project1.Entities
{
    public class LinkAttackState : ILinkState
    {
        private const double AttackDuration = 0.2; // Attack lasts 0.2 seconds
        private double elapsedTime;
        private Direction _previousDirection;

        public LinkAttackState(Direction previousDirection)
        {

            _previousDirection = previousDirection;
        }

        public void Enter(Link link)
        {
            //Debug.WriteLine("attack");
            link.SetAnimation(GetAttackAnimationName());
            elapsedTime = 0;
            link.SetSword(_previousDirection);
        }

        public void MoveLeft(Link link)
        {

        }
        public void MoveRight(Link link)
        {

        }
        public void MoveUp(Link link)
        {

        }
        public void MoveDown(Link link)
        {

        }
        public void Attack(Link link)
        {

        }
        public void Damage(Link link)
        {
            link.ChangeState(new LinkDamageState(_previousDirection));
        }
        public void Item(Link link, int itemNumber)
        {

        }
        public void Update(Link link, GameTime gameTime)
        {

            elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;

            if (elapsedTime >= AttackDuration)
            {
                //Debug.WriteLine("previous direction " + _previousDirection);
                link.ClearSword();
                link.ChangeState(new LinkIdleState(_previousDirection)); // Return to previous idle direction
            }
        }

        private string GetAttackAnimationName()
        {
            return _previousDirection switch
            {
                Direction.Up => "Attack_Up",
                Direction.Down => "Attack_Down",
                Direction.Left => "Attack_Left",
                Direction.Right => "Attack_Right",
                _ => "Attack_Down" // Default attack facing down
            };
        }
    }
}
