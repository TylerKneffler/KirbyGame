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
        //Enteties: Enemies

        public Avatar mario;//made public

        public LevelLoader levelLoader;

        public Checkpoints checkpoints;

        public TileMap map;

        //perhaps used for reset?
        public TileMap initialMap;

        GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;

        internal ControllerKeyboard KInput { get; set; }

        public Camera camera;
        public bool updateCamera = true;
        public Color color = Color.CornflowerBlue;

        public Vector2 gameBounds;

        public Viewport _viewport;
        public Hud Hud { get; set; }
        private bool isMuted = false;
        private Song soundtrack;
        public Boolean boundingBoxToggle;
        private Boolean isPaused = false;
        public Points points;



        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
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
            // TODO: Add your initialization logic here
            KInput = new ControllerKeyboard();

            _viewport = GraphicsDevice.Viewport;

            camera = new Camera(_viewport);
            levelLoader = new LevelLoader(this);

            boundingBoxToggle = false;
            camera.Limits = new Rectangle(new Point(36, -32), new Point(210 * 32, 15 * 32));
            Hud = new Hud(this);
            points = new Points(Hud);


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
            map.Insert(levelLoader.list);
            List<int> checkpointList = new List<int>();

            checkpoints = new Checkpoints(mario, this);
            //levelLoader.Hud.NumberOfLives = mario.numLives;
            //hud = new Hud(mario);
            soundtrack = Content.Load<Song>("Super Mario Bros. Theme Song");
            MediaPlayer.Play(soundtrack);
            MediaPlayer.IsRepeating = true;


            //load commands to controller
            KInput.addPressCommand(Keys.W, new MarioPressUp(mario));
            KInput.addPressCommand(Keys.A, new MarioPressLeft(mario));
            KInput.addPressCommand(Keys.S, new MarioPressDown(mario));
            KInput.addPressCommand(Keys.D, new MarioPressRight(mario));
            KInput.addReleaseCommand(Keys.W, new MarioReleaseUp(mario));
            KInput.addReleaseCommand(Keys.A, new MarioReleaseLeft(mario));
            KInput.addReleaseCommand(Keys.S, new MarioReleaseDown(mario));
            KInput.addReleaseCommand(Keys.D, new MarioReleaseRight(mario));

            KInput.addPressCommand(Keys.Up, new MarioPressUp(mario));
            KInput.addPressCommand(Keys.Left, new MarioPressLeft(mario));
            KInput.addPressCommand(Keys.Down, new MarioPressDown(mario));
            KInput.addPressCommand(Keys.Right, new MarioPressRight(mario));
            KInput.addReleaseCommand(Keys.Up, new MarioReleaseUp(mario));
            KInput.addReleaseCommand(Keys.Left, new MarioReleaseLeft(mario));
            KInput.addReleaseCommand(Keys.Down, new MarioReleaseDown(mario));
            KInput.addReleaseCommand(Keys.Right, new MarioReleaseRight(mario));

            KInput.addPressCommand(Keys.Y, new MakeMarioSmall(mario));
            KInput.addPressCommand(Keys.U, new MakeMarioSuper(mario));
            KInput.addPressCommand(Keys.I, new MakeMarioFire(mario));
            KInput.addPressCommand(Keys.C, new ToggleBoxes(this));
            KInput.addPressCommand(Keys.R, new ResetLevel(this));

            //TEST COMMAND BELOW!!!
            KInput.addPressCommand(Keys.Q, new HardReset(this));

            KInput.addPressCommand(Keys.Escape, new ExitCommand(this));
            KInput.addPressCommand(Keys.P, new PauseCommand(this));
            KInput.addPressCommand(Keys.Space, new MarioFireBall(mario));
            KInput.addPressCommand(Keys.M, new ToggleMute(this));

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
            if (!isPaused)
            {
                map.updateTileMap(levelLoader.list);
                Collision.PotentialCollisions(levelLoader.list, map);
                levelLoader.LevelUpdate(gameTime, camera, map);
                Hud.Update(gameTime);
                checkpoints.Update();
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
            if (!isPaused)
            {
                if (updateCamera) { camera.LookAt(new Vector2(mario.position.X, mario.position.Y)); }
                GraphicsDevice.Clear(color);

                levelLoader.LayerDraw(spriteBatch);

                spriteBatch.Begin(samplerState: SamplerState.PointClamp/*makes game look nicer*/, transformMatrix: camera.GetViewMatrix(new Vector2(1)));
                //Drawing Sprites
                levelLoader.LevelDraw(spriteBatch);
                spriteBatch.End();
                Hud.Draw(spriteBatch);
                base.Draw(gameTime);
            }
        }
        /// <summary>
        ///Controller methods called when the player initiates a keypress linked to 
        ///the associated view method which updates the sprite view value to appear onscreen
        /// </summary

        public void ExitCommand()
        {
            if (Hud.IsGameOver())
            {
                Exit();
            }
        }

        public void ToggleBoxesCommand()
        {
            boundingBoxToggle = !boundingBoxToggle;
        }

        public void ToggleMuteCommand()
        {
            isMuted = !isMuted;
            if (isMuted)
            {
                MediaPlayer.IsMuted = true;
            }
            else
            {
                MediaPlayer.IsMuted = false; ;
            }
        }

        public void resetLevel()
        {

            if (Hud.IsGameOver())
            {
                // Put everything below in this if statement when ready to lose reset functionality except when game is over

            }

            levelLoader = new LevelLoader(this);

            levelLoader.LevelInit(levelLoader.reader, this);


            mario.position = levelLoader.getMario().position;
            levelLoader.list.Remove(levelLoader.getMario());
            levelLoader.list.Add(mario);

            mario.setStateSmall();
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

        }

        public void hardReset()
        {
            if (Hud.IsGameOver())
            {
                levelLoader = new LevelLoader(this);

                levelLoader.LevelInit(levelLoader.reader, this);

                mario.position = levelLoader.getMario().position;
                levelLoader.list.Remove(levelLoader.getMario());
                levelLoader.list.Add(mario);

                mario.setStateSmall();
                mario.IsDead = false;

                map = new TileMap(levelLoader.Xbound, levelLoader.Ybound);
                map.Insert(levelLoader.list);

                checkpoints = new Checkpoints(mario, this);

                Hud.ResetHud();

                pause();
            }
        }
        public void pause()
        {
            if (isPaused)
                MediaPlayer.Resume();
            else
                MediaPlayer.Pause();
            isPaused = !isPaused;
        }

    }
}
