using Project1.Entities;
using Project1.HUDNamespace;
using Project1.Interfaces;

namespace Project1.Commands
{
    internal class MoveLeftCommand : ICommand
    {
        private readonly Link _link;
        private readonly HUD _hud;

        public MoveLeftCommand(Link link, HUD hud)
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
