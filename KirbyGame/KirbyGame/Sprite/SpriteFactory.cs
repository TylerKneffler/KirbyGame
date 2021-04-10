using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace KirbyGame
{
    abstract class SpriteFactory
    {
        private Game1 game;

        public abstract Sprite createSprite(int type, Vector2 location);

        public SpriteFactory(Game1 game)
        {
            this.game = game;
        }
    }
}
