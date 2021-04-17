using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace KirbyGame
{
    public class Hud
    {
        public Game1 marioGame;
        readonly SpriteFont font;

        public int coinTotal;
        public int pointTotal;
        public int TimeLeft { get; set; }
        public int NumberOfLives  { get; set; }
        public Vector2 PlayerSpritePos { get; set; }
        public Vector2 ScorePos { get; set; }
        public Vector2 CoinSpritePos { get; set; }
        public Vector2 CoinNumPos { get; set; }
        public Vector2 PlayerNamePos { get; set; }
        public Vector2 LivesLeftPos { get; set; }
        public Vector2 TimeStringPos { get; set; }
        public Vector2 TimeRemainingPos { get; set; }
        public Vector2 GameOverPos { get; set; }
        public Vector2 RetryPos { get; set; }
        public Vector2 ExitPos { get; set; }

        private Camera _camera;
        private int counter;
        private readonly char[] score = new char[] { '0', '0', '0', '0', '0', '0' };
        //private MarioSpriteFactory marioSpriteFactory;
        private ItemFactory itemFactory;

        private readonly Sprite playerSprite;
        private readonly Entity itemSprite;
        private const int limit = 50;

        //private const int LIVES = 3;
        private const int STARTING_TIME = 400;

        public Boolean DisplayGameOver;
        public Boolean DisplayWinner;

        public Lives testLives;
        public GameTimer testTimer;
        private SoundEffect player;

        public Hud(Game1 game)
        {
            marioGame = game;
            _camera = game.camera;
            font = marioGame.Content.Load<SpriteFont>("font");

            //***************************
            testLives = new Lives(AvatarData.INIT_NUM_LIVES);
            testLives.ZeroLives += c_ZeroLives;

            testTimer = new GameTimer(STARTING_TIME);
            testTimer.ZeroTime += TestTimer_ZeroTime;
            //***************************

            coinTotal = 0;
            pointTotal = 0;

            DisplayGameOver = false;
            DisplayWinner = false;

            //counter = 0;
            //marioSpriteFactory = new MarioSpriteFactory(new Avatar(marioGame, new Vector2(0,0)));
            //playerSprite = marioSpriteFactory.createSprite(new Vector2(0,0));
            itemFactory = new ItemFactory(marioGame);
            itemSprite = itemFactory.createItem(Item.eItemType.COIN, new Vector2(0,0));
        }


        public void Update(GameTime gameTime)
        {
            if (TimeLeft != 0) { 
                if (counter <= limit)
                {
                    counter++;
                }
                else
                {
                    TimeLeft--;
                    if(TimeLeft == 100)
                    {
                        this.player = this.marioGame.Content.Load<SoundEffect>("SoundEffects/smb_warning");
                        this.player.Play();
                    }
                    counter = 0;
                }
            }
       // {       
            testTimer.Update(gameTime);
            itemSprite.Update(gameTime);

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp/*makes game look nicer*/, transformMatrix: _camera.GetViewMatrix(new Vector2(0)));

            if (DisplayGameOver)
            {
                this.player = this.marioGame.Content.Load<SoundEffect>("SoundEffects/smb_gameover");
                this.player.Play();
                marioGame.pause(); //Might not be the best place to put this
                spriteBatch.Draw(marioGame.Content.Load<Texture2D>("BlackBackground"), new Rectangle(0, 0, 800, 480), Color.Black);
                spriteBatch.DrawString(font, "GAME OVER", GameOverPos, Color.White);
                spriteBatch.DrawString(font, "Press Q to Retry", RetryPos, Color.White);
                spriteBatch.DrawString(font, "Press ESC to Exit", ExitPos, Color.White);
            }

            if (DisplayWinner)
            {
                marioGame.pause(); //Might not be the best place to put this
                spriteBatch.Draw(marioGame.Content.Load<Texture2D>("BlackBackground"), new Rectangle(0, 0, 800, 480), Color.Black);
                spriteBatch.DrawString(font, "YOU WIN!", GameOverPos, Color.White);
                spriteBatch.DrawString(font, "Press Q to Play Again", RetryPos, Color.White);
                spriteBatch.DrawString(font, "Press ESC to Exit", ExitPos, Color.White);
            }

            spriteBatch.DrawString(font, "MARIO", PlayerNamePos, Color.White);
            char[] pointTotalArr = pointTotal.ToString().ToCharArray();
            int digit = score.Length - 1;
            for (int i = pointTotalArr.Length - 1; i >= 0; i--)
            {
                score[digit] = pointTotalArr[i];
                digit--;
            }
            string scoreStr = new string(score);
            spriteBatch.DrawString(font, scoreStr, ScorePos, Color.White);

            playerSprite.location = PlayerSpritePos;
            playerSprite.Draw(spriteBatch);

            itemSprite.X = (int)CoinSpritePos.X;
            itemSprite.Y = (int)CoinSpritePos.Y;
            itemSprite.Draw(spriteBatch);

            
            spriteBatch.DrawString(font, "X" + coinTotal.ToString(), CoinNumPos, Color.White);
            
            //spriteBatch.DrawString(font, "X" + NumberOfLives.ToString(), LivesLeftPos, Color.White);
            spriteBatch.DrawString(font, "X" + testLives.GetLives().ToString(), LivesLeftPos, Color.White);

            spriteBatch.DrawString(font, "TIME:", TimeStringPos, Color.White);
            spriteBatch.DrawString(font, testTimer.Get().ToString(), TimeRemainingPos, Color.White);
            spriteBatch.End();
        }

        public void c_ZeroLives(Object sender, EventArgs e)
        {
            DisplayGameOver = true;
        }

        private void TestTimer_ZeroTime(object sender, EventArgs e)
        {
            //marioGame.mario.powerState.DeadTransition();
            testTimer.Set(STARTING_TIME);
        }
        public void ResetTime()
        {
            testTimer.Set(STARTING_TIME);

        }

        public void HandleWin()
        {
            DisplayWinner = true;
        }

        public Boolean IsGameOver()
        {
            return DisplayGameOver || DisplayWinner;
        }

        public void ResetHud()
        {
            testLives.Add(AvatarData.INIT_NUM_LIVES);
            testTimer.Set(STARTING_TIME);
            DisplayGameOver = false;
            DisplayWinner = false;
            coinTotal = 0;
            pointTotal = 0;
        }

    }
}
