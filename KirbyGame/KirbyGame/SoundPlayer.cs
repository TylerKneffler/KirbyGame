using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace KirbyGame
{
    public class SoundPlayer
    {
        public bool IsMuted { get; set; }
        private SoundEffect player;
        private readonly Game1 kirbyGame;
        public SoundPlayer(bool isMuted, Game1 game)
        {
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
        
    }
}
