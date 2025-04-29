using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Project1.Collision;

using Project1.GameObjects.Environment;
using Project1.Interfaces;
using Project1.Audio;

public partial class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private KeyboardController keyboardController;
    private GamepadController gamepadController;
    private Link link;
    private KeyboardState previousKeyboard;

    private List<environmentTile> tiles = new List<environmentTile>();
    private List<IEnemy> enemies = new List<IEnemy>();

    private DungeonMusicPlayer dungeonMusicPlayer;

    private Camera Camera;
    private DevConsole devConsole;
    private KeyboardState prevKeyboardState;


    Texture2D linkTexture;
    Texture2D hudTexture;
    Texture2D heartsTexture;
    Texture2D coverTexture;
    Texture2D environmentTexture;
    Texture2D npcTexture;
    Texture2D itemTexture;
    Texture2D aquamentusTexture;
    Texture2D enemyTexture;
    Texture2D enemyDeathTexture;
    Texture2D enemySpawnTexture;
    Texture2D atlasTexture;
    private PortalManager portalManager;

    //ISprite enemyDeathCloud;
    //ISprite enemySpawnCloud;
    SpriteFont font1;
    
    private int currentBlockIndex = 0;
    private int currentItemIndex = 0;
    //private int currentNPCIndex = 0;
    
    private int currentEnemyIndex = 0;

    private List<IItem> itemsList = new List<IItem>();

    private List<IAnimation> animationsList = new List<IAnimation>();

    levelManager levels;
    HUD hud;

    private bool paused = false;
    private bool IsTransitioning = false;



    //Debug Variables
    Texture2D pixelTexture;
    bool debugDraw = false;

    private EntityBuilder entityBuilder;// = new EntityBuilder(aquamentusTexture, enemytexture, );
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    
    
    protected override void Update(GameTime gameTime)
    {
        if (!devConsole.IsOpen)
        {
            keyboardController.Update(gameTime);
            gamepadController.Update(gameTime);
        }

        hud.Update(gameTime);
        portalManager?.Update(gameTime);
        GameTimer.Update(gameTime);// Gamepad input added
        if (!paused)
        { 
            hud.slideOut();
            DungeonMusicPlayer.Instance.PlayDungeonMusic();
            GameManager.Instance.Update(gameTime);
            
            if(!IsTransitioning)
            {
                //freezing link during transition breaks only the downward transition for some reason
            }
            link.Update(gameTime);
            foreach (var tile in tiles)
            {
                tile.Update(gameTime);
                    if(tile is pushableBlock block)
                    {
                        block.Update();
                    }
            }
                KeyboardState currentKeyboard = Keyboard.GetState();
                if (currentKeyboard.IsKeyDown(Keys.M) && previousKeyboard.IsKeyUp(Keys.M))
                {
                    DungeonMusicPlayer.Instance.ToggleMusic();
                }
                previousKeyboard = currentKeyboard;

                base.Update(gameTime);
            CollisionBox portalBox = portalManager.GetBlueCollider(); // or similar
            foreach (var tile in tiles)
            {
                var wallCollider = tile.GetCollider();
                if (wallCollider != null && portalBox != null && portalBox.Intersects(wallCollider))
                {
                    portalManager.StopBluePortal(); // or: portal.StopMoving()
                }
            }


            UpdateCollisions(gameTime);

            removeInactive();
        }
        else
        {
            hud.slideIn();
        }
        KeyboardState currentKeyboardState = Keyboard.GetState();

        if (currentKeyboardState.IsKeyDown(Keys.OemTilde) && prevKeyboardState.IsKeyUp(Keys.OemTilde))
        {
            devConsole.Toggle();
        }

        devConsole.Update(gameTime);

        if (devConsole.IsOpen)
        {
            
            prevKeyboardState = currentKeyboardState;
            return;
        }

        prevKeyboardState = currentKeyboardState;


    }

    public void removeInactive() {
        int x = enemies.Count - 1;

        while (x >= 0) {
            if (!enemies[x].Alive()) {
                IItem i = entityBuilder.buildItem(enemies[x].getLoot(), enemies[x].getPos());
                itemsList.Add(i);

                IAnimation death = entityBuilder.buildAnimation(1, enemies[x].getPos());
                animationsList.Add(death);

                enemies.RemoveAt(x);
                //TODO: play death animation also
            }
            x--;
        }

        int why = itemsList.Count - 1;
        while (why >= 0)
        {
            if (!itemsList[why].isActive())
            {
                itemsList.RemoveAt(why);
            }
            why--;
        }

        int z = animationsList.Count - 1;
        while (z >= 0) {
            if (!animationsList[z].isActive())
            {
                animationsList.RemoveAt(z);
            }
            z--;
        }
    }
   

    public void RestartGame()
    {
        // Clear all game objects
        tiles.Clear();
        itemsList.Clear();
        enemies.Clear();
        animationsList.Clear();

        // Reinitialize the game
        Initialize();
    }

    
    public void PauseGame()
    {
        paused = !paused;
    }
}
