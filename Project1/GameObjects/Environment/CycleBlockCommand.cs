using Project1.GameObjects.Environment;
using Project1.Interfaces;
using System.Collections.Generic;

namespace Project1.Commands
{
    public class CycleBlockCommand : ICommand
    {
        private Game1 _game;
        private bool _forward;

        public CycleBlockCommand(Game1 game, bool forward)
        {
            _game = game;
            _forward = forward;
        }

        public void Execute()
        {
            _game.CycleBlock(_forward);
        }
    }
}
