using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KirbyGame.HUD
{
    public class Lives
    {
        Health hp;
        private int lifeTotal;
        public event EventHandler ZeroLives;

        public Lives()
        {
            lifeTotal = 2;
            hp = new Health();
            hp.LifeLost += cl_LifeLost;
        }

        public int GetLives()
        {
            return lifeTotal;
        }
        public void cl_LifeLost(object sender, EventArgs e)
        {
            lifeTotal--;
            //trigger some event

            if(lifeTotal <= 0)
            {
                OnZeroLives(EventArgs.Empty);
            }
        }

        protected virtual void OnZeroLives(EventArgs e)
        {
            ZeroLives?.Invoke(this, e);
        }
    }
}
