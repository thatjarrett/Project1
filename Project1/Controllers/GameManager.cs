using System.Diagnostics;

public class GameManager
{
    private static GameManager instance;
    public static GameManager Instance => instance ??= new GameManager();

    private bool isGameOver = false;

    public void SetGameOver()
    {
        isGameOver = true;
        Debug.WriteLine("Game Over: Display Game Over Screen");

        // Load a Game Over screen or reset the game
        // Example: ScreenManager.Instance.ShowGameOverScreen();
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }
}
