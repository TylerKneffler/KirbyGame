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
        public Avatar avatar;
        public List<AirParticle> particleEffects;
        bool powerOn;
        private int Timer;
        Random rand;
        public SuckUp(Avatar avatar)
        {
            this.avatar = avatar;
            this.particle = new TextureDetails(avatar.game.Content.Load<Texture2D>("avatar"), new Rectangle(49,115,2,4), 1);
            particleEffects = new List<AirParticle>();
            powerOn = false;
            Timer = 0;
            rand = new Random();
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
                    GenerateParticle();
                    
                }
            }
            foreach (AirParticle particle in particleEffects)
            {
                particle.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (AirParticle particle in particleEffects)
            {
                particle.Draw(spriteBatch);
            }
        }

        public void ReleaseTrigger()
        {
            particleEffects.Clear();
            powerOn = false;
        }

        public void GenerateParticle()
        {
            int x, y;
            if(avatar.Sprite.Direction == Sprite.eDirection.Right)
            {
                x = rand.Next(avatar.BoundingBox.Right + 24, avatar.BoundingBox.Right + 32);
            } else
            {
                x = rand.Next(avatar.BoundingBox.Left - 32, avatar.BoundingBox.Left - 24);
            }
            y = rand.Next(avatar.BoundingBox.Top, avatar.BoundingBox.Bottom);



            Vector2 velocity = new Vector2((float)avatar.BoundingBox.Center.X - x, (float)avatar.BoundingBox.Center.Y - y);
            velocity = Vector2.Normalize(velocity);
            velocity = Vector2.Multiply(velocity, 3);
            Vector2 location = new Vector2(x,y);
            particleEffects.Add(new AirParticle(this, location, velocity, particle));
        }

    }

    public class AirParticle : Entity
    {
        public Avatar avatar;
        public SuckUp powerUp;

        public AirParticle(SuckUp powerUp, Vector2 location, Vector2 initVelocity, TextureDetails particle) : base(new Sprite(particle, location))
        {
            this.powerUp = powerUp;
            this.avatar = powerUp.avatar;
            this.game = this.avatar.game;
            this.velocity = initVelocity;
            game.map.Insert(this);
            
        }
        public override void HandleCollision(Collision collision, Entity collider)
        {
            if(collider == avatar)
            {
                powerUp.particleEffects.Remove(this);
            }
        }
    }
}
