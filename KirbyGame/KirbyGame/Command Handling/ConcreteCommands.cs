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

    class MarioPressJump : Command<Avatar>
    {
        public MarioPressJump(Avatar control)
            : base(control)
        {

        }
        public override void Execute()
        {
            control.pressJump();
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

    class MarioReleaseJump : Command<Avatar>
    {
        public MarioReleaseJump(Avatar control)
            : base(control)
        {

        }
        public override void Execute()
        {
            control.releaseJump();
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

    class MarioPressFloat : Command<Avatar>
    {
        public MarioPressFloat(Avatar control)
            : base(control)
        {

        }
        public override void Execute()
        {
            control.pressFloat();
        }
    }

    class MarioReleaseFloat : Command<Avatar>
    {
        public MarioReleaseFloat(Avatar control)
            : base(control)
        {

        }
        public override void Execute()
        {
            control.releaseFloat();
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

    class AvatarTrigger : Command<Avatar>
    {
        public AvatarTrigger(Avatar control)
            : base(control)
        {

        }
        public override void Execute()
        {
            control.Trigger();
        }
    }

    class AvatarReleaseTrigger : Command<Avatar>
    {
        public AvatarReleaseTrigger(Avatar control)
            : base(control)
        {

        }
        public override void Execute()
        {
            control.releaseTrigger();
        }
    }

    class AvatarClearPower : Command<Avatar>
    {
        public AvatarClearPower(Avatar control)
            : base(control)
        {

        }
        public override void Execute()
        {
            control.ClearPowerUp();
        }
    }

    class BoundingBoxToggle : Command<Game1>
    {
        public BoundingBoxToggle(Game1 control)
            : base(control)
        {

        }
        public override void Execute()
        {
            control.ToggleBoundingBoxes();
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
    class TogglePause : Command<Game1>
    {
        public TogglePause(Game1 control)
            : base(control)
        {

        }

        public override void Execute()
        {
            control.TogglePause();
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

    class ResetCommand : Command<Game1>
    {
        public ResetCommand(Game1 control)
            : base(control)
        {

        }

        public override void Execute()
        {
            control.HardReset();
        }
    }
}
