using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using System;

namespace Project1.Audio
{
    public class MusicManager
    {
        private static MusicManager instance;
        public static MusicManager Instance => instance ??= new MusicManager();

        private Song currentSong;
        private bool isRepeating;
        private bool isGameOverLocked = false;
    
        private Song dungeonSong;
        private Song deathSong;
        private Song gameOverSong;

        private MusicManager() { }

        public void LoadContent(ContentManager content)
        {
            dungeonSong = content.Load<Song>("Dungeon");
            deathSong = content.Load<Song>("Audio/Death");
           

            Debug.WriteLine("MusicManager: Songs loaded.");
        }


        public void LockToGameOver()
        {
            isGameOverLocked = true;
            Debug.WriteLine("MusicManager: Locked to Game Over.");
        }

        public void UnlockFromGameOver()
        {
            isGameOverLocked = false;
            Debug.WriteLine("MusicManager: Unlocked from Game Over.");
        }


        public void PlayDungeonMusic()
        {
            if (isGameOverLocked)
            {
                Debug.WriteLine("MusicManager: Dungeon music blocked — Game Over lock active.");
                return;
            }

            PlaySong(dungeonSong, loop: true);
        }

        public void PlayDeathMusic()
        {
            PlaySong(deathSong, loop: false);
        }

        public void PlayGameOverMusic()
        {
            PlaySong(gameOverSong, loop: false);
        }

        private void PlaySong(Song song, bool loop)
        {
            if (song == null)
            {
                Debug.WriteLine("Tried to play null song.");
                return;
            }

            if (isGameOverLocked && song != gameOverSong && song != deathSong)
            {
                Debug.WriteLine($"BLOCKED: Attempt to play {song.Name} during Game Over lock.");
                Debug.WriteLine(Environment.StackTrace);
                return;
            }


           
            if (MediaPlayer.State == MediaState.Playing &&
                currentSong == song &&
                isRepeating == loop)
            {
                
                return;
            }

            Debug.WriteLine($"Switching to song: {song.Name}");

            if (currentSong != song)
            {
                Debug.WriteLine("MediaPlayer STOP called from: [PlaySong - Song Changed]");
                MediaPlayer.Stop();
            }

            MediaPlayer.IsRepeating = loop;
            MediaPlayer.Volume = 1f;
            MediaPlayer.Play(song);

            currentSong = song;
            isRepeating = loop;

            Debug.WriteLine($"MusicManager: Now playing {song.Name}");
        }



        public void Pause()
        {
            MediaPlayer.Pause();
        }

        public void Resume()
        {
            MediaPlayer.Resume();
        }


        public void Stop()
        {
            Debug.WriteLine(" MediaPlayer STOP called from MusicManager.Stop()");
            Debug.WriteLine(Environment.StackTrace);
            MediaPlayer.Stop();
            currentSong = null;
        }

        public Song CurrentSong => currentSong;
    }
}
