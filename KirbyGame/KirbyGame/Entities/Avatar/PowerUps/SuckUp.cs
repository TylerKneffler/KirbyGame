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
    public class SuckUp : Entity, IPowerUp
    {
        TextureDetails particle;
        public Avatar avatar;
        public List<AirParticle> particleEffects;
        bool powerOn;
        private int Timer;
        Random rand;
        Game1 gamel;
        public SuckUp(Avatar avatar) : base(new Sprite(avatar.game.Content.Load<Texture2D>("avatar"), new Rectangle(150, 586, 37, 16), new Vector2(-1,-1), 1))
        {
            this.game = avatar.game;
            this.avatar = avatar;
            this.particle = new TextureDetails(game.Content.Load<Texture2D>("avatar"), new Rectangle(49,115,2,4), 1);
            particleEffects = new List<AirParticle>();
            powerOn = false;
            Timer = 0;
            rand = new Random();
            this.boundingColor = Color.Red;
        }
        public void Trigger()
        {
            if(avatar.Sprite.Direction == Sprite.eDirection.Right)
            {
                this.X = avatar.BoundingBox.Right;
            }
            else
            {
                this.X = avatar.BoundingBox.Left - this.BoundingBox.Width;
            }
            this.Y = avatar.Y;
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
                    
                    GenerateParticle();
                }
                if (particleEffects.Count > 2)
                {
                    particleEffects.RemoveAt(0);
                }
            }

            foreach (AirParticle particle in particleEffects)
            {
                particle.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
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
                x = rand.Next(avatar.BoundingBox.Right + this.BoundingBox.Width-24, avatar.BoundingBox.Right+ this.BoundingBox.Width);
            } else
            {
                x = rand.Next(avatar.BoundingBox.Left - this.BoundingBox.Width, avatar.BoundingBox.Left - this.BoundingBox.Width +24);
            }
            y = rand.Next(avatar.BoundingBox.Top, avatar.BoundingBox.Bottom);



            Vector2 velocity = new Vector2((float)avatar.BoundingBox.Center.X - x, (float)avatar.BoundingBox.Center.Y - y);
            velocity = Vector2.Normalize(velocity);
            velocity = Vector2.Multiply(velocity, 3);
            Vector2 location = new Vector2(x,y);
            particleEffects.Add(new AirParticle(this, location, velocity, particle));
        }

        public override void HandleCollision(Collision collision, Entity collider)
        {
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
        }
    }
}
