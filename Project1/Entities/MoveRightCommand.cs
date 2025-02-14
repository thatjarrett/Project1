using System.Diagnostics;
using Project1.Interfaces;

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

            _link.MoveRight();
        }
    }
}
