using Project1.GameObjects.Background;
using System.Collections.Generic;
using Project1.Interfaces;  
namespace Project1.Commands
{
    public class CycleNPCCommand : ICommand
    {
        private Game1 _game;
        private bool _forward;

        public CycleNPCCommand(Game1 game, bool forward)
        {
            _game = game;
            _forward = forward;
        }

        public void Execute()
        {
            _game.CycleNPC(_forward);
        }
    }
}
