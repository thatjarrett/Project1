using Project1.HUD;
using Project1.Interfaces;

namespace Project1.Entities
{
    internal class MoveLeftCommand : ICommand
    {
        private readonly Link _link;
        private readonly IHUD _hud;

        public MoveLeftCommand(Link link, IHUD hud)
        {
            _link = link;
            _hud = hud;
        }

        public void Execute()
        {

            _link.MoveLeft();
            if (_hud.active)
            {
                _hud.moveSelectorLeft();
            }
        }
    }
}
