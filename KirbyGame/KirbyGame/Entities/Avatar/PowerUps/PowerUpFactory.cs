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
            }else if (enemy.enemytype is SuckWaddleDooTest)
            {
                ret = new Lazer(avatar);
            }
            return ret;
        }
        
        public static Stats.ePower EnumGenerator(IPowerUp power)
        {
            Stats.ePower ret = Stats.ePower.NORMAL;
            if(power is Cutter)
            {
                ret = Stats.ePower.CUTTER;
            } else if(power is Lazer)
            {
                ret = Stats.ePower.BEAM;
            }
            return ret;
        }
    }
}
