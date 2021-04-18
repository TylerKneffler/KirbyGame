using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace KirbyGame
{
    public class SuckUp : IPowerUp
    {
        TextureDetails particle;
        Avatar avatar;
        List<AirParticle> particleEffects;
        bool powerOn;
        private int Timer;
        public SuckUp(Avatar avatar)
        {
            this.avatar = avatar;
            this.particle = new TextureDetails(avatar.game.Content.Load<Texture2D>("avatar"), new Rectangle(49,115,2,4), 1);
            particleEffects = new List<AirParticle>();
            powerOn = false;
            Timer = 0;
        }
        public void Trigger()
        {
            powerOn = true;
        }

        public void Update(GameTime gameTime)
        {
            if (powerOn)
            {
                Timer += gameTime.ElapsedGameTime.Milliseconds;
                if(Timer > 250)
                {
                    Timer -= 250;
                    //generate accurate random location
                    Vector2 location = new Vector2(avatar.X,avatar.Y);
                    particleEffects.Add(new AirParticle(location, particle));
                }
            }
        }

        public void ReleaseTrigger()
        {
            particleEffects.Clear();
            powerOn = false;
        }

    }

    public class AirParticle : Entity
    {
        public AirParticle(Vector2 location, TextureDetails particle) : base(new Sprite(particle, location))
        {
        }
        public override void HandleCollision(Collision collision, Entity collider)
        {
            
        }
    }
}
