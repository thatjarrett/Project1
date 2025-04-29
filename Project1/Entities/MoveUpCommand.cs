using Project1.HUDNamespace;
using Project1.Interfaces;

namespace Project1.Entities
{
    internal class MoveUpCommand : ICommand
    {
        private readonly Link _link;
        private readonly HUD _hud;

        public MoveUpCommand(Link link, HUD hud)
        {
            _link = link;
            _hud = hud;
        }

        public void Execute()
        {

            _link.MoveUp();
            if (_hud.active)
            {
                _hud.moveSelectorUp();
            }
        }
    }
}
