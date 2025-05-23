﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Project1.Interfaces;

namespace Project1.Controllers
{
    internal class GamepadController : IController
    {
        private readonly Dictionary<Buttons, ICommand> _buttonCommands;
        private readonly Dictionary<Direction, ICommand> _movementCommands;
        private readonly ICommand _idleCommand;
        private readonly Dictionary<Buttons, ICommand> _buttonReleaseCommands;
        private GamePadState _previousState;
        private const float DeadZoneThreshold = 0.2f;

        public GamepadController(Dictionary<Buttons, ICommand> buttonCommands,
                                 Dictionary<Direction, ICommand> movementCommands,
                                 ICommand idleCommand,
                                 Dictionary<Buttons, ICommand> buttonReleaseCommands = null)
        {
            _buttonCommands = buttonCommands;
            _movementCommands = movementCommands;
            _idleCommand = idleCommand;
            _buttonReleaseCommands = buttonReleaseCommands ?? new Dictionary<Buttons, ICommand>();
            _previousState = GamePad.GetState(PlayerIndex.One);
        }

        public void Update(GameTime gameTime)
        {
            GamePadState currentState = GamePad.GetState(PlayerIndex.One);
            if (!currentState.IsConnected)
                return;

            bool actionExecuted = false;

            // Handle button presses
            foreach (var entry in _buttonCommands)
            {
                if (currentState.IsButtonDown(entry.Key) && _previousState.IsButtonUp(entry.Key))
                {
                    entry.Value.Execute();
                    actionExecuted = true;
                }
            }
            foreach (var entry in _buttonReleaseCommands)
            {
                if (_previousState.IsButtonDown(entry.Key) && currentState.IsButtonUp(entry.Key))
                {
                    entry.Value.Execute();
                }
            }



            // Handle D-Pad movement
            if (currentState.IsButtonDown(Buttons.DPadUp)) { _movementCommands[Direction.Up].Execute(); actionExecuted = true; }
            else if (currentState.IsButtonDown(Buttons.DPadDown)) { _movementCommands[Direction.Down].Execute(); actionExecuted = true; }
            else if (currentState.IsButtonDown(Buttons.DPadLeft)) { _movementCommands[Direction.Left].Execute(); actionExecuted = true; }
            else if (currentState.IsButtonDown(Buttons.DPadRight)) { _movementCommands[Direction.Right].Execute(); actionExecuted = true; }

            // Thumbstick movement
            if (!actionExecuted)
            {
                Vector2 stick = currentState.ThumbSticks.Left;
                stick.Y *= -1;

                if (stick.LengthSquared() >= DeadZoneThreshold * DeadZoneThreshold)
                {
                    if (stick.Y < -0.5f) { _movementCommands[Direction.Up].Execute(); actionExecuted = true; }
                    else if (stick.Y > 0.5f) { _movementCommands[Direction.Down].Execute(); actionExecuted = true; }
                    else if (stick.X < -0.5f) { _movementCommands[Direction.Left].Execute(); actionExecuted = true; }
                    else if (stick.X > 0.5f) { _movementCommands[Direction.Right].Execute(); actionExecuted = true; }
                }
            }

            // Idle fallback
            if (!actionExecuted && WasPreviouslyMoving(_previousState))
            {
                _idleCommand.Execute();
            }

            _previousState = currentState;
        }


        private bool WasPreviouslyMoving(GamePadState prev)
        {
            return prev.IsButtonDown(Buttons.DPadUp) ||
                   prev.IsButtonDown(Buttons.DPadDown) ||
                   prev.IsButtonDown(Buttons.DPadLeft) ||
                   prev.IsButtonDown(Buttons.DPadRight) ||
                   prev.ThumbSticks.Left.LengthSquared() >= DeadZoneThreshold * DeadZoneThreshold;
        }
    }

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
}
