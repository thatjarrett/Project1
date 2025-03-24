using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1.Audio;

public class GameManager
{
    private static GameManager instance;
    public static GameManager Instance => instance ??= new GameManager();

    private bool isGameOver = false;
    private bool showGameOverScreen = false;
    private double gameOverTimer = 0;
    private double resetCooldown = 0;
    private const double GameOverMusicMinPlayTime = 0; 
    private bool gameOverMusicStarted = false;
    private bool resetKeyPreviouslyDown = false;
    private const double GameOverDelay = 2.0;
    private const double ResetDelay = 1.5;
 
    private bool allowReset = false;

    private Texture2D gameOverTexture;

    private GameManager() { }

    public void LoadContent(ContentManager content)
    {
        try
        {
            gameOverTexture = content.Load<Texture2D>("Images/Game_Over");
            Debug.WriteLine($"Game Over texture loaded: {gameOverTexture != null}");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Failed to load Game Over texture: {ex.Message}");
        }
    }

    public void SetGameOver()
    {
        if (isGameOver) return;

        Debug.WriteLine("Game Over triggered.");
        isGameOver = true;
        showGameOverScreen = false;
        gameOverTimer = 0;
        resetCooldown = 0;
        gameOverMusicStarted = false;
      

        MusicManager.Instance.Stop();
        MusicManager.Instance.LockToGameOver(); 
        MusicManager.Instance.PlayDeathMusic();
    }



    public void Update(GameTime gameTime)
    {
        if (isGameOver)
        {
            gameOverTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if (!showGameOverScreen && gameOverTimer >= GameOverDelay)
            {
                showGameOverScreen = true;
                allowReset = true; // allow reset now
                Debug.WriteLine("Game Over screen shown.");

                if (!gameOverMusicStarted)
                {
                    MusicManager.Instance.PlayGameOverMusic();
                    gameOverMusicStarted = true;
                }
            }

      
            KeyboardState kb = Keyboard.GetState();
            GamePadState gp = GamePad.GetState(PlayerIndex.One);
            bool resetKeyNow = kb.IsKeyDown(Keys.R) || gp.IsButtonDown(Buttons.Start);

            if (allowReset && resetKeyNow && !resetKeyPreviouslyDown)
            {
                Debug.WriteLine("Player triggered reset.");
                ResetGame();
            }

            resetKeyPreviouslyDown = resetKeyNow;
        }

    }


    public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
    {
        if (showGameOverScreen && gameOverTexture != null)
        {
            Rectangle fullScreen = new Rectangle(0, 0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height);
            spriteBatch.Draw(gameOverTexture, fullScreen, Color.White);
        }
    }

    public void ResetGame()
    {
        Debug.WriteLine("Game reset.");
        isGameOver = false;
        showGameOverScreen = false;
        gameOverTimer = 0;
        resetCooldown = 0;
        gameOverMusicStarted = false;
        resetKeyPreviouslyDown = false;
     

        MusicManager.Instance.UnlockFromGameOver(); // allow music again
        DungeonMusicPlayer.Instance.PlayDungeonMusic(); // now it's safe
    }




    public bool IsGameOver() => isGameOver;
}
