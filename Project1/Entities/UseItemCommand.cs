using Project1.Interfaces;
using Project1.Entities;
using System;

namespace Project1.Entities
{
    internal class UseItemCommand : ICommand
    {
        private readonly Link _link;
        private readonly int _itemNumber;
        public UseItemCommand(Link link, int itemNumber)
        {
            _link = link;
            _itemNumber = itemNumber;
        }

        public void Execute()
        {
            Console.WriteLine("UseItemCommand Executed! Changing state to LinkUseItemState...");
            _link.Item(_itemNumber);
        }
    }
}
