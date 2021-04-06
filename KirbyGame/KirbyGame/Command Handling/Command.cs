using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KirbyGame
{
    public abstract class Command<GameMethod> : ICommand
    {
        protected GameMethod control;
        protected Command(GameMethod control)
        {
            this.control = control;
        }
        public abstract void Execute();
    }

}
