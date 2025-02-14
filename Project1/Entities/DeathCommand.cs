using Project1.Interfaces;

namespace Project1.Entities
{
    internal class DeathCommand : ICommand
    {
        private readonly Link _link;

        public DeathCommand(Link link)
        {
            _link = link;
        }

        public void Execute()
        {
            _link.ChangeState(new LinkDeathState());
        }
    }
}
