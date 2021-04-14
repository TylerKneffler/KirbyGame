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
    class FireBallFactory
    {
        private Game1 game;
        private SoundEffect player;

        public FireBallFactory(Game1 game)
        {
            this.game = game;
        }

        public Fireball createFireball(Vector2 location, int direction)
        {
            this.player = this.game.Content.Load<SoundEffect>("SoundEffects/50 - Gunshot");
            this.player.Play();
            return new Fireball(new Sprite(new TextureDetails(game.Content.Load<Texture2D>("CannonBall"), 1) , location), direction, game);
        }


    }
}
