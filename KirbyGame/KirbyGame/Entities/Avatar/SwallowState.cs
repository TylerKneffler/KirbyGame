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
        public IPowerUp currentPower;
        public IPowerUp powerUp;
        protected SwallowState CurrentState { get { return avatar.swallowed; } set { avatar.swallowed = value; } }

        public SwallowState(Avatar avatar)
        {
            this.avatar = avatar;
        }

        protected virtual void Enter(SwallowState previousState)
        {
            this.previousState = previousState;
            this.powerUp = previousState.powerUp;
            avatar.UpdateSprite();
        }
        protected virtual void Exit()
        {

        }

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
        public virtual void PressDown()
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

        public abstract void Trigger();
        public abstract void ReleaseTrigger();

        public void HandleBlockCollision(Collision collision)
        {
            actionState.HandleBlockCollision(collision);
        }
        #endregion
        public void Draw(SpriteBatch spriteBatch)
        {
            currentPower?.Draw(spriteBatch);
        }
        public void Update(GameTime gameTime)
        {
            actionState.Update(gameTime);
            currentPower?.Update(gameTime);
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

        public void FullTransition(EnemyTest swallowed)
        {
            CurrentState.Exit();
            CurrentState = new FullSwallowState(avatar, swallowed);
            avatar.game.player.PlaySwallowSound();
            CurrentState.Enter(this);
        }
    }

    public class EmptySwallowState : SwallowState
    {
        public EmptySwallowState(Avatar avatar, EmptyActionState actionState) : base(avatar)
        {
            this.actionState = actionState;
            currentPower = new SuckUp(avatar);
        }

        public EmptySwallowState(Avatar avatar) : base(avatar)
        {
            this.actionState = new EmptyIdleState(this);
            currentPower = new SuckUp(avatar);
        }

        public override void Trigger()
        {
            if (powerUp != null)
                currentPower = powerUp;
            else
                currentPower = new SuckUp(avatar);
            actionState.Trigger();
            currentPower.Trigger();
        }
        public override void ReleaseTrigger()
        {
            actionState.ReleaseTrigger();
            currentPower.ReleaseTrigger();
            if (currentPower is SuckUp)
            {
                if(((SuckUp)currentPower).sucked != null)
                 this.FullTransition(((SuckUp)currentPower).sucked);
            }
        }
    }

    public class AirSwallowState : SwallowState
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
            currentPower = new AirPuff(avatar);
            currentPower.Trigger();
            actionState.Trigger();
        }
        public override void ReleaseTrigger()
        {
            currentPower?.ReleaseTrigger();
            actionState.ReleaseTrigger();
        }
    }

    public class FullSwallowState : SwallowState
    {
        public EnemyTest swallowed;
        public FullSwallowState(Avatar avatar, FullActionState actionState) : base(avatar)
        {
            this.actionState = actionState;
        }

        public FullSwallowState(Avatar avatar, EnemyTest swallowed) : base(avatar)
        {
            this.actionState = new FullIdleState(this);
            this.swallowed = swallowed;
        }

        public override void Trigger()
        {
            currentPower = new Star(avatar);
            currentPower.Trigger();
            actionState.Trigger();
        }
        public override void ReleaseTrigger()
        {
            currentPower?.ReleaseTrigger();
            actionState.ReleaseTrigger();
        }

        public override void PressDown()
        {
            this.setPower(swallowed);
            this.EmptyTransition();
        }

        public void setPower(EnemyTest type)
        {
            powerUp = PowerUpFactory.PowerUp(type, avatar);
        }
    }
}
