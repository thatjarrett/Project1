using Project1.Interfaces;
using Microsoft.Xna.Framework;

namespace Project1.Commands
{
    public class ResetCommand : ICommand
    {
        private Game1 _game;

        public ResetCommand(Game1 game)
        {
            _game = game;
        }

        public void Execute()
        {
            _game.RestartGame();
        }
    }
}
