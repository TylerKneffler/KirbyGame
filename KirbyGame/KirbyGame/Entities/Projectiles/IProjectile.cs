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
    public interface IProjectile
    {
        bool canHurtKirby();
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
        void HandleCollision(Collision collision, Entity collider);
    }
}