using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System.Media;

using Microsoft.Xna.Framework.Graphics;

namespace KirbyGame
{
    public class SoundEffectPlayer
    {
        public bool IsMuted { get; set; }
        private  bool canPlay = true;
        public bool FirstCollision { get; set; }
        private SoundEffect player;
        private SoundPlayer controlledSuctionPlayer;
        private SoundPlayer controlledSuctionProgressPlayer;
        private readonly Game1 kirbyGame;
        public bool IsInTransition { get; set; }
        public SoundEffectPlayer(bool isMuted, Game1 game)
        {
            controlledSuctionPlayer = new SoundPlayer(@".\suction.wav");
            controlledSuctionProgressPlayer = new SoundPlayer(@".\suctionprogress3.wav");
            IsMuted = isMuted;
            kirbyGame = game;
            IsInTransition = false;
            FirstCollision = true;
        }
        public void PlaySuckStartSound()
        {
            if (!IsMuted)
            {
                this.controlledSuctionPlayer.Play();
            }
        }
        public void StopSuckStartSound()
        {
            
             this.controlledSuctionPlayer.Stop();
           
        }
        public void PlaySuckInProgressSound()
        {
            if (!IsMuted && canPlay)
            {
                canPlay = false;
                this.controlledSuctionProgressPlayer.PlayLooping();
            }
        }
        public void StopSuckProggressSound()
        {

            this.controlledSuctionProgressPlayer.Stop();
            canPlay = true;

        }
        public void PlayShotSound()
        {
            if (!IsMuted)
            {
                this.player = this.kirbyGame.Content.Load<SoundEffect>("SoundEffects/50 - Gunshot");
                this.player.Play();
            }
        }
        public void PlayExplosionSound()
        {
            if (!IsMuted)
            {
                this.player = this.kirbyGame.Content.Load<SoundEffect>("SoundEffects/explosion");
                this.player.Play();
            }
        }

        public void PlayJumpSound()
        {
            if (!IsMuted)
            {
                this.player = this.kirbyGame.Content.Load<SoundEffect>("SoundEffects/jump");
                this.player.Play();
            }
        }

        public void PlaySpitSound()
        {
            if (!IsMuted)
            {
                this.player = this.kirbyGame.Content.Load<SoundEffect>("SoundEffects/spit");
                this.player.Play();
            }
        }
        public void PlayLandSound()
        {
            if (!IsMuted && FirstCollision)
            {
                this.player = this.kirbyGame.Content.Load<SoundEffect>("SoundEffects/land");
                this.player.Play();
                FirstCollision = false;
            }
        }

        public void PlayEnterSound()
        {
            if (!IsMuted)
            {
                this.player = this.kirbyGame.Content.Load<SoundEffect>("SoundEffects/enter");
                this.player.Play();

            }
        }
        public void PlayBoomerangSound()
        {
            if (!IsMuted)
            {
                this.player = this.kirbyGame.Content.Load<SoundEffect>("SoundEffects/boomerang");
                this.player.Play();

            }
        }
        public void PlayWhipSound()
        {
            if (!IsMuted)
            {
                this.player = this.kirbyGame.Content.Load<SoundEffect>("SoundEffects/whip");
                this.player.Play();

            }
        }
        public void PlaySwallowSound()
        {
            if (!IsMuted)
            {
                this.player = this.kirbyGame.Content.Load<SoundEffect>("SoundEffects/swallow");
                this.player.Play();

            }
        }
        public void PlayDamageSound()
        {
            if (!IsMuted)
            {
                this.player = this.kirbyGame.Content.Load<SoundEffect>("SoundEffects/damage");
                this.player.Play();

            }
        }
        public void PlayKillingBlowSound()
        {
            if (!IsMuted)
            {
                this.player = this.kirbyGame.Content.Load<SoundEffect>("SoundEffects/killingblow");
                this.player.Play();

            }
        }
        public void PlayCopySound()
        {
            if (!IsMuted)
            {
                this.player = this.kirbyGame.Content.Load<SoundEffect>("SoundEffects/copy");
                this.player.Play();

            }
        }
    }
}
