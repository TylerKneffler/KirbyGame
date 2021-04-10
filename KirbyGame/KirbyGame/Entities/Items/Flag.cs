using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KirbyGame
{
    class Flag : Item
    {
        public Flag(Sprite sprite, Game1 game) : base(sprite, game)
        {

        }

        public int Points()
        {
            return 4000;
        }
    }
}
