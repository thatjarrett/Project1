using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Project1.Interfaces;

namespace Project1.Controllers
{
    internal class KeyboardController : IController
    {
        private readonly Dictionary<Keys, ICommand> _commands;
        private readonly HashSet<Keys> _movementKeys;
        private readonly HashSet<Keys> _itemKeys;
        private ICommand _idleCommand;
        private KeyboardState _previousState;

        public KeyboardController(Dictionary<Keys, ICommand> commands, ICommand idleCommand)
        {
            _commands = commands;
            _idleCommand = idleCommand;
            _previousState = Keyboard.GetState();
            _movementKeys = new HashSet<Keys> { Keys.W, Keys.A, Keys.S, Keys.D, Keys.Up, Keys.Left, Keys.Down, Keys.Right };
            _itemKeys = new HashSet<Keys> { Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.D5, Keys.D6, Keys.D7, Keys.D8, Keys.D9, Keys.D0 };
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();
            bool movementKeyPressed = false;

            foreach (var key in _commands.Keys)
            {
                if (state.IsKeyDown(key))
                {
                    _commands[key].Execute();
                    if (_movementKeys.Contains(key))
                    {
                        movementKeyPressed = true;
                    }
                }
            }

            if (!movementKeyPressed && WasMovementKeyPressed())
            {
                _idleCommand.Execute();
            }

            _previousState = state;
        }

        private bool WasMovementKeyPressed()
        {
            return _movementKeys.Any(key => _previousState.IsKeyDown(key));
        }
    }
}
