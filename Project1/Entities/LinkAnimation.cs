using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Interfaces;
using Project1.Sprites;

namespace Project1.Entities
{
    public partial class Link
    {
        public void SetAnimation(string action)
        {
            switch (action)
            {
                case string a when a.Contains("Idle"):
                    linkSprite = currentIdleSprite;
                    break;
                case string a when a.Contains("MoveUp"):
                    SetDirectionalSprites(walkUpSprite, idleUpSprite, attackUpSprite, interactUpSprite, new Vector2(0, -1), SpriteEffects.None);
                    break;
                case string a when a.Contains("MoveDown"):
                    SetDirectionalSprites(walkDownSprite, idleDownSprite, attackDownSprite, interactDownSprite, new Vector2(0, 1), SpriteEffects.None);
                    break;
                case string a when a.Contains("MoveLeft"):
                    SetDirectionalSprites(walkSideSprite, idleSideSprite, attackSideSprite2, interactSideSprite, new Vector2(-1, 0), SpriteEffects.FlipHorizontally);
                    break;
                case string a when a.Contains("MoveRight"):
                    SetDirectionalSprites(walkSideSprite, idleSideSprite, attackSideSprite, interactSideSprite, new Vector2(1, 0), SpriteEffects.None);
                    break;
                case string a when a.Contains("Attack"):
                    linkSprite = currentAttackSprite;
                    break;
                case string a when a.Contains("Item"):
                    linkSprite = currentInteractSprite;
                    break;
                case string a when a.Contains("Damage"):
                    hurting = true;
                    break;
                case string a when a.Contains("Death"):
                    linkSprite = deathSprite;
                    dying = true;
                    break;
            }

            if (!action.Contains("Damage"))
                hurting = false;
        }

        private void SetDirectionalSprites(ISprite walk, ISprite idle, ISprite attack, ISprite interact, Vector2 face, SpriteEffects effect)
        {
            linkSprite = walk;
            currentIdleSprite = idle;
            currentAttackSprite = attack;
            currentInteractSprite = interact;
            faceDirection = face;
            currentSpriteEffect = effect;
        }
    }
}