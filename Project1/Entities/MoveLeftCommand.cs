using Project1.Interfaces;
using Project1.Entities;

namespace Project1.Entities
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
            _link.Move(-2, 0); // Moves Link left
            _link.ChangeState(new LinkMoveLeftState());
        }
    }
}
