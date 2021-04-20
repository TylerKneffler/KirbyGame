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
    class AirPuff : Entity, IPowerUp
    {
        private Avatar avatar;
        private int removeTimer;
        public AirPuff(Avatar avatar) : base(new Sprite(avatar.game.Content.Load<Texture2D>("avatar"), new Rectangle(248, 83, 16, 16), new Vector2(-16, -16), 1))
        {
            this.game = avatar.game;
            this.avatar = avatar;
            this.boundingColor = Color.Red;
            removeTimer = 0;
        }
        public override void HandleCollision(Collision collision, Entity collider)
        {

        }

        public void ReleaseTrigger()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            removeTimer += gameTime.ElapsedGameTime.Milliseconds;
            if (removeTimer > 1000)
            {
                game.levelLoader.RemoveEntity(this);
                game.map.Remove(this);
            }
        }



        public void Trigger()
        {
            this.Y = avatar.Y;
            if(avatar.Sprite.Direction == Sprite.eDirection.Left)
            {
                this.Sprite.Direction = Sprite.eDirection.Left;
                this.X = avatar.BoundingBox.Left - this.BoundingBox.Width;
                this.velocity.X = -3;
            } else
            {
                this.Sprite.Direction = Sprite.eDirection.Right;
                this.X = avatar.BoundingBox.Right;
                this.velocity.X = 3;
            }
            game.map.Insert(this);
            game.levelLoader.list.Add(this);
        }
    }
}
