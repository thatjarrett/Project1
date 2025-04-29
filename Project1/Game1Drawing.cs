using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Collision;
using Project1.GameObjects.Environment;



public partial class Game1 : Game
{
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);


        _spriteBatch.Begin(
            SpriteSortMode.Deferred,
            BlendState.NonPremultiplied,
            SamplerState.PointClamp,
            null,
            null,
            null,
            Camera.GetTransformation(link.GetCenterPos(), ref IsTransitioning)
        );

        // Draw regular tiles first
        foreach (var tile in tiles)
        {
            if (tile is not pushableBlock/* && tile is not doorTile*/)
                tile.Draw(_spriteBatch);
        }

        // Then draw pushable blocks on top
        foreach (var tile in tiles)
        {
            if (tile is pushableBlock)
                tile.Draw(_spriteBatch);
        }

        foreach (var tile in tiles)
        {
            if (tile is doorTile)
            {
                CollisionBox collider = tile.GetCollider();
                if (collider != null)
                {
                    //tile.GetCollider().DebugDraw(_spriteBatch, pixelTexture, collider.hitbox, Color.White);
                }

            }
        }

        foreach (var anim in animationsList)
        {
            anim.Draw(_spriteBatch, SpriteEffects.None);
        }

        foreach (var item in itemsList)
        {
            item.Draw(_spriteBatch, SpriteEffects.None);
            if (debugDraw)
            {
                item.GetCollider().DebugDraw(_spriteBatch, pixelTexture, item.GetCollider().hitbox, Color.White);
            }
        }

        foreach (var enemy in enemies)
        {
            enemy.Draw(_spriteBatch);
            if (debugDraw)
            {
                enemy.GetCollider().DebugDraw(_spriteBatch, pixelTexture, enemy.GetCollider().hitbox, Color.Red);
            }
        }

        //Keep link below the tiles so he's drawn above them
        foreach (var tile in tiles)
        {

            if (tile is pushableBlock)
            {
                tile.Draw(_spriteBatch);
            }
        }
        portalManager?.Draw(_spriteBatch);
        if (!paused && !IsTransitioning)
        {
            link.Draw(_spriteBatch);
        }

        if (debugDraw)
        {
            link.GetCollider().DebugDraw(_spriteBatch, pixelTexture, link.GetCollider().hitbox, Color.Blue);
        }



        _spriteBatch.End();

        _spriteBatch.Begin(
            SpriteSortMode.Deferred,
            BlendState.NonPremultiplied,
            SamplerState.PointClamp
        );

        hud.Draw(_spriteBatch);
        GameManager.Instance.Draw(_spriteBatch, GraphicsDevice);

        _spriteBatch.End();

        base.Draw(gameTime);

        _spriteBatch.Begin();
        devConsole.Draw(_spriteBatch);
        _spriteBatch.End();
    }

}
