using Project1.Interfaces;

namespace Project1.Commands
{

    public class PauseCommand : ICommand
    {
        private static double lastPauseTime = 0; 
        private const double pauseCooldown = 0.2; 

        private Game1 _game;

        public PauseCommand(Game1 game)
        {
            _game = game;
        }

        public void Execute()
        {
            double currentTime = GameTimer.TotalGameTime; // Get current time

            if (currentTime - lastPauseTime >= pauseCooldown)
            {
                _game.PauseGame();
                lastPauseTime = currentTime;
            } 
               
        }
    }
}
