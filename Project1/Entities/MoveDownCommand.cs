using Project1.Interfaces;

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

            _link.MoveDown();
        }
    }
}
