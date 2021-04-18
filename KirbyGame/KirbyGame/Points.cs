using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace KirbyGame
{

    public class Points
    {
        public Hud hud;
        private Boolean flagPointsCollected;

        public Points(Hud hud)
        {
            this.hud = hud;
            flagPointsCollected = false;
        }

        public void OnAddPoints(object source, Collision collision)
        {
            //if (collision.A is IPointable)
            //    hud.pointTotal += collision.A.Points();
            //if (collision.B is IPointable)
            //    hud.pointTotal += collision.B.Points();
        }
    }

}
