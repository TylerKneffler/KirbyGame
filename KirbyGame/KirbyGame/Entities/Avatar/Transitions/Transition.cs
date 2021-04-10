using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KirbyGame
{
    public abstract class Transition
    {
        protected Avatar mario;

        public Point teleport;
        public Transition(Avatar avatar)
        {
            this.mario = avatar;
        }
        public abstract void Enter();
        public abstract void Update(GameTime gameTime);
        public abstract void Exit();

    }
}
