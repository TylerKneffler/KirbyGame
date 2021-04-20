using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KirbyGame.HUD
{
    public class Health
    {
        Collision cl;
        private int healthBars;
        public event EventHandler LifeLost;
  
        public Health()
        {
            healthBars = 6;
            cl = new Collision();
            cl.CollisionEvent += cl_CollisionEvent;
        }

        public int GetHealth()
        {
            return healthBars;
        }
        public void cl_CollisionEvent(object sender, Collision collision)
        {
            if (collision.A is EnemyTest && collision.B is Avatar)
            {
                healthBars--;
                //trigger health reduced event
            }
            if (collision.B is EnemyTest && collision.A is Avatar)
            {
                healthBars--;
                //trigger health reduced event
            }

            if(healthBars <= 0)
            {
                OnLifeLost(EventArgs.Empty);
            }
        }

        protected virtual void OnLifeLost(EventArgs e)
        {
            LifeLost?.Invoke(this, e);
        }
    }
}
