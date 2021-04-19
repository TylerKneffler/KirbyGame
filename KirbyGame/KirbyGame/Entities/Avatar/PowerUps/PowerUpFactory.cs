using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KirbyGame
{
    class PowerUpFactory
    {
        public static IPowerUp PowerUp(EnemyTest enemy, Avatar avatar)
        {
            IPowerUp ret = null;
            if(enemy.enemytype is SuckSirKibbleTest)
            {
                ret = new Cutter(avatar);
            }
            return ret;
        } 
    }
}
