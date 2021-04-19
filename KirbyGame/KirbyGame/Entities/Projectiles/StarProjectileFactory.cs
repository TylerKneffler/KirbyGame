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
    class StarProjectileFactory
    {
        private Game1 game;
        private SoundEffect player;

        public StarProjectileFactory(Game1 game)
        {
            this.game = game;
        }

        public StarProjectile CreateStarProjectile(Vector2 location, int direction)
        {
            //this.player.Play();
            return new StarProjectile(new Sprite(new TextureDetails(game.Content.Load<Texture2D>("starproj"), 4) , location), direction, game);
        }

    }
}
