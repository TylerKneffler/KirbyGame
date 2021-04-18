using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace KirbyGame
{
    public abstract class SwallowState
    {
        public Avatar avatar;
        protected SwallowState previousState;
        public ActionState actionState;
        public PowerUp power;
        protected SwallowState CurrentState { get { return avatar.swallowed; } set { avatar.swallowed = value; } }

        public SwallowState(Avatar avatar)
        {
            this.avatar = avatar;
        }

        protected virtual void Enter(SwallowState previousState)
        {
            this.previousState = previousState;
            avatar.UpdateSprite();
        }
        protected virtual void Exit()
        {

        }

        public abstract void Trigger();
        public abstract void ReleaseTrigger();

        #region DEFERRED_COMMANDS
        public void PressFloat()
        {
            actionState.Float();
        }

        public void ReleaseFloat()
        {
            actionState.ReleaseFloat();
        }

        public void PressJump()
        {
            actionState.Jump();
        }
        public void PressDown()
        {
            actionState.Down();
        }
        public void PressRight()
        {
            actionState.Right();
        }
        public void PressLeft()
        {
            actionState.Left();
        }

        public void ReleaseJump()
        {

            actionState.releaseJump();
        }

        public void ReleaseDown()
        {
            actionState.releaseDown();
        }

        public void ReleaseRight()
        {
            actionState.releaseRight();
        }

        public void ReleaseLeft()
        {
            actionState.releaseLeft();
        }

        public void HandleBlockCollision(Collision collision)
        {
            actionState.HandleBlockCollision(collision);
        }
        #endregion

        public void Update(GameTime gameTime)
        {
            actionState.Update(gameTime);
        }

        public void EmptyTransition()
        {
            CurrentState.Exit();
            CurrentState = new EmptySwallowState(avatar);
            CurrentState.Enter(this);
        }

        public void AirTransition()
        {
            CurrentState.Exit();
            CurrentState = new AirSwallowState(avatar);
            CurrentState.Enter(this);
        }

        public void FullTransition()
        {
            CurrentState.Exit();
            CurrentState = new FullSwallowState(avatar);
            CurrentState.Enter(this);
        }
    }

    class EmptySwallowState : SwallowState
    {
        public EmptySwallowState(Avatar avatar, EmptyActionState actionState) : base(avatar)
        {
            this.actionState = actionState;
        }

        public EmptySwallowState(Avatar avatar) : base(avatar)
        {
            this.actionState = new EmptyIdleState(this);
        }

        public override void Trigger()
        {

        }
        public override void ReleaseTrigger()
        {

        }
    }

    class AirSwallowState : SwallowState
    {
        public AirSwallowState(Avatar avatar, AirActionState actionState) : base(avatar)
        {
            this.actionState = actionState;
        }

        public AirSwallowState(Avatar avatar) : base(avatar)
        {
            this.actionState = new AirFloatingState(this);
        }

        public override void Trigger()
        {
            this.EmptyTransition();
        }
        public override void ReleaseTrigger()
        {

        }
    }

    class FullSwallowState : SwallowState
    {
        public FullSwallowState(Avatar avatar, FullActionState actionState) : base(avatar)
        {
            this.actionState = actionState;
        }

        public FullSwallowState(Avatar avatar) : base(avatar)
        {
            this.actionState = new FullIdleState(this);
        }

        public override void Trigger()
        {
            this.EmptyTransition();
        }
        public override void ReleaseTrigger()
        {

        }

    }

}
