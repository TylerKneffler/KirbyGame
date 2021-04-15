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
    class CannonballFactory
    {
        private Game1 game;
        private SoundEffect player;

        public CannonballFactory(Game1 game)
        {
            this.game = game;
        }

        public Cannonball CreateCannonball(Vector2 location, int direction)
        {
            this.player = this.game.Content.Load<SoundEffect>("SoundEffects/50 - Gunshot");
            this.player.Play();
            return new Cannonball(new Sprite(new TextureDetails(game.Content.Load<Texture2D>("CannonBall"), 1) , location), direction, game);
        }

    }
}
