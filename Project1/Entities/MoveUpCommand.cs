using Project1.Interfaces;
using Project1.Entities;

namespace Project1.Commands
{
    internal class MoveUpCommand : ICommand
    {
        private readonly Link _link;

        public MoveUpCommand(Link link)
        {
            _link = link;
        }

        public void Execute()
        {
            _link.ChangeState(new LinkMoveUpState());
        }
    }
}
