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
        private List<LazerProjectile> lazer;
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

            if (powerOn)
            {
                Attack();
            }
            ReleaseTrigger();
        }
        private void Attack()
        {
            lazer = new List<LazerProjectile>();
            if (this.avatar.Sprite.Direction == Sprite.eDirection.Left)
            {
                lazer.Add(factory.CreateLazerProjectile(new Vector2((this.avatar.position.X - 16), (this.avatar.position.Y - 0)), 0, false));
                lazer.Add(factory.CreateLazerProjectile(new Vector2((this.avatar.position.X - 32), (this.avatar.position.Y - 16)), 0, false));
                lazer.Add(factory.CreateLazerProjectile(new Vector2((this.avatar.position.X - 48), (this.avatar.position.Y - 32)), 0, false));
                lazer.Add(factory.CreateLazerProjectile(new Vector2((this.avatar.position.X - 64), (this.avatar.position.Y - 48)), 0, false));
            }
            else
            {
                lazer.Add(factory.CreateLazerProjectile(new Vector2((this.avatar.position.X + 48), (this.avatar.position.Y - 0)), 1, false));
                lazer.Add(factory.CreateLazerProjectile(new Vector2((this.avatar.position.X + 64), (this.avatar.position.Y - 16)), 1, false));
                lazer.Add(factory.CreateLazerProjectile(new Vector2((this.avatar.position.X + 80), (this.avatar.position.Y - 32)), 1, false));
                lazer.Add(factory.CreateLazerProjectile(new Vector2((this.avatar.position.X + 96), (this.avatar.position.Y - 48)), 1, false));
            }
            foreach (LazerProjectile beam in lazer)
            {
                avatar.game.map.Insert(beam);
                avatar.game.levelLoader.list.Add(beam);
            }
        }
        public void Draw(SpriteBatch spritebatch)
        {
           //Empty
        }
    }
   
}
