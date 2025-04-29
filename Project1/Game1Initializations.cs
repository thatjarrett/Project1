using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1.Commands;
using Project1.Controllers;
using Project1.Entities;
using Project1.GameObjects.Environment;
using Project1.Interfaces;
using Project1.LevelLoading;
using Project1.Audio;
using Project1.HUDNamespace;
using static Project1.Entities.Link;
using Project1;



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
    private EntityBuilder entityBuilder;// = new EntityBuilder(aquamentusTexture, enemytexture, );


    //Debug Variables
    Texture2D pixelTexture;
    bool debugDraw = false;
    protected override void Initialize()
    {
        base.Initialize();

        Camera = new Camera(new Viewport());
        hud = new HUD(link, hudTexture, heartsTexture, coverTexture, atlasTexture, font1, Camera);
        devConsole = new DevConsole(font1, GraphicsDevice, link);


        var commands = new Dictionary<Keys, ICommand>
    {{ Keys.C, new StartBluePortalCommand(link) },
    { Keys.V, new StartOrangePortalCommand(link) },
{ Keys.W, new MoveUpCommand(link,hud) },
{ Keys.Up, new MoveUpCommand(link,hud) },
{ Keys.S, new MoveDownCommand(link,hud) },
{ Keys.Down, new MoveDownCommand(link,hud) },
{ Keys.A, new MoveLeftCommand(link,hud) },
{ Keys.Left, new MoveLeftCommand(link,hud) },
{ Keys.D, new MoveRightCommand(link,hud) },
{ Keys.Right, new MoveRightCommand(link,hud) },
{ Keys.Z, new AttackCommand(link) },
{ Keys.N, new AttackCommand(link) },
{ Keys.E, new DamageCommand(link) },
{ Keys.X, new UseItemCommand(link)},
{ Keys.G, new DeathCommand(link) },
{ Keys.Q, new QuitCommand(this) },
{ Keys.R, new ResetCommand(this) },
{ Keys.Escape, new PauseCommand(this) },

/*{ Keys.T, new CycleBlockCommand(this, false) }, // Previous block
{ Keys.Y, new CycleBlockCommand(this, true) },  // Next block
{ Keys.U, new CycleItemCommand(this, false) }, // Previous item
{ Keys.I, new CycleItemCommand(this, true) },  // Next item
{ Keys.O, new CycleNPCCommand(this, false) }, // Previous NPC
{ Keys.P, new CycleNPCCommand(this, true) }   // Next NPC
*/
    };
        var releaseCommands = new Dictionary<Keys, ICommand>
{
    { Keys.C, new EndBluePortalCommand(link) },
    { Keys.V, new EndOrangePortalCommand(link) }
};

        keyboardController = new KeyboardController(commands, new IdleCommand(link), releaseCommands);

        var movementCommands = new Dictionary<Project1.Controllers.Direction, ICommand>
{
    { Project1.Controllers.Direction.Up, new MoveUpCommand(link, hud) },
    { Project1.Controllers.Direction.Down, new MoveDownCommand(link, hud) },
    { Project1.Controllers.Direction.Left, new MoveLeftCommand(link, hud) },
    { Project1.Controllers.Direction.Right, new MoveRightCommand(link, hud) },

};


        var gamepadCommands = new Dictionary<Buttons, ICommand>
{
    { Buttons.DPadUp, new MoveUpCommand(link,hud) },
    { Buttons.LeftThumbstickUp, new MoveUpCommand(link,hud) },
    { Buttons.DPadDown, new MoveDownCommand(link,hud) },
    { Buttons.LeftThumbstickDown, new MoveDownCommand(link,hud) },
    { Buttons.DPadLeft, new MoveLeftCommand(link,hud) },
    { Buttons.LeftThumbstickLeft, new MoveLeftCommand(link,hud) },
    { Buttons.DPadRight, new MoveRightCommand(link,hud) },
    { Buttons.LeftThumbstickRight, new MoveRightCommand(link,hud) },
    { Buttons.A, new AttackCommand(link) },
    { Buttons.B, new UseItemCommand(link) },
    { Buttons.LeftShoulder, new StartBluePortalCommand(link) },
{ Buttons.RightShoulder, new StartOrangePortalCommand(link) },

    { Buttons.Back, new QuitCommand(this) },
    { Buttons.Start, new ResetCommand(this) },
    { Buttons.BigButton, new ResetCommand(this) }
};
        var gamepadReleaseCommands = new Dictionary<Buttons, ICommand>
{
    { Buttons.LeftShoulder, new EndBluePortalCommand(link) },
    { Buttons.RightShoulder, new EndOrangePortalCommand(link) }
};
        gamepadController = new GamepadController(gamepadCommands, movementCommands, new IdleCommand(link), gamepadReleaseCommands);


        _graphics.PreferredBackBufferWidth = 768;
        _graphics.PreferredBackBufferHeight = 648;
        _graphics.ApplyChanges();


    }

}
