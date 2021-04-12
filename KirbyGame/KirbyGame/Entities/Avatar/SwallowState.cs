using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KirbyGame
{
    public abstract class SwallowState
    {
        protected Avatar avatar;
        protected SwallowState previousState;
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
    }

    class EmptySwallowState : SwallowState
    {
        public EmptySwallowState(Avatar avatar) : base(avatar)
        {

        }
    }

    class AirSwallowState : SwallowState
    {
        public AirSwallowState(Avatar avatar) : base(avatar)
        {

        }
    }
}
