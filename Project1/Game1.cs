using System.Collections.Generic;
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
        private MouseController mouseController;
        private KeyboardController keyboardController;

        private ISprite _textSprite;   
        private ISprite _activeSprite; 

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        public ISprite ActiveSprite
        {
            get => _activeSprite;
            set => _activeSprite = value;
        }
        protected override void Initialize()
        {
            var commands = new Dictionary<MouseRegion, ICommand>
    {
        { MouseRegion.TopLeft, new TopLeftCommand(this) },
        { MouseRegion.TopRight, new TopRightCommand(this) },
        { MouseRegion.BottomLeft, new BottomLeftCommand(this) },
        { MouseRegion.BottomRight, new BottomRightCommand(this) }
    };
            var keyboardCommands = new Dictionary<Keys, ICommand>
    {
        { Keys.D1, new TopLeftCommand(this) },
        { Keys.D2, new TopRightCommand(this) },
        { Keys.D3, new BottomLeftCommand(this) },
        { Keys.D4, new BottomRightCommand(this) }
    };

            // Initialize the KeyboardController
            keyboardController = new KeyboardController(keyboardCommands);
            mouseController = new MouseController(GraphicsDevice, commands);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            
            Texture2D spriteSheet = Content.Load<Texture2D>("Images/Link Spritesheet");
            Texture2D staticTexture = Content.Load<Texture2D>("Images/Link");
            SpriteFont font = Content.Load<SpriteFont>("Images/File");
            _activeSprite = new NMoveNAnim
            (
                staticTexture,
                new Vector2(350, 170)
            );

            // Create a static text sprite
            _textSprite = new TextSprite(
                "Credits\n Made by Jarrett Reeves\n Sprites from: https://www.spriters-resource.com/fullview/8366/",
                font,
                new Vector2(200, 400)
            );

            

        }

        protected override void Update(GameTime gameTime)
        {
            var viewport = GraphicsDevice.Viewport;
            int screenWidth = viewport.Width;
            int screenHeight = viewport.Height;
            var mouseState = Mouse.GetState();
            var keyboardState = Keyboard.GetState();
            if (mouseState.RightButton == ButtonState.Pressed || (keyboardState.IsKeyDown(Keys.D0) || keyboardState.IsKeyDown(Keys.NumPad0)))
            {
                Exit();
            }
            // Keyboard input to switch sprites

            keyboardController?.Update(gameTime);

            mouseController.Update(gameTime);
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

            
            _textSprite?.Draw(_spriteBatch);
            _activeSprite?.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
