using System;
using System.Diagnostics;
using Project1.Interfaces;

namespace Project1.Entities
{
    internal class UseItemCommand : ICommand
    {
        private readonly Link _link;
        private readonly int _itemNumber;
        private static double lastUseTime = 0; // Tracks the last time an item was used
        private const double CooldownDuration = 1.0; // 1-second cooldown

        public UseItemCommand(Link link, int itemNumber)
        {
            _link = link;
            _itemNumber = itemNumber;
        }

        public void Execute()
        {
            double currentTime = (double)DateTime.UtcNow.Ticks / TimeSpan.TicksPerSecond;

            if (currentTime - lastUseTime < CooldownDuration)
            {
                Debug.WriteLine("Item is on cooldown");
                return;
            }

            Debug.WriteLine("UseItemCommand Executed! Changing state to LinkUseItemState...");
            _link.Item(_itemNumber);
            lastUseTime = currentTime; // Update last used time
        }
    }
}
