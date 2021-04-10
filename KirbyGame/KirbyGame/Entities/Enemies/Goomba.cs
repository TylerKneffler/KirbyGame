using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KirbyGame
{
    class Goomba : Enemy
    {
        //added timer and visible so that dead frame will be shown for a bit before disappearing
        private int timer;
        public Boolean visible;
        private const int deadFrame = 3;


        public Goomba(Sprite sprite, Game1 game) : base(sprite, game)
        {
            visible = true;
            timer = 0;
        }
        public override void Update(GameTime gameTime)
        {
            if(isDead)
            {
                Sprite.texture.SetCurrentFrame(deadFrame);//dead frame
                timer++;
                if (timer > 50)
                {
                    visible = false;
                    X = -1;
                    Y = -1;
                }
            }
            else
            {
                base.Update(gameTime);
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (visible)
            {
                base.Draw(spriteBatch);
            }
        }
    }
}
