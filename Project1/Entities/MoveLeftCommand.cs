using Project1.Interfaces;

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

            _link.MoveLeft();
        }
    }
}
