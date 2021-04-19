﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace KirbyGame
{
    class Star : Entity, IPowerUp
    {
        private Avatar avatar;
        public Star(Avatar avatar) : base(new Sprite(avatar.game.Content.Load<Texture2D>("avatar"), new Rectangle(6, 140, 16, 16), new Vector2(-16, -16), 1))
        {
            this.game = avatar.game;
            this.avatar = avatar;
            this.boundingColor = Color.Red;
            Sprite.texture.AddFrame(new Rectangle(26, 140, 16, 16));
            Sprite.texture.AddFrame(new Rectangle(46, 140, 16, 16));
            Sprite.texture.AddFrame(new Rectangle(66, 140, 16, 16));
        }
        public override void HandleCollision(Collision collision, Entity collider)
        {
            
        }

        public void ReleaseTrigger()
        {
            
        }

        public void Trigger()
        {
            this.Y = avatar.Y;
            if(avatar.Sprite.Direction == Sprite.eDirection.Left)
            {
                this.Sprite.Direction = Sprite.eDirection.Left;
                this.X = avatar.BoundingBox.Left - this.BoundingBox.Width;
                this.velocity.X = -5;
            } else
            {
                this.Sprite.Direction = Sprite.eDirection.Right;
                this.X = avatar.BoundingBox.Right;
                this.velocity.X = 5;
            }
            game.map.Insert(this);
        }
    }
}
