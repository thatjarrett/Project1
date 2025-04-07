using Microsoft.Xna.Framework.Content;
using Project1.Audio; // Assuming MusicManager lives here

namespace Project1.Audio
{
    public class DungeonMusicPlayer
    {
        private static DungeonMusicPlayer instance;
        public static DungeonMusicPlayer Instance => instance ??= new DungeonMusicPlayer();

        private bool warnedGameOver = false;
        private bool isPaused = false;

        private DungeonMusicPlayer() { }

        public void LoadContent(ContentManager content)
        {
            // No-op or delegate to MusicManager if needed
        }

        public void PlayDungeonMusic()
        {
            if (GameManager.Instance.IsGameOver())
            {
                if (!warnedGameOver)
                    warnedGameOver = true;
                return;
            }

            warnedGameOver = false;
            if (!isPaused)
                MusicManager.Instance.PlayDungeonMusic();
        }

        public void StopDungeonMusic()
        {
            if (MusicManager.Instance.CurrentSong != null)
            {
                MusicManager.Instance.Stop();
            }
        }

        public void ToggleMusic()
        {
            if (isPaused)
            {
                isPaused = false;
                MusicManager.Instance.Resume();
            }
            else
            {
                isPaused = true;
                MusicManager.Instance.Pause();
            }
        }
    }
}
