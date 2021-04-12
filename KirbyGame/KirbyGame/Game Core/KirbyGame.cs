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
        private bool isMuted = false;
        private Song soundtrack;
        public Boolean boundingBoxToggle;
        private Boolean isPaused = false;



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


            KInput.addPressCommand(Keys.Space, new MarioFireBall(mario));

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
                base.Draw(gameTime);
            }
        }

    }
}
