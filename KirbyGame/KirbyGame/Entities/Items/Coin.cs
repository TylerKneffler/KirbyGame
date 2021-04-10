using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KirbyGame
{
    class Coin : Item
    {
        private int initialY;
        private SoundEffect player;
        
        public Coin(Sprite sprite, Game1 game) : base(sprite, game)
        {
            justSpawned = true;
            initialY = Y - 16;
        }

        public override void Update(GameTime gameTime)
        {
            if (justSpawned)
            {
                if (Y > initialY)
                {
                    velocity.Y = -1;
                }
                else
                {
                    velocity.Y = 0;
                    justSpawned = false;
                }
            }
            base.Update(gameTime);
        }

        public override void HandleCollision(Collision collision, Entity collider)
        {
            if (collider is Avatar)
            {
                this.player = this.game.Content.Load<SoundEffect>("SoundEffects/smb_coin");
                this.player.Play();
                this.boundingBoxSize = new Point();
                this.position = new Point();
                game.levelLoader.list.Remove(this);
            }
        }
    }
}

