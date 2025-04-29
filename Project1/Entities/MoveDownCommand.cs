using Project1.HUDNamespace;
using Project1.Interfaces;

namespace Project1.Entities
{
    internal class MoveDownCommand : ICommand
    {
        private readonly Link _link;
        private readonly HUD _hud;

        public MoveDownCommand(Link link, HUD hud)
        {
            _link = link;
            _hud = hud;
        }

        public void Execute()
        {

            _link.MoveDown();
            if (_hud.active)
            {
                _hud.moveSelectorDown();
            }
        }
    }
}
