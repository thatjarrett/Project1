using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1.Entities;
using System.Collections.Generic;

namespace Project1.Interfaces;

public class DevConsole
{
    private SpriteFont font;
    private Texture2D background;
    private string currentInput = "";
    private List<string> commandHistory = new();
    private bool isOpen = false;
    private KeyboardState prevKeyboardState;
    private Link link;


    public bool IsOpen => isOpen;

    public DevConsole(SpriteFont font, GraphicsDevice graphics, Link link)
    {
        this.link = link;
        this.font = font;
        background = new Texture2D(graphics, 1, 1);
        background.SetData(new[] { Color.Black * 0.75f }); // semi-transparent background
    }

    public void Toggle()
    {
        isOpen = !isOpen;
    }

    public void Update(GameTime gameTime)
    {
        if (!isOpen) return;

        KeyboardState state = Keyboard.GetState();

        foreach (var key in state.GetPressedKeys())
        {
            if (prevKeyboardState.IsKeyUp(key))
            {
                if (key == Keys.Enter)
                {
                    commandHistory.Add($"> {currentInput}");
                    HandleCommand(currentInput);
                    currentInput = "";
                }
                else if (key == Keys.Back && currentInput.Length > 0)
                {
                    currentInput = currentInput[..^1];
                }
                else if (key == Keys.Space)
                {
                    currentInput += " ";
                }
                else if (key >= Keys.A && key <= Keys.Z)
                {
                    // Add shift support
                    bool shift = state.IsKeyDown(Keys.LeftShift) || state.IsKeyDown(Keys.RightShift);
                    currentInput += shift ? key.ToString().ToUpper() : key.ToString().ToLower();
                }
                else if (key >= Keys.D0 && key <= Keys.D9)
                {
                    currentInput += key.ToString().Replace("D", "");
                }
            }
        }

        prevKeyboardState = state;
    }

    private void HandleCommand(string command)
    {
        string[] parts = command.Trim().Split(' ');

        if (parts.Length == 0) return;

        if (parts[0] == "help")
        {
            commandHistory.Add("Available commands:");
            commandHistory.Add("- set health [number]");
            commandHistory.Add("- set speed [number]");
            commandHistory.Add("- set keys [number]");
            commandHistory.Add("- set bombs [number]");
        }
        else if (parts[0] == "set" && parts.Length == 3)
        {
            string target = parts[1];
            if (!int.TryParse(parts[2], out int value))
            {
                commandHistory.Add("Invalid number.");
                return;
            }

            switch (target)
            {
                case "health":
                    link.SetHealth(value);
                    commandHistory.Add($"Set health to {value}");
                    break;
                case "speed":
                    //link.SetSpeed(value); // You need to implement this if not already available
                    commandHistory.Add($"Set speed to {value}");
                    break;
                case "keys":
                    link.SetKeys(value); // Implement if needed
                    commandHistory.Add($"Set keys to {value}");
                    break;
                case "bombs":
                    //link.SetBombCount(value); // Implement if needed
                    commandHistory.Add($"Set bombs to {value}");
                    break;
                default:
                    commandHistory.Add("Unknown set target.");
                    break;
            }
        }
        else
        {
            commandHistory.Add($"Unknown command: {command}");
        }
    }


    public void Draw(SpriteBatch spriteBatch)
    {
        if (!isOpen) return;

        spriteBatch.Draw(background, new Rectangle(0, 0, 768, 200), Color.Black * 0.8f);
        int y = 10;
        foreach (string line in commandHistory)
        {
            spriteBatch.DrawString(font, line, new Vector2(10, y), Color.White);
            y += 20;
        }

        spriteBatch.DrawString(font, "> " + currentInput, new Vector2(10, y), Color.Yellow);
    }
}
