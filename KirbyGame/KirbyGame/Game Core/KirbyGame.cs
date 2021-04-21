using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Media;

namespace KirbyGame

{
    //test commit Tyler
    //test commit Jackson
    /// <summary> JohnTest342= Commit
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public int level;
        public SoundEffectPlayer player;
        //Enteties: Enemies

        public Avatar mario;//made public

        public Stats stats;

        public LevelLoader levelLoader;


        public TileMap map;

        //perhaps used for reset?
        public TileMap initialMap;

        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;

        internal ControllerKeyboard KInput { get; set; }

        public Camera camera;
        public bool updateCamera = true;
        public Color color = Color.CornflowerBlue;

        public Vector2 gameBounds;

        public Viewport _viewport;
        private bool isMuted = false;
        public Song soundtrack;
        public Boolean boundingBoxToggle;
        private Boolean isPaused = false;
        public Hud Hud;
        public Points points;
        public Checkpoints checkpoints;

        public EventHandler Pause;



        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 496;
            graphics.PreferredBackBufferHeight = graphics.PreferredBackBufferHeight-64;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            player = new SoundEffectPlayer(false, this);
            // TODO: Add your initialization logic here
            KInput = new ControllerKeyboard();
            _viewport = GraphicsDevice.Viewport;

            camera = new Camera(_viewport);
            levelLoader = new LevelLoader(this);

            boundingBoxToggle = false;
            camera.Limits = new Rectangle(new Point(36, -32), new Point(62 * 32, graphics.PreferredBackBufferHeight - 64));
            Hud = new Hud(this);
            points = new Points(Hud);
            level = 1;
            stats = new Stats(2, 6, 0);
            stats.ZeroLives += stats_ZeroLives;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {


            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //loading block textures
            //load item textures and initialize items
            levelLoader.LevelInit(levelLoader.reader, this);
            map = new TileMap(levelLoader.Xbound, levelLoader.Ybound);
            gameBounds = new Vector2(levelLoader.Xbound, levelLoader.Ybound);

            mario = levelLoader.getMario();
            mario.PowerUpChange += stats.mario_PowerUpChange;
            mario.KirbyHurt += stats.mario_TakeDamage;

            foreach (Entity entity in levelLoader.list)
            {
                if (entity is EnemyTest)
                {
                    ((EnemyTest)entity).DeathPoints += stats.AddEnemyDeathPoints;
                    if (((EnemyTest)entity).enemytype is WhispyWoods)
                    {
                        ((EnemyTest)entity).GameWin += _WinGame;
                    }
                }
            }

            map.Insert(levelLoader.list);
            List<int> checkpointList = new List<int>();

            //levelLoader.Hud.NumberOfLives = mario.numLives;
            //hud = new Hud(mario);
            soundtrack = Content.Load<Song>("Kirby dream land theme song");
            MediaPlayer.Play(soundtrack);
            //MediaPlayer.Pause();
            MediaPlayer.IsRepeating = true;


            //load commands to controller


            KInput.LoadDefaultCommands(mario, this);

            //Enemies


        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here (No Content to unload yet)
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            KInput.UpdateInput();
            if (!isPaused && !player.IsInTransition)
            {
                map.updateTileMap(levelLoader.list);
                Collision.PotentialCollisions(levelLoader.list, map);
                levelLoader.LevelUpdate(gameTime, camera, map);

                //Update entities


                base.Update(gameTime);
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            if (!isPaused && !player.IsInTransition)
            {
                if (updateCamera) { camera.LookAt(new Vector2(mario.position.X, mario.position.Y)); }
                GraphicsDevice.Clear(color);

                levelLoader.LayerDraw(spriteBatch);
                levelLoader.HudDraw(spriteBatch,stats);

                spriteBatch.Begin(samplerState: SamplerState.PointClamp/*makes game look nicer*/, transformMatrix: camera.GetViewMatrix(new Vector2(1)));
                //Drawing Sprites
                levelLoader.LevelDraw(spriteBatch);
                spriteBatch.End();
                base.Draw(gameTime);
            }
        }

        public void resetLevel()
        {
            levelLoader = new LevelLoader(this);

            levelLoader.LevelInit(levelLoader.reader, this);


            mario.position = levelLoader.getMario().position;
            levelLoader.list.Remove(levelLoader.getMario());
            levelLoader.list.Add(mario);

            //mario.setStateSmall();
            mario.IsDead = false;
            Hud.ResetTime();

            map = new TileMap(levelLoader.Xbound, levelLoader.Ybound);
            if (mario.Y > 12)
                camera.Limits = new Rectangle(new Point(36, -32), new Point(210 * 32, 15 * 32));
            else
                camera.Limits = new Rectangle(new Point(32, 18 * 32), new Point(20 * 32, 13 * 32));
            //mario = levelLoader.getMario();
            map.Insert(levelLoader.list);
            checkpoints.resetFromCheckpoint();
            MediaPlayer.IsMuted = false;

        }

        public void HardReset()
        {
            levelLoader = new LevelLoader(this);

            levelLoader.LevelInit(levelLoader.reader, this);

            mario.position = levelLoader.getMario().position;
            levelLoader.list.Remove(levelLoader.getMario());
            levelLoader.list.Add(mario);

            //mario.setStateSmall();
            mario.IsDead = false;

            map = new TileMap(levelLoader.Xbound, levelLoader.Ybound);
            map.Insert(levelLoader.list);

            //checkpoints = new Checkpoints(mario, this);
            camera.Limits = new Rectangle(new Point(36, -32), new Point(62 * 32, graphics.PreferredBackBufferHeight - 64));

            stats = new Stats(2, 6, 0);
            mario.PowerUpChange += stats.mario_PowerUpChange;
            mario.KirbyHurt += stats.mario_TakeDamage;
            soundtrack = Content.Load<Song>("Kirby dream land theme song");
            MediaPlayer.Play(soundtrack);
            MediaPlayer.IsRepeating = true;
        }
        public void TogglePause()
        {
            levelLoader.ScreenDraw(spriteBatch, true, false, false);
            if (isPaused)
            {
                onPause(EventArgs.Empty);
                MediaPlayer.Resume();
            }
            else
            {
                onPause(EventArgs.Empty);
                MediaPlayer.Pause();
            }
            isPaused = !isPaused;
        }

        public void stats_ZeroLives(object sender, EventArgs e)
        {

            TogglePause();
            KInput.clearCommands();
            KInput.addPressCommand(Keys.Escape, new ExitCommand(this));
            KInput.addPressCommand(Keys.Q, new ResetCommand(this));
            levelLoader.ScreenDraw(spriteBatch, false, false, true);
            soundtrack = Content.Load<Song>("gameover");
            System.Threading.Thread.Sleep(1000);
            MediaPlayer.Play(soundtrack);
            MediaPlayer.IsRepeating = true;
        }

        public void _WinGame(object sender, EventArgs e)
        {
            TogglePause();
            KInput.clearCommands();
            KInput.addPressCommand(Keys.Q, new ExitCommand(this));
            KInput.addPressCommand(Keys.R, new ResetCommand(this));
            levelLoader.ScreenDraw(spriteBatch, false, true, false);
            
        }

        public void onPause(EventArgs e)
        {
            Pause?.Invoke(this, e);
        }

        public void ExitCommand()
        {
            Exit();
        }

        public void ResetCommand()
        {
            HardReset();
        }

        public void ToggleBoundingBoxes()
        {
            this.boundingBoxToggle = !boundingBoxToggle;
        }

        public void ToggleMuteCommand()
        {
            isMuted = !isMuted;
            player.IsMuted = !player.IsMuted;
            if (isMuted)
            {
                MediaPlayer.IsMuted = true;
                player.IsMuted = true;
            }
            else
            {
                MediaPlayer.IsMuted = false; ;
                player.IsMuted = false;
            }
        }
    }

}
