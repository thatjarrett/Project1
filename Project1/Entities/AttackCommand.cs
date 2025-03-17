using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Project1.Interfaces;
using System.Diagnostics;

namespace Project1.Entities
{
    internal class AttackCommand : ICommand
    {
        private readonly Link _link;
        private static SoundEffect swordSound;
        private static bool soundLoaded = false;

        private static double lastAttackTime = 0; // ⏳ Track last attack time
        private const double attackCooldown = 0.2; // 🔄 Cooldown in seconds

        public AttackCommand(Link link)
        {
            _link = link;
        }

        public static void LoadContent(ContentManager content)
        {
            if (!soundLoaded)
            {
                try
                {
                    swordSound = content.Load<SoundEffect>("Audio/Sword"); // Load the sound
                    soundLoaded = true;
                    Debug.WriteLine("✅ Sword sound loaded successfully.");
                }
                catch
                {
                    Debug.WriteLine("❌ Failed to load sword sound.");
                }
            }
        }

        public void Execute()
        {
            double currentTime = GameTimer.TotalGameTime; // Get current time

            if (currentTime - lastAttackTime >= attackCooldown) // Check cooldown
            {
                lastAttackTime = currentTime; // Update attack time

                if (swordSound != null)
                {
                    swordSound.Play();
                    Debug.WriteLine("🔊 Sword sound played.");
                }

                _link.ChangeState(new LinkAttackState(_link.PreviousDirection));
            }
            else
            {
                Debug.WriteLine("⏳ Attack on cooldown.");
            }
        }
    }
}
