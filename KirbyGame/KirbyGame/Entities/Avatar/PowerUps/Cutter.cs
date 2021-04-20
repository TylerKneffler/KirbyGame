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
    class Cutter : IPowerUp
    {
        public Avatar avatar;
        private BoomerangFactory factory;
        private bool powerOn;
        private Boomerang boomerang;

        public Cutter(Avatar avatar)
        {
            this.avatar = avatar;
            factory = new BoomerangFactory(avatar.game);
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
        public void Update(GameTime gametime)
        {
            if (powerOn)
            {
                if (this.avatar.Sprite.Direction == Sprite.eDirection.Left) { 
                    boomerang = factory.CreateBoomerang(new Vector2(this.avatar.position.X, this.avatar.position.Y), 0, false);
                }
                else
                {
                    boomerang = factory.CreateBoomerang(new Vector2(this.avatar.position.X, this.avatar.position.Y), 1, false);
                }
                ReleaseTrigger();
                avatar.game.levelLoader.list.Add(boomerang);
                avatar.game.map.Insert(boomerang);
            }
            
            //boomerang?.Update(gametime);
        }
        public void Draw(SpriteBatch spritebatch)
        {
            //boomerang?.Draw(spritebatch);
        }
    }
   
}
