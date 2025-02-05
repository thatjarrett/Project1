using Project1.Interfaces;
using Project1.Entities;

namespace Project1.Entities
{
    internal class AttackCommand : ICommand
    {
        private readonly Link _link;

        public AttackCommand(Link link)
        {
            _link = link;
        }

        public void Execute()
        {
            _link.ChangeState(new LinkAttackState(_link.PreviousDirection)); 
        }
    }
}
