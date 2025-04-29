using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project1.Interfaces;
using Project1.Commands;
using Project1.Entities;
using Project1.HUD;
using static Project1.Entities.Link;

namespace Project1.Controllers
{
    class CommandFactory
    {

        public Dictionary<Keys, ICommand> commands(Link link, IHUD hud, Game1 game) {
            var commands = new Dictionary<Keys, ICommand>
            {{ Keys.C, new StartBluePortalCommand(link) },
                { Keys.V, new StartOrangePortalCommand(link) },
                { Keys.W, new MoveUpCommand(link,hud) },
                { Keys.Up, new MoveUpCommand(link,hud) },
                { Keys.S, new MoveDownCommand(link,hud) },
                { Keys.Down, new MoveDownCommand(link,hud) },
{ Keys.A, new MoveLeftCommand(link,hud) },
{ Keys.Left, new MoveLeftCommand(link,hud) },
{ Keys.D, new MoveRightCommand(link,hud) },
{ Keys.Right, new MoveRightCommand(link,hud) },
{ Keys.Z, new AttackCommand(link) },
{ Keys.N, new AttackCommand(link) },
{ Keys.E, new DamageCommand(link) },
{ Keys.X, new UseItemCommand(link)},
{ Keys.G, new DeathCommand(link) },
{ Keys.Q, new QuitCommand(game) },
{ Keys.R, new ResetCommand(game) },
{ Keys.Escape, new PauseCommand(game) },};
            return commands;
        }

        public Dictionary<Keys, ICommand> releaseCommands(Link link) {
            var releaseCommands = new Dictionary<Keys, ICommand>
            {{ Keys.C, new EndBluePortalCommand(link) },
            { Keys.V, new EndOrangePortalCommand(link) }};
            return releaseCommands;
        }

        public Dictionary<Project1.Controllers.Direction, ICommand> moveCommands(Link link, IHUD hud) {
            var movementCommands = new Dictionary<Project1.Controllers.Direction, ICommand>
            {{ Project1.Controllers.Direction.Up, new MoveUpCommand(link, hud) },
            { Project1.Controllers.Direction.Down, new MoveDownCommand(link, hud) },
            { Project1.Controllers.Direction.Left, new MoveLeftCommand(link, hud) },
            { Project1.Controllers.Direction.Right, new MoveRightCommand(link, hud) },};
            return movementCommands;
        }

        public Dictionary<Buttons, ICommand> gamepadComm(Link link, IHUD hud, Game1 game) {
            var gamepadCommands = new Dictionary<Buttons, ICommand>
{
    { Buttons.DPadUp, new MoveUpCommand(link,hud) },
    { Buttons.LeftThumbstickUp, new MoveUpCommand(link,hud) },
    { Buttons.DPadDown, new MoveDownCommand(link,hud) },
    { Buttons.LeftThumbstickDown, new MoveDownCommand(link,hud) },
    { Buttons.DPadLeft, new MoveLeftCommand(link,hud) },
    { Buttons.LeftThumbstickLeft, new MoveLeftCommand(link,hud) },
    { Buttons.DPadRight, new MoveRightCommand(link,hud) },
    { Buttons.LeftThumbstickRight, new MoveRightCommand(link,hud) },
    { Buttons.A, new AttackCommand(link) },
    { Buttons.B, new UseItemCommand(link) },
    { Buttons.LeftShoulder, new StartBluePortalCommand(link) },
{ Buttons.RightShoulder, new StartOrangePortalCommand(link) },

    { Buttons.Back, new QuitCommand(game) },
    { Buttons.Start, new ResetCommand(game) },
    { Buttons.BigButton, new ResetCommand(game) }
};
            return gamepadCommands;
        }

        public Dictionary<Buttons, ICommand> gamepadReleaseComm(Link link) {
            var gamepadReleaseCommands = new Dictionary<Buttons, ICommand>
{
    { Buttons.LeftShoulder, new EndBluePortalCommand(link) },
    { Buttons.RightShoulder, new EndOrangePortalCommand(link) }
};
            return gamepadReleaseCommands;
        }
    }
}
