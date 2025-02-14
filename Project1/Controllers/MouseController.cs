using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1.Interfaces;
public enum MouseRegion
{
    None,        // No region selected
    TopLeft,     // Top-left quarter of the screen
    TopRight,    // Top-right quarter of the screen
    BottomLeft,  // Bottom-left quarter of the screen
    BottomRight, // Bottom-right quarter of the screen
    Quit         // Right-click to quit
}

public class MouseController : IController
{
    private readonly GraphicsDevice _graphicsDevice;
    private readonly Dictionary<MouseRegion, ICommand> _commands;

    public MouseController(GraphicsDevice graphicsDevice, Dictionary<MouseRegion, ICommand> commands)
    {
        _graphicsDevice = graphicsDevice;
        _commands = commands;
    }

    public void Update(GameTime gameTime)
    {
        var mouseState = Mouse.GetState();
        var viewport = _graphicsDevice.Viewport;

        int screenWidth = viewport.Width;
        int screenHeight = viewport.Height;

        MouseRegion region = GetMouseRegion(mouseState, screenWidth, screenHeight);

        if (region != MouseRegion.None && _commands.ContainsKey(region))
        {
            _commands[region].Execute();
        }
    }

    private MouseRegion GetMouseRegion(MouseState mouseState, int screenWidth, int screenHeight)
    {
        if (mouseState.LeftButton == ButtonState.Pressed)
        {
            if (mouseState.X < screenWidth / 2 && mouseState.Y < screenHeight / 2)
                return MouseRegion.TopLeft;
            if (mouseState.X >= screenWidth / 2 && mouseState.Y < screenHeight / 2)
                return MouseRegion.TopRight;
            if (mouseState.X < screenWidth / 2 && mouseState.Y >= screenHeight / 2)
                return MouseRegion.BottomLeft;
            if (mouseState.X >= screenWidth / 2 && mouseState.Y >= screenHeight / 2)
                return MouseRegion.BottomRight;
        }

        if (mouseState.RightButton == ButtonState.Pressed)
            return MouseRegion.Quit;

        return MouseRegion.None;
    }
}
