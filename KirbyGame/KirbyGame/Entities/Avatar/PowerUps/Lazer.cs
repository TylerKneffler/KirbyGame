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
    class Lazer : IPowerUp
    {
        public Avatar avatar;
        private LazerProjectileFactory factory;
        private bool powerOn;
        private LazerProjectile lazer;
        private int cooldown;
        private int delay;
        private float tempX;

        public Lazer(Avatar avatar)
        {
            this.avatar = avatar;
            factory = new LazerProjectileFactory(avatar.game);
            powerOn = false;
        }
        public void Trigger()
        {
            powerOn = true;
        }
        public void ReleaseTrigger()
        {
            powerOn = false;
        }
        public void Update(GameTime gameTime)
        {

            if (cooldown == 0)
            {
                tempX = avatar.velocity.X;
                if (cooldown == 0)
                {
                    Attack();
                    cooldown = 400;
                    delay = 30;
                }
                cooldown = 100;
                delay = 30;
            }
            if (cooldown != 0)
            {
                cooldown--;
                if (delay == 0)
                {
                    this.avatar.Sprite = new Sprite(new TextureDetails(this.avatar.game.Content.Load<Texture2D>("WaddleDooFixed"), 2), this.avatar.Sprite.location);
                    this.avatar.velocity.X = tempX;
                }
                delay--;
            }

        }
        private void Attack()
        {
            this.avatar.Sprite = new Sprite(new TextureDetails(this.avatar.game.Content.Load<Texture2D>("WaddleDooFixed"), new Rectangle(new Point(16, 0), new Point(16, 16)), 1), this.avatar.Sprite.location);
            this.avatar.velocity.X = 0;
            if (this.avatar.Sprite.Direction == Sprite.eDirection.Right)
            {
                avatar.game.levelLoader.list.Add(factory.CreateLazerProjectile(new Vector2((this.avatar.position.X - 16), (this.avatar.position.Y - 0)), 0, false));
                avatar.game.levelLoader.list.Add(factory.CreateLazerProjectile(new Vector2((this.avatar.position.X - 32), (this.avatar.position.Y - 16)), 0, false));
                avatar.game.levelLoader.list.Add(factory.CreateLazerProjectile(new Vector2((this.avatar.position.X - 48), (this.avatar.position.Y - 32)), 0, false));
                avatar.game.levelLoader.list.Add(factory.CreateLazerProjectile(new Vector2((this.avatar.position.X - 64), (this.avatar.position.Y - 48)), 0, false));
            }
            else
            {
                avatar.game.levelLoader.list.Add(factory.CreateLazerProjectile(new Vector2((this.avatar.position.X + 48), (this.avatar.position.Y - 0)), 0, false));
                avatar.game.levelLoader.list.Add(factory.CreateLazerProjectile(new Vector2((this.avatar.position.X + 64), (this.avatar.position.Y - 16)), 0, false));
                avatar.game.levelLoader.list.Add(factory.CreateLazerProjectile(new Vector2((this.avatar.position.X + 80), (this.avatar.position.Y - 32)), 0, false));
                avatar.game.levelLoader.list.Add(factory.CreateLazerProjectile(new Vector2((this.avatar.position.X + 96), (this.avatar.position.Y - 48)), 0, false));
            }
        }
        public void Draw(SpriteBatch spritebatch)
        {
           //Empty
        }
    }
   
}
