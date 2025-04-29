using System.Diagnostics;
using Project1.Entities;
using Project1.HUDNamespace;
using Project1.Interfaces;

namespace Project1.Commands
{
    internal class MoveRightCommand : ICommand
    {
        private readonly Link _link;
        private readonly HUD _hud;

        public MoveRightCommand(Link link, HUD hud)
        {
            _link = link;
            _hud = hud;
        }

        public void Execute()
        {
            //Debug.WriteLine("move right");

            _link.MoveRight();
            if (_hud.active)
            {
                _hud.moveSelectorRight();
            }
        }
    }
}
