using Project1.Interfaces;
using Project1.Entities;
using System;

namespace Project1.Entities
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
            Console.WriteLine("DamageCommand Executed! Changing state to LinkDamageState...");
            _link.ChangeState(new LinkDamageState(_link.PreviousDirection));
        }
    }
}
