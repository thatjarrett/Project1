using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1.Controllers;
using Project1.Interfaces;

namespace Project1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private ISprite _textSprite;   // For rendering text
        private ISprite _activeSprite; // For rendering the animated sprite

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load the sprite sheet and font
            Texture2D spriteSheet = Content.Load<Texture2D>("Images/Link Spritesheet");
            Texture2D staticTexture = Content.Load<Texture2D>("Images/Link");
            SpriteFont font = Content.Load<SpriteFont>("Images/File");
            _activeSprite = new NMoveNAnim
            (
                staticTexture,
                new Vector2(100, 100)
            );
            // Create a static text sprite
            _textSprite = new TextSprite(
                "Credits\n Made by Jarrett Reeves\n Sprites from: https://www.spriters-resource.com/fullview/8366/",
                font,
                new Vector2(200, 400)
            );

            // Create the animated sprite (e.g., 4 frames, 0.25 seconds per frame)

        }

        protected override void Update(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();
            var keyboardState = Keyboard.GetState();
            if (mouseState.RightButton == ButtonState.Pressed || (keyboardState.IsKeyDown(Keys.D0) || keyboardState.IsKeyDown(Keys.NumPad0)))
            {
                Exit();
            }
            // Keyboard input to switch sprites

            if (keyboardState.IsKeyDown(Keys.D1) || keyboardState.IsKeyDown(Keys.NumPad1)) // Static sprite
            {
                _activeSprite = new NMoveNAnim(
                    Content.Load<Texture2D>("Images/Link"),
                    new Vector2(100, 100)
                );
            }
            if (keyboardState.IsKeyDown(Keys.D2) || keyboardState.IsKeyDown(Keys.NumPad2)) // Animated sprite
            {
                _activeSprite = new NMoveAnim(
                    Content.Load<Texture2D>("Images/Link Spritesheet"),
                    new Vector2(300, 200),
                    20, 24, 60, 4, 16, 0.15
                );
            }
            if (keyboardState.IsKeyDown(Keys.D3) || keyboardState.IsKeyDown(Keys.NumPad3))
            {
                _activeSprite = new MoveNAnim(
                    Content.Load<Texture2D>("Images/LinkJump"),
                    new Vector2(300, 200),
                    new Vector2(0, 100), 50
                );
            }
            if (keyboardState.IsKeyDown(Keys.D4) || keyboardState.IsKeyDown(Keys.NumPad4))
            {
                _activeSprite = new MoveAnim(
                    Content.Load<Texture2D>("Images/Link Spritesheet"), // Sprite sheet
                    new Vector2(300, 200),                             // Starting position
                    new Vector2(100, 0),                               // Velocity 
                    16,                                                // Frame width (pixels)
                    23,                                                // Frame height (pixels)
                    2,
                    3,
                    0.2
                );
            }

            // Update the active sprite
            if (_activeSprite is MoveAnim moveAnim)
            {
                moveAnim.Update(gameTime, GraphicsDevice.Viewport);
            }
            else
            {
                _activeSprite?.Update(gameTime);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            // Draw both sprites
            _textSprite?.Draw(_spriteBatch);
            _activeSprite?.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
