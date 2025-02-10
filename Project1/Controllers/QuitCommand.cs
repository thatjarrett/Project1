
using Microsoft.Xna.Framework;
using Project1.Interfaces;
namespace Project1.Commands
{
    public class QuitCommand : ICommand
    {
        private Game _game;

        public QuitCommand(Game game)
        {
            _game = game;
        }

        public void Execute()
        {
            _game.Exit();
        }
    }
}
