using Project1.GameObjects.Items;
using Project1.Interfaces;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Project1.Entities
{
    public partial class Link
    {
        public void Pickup(IItem item)
        {
            switch (item)
            {
                case Bomb:
                    bombCount++;
                    inventory.Add(item);
                    break;
                case Rupee:
                    rupeeCount++;
                    break;
                case Heart:
                    if (health < maxHealth) health++;
                    break;
                case Key:
                    keyCount++;
                    break;
                case Arrow:
                    arrowCount++;
                    break;
                case Bow:
                    hasBow = true;
                    inventory.Add(item);
                    break;
                case Boomerang:
                    hasBoomerang = true;
                    inventory.Add(item);
                    break;
                case Clock:
                    freezeEnemies = true;
                    freezeTimer = 5;
                    break;
                case Compass:
                    hasCompass = true;
                    inventory.Add(item);
                    break;
                case Fairy:
                    health = maxHealth;
                    break;
                case HeartContainer:
                    health += 2;
                    break;
                case Map:
                    hasMap = true;
                    inventory.Add(item);
                    break;
                case TriForcePiece:
                    hasTriforce = true;
                    inventory.Add(item);
                    break;
                default:
                    Debug.WriteLine("ERROR: UNKNOWN ITEM");
                    break;
            }
        }

        public bool HasKey() => keyCount > 0;
        public void UseKey() => keyCount--;

        public void TakeDamage()
        {
            if (health > 0 && !isInvincible)
                health--;
        }

        public int GetHealth() => health;
        public int GetRupeeCount() => rupeeCount;
        public int GetBombCount() => bombCount;
        public int GetKeyCount() => keyCount;
        public Collection<IItem> GetInventory() => inventory;

        public bool GetMap() => hasMap;
        public bool IsFrozen()
        {
            if (freezeEnemies)
                Debug.WriteLine("⏸ Link freezeEnemies is TRUE");

            return freezeEnemies;
        }

        public int GetCurrentItem() => currentItemIndex;
        public void SetCurrentItem(int item) => currentItemIndex = item;
    }
}