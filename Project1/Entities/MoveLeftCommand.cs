using Project1.Interfaces;
using Project1.Entities;

namespace Project1.Commands
{
    internal class MoveLeftCommand : ICommand
    {
        private readonly Link _link;

        public MoveLeftCommand(Link link)
        {
            _link = link;
        }

        public void Execute()
        {
            _link.ChangeState(new LinkMoveLeftState());
        }
    }
}
