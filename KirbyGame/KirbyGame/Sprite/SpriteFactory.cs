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
        private KirbyGame game;

        public abstract Sprite createSprite(int type, Vector2 location);

        public SpriteFactory(KirbyGame game)
        {
            this.game = game;
        }
    }
}
