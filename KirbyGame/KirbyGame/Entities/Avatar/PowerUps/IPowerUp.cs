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
    public interface IPowerUp
    {
        void Trigger();
        void ReleaseTrigger();
        void Update(GameTime gameTime);
    }
}
