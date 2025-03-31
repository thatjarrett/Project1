using Project1.HUD;
using Project1.Interfaces;

namespace Project1.Entities
{
    internal class MoveDownCommand : ICommand
    {
        private readonly Link _link;
        private readonly IHUD _hud;

        public MoveDownCommand(Link link, IHUD hud)
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
