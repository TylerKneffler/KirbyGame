using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KirbyGame
{
    class PipeTopTransition : Transition
    {
        private int Timer;
        bool tele = false;
        private SoundEffect player;

        public PipeTopTransition(Avatar avatar) : base(avatar)
        {
            Timer = 0;
        }

        public override void Enter()
        {
            this.player = this.mario.game.Content.Load<SoundEffect>("SoundEffects/smb_pipe");
            this.player.Play();
            mario.velocity.Y = 1;
            mario.Y += 15;

        }
        public void Teleport()
        {
            mario.velocity.Y = -1;
            mario.position = teleport;
            tele = true;
        }

        public override void Exit()
        {
            mario.actionState.FallingTransition();
            mario.Y -= 15;
            mario.game.checkpoints.currentRespawn = new Vector2(mario.X, mario.Y - 2);
            this.player = this.mario.game.Content.Load<SoundEffect>("SoundEffects/smb_pipe");
            this.player.Play();

        }

        public override void Update(GameTime gameTime)
        {
            Timer += gameTime.ElapsedGameTime.Milliseconds;
            if (mario.powerState is MarioSmallState)
            {
                if (Timer > 1000 && Timer < 2100)
                {
                    if (tele == false)
                    {

                        Teleport();
                    }
                }
                else if (Timer > 2100)
                {
                    Exit();
                }
            }
            else
            {
                if (Timer > 1000 && Timer < 2400)
                {
                    if (tele == false)
                    {
                        Teleport();
                    }
                }
                else if (Timer > 2400)
                {
                    Exit();
                }
            }
        }
    }
}
