using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace KirbyGame
{
    class LazerProjectileFactory
    {
        private Game1 game;
        private SoundEffect player;

        public LazerProjectileFactory(Game1 game)
        {
            this.game = game;
        }

        public LazerProjectile CreateLazerProjectile(Vector2 location, int direction, bool canHurtKirby)
        {
            return new LazerProjectile(new Sprite(new TextureDetails(game.Content.Load<Texture2D>("Lazer"), 1) , location), location, direction,canHurtKirby, game);
        }

    }
}
