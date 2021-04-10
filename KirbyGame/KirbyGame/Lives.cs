using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KirbyGame
{
    public class Lives
    {
        private int lifeTotal;

        public Lives(int startingLives)
        {
            lifeTotal = startingLives;
        }

        public int GetLives()
        {
            return lifeTotal;
        }

        public void Add(int x)
        {
            lifeTotal += x;
        }
        public void Add()
        {
            lifeTotal++;
        }
        public void Remove(int x)
        {
            lifeTotal -= x;
            if(lifeTotal <= 0)
            {
                OnZeroLives(EventArgs.Empty);
            }
        }

        public void Remove()
        {
            lifeTotal--;
            if (lifeTotal <= 0)
            {
                OnZeroLives(EventArgs.Empty);
            }
        }

        protected virtual void OnZeroLives(EventArgs e)
        {
            EventHandler handler = ZeroLives;
            if(handler!= null)
            {
                handler(this, e);
            }
        }

        public event EventHandler ZeroLives;
    }
}
