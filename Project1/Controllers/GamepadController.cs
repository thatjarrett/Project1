using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Project1.Interfaces;

namespace Project1.Controllers
{
    internal class GamepadController : IController
    {
        private readonly Dictionary<Buttons, ICommand> _commands;
        private ICommand _idleCommand;
        private GamePadState _previousState;

        private const float DeadZoneThreshold = 0.2f; // threshold for thumbstick movement

        public GamepadController(Dictionary<Buttons, ICommand> commands, ICommand idleCommand)
        {
            _commands = commands;
            _idleCommand = idleCommand;
            _previousState = GamePad.GetState(PlayerIndex.One);
        }

        public void Update(GameTime gameTime)
        {
            GamePadState state = GamePad.GetState(PlayerIndex.One);
            bool movementDetected = false;
            if (state.IsButtonDown(Buttons.A))
            {
                System.Diagnostics.Debug.WriteLine("A button pressed!");
            }

            // Prioritize D-Pad input first
            if (state.IsButtonDown(Buttons.DPadUp))
            {
                _commands[Buttons.DPadUp]?.Execute();
                movementDetected = true;
            }
            else if (state.IsButtonDown(Buttons.DPadDown))
            {
                _commands[Buttons.DPadDown]?.Execute();
                movementDetected = true;
            }
            else if (state.IsButtonDown(Buttons.DPadLeft))
            {
                _commands[Buttons.DPadLeft]?.Execute();
                movementDetected = true;
            }
            else if (state.IsButtonDown(Buttons.DPadRight))
            {
                _commands[Buttons.DPadRight]?.Execute();
                movementDetected = true;
            }
            else
            {
               
                movementDetected = HandleThumbstickMovement(state.ThumbSticks.Left);
            }

        
            if (!movementDetected && WasMovingPreviously())
            {
                _idleCommand.Execute();
            }

            _previousState = state;
        }

        private bool HandleThumbstickMovement(Vector2 stick)
        {
            if (stick.LengthSquared() < DeadZoneThreshold * DeadZoneThreshold)
            {
                return false; // Ignore small movements
            }

            if (stick.Y > 0.5f) { _commands[Buttons.LeftThumbstickUp]?.Execute(); return true; }
            if (stick.Y < -0.5f) { _commands[Buttons.LeftThumbstickDown]?.Execute(); return true; }
            if (stick.X > 0.5f) { _commands[Buttons.LeftThumbstickRight]?.Execute(); return true; }
            if (stick.X < -0.5f) { _commands[Buttons.LeftThumbstickLeft]?.Execute(); return true; }

            return false;
        }

        private bool WasMovingPreviously()
        {
            return _previousState.DPad.Up == ButtonState.Pressed ||
                   _previousState.DPad.Down == ButtonState.Pressed ||
                   _previousState.DPad.Left == ButtonState.Pressed ||
                   _previousState.DPad.Right == ButtonState.Pressed ||
                   _previousState.ThumbSticks.Left.LengthSquared() >= DeadZoneThreshold * DeadZoneThreshold;
        }
    }
}
