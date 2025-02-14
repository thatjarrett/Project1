using Project1.Entities;
using Project1.Interfaces;

namespace Project1.Commands
{
    internal class IdleCommand : ICommand
    {
        private readonly Link _link;

        public IdleCommand(Link link)
        {
            _link = link;
        }

        public void Execute()
        {
            _link.ChangeState(new LinkIdleState(_link.PreviousDirection)); // Stop movement
        }
    }
}
