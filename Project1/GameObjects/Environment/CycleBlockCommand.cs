using Project1.GameObjects.Environment;
using Project1.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Project1.Commands
{
    public class CycleBlockCommand : ICommand
    {
        private Game1 _game;
        private bool _forward;
        private static double lastExecutionTime = 0; 
        private const double CooldownDuration = 0.2; 

        public CycleBlockCommand(Game1 game, bool forward)
        {
            _game = game;
            _forward = forward;
        }

        public void Execute()
        {
            double currentTime = (double)DateTime.UtcNow.Ticks / TimeSpan.TicksPerSecond;

            if (currentTime - lastExecutionTime < CooldownDuration)
            {
                Debug.WriteLine("CycleBlockCommand is on cooldown!");
                return;
            }

            _game.CycleBlock(_forward);
            lastExecutionTime = currentTime;
        }
    }
}
