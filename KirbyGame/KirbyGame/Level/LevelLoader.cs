using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System;

namespace KirbyGame
{
    public class LevelLoader
    {
        private string level = @"C:\Users\Jackson Jiang\Source\Repos\KirbyGame\KirbyGame\KirbyGame\Content\Level1.xml"; 
        public XmlTextReader reader;
        public List<Entity> list;
        private BlockFactory blockFactory;
        private ItemFactory itemFactory;
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
                        case "MountainLayer":
                            Xpos = reader.GetAttribute("Xpos");
                            Ypos = reader.GetAttribute("Ypos");
                            Paral = reader.GetAttribute("Paral");
                            _layers.Add(new Layer(game.camera, game.Content.Load<Texture2D>("MountainLayer"), new Vector2(int.Parse(Xpos) , int.Parse(Ypos) ), game._viewport) { Parallax = new Vector2(float.Parse(Paral), 1.0f) });
                            break;
                        case "BushLayer":
                            Xpos = reader.GetAttribute("Xpos");
                            Ypos = reader.GetAttribute("Ypos");
                            Paral = reader.GetAttribute("Paral");
                            _layers.Add(new Layer(game.camera, game.Content.Load<Texture2D>("BushLayer"), new Vector2(int.Parse(Xpos) , int.Parse(Ypos) ), game._viewport) { Parallax = new Vector2(float.Parse(Paral), 1.0f) });
                            break;
                        case "CloudLayer":
                            Xpos = reader.GetAttribute("Xpos");
                            Ypos = reader.GetAttribute("Ypos");
                            Paral = reader.GetAttribute("Paral");
                            _layers.Add(new Layer(game.camera, game.Content.Load<Texture2D>("CloudLayer"), new Vector2(int.Parse(Xpos) , int.Parse(Ypos) ), game._viewport) { Parallax = new Vector2(float.Parse(Paral), 1.0f) });
                            break;
                        case "Size":
                            this.Xbound = int.Parse(reader.GetAttribute("XBound")) * 32;
                            this.Ybound = int.Parse(reader.GetAttribute("YBound")) * 32;
                            Debug.WriteLine("Creating :" + Xbound + ", " + Ybound);
                            break;
                        case "Mario":
                            Xpos = reader.GetAttribute("Xpos");
                            Ypos = reader.GetAttribute("Ypos");
                            mario = new Avatar(game, new Vector2(int.Parse(Xpos) * 32, int.Parse(Ypos) * 32));
                            list.Add(mario);
                            Debug.WriteLine("Creating mario at:" + Xpos + ", " + Ypos);
                            break;
                        case "BrickBlock":
                            Xpos = reader.GetAttribute("Xpos");
                            Ypos = reader.GetAttribute("Ypos");
                            Coins = reader.GetAttribute("Coins");
                            PowerUp = reader.GetAttribute("PowerUp");
                            list.Add(blockFactory.createBlock(Block.blocktypes.BRICK, new Vector2(int.Parse(Xpos) * 32, int.Parse(Ypos) * 32), int.Parse(Coins), int.Parse(PowerUp), 0));
                            Debug.WriteLine("Creating BRICK at:" + Xpos + ", " + Ypos + "Coins: " + Coins + "PowerUp: " + PowerUp);
                            break;
                        case "QuestionBlock":
                            Xpos = reader.GetAttribute("Xpos");
                            Ypos = reader.GetAttribute("Ypos");
                            Coins = reader.GetAttribute("Coins");
                            PowerUp = reader.GetAttribute("PowerUp");
                            list.Add(blockFactory.createBlock(Block.blocktypes.QUESTION, new Vector2(int.Parse(Xpos) * 32, int.Parse(Ypos) * 32), int.Parse(Coins), int.Parse(PowerUp),0));
                            Debug.WriteLine("Creating QUESTION at:" + Xpos + ", " + Ypos + "Coins: " + Coins + "PowerUp: " + PowerUp);
                            break;
                        case "HiddenBlock":
                            Xpos = reader.GetAttribute("Xpos");
                            Ypos = reader.GetAttribute("Ypos");
                            Coins = reader.GetAttribute("Coins");
                            PowerUp = reader.GetAttribute("PowerUp");
                            list.Add(blockFactory.createBlock(Block.blocktypes.HIDDEN, new Vector2(int.Parse(Xpos) * 32, int.Parse(Ypos) * 32), int.Parse(Coins), int.Parse(PowerUp),0));
                            Debug.WriteLine("Creating HIDDEN at:" + Xpos + ", " + Ypos + "Coins: " + Coins + "PowerUp: " + PowerUp);
                            break;
                        case "FloorBlock":
                            Xpos = reader.GetAttribute("Xpos");
                            Ypos = reader.GetAttribute("Ypos");
                            list.Add(blockFactory.createBlock(Block.blocktypes.FLOOR, new Vector2(int.Parse(Xpos) * 32, int.Parse(Ypos) * 32), 0, 0,0));
                            Debug.WriteLine("Creating FLOOR at:" + Xpos + ", " + Ypos);
                            break;
                        case "Floor":
                            Xpos = reader.GetAttribute("Xpos");
                            Ypos = reader.GetAttribute("Ypos");
                            Length = reader.GetAttribute("Length");
                            for (int i = 0; i < int.Parse(Length); i++)
                            {
                                list.Add(blockFactory.createBlock(Block.blocktypes.FLOOR, new Vector2((int.Parse(Xpos) + i) * 32, int.Parse(Ypos) * 32), 0, 0,0));
                                list.Add(blockFactory.createBlock(Block.blocktypes.FLOOR, new Vector2((int.Parse(Xpos) + i) * 32, (int.Parse(Ypos) - 1) * 32), 0, 0,0));
                            }
                            Debug.WriteLine("Creating FLOOR at:" + Xpos + ", " + Ypos);
                            break;
                        case "FloorBlue":
                            Xpos = reader.GetAttribute("Xpos");
                            Ypos = reader.GetAttribute("Ypos");
                            Length = reader.GetAttribute("Length");
                            for (int i = 0; i < int.Parse(Length); i++)
                            {
                                list.Add(blockFactory.createBlock(Block.blocktypes.FLOORBLUE, new Vector2((int.Parse(Xpos) + i) * 32, int.Parse(Ypos) * 32), 0, 0,0));
                                list.Add(blockFactory.createBlock(Block.blocktypes.FLOORBLUE, new Vector2((int.Parse(Xpos) + i) * 32, (int.Parse(Ypos) - 1) * 32), 0, 0,0));
                            }
                            Debug.WriteLine("Creating FLOOR at:" + Xpos + ", " + Ypos);
                            break;
                        case "BrickBlockBlue":
                            Xpos = reader.GetAttribute("Xpos");
                            Ypos = reader.GetAttribute("Ypos");
                            Coins = reader.GetAttribute("Coins");
                            PowerUp = reader.GetAttribute("PowerUp");
                            list.Add(blockFactory.createBlock(Block.blocktypes.BRICKBlUE, new Vector2(int.Parse(Xpos) * 32, int.Parse(Ypos) * 32), int.Parse(Coins), int.Parse(PowerUp),0));
                            Debug.WriteLine("Creating BRICK at:" + Xpos + ", " + Ypos + "Coins: " + Coins + "PowerUp: " + PowerUp);
                            break;
                        case "StairBlock":
                            Xpos = reader.GetAttribute("Xpos");
                            Ypos = reader.GetAttribute("Ypos");
                            list.Add(blockFactory.createBlock(Block.blocktypes.STAIR, new Vector2(int.Parse(Xpos) * 32, int.Parse(Ypos) * 32), 0, 0,0));
                            Debug.WriteLine("Creating STAIR at:" + Xpos + ", " + Ypos);
                            break;
                        case "UsedBlock":
                            Xpos = reader.GetAttribute("Xpos");
                            Ypos = reader.GetAttribute("Ypos");
                            list.Add(blockFactory.createBlock(Block.blocktypes.USED, new Vector2(int.Parse(Xpos) * 32, int.Parse(Ypos) * 32), 0, 0,0));
                            Debug.WriteLine("Creating Usedblock at:" + Xpos + ", " + Ypos);
                            break;
                        case "OneUp":
                            Xpos = reader.GetAttribute("Xpos");
                            Ypos = reader.GetAttribute("Ypos");
                            Entity item = itemFactory.createItem(Item.eItemType.ONE_UP_MUSHROOM, new Vector2(int.Parse(Xpos) * 32, int.Parse(Ypos) * 32));
                            item.remove = false;
                            list.Add(item);
                            Debug.WriteLine("Creating Oneup at:" + Xpos + ", " + Ypos);
                            break;
                        case "Coin":
                            Xpos = reader.GetAttribute("Xpos");
                            Ypos = reader.GetAttribute("Ypos");
                            list.Add(itemFactory.createItem(Item.eItemType.COIN, new Vector2(int.Parse(Xpos) * 32, int.Parse(Ypos) * 32)));
                            Debug.WriteLine("Creating Coin at:" + Xpos + ", " + Ypos);
                            break;
                        case "FireFlower":
                            Xpos = reader.GetAttribute("Xpos");
                            Ypos = reader.GetAttribute("Ypos");
                            list.Add(itemFactory.createItem(Item.eItemType.FIRE_FLOWER, new Vector2(int.Parse(Xpos) * 32, int.Parse(Ypos) * 32)));
                            Debug.WriteLine("Creating FireFlower at:" + Xpos + ", " + Ypos);
                            break;
                        case "Star":
                            Xpos = reader.GetAttribute("Xpos");
                            Ypos = reader.GetAttribute("Ypos");
                            list.Add(itemFactory.createItem(Item.eItemType.STAR, new Vector2(int.Parse(Xpos) * 32, int.Parse(Ypos) * 32)));
                            Debug.WriteLine("Creating Star at:" + Xpos + ", " + Ypos);
                            break;
                        case "SuperMushroom":
                            Xpos = reader.GetAttribute("Xpos");
                            Ypos = reader.GetAttribute("Ypos");
                            list.Add(itemFactory.createItem(Item.eItemType.SUPER_MUSHROOM, new Vector2(int.Parse(Xpos) * 32, int.Parse(Ypos) * 32)));
                            Debug.WriteLine("Creating Super at:" + Xpos + ", " + Ypos);
                            break;
                        case "Flag":
                            Xpos = reader.GetAttribute("Xpos");
                            Ypos = reader.GetAttribute("Ypos");
                            list.Add(itemFactory.createItem(Item.eItemType.FLAG, new Vector2(int.Parse(Xpos) * 32 - 17, int.Parse(Ypos) * 32)));
                            Debug.WriteLine("Creating Flag at:" + Xpos + ", " + Ypos);
                            break;
                        case "Pole":
                            Xpos = reader.GetAttribute("Xpos");
                            Ypos = reader.GetAttribute("Ypos");
                            list.Add(itemFactory.createItem(Item.eItemType.POLE, new Vector2(int.Parse(Xpos) * 32, int.Parse(Ypos) * 32)));
                            Debug.WriteLine("Creating Pole at:" + Xpos + ", " + Ypos);
                            break;
                        case "PoleTop":
                            Xpos = reader.GetAttribute("Xpos");
                            Ypos = reader.GetAttribute("Ypos");
                            list.Add(itemFactory.createItem(Item.eItemType.POLETOP, new Vector2(int.Parse(Xpos) * 32, int.Parse(Ypos) * 32)));
                            Debug.WriteLine("Creating Poletop at:" + Xpos + ", " + Ypos);
                            break;
                        case "Koopa":
                            Xpos = reader.GetAttribute("Xpos");
                            Ypos = reader.GetAttribute("Ypos");
                            list.Add(enemyFactoryTest.createEnemy(EnemyTest.enemytypes.KOOPA, new Vector2(int.Parse(Xpos) * 32, int.Parse(Ypos) * 32 - 16)));
                            Debug.WriteLine("Creating Koopa at:" + Xpos + ", " + Ypos);
                            break;
                        case "Goomba":
                            Xpos = reader.GetAttribute("Xpos");
                            Ypos = reader.GetAttribute("Ypos");
                            list.Add(enemyFactoryTest.createEnemy(EnemyTest.enemytypes.GOOMBA, new Vector2(int.Parse(Xpos) * 32, int.Parse(Ypos) * 32)));
                            Debug.WriteLine("Creating Goomba at:" + Xpos + ", " + Ypos);
                            break;
                        case "Parana":
                            Xpos = reader.GetAttribute("Xpos");
                            Ypos = reader.GetAttribute("Ypos");
                            list.Add(enemyFactoryTest.createEnemy(EnemyTest.enemytypes.PARANA, new Vector2(int.Parse(Xpos) * 32 +16, int.Parse(Ypos) * 32 )));
                            Debug.WriteLine("Creating Goomba at:" + Xpos + ", " + Ypos);
                            break;
                        case "RightStair":
                            Xpos = reader.GetAttribute("Xpos");
                            Ypos = reader.GetAttribute("Ypos");
                            Length = reader.GetAttribute("Length");
                            Height = reader.GetAttribute("Height");
                            for (int i = 0; i < int.Parse(Length); i++)
                            { 
                                for (int j = 0; j < int.Parse(Height); j++)
                                {
                                    if (j <= i)
                                    {
                                        list.Add(blockFactory.createBlock(Block.blocktypes.STAIR, new Vector2((int.Parse(Xpos) + i) * 32, (int.Parse(Ypos) - j) * 32), 0, 0,0));
                                    }
                                }
                            }
                            Debug.WriteLine("Creating Stair at:" + Xpos + ", " + Ypos);
                            break;
                        case "LeftStair":
                            Xpos = reader.GetAttribute("Xpos");
                            Ypos = reader.GetAttribute("Ypos");
                            Length = reader.GetAttribute("Length");
                            Height = reader.GetAttribute("Height");
                            for (int i = 0; i < int.Parse(Length); i++)
                            {
                                for (int j = 0; j < int.Parse(Height); j++)
                                {
                                    if ( j < (int.Parse(Length) - i))
                                    {
                                        list.Add(blockFactory.createBlock(Block.blocktypes.STAIR, new Vector2((int.Parse(Xpos) + i) * 32, (int.Parse(Ypos) - j) * 32), 0, 0,0));
                                    }
                                }
                            }
                            Debug.WriteLine("Creating Stair at:" + Xpos + ", " + Ypos);
                            break;
                        case "PipeTop":
                            Xpos = reader.GetAttribute("Xpos");
                            Ypos = reader.GetAttribute("Ypos");
                            PowerUp = reader.GetAttribute("PowerUp");
                            Enemy = reader.GetAttribute("Enemy");
                            Block pipe = blockFactory.createBlock(Block.blocktypes.PIPETOP, new Vector2(int.Parse(Xpos) * 32, int.Parse(Ypos) * 32), 0, int.Parse(PowerUp), int.Parse(Enemy));
                            list.Add(pipe);
                            Debug.WriteLine("Creating PIPE at:" + Xpos + ", " + Ypos);
                            break;
                        case "PipeTeleTop":
                            Xpos = reader.GetAttribute("Xpos");
                            Ypos = reader.GetAttribute("Ypos");
                            Block pipeteletop = blockFactory.createBlock(Block.blocktypes.PIPETELEPORTTOP, new Vector2(int.Parse(Xpos) * 32, int.Parse(Ypos) * 32), 0, 0,0);
                            pipeteletop.blocktype.Teleportation.X = int.Parse(reader.GetAttribute("XTele")) * 32 + 8;
                            pipeteletop.blocktype.Teleportation.Y = int.Parse(reader.GetAttribute("YTele")) * 32;
                            list.Add(pipeteletop);
                            Debug.WriteLine("Creating PIPE at:" + Xpos + ", " + Ypos);
                            break;
                        case "PipeTeleTopBonus":
                            Xpos = reader.GetAttribute("Xpos");
                            Ypos = reader.GetAttribute("Ypos");
                            Block pipeteletopbonus = blockFactory.createBlock(Block.blocktypes.PIPETELEPORTTOPBONUS, new Vector2(int.Parse(Xpos) * 32, int.Parse(Ypos) * 32), 0, 0,0);
                            pipeteletopbonus.blocktype.Teleportation.X = int.Parse(reader.GetAttribute("XTele")) * 32 + 8;
                            pipeteletopbonus.blocktype.Teleportation.Y = int.Parse(reader.GetAttribute("YTele")) * 32;
                            list.Add(pipeteletopbonus);
                            Debug.WriteLine("Creating PIPE at:" + Xpos + ", " + Ypos);
                            break;
                        case "PipeTeleSide":
                            Xpos = reader.GetAttribute("Xpos");
                            Ypos = reader.GetAttribute("Ypos");
                            Block pipeteleside = blockFactory.createBlock(Block.blocktypes.PIPETELEPORTSIDE, new Vector2(int.Parse(Xpos) * 32, int.Parse(Ypos) * 32), 0, 0,0);
                            pipeteleside.blocktype.Teleportation.X = int.Parse(reader.GetAttribute("XTele")) * 32 + 8;
                            pipeteleside.blocktype.Teleportation.Y = int.Parse(reader.GetAttribute("YTele")) * 32;
                            list.Add(pipeteleside);
                            Debug.WriteLine("Creating PIPE at:" + Xpos + ", " + Ypos);
                            break;
                        case "PipeMiddle":
                            Xpos = reader.GetAttribute("Xpos");
                            Ypos = reader.GetAttribute("Ypos");
                            list.Add(blockFactory.createBlock(Block.blocktypes.PIPEMIDDLE, new Vector2(int.Parse(Xpos) * 32, int.Parse(Ypos) * 32), 0, 0,0));
                            Debug.WriteLine("Creating PIPE at:" + Xpos + ", " + Ypos);
                            break;
                        case "PipeSidways":
                            Xpos = reader.GetAttribute("Xpos");
                            Ypos = reader.GetAttribute("Ypos");
                            list.Add(blockFactory.createBlock(Block.blocktypes.PIPESIDWAYS, new Vector2(int.Parse(Xpos) * 32, int.Parse(Ypos) * 32), 0, 0,0));
                            Debug.WriteLine("Creating PIPE at:" + Xpos + ", " + Ypos);
                            break;
                        case "PipeMiddleSidways":
                            Xpos = reader.GetAttribute("Xpos");
                            Ypos = reader.GetAttribute("Ypos");
                            list.Add(blockFactory.createBlock(Block.blocktypes.PIPEMIDDLESIDWAYS, new Vector2(int.Parse(Xpos) * 32, int.Parse(Ypos) * 32), 0, 0,0));
                            Debug.WriteLine("Creating PIPE at:" + Xpos + ", " + Ypos);
                            break;
                        case "PipeRot":
                            Xpos = reader.GetAttribute("Xpos");
                            Ypos = reader.GetAttribute("Ypos");
                            list.Add(blockFactory.createBlock(Block.blocktypes.PIPEROT, new Vector2(int.Parse(Xpos) * 32, int.Parse(Ypos) * 32), 0, 0,0));
                            Debug.WriteLine("Creating PIPE at:" + Xpos + ", " + Ypos);
                            break;
                        case "PipeRightSide":
                            Xpos = reader.GetAttribute("Xpos");
                            Ypos = reader.GetAttribute("Ypos");
                            list.Add(blockFactory.createBlock(Block.blocktypes.PIPERIGHTSIDE, new Vector2(int.Parse(Xpos) * 32, int.Parse(Ypos) * 32), 0, 0,0));
                            Debug.WriteLine("Creating PIPE at:" + Xpos + ", " + Ypos);
                            break;
                        case "Castle":
                            Xpos = reader.GetAttribute("Xpos");
                            Ypos = reader.GetAttribute("Ypos");
                            list.Add(blockFactory.createBlock(Block.blocktypes.CASTLE, new Vector2(int.Parse(Xpos) * 32, int.Parse(Ypos) * 32), 0, 0,0));
                            Debug.WriteLine("Creating Castle at:" + Xpos + ", " + Ypos);
                            break;
                    }
                }
            }
        }

        public void LevelUpdate(GameTime gameTime, Camera camera, TileMap map)
        {

            timer += gameTime.ElapsedGameTime.Milliseconds;
            List<Entity> copy = new List<Entity>();
            foreach (Entity entity in list){
                entity.Update(gameTime);
                copy.Add(entity);
            }
            foreach (Entity entity in copy)
            {
                if (entity.GetType().Name == "Fireball")
                {
                    if (entity.position.X > this.Xbound || entity.position.X < 0 || entity.position.Y > this.Ybound || entity.position.Y < 0 || entity.position.X < camera.Position.X
                    || entity.position.X > (camera.Position.X + 25 * 32) || entity.position.Y < camera.Position.Y || entity.position.Y > (camera.Position.Y + 13 * 32) || entity.remove)
                    {

                        list.Remove(entity);
                        map.Remove(entity);
                        mario.fireBallNum--;
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
