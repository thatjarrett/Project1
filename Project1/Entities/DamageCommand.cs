using Project1.Interfaces;
using Project1.Entities;

namespace Project1.Commands
{
    internal class DamageCommand : ICommand
    {
        private readonly Link _link;

        public DamageCommand(Link link)
        {
            _link = link;
        }

        public void Execute()
        {
            _link.ChangeState(new LinkDamageState(_link.PreviousDirection));
        }
    }
}
