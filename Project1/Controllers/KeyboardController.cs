using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Project1.Interfaces;
using Microsoft.Xna.Framework;


namespace Project1.Controllers
{
    internal class KeyboardController : IController
    {
        private readonly Dictionary<Keys, ICommand> _commands;

        public KeyboardController(Dictionary<Keys, ICommand> commands)
        {
            _commands = commands;
        }
        public void Update(GameTime gameTime) {

            KeyboardState state = Keyboard.GetState();
            foreach (var key in _commands.Keys)
            {
                if (state.IsKeyDown(key))
                {
                    _commands[key].Execute();
                }
            }
            
        }
    }
}
