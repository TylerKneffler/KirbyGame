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
    class BoomerangFactory
    {
        private Game1 game;
        private SoundEffect player;

        public BoomerangFactory(Game1 game)
        {
            this.game = game;
        }

        public Boomerang CreateBoomerang(Vector2 location, int direction, bool canHurtKirby)
        {
            //this.player.Play();
            return new Boomerang(new Sprite(new TextureDetails(game.Content.Load<Texture2D>("CutterFixed"), 4), location), direction, game, canHurtKirby);
        }

    }
}