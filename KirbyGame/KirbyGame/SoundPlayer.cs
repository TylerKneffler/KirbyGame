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
        public bool Stop { get; set; }
        private SoundEffect player;
        private SoundPlayer controlledSuctionPlayer;
        private SoundPlayer controlledSuctionProgressPlayer;
        private readonly Game1 kirbyGame;
        public SoundEffectPlayer(bool isMuted, Game1 game)
        {
            controlledSuctionPlayer = new SoundPlayer(@"C:\Users\Jackson Jiang\source\repos\KirbyGame\KirbyGame\KirbyGame\Content\suction.wav");
            controlledSuctionProgressPlayer = new SoundPlayer(@"C:\Users\Jackson Jiang\source\repos\KirbyGame\KirbyGame\KirbyGame\Content\suctionprogress3.wav");
            IsMuted = isMuted;
            kirbyGame = game;
        }
        public void PlayShotSound()
        {
            if (!IsMuted)
            {
                this.player = this.kirbyGame.Content.Load<SoundEffect>("SoundEffects/50 - Gunshot");
                this.player.Play();
            }
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

    }
}
