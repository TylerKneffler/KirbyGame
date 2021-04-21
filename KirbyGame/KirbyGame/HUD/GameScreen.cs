using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using Microsoft.Xna.Framework.Media;

namespace KirbyGame
{
    public class GameScreen : Layer
    {
        Texture2D pause;
        Texture2D win;
        Texture2D lose;

        readonly SpriteFont font;


        bool isPause;
        bool isWin;
        bool isLose;
        public GameScreen(Camera camera, Vector2 location, Viewport viewport, Game1 game) : base(camera, location, viewport)
        {
            pause = game.Content.Load<Texture2D>("kirby_pause");
            win = game.Content.Load<Texture2D>("kirby_win");
            lose = game.Content.Load<Texture2D>("game_over");
            font = game.Content.Load<SpriteFont>("Kirby_font");

            game.Pause += this.mario_PauseScreen;
            game.stats.ZeroLives += this.mario_LoseScreen;
    
            isPause = false;
            isWin = false;
            isLose = false;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //Debug.WriteLine("im being drawn WRONG");
            //if (isPause)
            //{
            //    spriteBatch.Draw(pause, bounds, Color.White);
            //}
            //else if (isWin)
            //{
            //    spriteBatch.Draw(win, bounds, Color.White);
            //}
            //else if (isLose)
            //{
            //    spriteBatch.Draw(lose, bounds, Color.White);
            //}
        }

        public override void Draw(SpriteBatch spriteBatch, bool _pause, bool _win, bool _lose)
        {
            spriteBatch.Begin();
            if (_pause)
            {
                spriteBatch.Draw(pause, new Rectangle(0,0,500,420), Color.White);
            }
            if (_win)
            {
                spriteBatch.Draw(win, new Rectangle(0, 0, 500, 420), Color.White);
                spriteBatch.DrawString(font, "Press R to Play Again", new Vector2(150,150), Color.Black);
                spriteBatch.DrawString(font, "Press Q to Exit", new Vector2(150, 200), Color.White);
            }
            else if (_lose)
            {
                spriteBatch.Draw(lose, new Rectangle(0, 0, 500, 420), Color.White);
                spriteBatch.DrawString(font, "Press R to Retry", new Vector2(150, 150), Color.Black);
                spriteBatch.DrawString(font, "Press Q to Exit", new Vector2(150, 200), Color.White);
            }
            spriteBatch.End();
        }

        public void mario_PauseScreen(object sender, EventArgs e)
        {
            isPause = !isPause;
        }
        public void mario_WinScreen(object sender, EventArgs e)
        {
            isWin = true;
        }
        public void mario_LoseScreen(object sender, EventArgs e)
        {
            isLose = true;
        }
    }
}

//spriteBatch.Draw(marioGame.Content.Load<Texture2D>("BlackBackground"), new Rectangle(0, 0, 800, 480), Color.Black);