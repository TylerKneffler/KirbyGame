using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KirbyGame
{
    class FlagpoleTransition : Transition
    {
        private SoundEffect player;
        private int Timer;
        private int flagPoleRight;
        private bool marioOnFlagpole;
        public FlagpoleTransition(Avatar mario, int flagPoleRight) : base(mario)
        {
            Timer = 0;
            this.flagPoleRight = flagPoleRight;
            marioOnFlagpole = false;
        }
        public override void Enter()
        {
            this.player = this.mario.game.Content.Load<SoundEffect>("SoundEffects/smb_flagpole");
            this.player.Play();
            mario.acceleration = new Vector2();
        }

        public override void Exit()
        {
            mario.actionState.IdleTransition();
            mario.game.Hud.HandleWin();
        }

        public override void Update(GameTime gameTime)
        {
            if (marioOnFlagpole)
                marioOnFlagpoleUpdate(gameTime);
            else
                marioOffFlagpoleUpdate(gameTime);
        }

        public void marioOffFlagpoleUpdate(GameTime gameTime)
        {
            mario.velocity.X = 2;
            if(mario.BoundingBox.Right >= flagPoleRight)
            {
                marioOnFlagpole = true;
                mario.X = flagPoleRight - mario.boundingBoxSize.X;
                mario.velocity.X = 0;
            }
        }

        public void marioOnFlagpoleUpdate(GameTime gameTime)
        {
            Timer += gameTime.ElapsedGameTime.Milliseconds;
            if (mario.Y > 10 * TileMap.CELL_SIZE)
            {
                mario.Y = 11 * TileMap.CELL_SIZE - mario.BoundingBox.Height;
                this.Exit();
            }
            else if (Timer > 1000)
            {
                mario.velocity.Y = 2;
            }
        }

    }
}
