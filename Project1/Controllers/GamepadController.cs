using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Project1.Interfaces;
using System.Linq;

namespace Project1.Controllers
{
    internal class GamepadController : IController
    {
        private readonly Dictionary<Buttons, ICommand> _commands;
        private readonly HashSet<Buttons> _movementButtons;
        private ICommand _idleCommand;
        private GamePadState _previousState;

        private const float DeadZoneThreshold = 0.2f; // Threshold for ignoring small stick movement

        public GamepadController(Dictionary<Buttons, ICommand> commands, ICommand idleCommand)
        {
            _commands = commands;
            _idleCommand = idleCommand;
            _previousState = GamePad.GetState(PlayerIndex.One);

            _movementButtons = new HashSet<Buttons>
            {
                Buttons.DPadUp, Buttons.DPadDown, Buttons.DPadLeft, Buttons.DPadRight,
                Buttons.LeftThumbstickUp, Buttons.LeftThumbstickDown,
                Buttons.LeftThumbstickLeft, Buttons.LeftThumbstickRight
            };
        }

        public void Update(GameTime gameTime)
        {
            GamePadState state = GamePad.GetState(PlayerIndex.One);
            if (!state.IsConnected) return; // Ignore input if controller is disconnected

            bool movementDetected = false;

            // Handle button presses
            foreach (var button in _commands.Keys)
            {
                if (state.IsButtonDown(button) && _previousState.IsButtonUp(button))
                {
                    _commands[button].Execute();
                    if (_movementButtons.Contains(button))
                    {
                        movementDetected = true;
                    }
                }
            }

            // Handle D-Pad movement first (if no button was pressed)
            if (!movementDetected)
            {
                movementDetected = HandleDPadMovement(state);
            }

            // Handle joystick movement if D-Pad is not being used
            if (!movementDetected)
            {
                movementDetected = HandleThumbstickMovement(state.ThumbSticks.Left);
            }

            // Execute idle command if no movement was detected
            if (!movementDetected && WasMovingPreviously())
            {
                _idleCommand.Execute();
            }

            _previousState = state;
        }

        private bool HandleDPadMovement(GamePadState state)
        {
            if (state.IsButtonDown(Buttons.DPadUp)) { _commands[Buttons.DPadUp]?.Execute(); return true; }
            if (state.IsButtonDown(Buttons.DPadDown)) { _commands[Buttons.DPadDown]?.Execute(); return true; }
            if (state.IsButtonDown(Buttons.DPadLeft)) { _commands[Buttons.DPadLeft]?.Execute(); return true; }
            if (state.IsButtonDown(Buttons.DPadRight)) { _commands[Buttons.DPadRight]?.Execute(); return true; }

            return false;
        }

        private bool HandleThumbstickMovement(Vector2 stick)
        {
            if (stick.LengthSquared() < DeadZoneThreshold * DeadZoneThreshold) return false; // Ignore small movements

            if (stick.Y > 0.5f) { _commands[Buttons.LeftThumbstickUp]?.Execute(); return true; }
            if (stick.Y < -0.5f) { _commands[Buttons.LeftThumbstickDown]?.Execute(); return true; }
            if (stick.X > 0.5f) { _commands[Buttons.LeftThumbstickRight]?.Execute(); return true; }
            if (stick.X < -0.5f) { _commands[Buttons.LeftThumbstickLeft]?.Execute(); return true; }

            return false;
        }

        private bool WasMovingPreviously()
        {
            return _movementButtons.Any(button => _previousState.IsButtonDown(button)) ||
                   _previousState.ThumbSticks.Left.LengthSquared() >= DeadZoneThreshold * DeadZoneThreshold;
        }
    }
}
