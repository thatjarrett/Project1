using Project1.Interfaces;

namespace Project1.Entities
{
    internal class DeathCommand : ICommand
    {
        private readonly Link _link;
        private bool _hasTriggered;

        public DeathCommand(Link link)
        {
            _link = link;
            _hasTriggered = false;
        }

        public void Execute()
        {
            if (!_hasTriggered)
            {
                _link.ChangeState(new LinkDeathState());
                _hasTriggered = true;
            }
        }
    }
}