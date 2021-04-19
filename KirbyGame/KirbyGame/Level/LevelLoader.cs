using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System;
using System.Linq;

namespace KirbyGame
{
    public class LevelLoader
    {
        private string level = @".\Level1.xml";
        public XmlTextReader reader;
        public List<Entity> list;
        private BlockFactory blockFactory;
        private ItemFactory itemFactory;
        private HudFactory hudFactory;
        private CannonballFactory cannonballFactory;
        private int delay = 0;

        //private EnemyFactory enemyFactory;
        private EnemyFactoryTest enemyFactoryTest;
        public int Xbound;
        public int Ybound;
        private Game game;
        private List<Layer> _layers;
        //public Hud Hud { get; set; }
        private Avatar mario;
        int timer;

        public Avatar getMario()
        {
            return mario;
        }

        public LevelLoader(Game1 game) {
           
            //Hud = new Hud(game);
            reader = new XmlTextReader(level);
            reader.WhitespaceHandling = WhitespaceHandling.None;
            list = new List<Entity>();
            blockFactory = new BlockFactory(game);
            cannonballFactory = new CannonballFactory(game);
            itemFactory = new ItemFactory(game);
            //enemyFactory = new EnemyFactory(game);
            enemyFactoryTest = new EnemyFactoryTest(game);
            this.game = game;
            _layers = new List<Layer>();
  
        }

        public void LevelInit(XmlTextReader reader, Game1 game)
        {
            while (reader.Read())
            {
                string Xpos;
                string Ypos;
                string Coins;
                //string Time;
                string PowerUp;
                string Enemy;
                string Length;
                string Height;
                string Paral;
                Vector2 location;

                if (reader.IsStartElement())
                {
                    switch (reader.Name.ToString())
                    {
                        //case "TimeLeft":
                        //    Time = reader.GetAttribute("Time");
                        //    game.Hud.TimeLeft = int.Parse(Time);
                        //    break;
                        case "PlayerSpritePos":
                            Xpos = reader.GetAttribute("Xpos");
                            Ypos = reader.GetAttribute("Ypos");
                            location = new Vector2(int.Parse(Xpos), int.Parse(Ypos));
                            game.Hud.PlayerSpritePos = location;
                            break;
                        case "CoinNumPos":
                            Xpos = reader.GetAttribute("Xpos");
                            Ypos = reader.GetAttribute("Ypos");
                            location = new Vector2(int.Parse(Xpos), int.Parse(Ypos));
                            game.Hud.CoinNumPos = location;
                            break;
                        case "ScorePos":
                            Xpos = reader.GetAttribute("Xpos");
                            Ypos = reader.GetAttribute("Ypos");
                            location = new Vector2(int.Parse(Xpos), int.Parse(Ypos));
                            game.Hud.ScorePos = location;
                            break;
                        case "CoinSpritePos":
                            Xpos = reader.GetAttribute("Xpos");
                            Ypos = reader.GetAttribute("Ypos");
                            location = new Vector2(int.Parse(Xpos), int.Parse(Ypos));
                            game.Hud.CoinSpritePos = location;
                            break;
                        case "PlayerNamePos":
                            Xpos = reader.GetAttribute("Xpos");
                            Ypos = reader.GetAttribute("Ypos");
                            location = new Vector2(int.Parse(Xpos), int.Parse(Ypos));
                            game.Hud.PlayerNamePos = location;
                            break;
                        case "LivesLeftPos":
                            Xpos = reader.GetAttribute("Xpos");
                            Ypos = reader.GetAttribute("Ypos");
                            location = new Vector2(int.Parse(Xpos), int.Parse(Ypos));
                            game.Hud.LivesLeftPos = location;
                            break;
                        case "TimeStringPos":
                            Xpos = reader.GetAttribute("Xpos");
                            Ypos = reader.GetAttribute("Ypos");
                            location = new Vector2(int.Parse(Xpos), int.Parse(Ypos));
                            game.Hud.TimeStringPos = location;
                            break;
                        case "TimeRemainingPos":
                            Xpos = reader.GetAttribute("Xpos");
                            Ypos = reader.GetAttribute("Ypos");
                            location = new Vector2(int.Parse(Xpos), int.Parse(Ypos));
                            game.Hud.TimeRemainingPos = location;
                            break;
                        case "GameOverPos":
                            Xpos = reader.GetAttribute("Xpos");
                            Ypos = reader.GetAttribute("Ypos");
                            location = new Vector2(int.Parse(Xpos), int.Parse(Ypos));
                            game.Hud.GameOverPos = location;
                            break;
                        case "RetryPos":
                            Xpos = reader.GetAttribute("Xpos");
                            Ypos = reader.GetAttribute("Ypos");
                            location = new Vector2(int.Parse(Xpos), int.Parse(Ypos));
                            game.Hud.RetryPos = location;
                            break;
                        case "ExitPos":
                            Xpos = reader.GetAttribute("Xpos");
                            Ypos = reader.GetAttribute("Ypos");
                            location = new Vector2(int.Parse(Xpos), int.Parse(Ypos));
                            game.Hud.ExitPos = location;
                            break;
                        case "BackgroundHud":
                            Xpos = reader.GetAttribute("Xpos");
                            Ypos = reader.GetAttribute("Ypos");
                            Paral = reader.GetAttribute("Paral");
                            _layers.Add(new Layer(game.camera, game.Content.Load<Texture2D>("Kirby hud"), new Vector2(int.Parse(Xpos), int.Parse(Ypos)), game._viewport) { Parallax = new Vector2(float.Parse(Paral), 1.0f) });
                            break;
                        case "KirbyHud":
                            Xpos = reader.GetAttribute("Xpos");
                            Ypos = reader.GetAttribute("Ypos");
                            Paral = reader.GetAttribute("Paral");
                            _layers.Add(new KirbyHud(game.camera, new Vector2(int.Parse(Xpos), int.Parse(Ypos)), game._viewport, game) { Parallax = new Vector2(float.Parse(Paral), 1.0f) });
                            break;
                        case "Level_1-1":
                            Xpos = reader.GetAttribute("Xpos");
                            Ypos = reader.GetAttribute("Ypos");
                            Paral = reader.GetAttribute("Paral");
                            _layers.Add(new Layer(game.camera, game.Content.Load<Texture2D>("Kirby Tree level 1-1"), new Vector2(int.Parse(Xpos)  , int.Parse(Ypos)  ), game._viewport) { Parallax = new Vector2(float.Parse(Paral), 1.0f) });
                            break;
                        case "Level_1-2":
                            Xpos = reader.GetAttribute("Xpos");
                            Ypos = reader.GetAttribute("Ypos");
                            Paral = reader.GetAttribute("Paral");
                            _layers.Add(new Layer(game.camera, game.Content.Load<Texture2D>("Kirby Tree level 1-2"), new Vector2(int.Parse(Xpos) , int.Parse(Ypos) ), game._viewport) { Parallax = new Vector2(float.Parse(Paral), 1.0f) });
                            break;
                        case "Level_1-3":
                            Xpos = reader.GetAttribute("Xpos");
                            Ypos = reader.GetAttribute("Ypos");
                            Paral = reader.GetAttribute("Paral");
                            _layers.Add(new Layer(game.camera, game.Content.Load<Texture2D>("Kirby Tree level 1-3"), new Vector2(int.Parse(Xpos) , int.Parse(Ypos) ), game._viewport) { Parallax = new Vector2(float.Parse(Paral), 1.0f) });
                            break;
                        case "Level_1-4":
                            Xpos = reader.GetAttribute("Xpos");
                            Ypos = reader.GetAttribute("Ypos");
                            Paral = reader.GetAttribute("Paral");
                            _layers.Add(new Layer(game.camera, game.Content.Load<Texture2D>("Kirby Tree level 1-4"), new Vector2(int.Parse(Xpos) , int.Parse(Ypos) ), game._viewport) { Parallax = new Vector2(float.Parse(Paral), 1.0f) });
                            break;
                        case "Size":
                            this.Xbound = int.Parse(reader.GetAttribute("XBound")) * 16;
                            this.Ybound = int.Parse(reader.GetAttribute("YBound")) * 16;
                            Debug.WriteLine("Creating :" + Xbound + ", " + Ybound);
                            break;
                        case "Mario":
                            Xpos = reader.GetAttribute("Xpos");
                            Ypos = reader.GetAttribute("Ypos");
                            mario = new Avatar(game, new Vector2(int.Parse(Xpos) * 32, int.Parse(Ypos) * 32));
                            list.Add(mario);
                            Debug.WriteLine("Creating mario at:" + Xpos + ", " + Ypos);
                            break;
                       
                        case "FloorBlock":
                            Xpos = reader.GetAttribute("Xpos");
                            Ypos = reader.GetAttribute("Ypos");
                            list.Add(blockFactory.createBlock(Block.blocktypes.FLOOR, new Vector2(int.Parse(Xpos), int.Parse(Ypos)), 0, 0,0));
                            Debug.WriteLine("Creating FLOOR at:" + Xpos + ", " + Ypos);
                            break;
                        case "Floor":
                            Xpos = reader.GetAttribute("Xpos");
                            Ypos = reader.GetAttribute("Ypos");
                            Length = reader.GetAttribute("Length");
                            for (int i = 0; i < int.Parse(Length); i++)
                            {
                                list.Add(blockFactory.createBlock(Block.blocktypes.FLOOR, new Vector2((int.Parse(Xpos) + i) * 32, int.Parse(Ypos) * 32), 0, 0,0));
                            }
                            Debug.WriteLine("Creating FLOOR at:" + Xpos + ", " + Ypos);
                            break;
                        case "Platform":
                            Xpos = reader.GetAttribute("Xpos");
                            Ypos = reader.GetAttribute("Ypos");
                            list.Add(blockFactory.createBlock(Block.blocktypes.STAIR, new Vector2((int.Parse(Xpos)) * 16, int.Parse(Ypos) * 16), 0, 0, 0));
                            Debug.WriteLine("Creating FLOOR at:" + Xpos + ", " + Ypos);
                            break;
                        case "Door":
                            Xpos = reader.GetAttribute("Xpos");
                            Ypos = reader.GetAttribute("Ypos");
                            string Stage = reader.GetAttribute("Stage");
                            Block temp = blockFactory.createBlock(Block.blocktypes.HIDDEN, new Vector2((int.Parse(Xpos)) * 16, int.Parse(Ypos) * 32), 0, 0, 0);
                            temp.stage = int.Parse(Stage);
                            list.Add(temp);
                            Debug.WriteLine("Creating FLOOR at:" + Xpos + ", " + Ypos);
                            break;
                        case "Shotzo":
                            Xpos = reader.GetAttribute("Xpos");
                            Ypos = reader.GetAttribute("Ypos");
                            list.Add(enemyFactoryTest.createEnemy(EnemyTest.enemytypes.SHOTZO, new Vector2(int.Parse(Xpos) * 32 + 16, int.Parse(Ypos) * 32)));
                            Debug.WriteLine("Creating Goomba at:" + Xpos + ", " + Ypos);
                            break;
                        case "WhispyWoods":
                            Xpos = reader.GetAttribute("Xpos");
                            Ypos = reader.GetAttribute("Ypos");
                            list.Add(enemyFactoryTest.createEnemy(EnemyTest.enemytypes.WHISPYWOODS, new Vector2((int.Parse(Xpos)) * 32, int.Parse(Ypos) * 32)));
                            Debug.WriteLine("Creating FLOOR at:" + Xpos + ", " + Ypos);
                            break;
                    }
                }
            }
        }

        public void LevelUpdate(GameTime gameTime, Camera camera, TileMap map)
        {

            timer += gameTime.ElapsedGameTime.Milliseconds;
            List<Entity> copy = new List<Entity>();
            foreach (Entity entity in list.ToList()){
                entity.Update(gameTime);
                copy.Add(entity);
            }
            foreach (Entity entity in copy)
            {
                if (entity.GetType().Name == "Cannonball" || entity.GetType().Name == "Boomerang")
                {
                    if (entity.position.X > this.Xbound || entity.position.X < 0 || entity.position.Y > this.Ybound || entity.position.Y < 0 || entity.position.X < camera.Position.X
                    || entity.position.X > (camera.Position.X + 25 * 32) || entity.position.Y < camera.Position.Y || entity.position.Y > (camera.Position.Y + 13 * 32) || entity.remove)
                    {

                        list.Remove(entity);
                        map.Remove(entity);
                        //mario.fireBallNum--;
                    }
                }
                else if(entity is EnemyTest)
                {
                    if(!(entity.position.X > this.Xbound || entity.position.X < 0 || entity.position.Y > this.Ybound || entity.position.Y < 0 || entity.position.X < camera.Position.X
                    || entity.position.X > (camera.Position.X + 25 * 32) || entity.position.Y < camera.Position.Y || entity.position.Y > (camera.Position.Y + 13 * 32) || entity.remove))
                    {
                        ((EnemyTest)entity).seen = true;
                    }
                    
                }
                else if(entity is Block)
                {
                   if(((Block)entity).blocktype is PipeTop)
                    {
                        timer += gameTime.ElapsedGameTime.Milliseconds;
                        if (((Block)entity).enemy > 0 && timer > 10000)
                        {
                            ((Block)entity).ReleaseEnemie();

                        }
                        else if (((Block)entity).item > 0 && timer > 10000)
                        {
                            ((Block)entity).ReleaseItem();
                            timer = 0;
                        }
                    }
                }
            }
            ///Update layers
            foreach (Layer layerSprite in _layers)
            {
                layerSprite.Update();
            }

            //Hud.Update(gameTime);
        }

       

        public void LevelDraw(SpriteBatch spriteBatch)
        {
            foreach (Entity entity in list)
            {
                entity.Draw(spriteBatch);
            }
        }

        public void LayerDraw(SpriteBatch spriteBatch)
        {
            foreach (Layer layer in _layers)
            {
                layer.Draw(spriteBatch);
            }
        }

        //public void HudDraw(SpriteBatch spriteBatch)
        //{
        //    Hud.Draw(spriteBatch);
        //}
    }
}
