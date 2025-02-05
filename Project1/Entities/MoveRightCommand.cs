using Project1.Interfaces;
using Project1.Entities;
using System;
using System.Diagnostics;

namespace Project1.Entities
{
    internal class MoveRightCommand : ICommand
    {
        private readonly Link _link;

        public MoveRightCommand(Link link)
        {
            _link = link;
        }

        public void Execute()
        {
            Debug.WriteLine("move right");
            _link.Move(2,0); // Moves Link right
            _link.ChangeState(new LinkMoveRightState());
        }
    }
}
