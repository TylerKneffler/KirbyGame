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
        public EnemyTest sucked;
        TextureDetails particle;
        public Avatar avatar;
        public List<AirParticle> particleEffects;
        bool powerOn;
        private int Timer;
        Random rand;
        public SuckUp(Avatar avatar) : base(new Sprite(avatar.game.Content.Load<Texture2D>("avatar"), new Rectangle(150, 586, 3, 16), new Vector2(-1,-1), 1))
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
            avatar.game.map.Insert(this);
            avatar.game.levelLoader.list.Add(this);
            if(avatar.Sprite.Direction == Sprite.eDirection.Right)
            {
                this.X = avatar.BoundingBox.Right;
                this.velocity.X = 2;
            }
            else
            {
                this.X = avatar.BoundingBox.Left - this.BoundingBox.Width;
                this.velocity.X = -2;
            }
            this.Y = avatar.Y;
            powerOn = true;
        }

        public void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (powerOn)
            {
                Timer += gameTime.ElapsedGameTime.Milliseconds;

                if(Timer > 125)
                {
                    Timer -= 125;
                    
                    GenerateParticle();
                }
                if (particleEffects.Count > 2)
                {
                    particleEffects.RemoveAt(0);
                }
                if (avatar.Sprite.Direction == Sprite.eDirection.Right && this.BoundingBox.Right > avatar.BoundingBox.Right + 48)
                {
                    this.X = avatar.BoundingBox.Right;   
                }
                else if(this.BoundingBox.Left < avatar.BoundingBox.Left - 48)
                {
                    this.X = avatar.BoundingBox.Left - this.BoundingBox.Width;
                }
                this.Y = avatar.Y;
            }

            foreach (AirParticle particle in particleEffects)
            {
                Vector2 difference = Vector2.Normalize(Vector2.Add(new Vector2(particle.X, particle.Y), Vector2.Negate(new Vector2(avatar.BoundingBox.Center.X, avatar.BoundingBox.Center.Y))));
                particle.acceleration = Vector2.Negate(difference);
                acceleration.X = acceleration.X * 3F;
                acceleration.Y = acceleration.Y * 3F;

                particle.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (powerOn)
            {
                base.Draw(spriteBatch);
                foreach (AirParticle particle in particleEffects)
                {
                    particle.Draw(spriteBatch);
                }
            }
        }

        public void ReleaseTrigger()
        {
            particleEffects.Clear();
            velocity.X = 0;
            powerOn = false;
            avatar.game.map.Remove(this);
            game.levelLoader.list.Remove(this);
        }

        public void GenerateParticle()
        {
            Vector2 velocity;
            int x, y;
            y = rand.Next(avatar.BoundingBox.Top, avatar.BoundingBox.Bottom);
            if (y > avatar.BoundingBox.Center.Y)
            {
                velocity.Y = -2;
            }
            else
            {
                velocity.Y = 2;
            }

            if (avatar.Sprite.Direction == Sprite.eDirection.Right)
            {
                x = rand.Next(avatar.BoundingBox.Right + 24, avatar.BoundingBox.Right+ 36);
                velocity.X = -1;
            } else
            {
                x = rand.Next(avatar.BoundingBox.Left - 36, avatar.BoundingBox.Left - 24);
                velocity.X = 1;
            }     
            Vector2 location = new Vector2(x,y);
            particleEffects.Add(new AirParticle(this, location, velocity, particle));
        }

        public override void HandleCollision(Collision collision, Entity collider)
        {
            if (sucked == null)
            {
                if (collider is EnemyTest)
                {
                    sucked = (EnemyTest)collider;
                }
            }
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
