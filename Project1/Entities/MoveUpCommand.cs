using Project1.Interfaces;

namespace Project1.Entities
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

            _link.MoveUp();
        }
    }
}
