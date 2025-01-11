using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1.Controllers;
using Project1.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace Project1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private ISprite _textSprite;
        private ISprite _activeSprite;
        private Texture2D _texture;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
           

            
            Texture2D texture = Content.Load<Texture2D>("Images/Link");
            _texture = Content.Load<Texture2D>("Images/Link");

            _activeSprite = new NMoveNAnim(texture, new Vector2(250, 250));
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            _activeSprite?.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            Rectangle destinationRectangle = new Rectangle(325, 250, 64, 64); // x, y, width, height
            _spriteBatch.Draw(_texture, destinationRectangle, Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
