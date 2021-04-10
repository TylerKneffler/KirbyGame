using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KirbyGame
{
    class DyingTransition : Transition
    {
        private int Timer;
        public DyingTransition(Avatar avatar) : base(avatar)
        {
            Timer = 0;
        }

        public override void Enter()
        {
            mario.velocity.Y = -5;
        }

        public override void Exit()
        {
            mario.game.resetLevel();
            mario.actionState.IdleTransition();
            mario.powerState.SmallTransition();
            mario.game.Hud.testLives.Remove();
            //add some level reset stuff
        }

        public override void Update(GameTime gameTime)
        {
            Timer += gameTime.ElapsedGameTime.Milliseconds;
            
            if (Timer > 4000 || mario.BoundingBox.Bottom > mario.game.gameBounds.Y + TileMap.CELL_SIZE)
            {
                this.Exit();
            }
            else if (Timer > 250)
                mario.acceleration.Y = .3F;
        }
    }
}
