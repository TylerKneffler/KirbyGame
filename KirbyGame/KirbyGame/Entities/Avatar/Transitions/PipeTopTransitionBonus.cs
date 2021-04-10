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
    class PipeTopTransitionBonus : Transition
    {
        private int Timer;
        bool tele = false;
        private SoundEffect player;
        public PipeTopTransitionBonus(Avatar avatar) : base(avatar)
        {

            mario.game.updateCamera = false;
            Timer = 0;
        }

        public override void Enter()
        {
            mario.velocity.Y = 1;
            mario.Y += 15;
        }
        public void Teleport()
        {
            mario.game.color = Color.Black;
            mario.velocity.Y = 0;
            mario.position = teleport;
            this.mario.game.camera.Limits = new Rectangle(new Point(32, 18 * 32), new Point( 20 * 32, 13 * 32));
            this.mario.game.camera.LookAt(new Vector2(mario.position.X, mario.position.Y));
            tele = true;
            this.player = this.mario.game.Content.Load<SoundEffect>("SoundEffects/smb_pipe");
            this.player.Play();
        }

        public override void Exit()
        {
            mario.Y -= 15;
            mario.actionState.FallingTransition();
            mario.game.checkpoints.currentRespawn = new Vector2(mario.X, mario.Y - 2);
            mario.game.updateCamera = true;
            this.player = this.mario.game.Content.Load<SoundEffect>("SoundEffects/smb_pipe");
            this.player.Play();
        }

        public override void Update(GameTime gameTime)
        {
            Timer += gameTime.ElapsedGameTime.Milliseconds;
            if (Timer > 1000)
            {

                Teleport();
                Exit();
            }
        }
    }
}
