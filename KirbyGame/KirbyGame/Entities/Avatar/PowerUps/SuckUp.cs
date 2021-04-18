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
        public SuckUp(Avatar avatar)
        {
            this.avatar = avatar;
            this.particle = new TextureDetails(avatar.game.Content.Load<Texture2D>("avatar"), new Rectangle(49,115,2,4), 1);
        }
        public void Trigger()
        {

        }

        public void ReleaseTrigger()
        {

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
