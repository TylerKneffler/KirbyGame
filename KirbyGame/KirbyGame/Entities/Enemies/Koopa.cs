using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KirbyGame
{
    class Koopa : Enemy
    {
        public Koopa(Sprite sprite, Game1 game) : base(sprite, game)
        {

        }

        public override void HandleCollision(Collision collision, Entity collider)
        {
            base.HandleCollision(collision, collider);
        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}
