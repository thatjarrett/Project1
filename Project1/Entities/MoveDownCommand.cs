using Project1.Interfaces;
using Project1.Entities;

namespace Project1.Entities
{
    internal class MoveDownCommand : ICommand
    {
        private readonly Link _link;

        public MoveDownCommand(Link link)
        {
            _link = link;
        }

        public void Execute()
        {
            _link.Move(0, 2); // Moves Link down
            _link.ChangeState(new LinkMoveDownState());
        }
    }
}
