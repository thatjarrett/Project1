using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Project1.Interfaces;

namespace Project1.Controllers
{
    internal class KeyboardController : IController
    {
        private readonly Dictionary<Keys, ICommand> _commands;
        private ICommand _idleCommand;
        private KeyboardState _previousState;

        public KeyboardController(Dictionary<Keys, ICommand> commands, ICommand idleCommand)
        {
            _commands = commands;
            _idleCommand = idleCommand;
            _previousState = Keyboard.GetState();
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
                    if (key == Keys.W || key == Keys.A || key == Keys.S || key == Keys.D ||
                        key == Keys.Up || key == Keys.Left || key == Keys.Down || key == Keys.Right)
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
            return _previousState.IsKeyDown(Keys.W) || _previousState.IsKeyDown(Keys.A) ||
                   _previousState.IsKeyDown(Keys.S) || _previousState.IsKeyDown(Keys.D) ||
                   _previousState.IsKeyDown(Keys.Up) || _previousState.IsKeyDown(Keys.Left) ||
                   _previousState.IsKeyDown(Keys.Down) || _previousState.IsKeyDown(Keys.Right);
        }
    }
}
