using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;

namespace Project1.Audio
{
    public class DungeonMusicPlayer
    {
        private static DungeonMusicPlayer instance;
        public static DungeonMusicPlayer Instance => instance ??= new DungeonMusicPlayer();

        private Song dungeonMusic;

        private DungeonMusicPlayer() { }

        public void LoadContent(ContentManager content)
        {
            dungeonMusic = content.Load<Song>("Dungeon"); // Ensure "Dungeon.xnb" exists
        }
        public void PlayDungeonMusic()
        {
            if (GameManager.Instance.IsGameOver())  // 🔥 Prevents Dungeon music from starting at Game Over
            {
                Debug.WriteLine("🚫 Dungeon music prevented from playing due to Game Over.");
                return;
            }

            if (dungeonMusic != null && MediaPlayer.State != MediaState.Playing)
            {
                MediaPlayer.Stop(); // Stop other music before playing
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Play(dungeonMusic);
                Debug.WriteLine("🎵 Dungeon music started.");
            }
        }




        public void StopMusic()
        {
            if (MediaPlayer.State == MediaState.Playing)
            {
                MediaPlayer.Pause(); // Pause instead of stopping
                Debug.WriteLine("Dungeon music paused.");
            }
        }

    }
}
