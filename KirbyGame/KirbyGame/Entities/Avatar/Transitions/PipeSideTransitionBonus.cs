using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Microsoft.Xna.Framework.Audio;

namespace KirbyGame
{
    class PipeSideTransitionBonus : Transition
    {
        private int Timer;
        bool tele = false;
        private SoundEffect player;
        public PipeSideTransitionBonus(Avatar avatar) : base(avatar)
        {

            mario.game.updateCamera = false;
            Timer = 0;
        }

        public override void Enter()
        {
            mario.velocity.X = 1;
        }
        public void Teleport()
        {
            mario.game.color = Color.CornflowerBlue;
            mario.velocity.Y = -1;
            mario.velocity.X = 0;
            mario.position = teleport;
            this.mario.game.camera.Limits = new Rectangle(new Point(36, -32), new Point(210 * 32, 15 * 32));
            this.mario.game.camera.LookAt(new Vector2(mario.position.X, mario.position.Y));
            tele = true;
            //this.player = this.mario.game.Content.Load<SoundEffect>("SoundEffects/smb_pipe");
            this.player.Play();
        }

        public override void Exit()
        {
            mario.velocity.Y = 0;
            mario.game.updateCamera = true;
            mario.actionState.FallingTransition();
            mario.game.checkpoints.currentRespawn = new Vector2(mario.X, mario.Y - 2);
            //this.player = this.mario.game.Content.Load<SoundEffect>("SoundEffects/smb_pipe");
            this.player.Play();
            mario.Y -= 15;
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
                if (Timer > 1000 && Timer < 2300)
                {
                    if (tele == false)
                    {

                        Teleport();
                    }
                }
                else if (Timer > 2300)
                {

                    Exit();
                }
            }
        }
    }
}
