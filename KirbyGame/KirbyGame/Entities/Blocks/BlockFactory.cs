using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace KirbyGame
{
    class BlockFactory
    {
        private Game1 game;

        public BlockFactory(Game1 game)
        {
            this.game = game;
        }

        public Block createBlock(Block.blocktypes type, Vector2 location, int coins, int item, int enemy)
        {
            return new Block(type, location, coins, item ,enemy,game);
        }
    }
}
