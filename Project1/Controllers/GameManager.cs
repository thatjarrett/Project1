using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Project1.Audio;

public class GameManager
{
    private static GameManager instance;
    public static GameManager Instance => instance ??= new GameManager();

    private bool isGameOver = false;
    private SoundEffect deathSound;
    private SoundEffectInstance deathSoundInstance;
    private SoundEffect gameOverMusic;
    private SoundEffectInstance gameOverMusicInstance;
    private Texture2D gameOverScreen;
    private bool showGameOverScreen = false;
    private double gameOverTimer = 0;
    private const double GameOverDelay = 2.0; // 2 seconds delay

    private GameManager() { }

    public void LoadContent(ContentManager content)
    {
        try
        {
            // Load death sound and Game Over music
            deathSound = content.Load<SoundEffect>("Audio/Death");
            deathSoundInstance = deathSound?.CreateInstance();
            gameOverMusic = content.Load<SoundEffect>("Audio/GameOver"); // Ensure "GameOver.wav" exists
            gameOverMusicInstance = gameOverMusic?.CreateInstance();
            gameOverScreen = content.Load<Texture2D>("Images/Game_Over"); // Ensure this exists

            Debug.WriteLine("✅ Sounds and Game Over screen loaded successfully.");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"❌ Failed to load sounds or Game Over screen: {ex.Message}");
        }
    }

    public void SetGameOver()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            showGameOverScreen = false;
            gameOverTimer = 0;

            Debug.WriteLine("🔥 Game Over Triggered! Stopping all music...");
            StopAllMusic();

            PlayDeathSound();
        }
    }

    private void StopAllMusic()
    {
        Debug.WriteLine("🎵 Attempting to stop all music...");

        // Stop MediaPlayer (Dungeon music)
        if (MediaPlayer.State == MediaState.Playing)
        {
            MediaPlayer.Stop();
            Debug.WriteLine("⛔ MediaPlayer stopped.");
        }

        // Stop Game Over music
        if (gameOverMusicInstance != null && gameOverMusicInstance.State == SoundState.Playing)
        {
            gameOverMusicInstance.Stop();
            Debug.WriteLine("⛔ Game Over music stopped.");
        }
    }



    private void PlayDeathSound()
    {
        if (deathSoundInstance != null)
        {
            deathSoundInstance.Play();
            Debug.WriteLine("🎵 Death sound played.");
        }
    }

    private void PlayGameOverMusic()
    {
        if (gameOverMusicInstance == null)
        {
            Debug.WriteLine("🚫 No Game Over music loaded.");
            return;
        }

        if (gameOverMusicInstance.State != SoundState.Playing)
        {
            StopAllMusic(); // Ensure nothing else is playing
            gameOverMusicInstance.IsLooped = false; // Play only once
            gameOverMusicInstance.Play();
            Debug.WriteLine("🎶 Game Over music playing.");
        }
        else
        {
            Debug.WriteLine("🚫 Game Over music already playing, skipping...");
        }
    }



    public bool IsGameOver()
    {
        return isGameOver;
    }

    public void Update(GameTime gameTime)
    {
        if (isGameOver && !showGameOverScreen)
        {
            gameOverTimer += gameTime.ElapsedGameTime.TotalSeconds;
            Debug.WriteLine($"⏳ Game Over Timer: {gameOverTimer:F2}s");

            if (gameOverTimer >= GameOverDelay)
            {
                showGameOverScreen = true;
                Debug.WriteLine("🎮 Game Over screen should now be visible!");
                PlayGameOverMusic();
            }
        }

        // Check for reset input (keyboard or gamepad)
        KeyboardState keyboardState = Keyboard.GetState();
        GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);

        if (keyboardState.IsKeyDown(Keys.R) || gamePadState.IsButtonDown(Buttons.Start))
        {
            ResetGame();
        }
    }

    public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
    {
        if (showGameOverScreen && gameOverScreen != null)
        {
            

            // Get screen size
            int screenWidth = graphicsDevice.Viewport.Width;
            int screenHeight = graphicsDevice.Viewport.Height;

            // Scale to fit the screen
            Rectangle fullscreenRect = new Rectangle(0, 0, screenWidth, screenHeight);

            spriteBatch.Draw(gameOverScreen, fullscreenRect, Color.White);
        }
    }

    public void ResetGame()
    {
        if (gameOverMusicInstance != null)
        {
            gameOverMusicInstance.Stop();
            Debug.WriteLine("⛔ Game Over music force stopped in ResetGame.");
        }

        Debug.WriteLine("🔄 Resetting Game...");
        isGameOver = false;
        showGameOverScreen = false;
        gameOverTimer = 0;

        // Stop Game Over music BEFORE restarting anything
        StopAllMusic();

        // Restart Dungeon Music ONLY if Game Over isn't active
        DungeonMusicPlayer.Instance.PlayDungeonMusic();
    }









}
