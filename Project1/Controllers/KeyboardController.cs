using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Project1.Interfaces;

namespace Project1.Controllers
{
    internal class KeyboardController : IController
    {
        private readonly Dictionary<Keys, ICommand> _commands;
        private readonly HashSet<Keys> _movementKeys;
        private ICommand _idleCommand;
        private KeyboardState _previousState;

        public KeyboardController(Dictionary<Keys, ICommand> commands, ICommand idleCommand)
        {
            _commands = commands;
            _idleCommand = idleCommand;
            _previousState = Keyboard.GetState();
            _movementKeys = new HashSet<Keys> { Keys.W, Keys.A, Keys.S, Keys.D, Keys.Up, Keys.Left, Keys.Down, Keys.Right };
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
            foreach (var key in _movementKeys)
            {
                if (_previousState.IsKeyDown(key))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
