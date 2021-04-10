using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KirbyGame
{
    class PoleTop : Item/*, IPointable*/
    {
        public PoleTop(Sprite sprite, Game1 game) : base(sprite, game)
        {

        }


        public int Points()
        {
            return 10000;
        }
    }
}
