using Microsoft.Xna.Framework;

public static class GameTimer
{
    public static double TotalGameTime { get; private set; }

    public static void Update(GameTime gameTime)
    {
        TotalGameTime = gameTime.TotalGameTime.TotalSeconds;
    }
}
