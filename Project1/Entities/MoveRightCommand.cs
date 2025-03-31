using System.Diagnostics;
using Project1.HUD;
using Project1.Interfaces;

namespace Project1.Entities
{
    internal class MoveRightCommand : ICommand
    {
        private readonly Link _link;
        private readonly IHUD _hud;

        public MoveRightCommand(Link link, IHUD hud)
        {
            _link = link;
            _hud = hud;
        }

        public void Execute()
        {
            Debug.WriteLine("move right");

            _link.MoveRight();
            if (_hud.active)
            {
                _hud.moveSelectorRight();
            }
        }
    }
}
