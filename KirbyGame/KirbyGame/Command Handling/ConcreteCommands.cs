using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace KirbyGame
{

    class PauseCommand : Command<KirbyGame>
    {
        public PauseCommand(KirbyGame control)
            : base(control)
        {

        }
        public override void Execute()
        {
            control.pause();
        }
    }

}
