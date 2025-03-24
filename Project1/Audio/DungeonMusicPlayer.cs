using Microsoft.Xna.Framework.Content;
using System.Diagnostics;

namespace Project1.Audio
{
    public class DungeonMusicPlayer
    {
        private static DungeonMusicPlayer instance;
        public static DungeonMusicPlayer Instance => instance ??= new DungeonMusicPlayer();

        private DungeonMusicPlayer() { }

        public void LoadContent(ContentManager content)
        {
            
            Debug.WriteLine("DungeonMusicPlayer: Delegating content loading to MusicManager.");
        }

        private bool warnedGameOver = false;

        public void PlayDungeonMusic()
        {
            if (GameManager.Instance.IsGameOver())
            {
                if (!warnedGameOver)
                {
                    Debug.WriteLine("DungeonMusicPlayer: Blocked dungeon music due to Game Over.");
                    warnedGameOver = true;
                }
                return;
            }

            warnedGameOver = false;
            MusicManager.Instance.PlayDungeonMusic();
        }



        public void StopDungeonMusic()
        {
            if (MusicManager.Instance.CurrentSong != null)
            {
                MusicManager.Instance.Stop();
                Debug.WriteLine("DungeonMusicPlayer: Music stopped.");
            }
        }
    }
}
