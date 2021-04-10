using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

using Microsoft.Xna.Framework.Input;

namespace KirbyGame
{
    public class GameTimer
    {
        private int Counter;
        public int TimeLeft { get; set; }
        private const int Limit = 50;

        public GameTimer(int startingTime)
        {
            TimeLeft = startingTime;
        }

        public void Set(int startingTime)
        {
            TimeLeft = startingTime;
        }

        public int Get()
        {
            return TimeLeft;
        }

        public void Update(GameTime gameTime)
        {
            if (TimeLeft != 0)
            {
                if (Counter <= Limit)
                {
                    Counter++;
                }
                else
                {
                    TimeLeft--;
                    Counter = 0;
                }
            }
            else
            {
                OnZeroTime(EventArgs.Empty);
            }
        }

        protected virtual void OnZeroTime(EventArgs e)
        {
            EventHandler handler = ZeroTime;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler ZeroTime;
    }
}
