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

    class PauseCommand : Command<Game1>
    {
        public PauseCommand(Game1 control)
            : base(control)
        {

        }
        public override void Execute()
        {
            control.pause();
        }
    }
    class ExitCommand : Command<Game1>
    {
        public ExitCommand(Game1 control)
            : base(control)
        {

        }
        public override void Execute()
        {
            control.ExitCommand();
        }
    }

    class CoinBlockBump : Command<Block>
    {
        public CoinBlockBump(Block control)
            : base(control)
        {

        }
        public override void Execute()
        {
            control.QuestionStateChange();
        }
    }

    class HiddenBlockBump : Command<Block>
    {
        public HiddenBlockBump(Block control)
            : base(control)
        {

        }
        public override void Execute()
        {
            control.QuestionStateChange();
            control.HiddenStateChange();
        }
    }

    /*class BrickBlockBump : Command<Block>
    {
        public BrickBlockBump(Block control)
            : base(control)
        {

        }
        public override void Execute()
        {
            control.BrickStateChange();
        }
    }*/

    class MakeMarioSmall : Command<Avatar>
    {
        public MakeMarioSmall(Avatar control)
            : base(control)
        {
        }
        public override void Execute()
        {
            control.setStateSmall();
        }
    }
    class MakeMarioSuper : Command<Avatar>
    {
        public MakeMarioSuper(Avatar control)
            : base(control)
        {
        }
        public override void Execute()
        {
            control.setStateSuper();
        }
    }
    class MakeMarioFire : Command<Avatar>
    {
        public MakeMarioFire(Avatar control)
            : base(control)
        {
        }
        public override void Execute()
        {
            control.setStateFire();
        }
    }
    /*class MarioTakeDamage : Command<Avatar>
    {
        public MarioTakeDamage(Avatar control)
            : base(control)
        {
        }
        public override void Execute()
        {
            control.TakeDamage();
        }
    }*/

    class MarioPressUp : Command<Avatar>
    {
        public MarioPressUp(Avatar control)
            : base(control)
        {

        }
        public override void Execute()
        {
            control.pressUp();
        }
    }

    class MarioPressDown : Command<Avatar>
    {
        public MarioPressDown(Avatar control)
            : base(control)
        {

        }
        public override void Execute()
        {
            control.pressDown();
        }
    }

    class MarioPressLeft : Command<Avatar>
    {
        public MarioPressLeft(Avatar control)
            : base(control)
        {

        }
        public override void Execute()
        {
            control.pressLeft();
        }
    }

    class MarioPressRight : Command<Avatar>
    {
        public MarioPressRight(Avatar control)
            : base(control)
        {

        }
        public override void Execute()
        {
            control.pressRight();
        }
    }

    class MarioReleaseUp : Command<Avatar>
    {
        public MarioReleaseUp(Avatar control)
            : base(control)
        {

        }
        public override void Execute()
        {
            control.releaseUp();
        }
    }

    class MarioReleaseDown : Command<Avatar>
    {
        public MarioReleaseDown(Avatar control)
            : base(control)
        {

        }
        public override void Execute()
        {
            control.releaseDown();
        }
    }

    class MarioReleaseLeft : Command<Avatar>
    {
        public MarioReleaseLeft(Avatar control)
            : base(control)
        {

        }
        public override void Execute()
        {
            control.releaseLeft();
        }
    }

    class MarioReleaseRight : Command<Avatar>
    {
        public MarioReleaseRight(Avatar control)
            : base(control)
        {

        }
        public override void Execute()
        {
            control.releaseRight();
        }
    }

    class MarioFireBall : Command<Avatar>
    {
        public MarioFireBall(Avatar control)
            : base(control)
        {

        }
        public override void Execute()
        {
            //control.marioFireBall();
        }
    }

    class ToggleBoxes : Command<Game1>
    {
        public ToggleBoxes(Game1 control)
            : base(control)
        {

        }

        public override void Execute()
        {
            control.ToggleBoxesCommand();
        }
    }

    class ResetLevel : Command<Game1>
    {
        public ResetLevel(Game1 control)
            : base(control)
        {

        }

        public override void Execute()
        {
            control.resetLevel();
        }
    }

    class HardReset : Command<Game1>
    {
        public HardReset(Game1 control)
            : base(control)
        {

        }

        public override void Execute()
        {
            control.hardReset();
        }
    }

    class ToggleMute : Command<Game1>
    {
        public ToggleMute(Game1 control)
            : base(control)
        {

        }

        public override void Execute()
        {
            control.ToggleMuteCommand();
        }
    }


}
