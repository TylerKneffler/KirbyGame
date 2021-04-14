using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Xna.Framework.Audio;

namespace KirbyGame
{
    public abstract class PowerState
    {
        protected Avatar avatar;
        protected PowerState previousState;
        protected PowerState CurrentState { get { return avatar.powerState; } set { avatar.powerState = value; } }

        protected SoundEffect player;

        public PowerState(Avatar avatar)
        {
            this.avatar = avatar;
        }
        
        public abstract void TakeDamage();

        protected virtual void Enter(PowerState previousState)
        {
            this.previousState = previousState;
            avatar.updateState();
        }
        protected virtual void Exit()
        {

        }

        public void DeadTransition()
        {
            CurrentState.Exit();
            CurrentState = new MarioDeadState(avatar);
            CurrentState.Enter(this);
        }

        public void SmallTransition()
        {
            CurrentState.Exit();
            CurrentState = new MarioSmallState(avatar);
            CurrentState.Enter(this);
        }

        public void SuperTransition()
        {
            CurrentState.Exit();
            CurrentState = new MarioSuperState(avatar);
            CurrentState.Enter(this);
        }

        public void FireTransition()
        {
            CurrentState.Exit();
            CurrentState = new MarioFireState(avatar);
            CurrentState.Enter(this);
        }
    }

    class MarioSmallState : PowerState
    {
        public MarioSmallState(Avatar avatar) : base(avatar)
        {

        }

        public override void TakeDamage()
        {
            this.DeadTransition();
        }

        protected override void Enter(PowerState previousState)
        {
            base.Enter(previousState);
            if(previousState is MarioSuperState || previousState is MarioFireState)
            {
                avatar.Y = avatar.Y + AvatarData.POWER_TRANSITION_ADJUST;
            }
        }
    }

    class MarioSuperState : PowerState
    {
        public MarioSuperState(Avatar avatar) : base(avatar)
        {

        }

        public override void TakeDamage()
        {
            this.player = this.avatar.game.Content.Load<SoundEffect>("SoundEffects/smb_pipe");
            this.player.Play();
            this.SmallTransition();
        }

        protected override void Enter(PowerState previousState)
        {
            base.Enter(previousState);
            if (previousState is MarioDeadState || previousState is MarioSmallState)
            {
                avatar.Y = avatar.Y - AvatarData.POWER_TRANSITION_ADJUST;
            }
        }
    }

    class MarioFireState : PowerState
    {
        public MarioFireState(Avatar avatar) : base(avatar)
        {

        }

        public override void TakeDamage()
        {
            this.player = this.avatar.game.Content.Load<SoundEffect>("SoundEffects/smb_pipe");
            this.player.Play();
            this.SuperTransition();
        }

        protected override void Enter(PowerState previousState)
        {
            base.Enter(previousState);
            if (previousState is MarioDeadState || previousState is MarioSmallState)
            {
                avatar.Y = avatar.Y - AvatarData.POWER_TRANSITION_ADJUST;
            }
        }
    }

    class MarioDeadState : PowerState
    {
        public MarioDeadState(Avatar avatar) : base(avatar)
        {

        }

        public override void TakeDamage()
        {
            
        }

        protected override void Enter(PowerState previousState)
        {
            this.player = this.avatar.game.Content.Load<SoundEffect>("SoundEffects/kirby-death-sound");
            this.player.Play();
            base.Enter(previousState);
            if (previousState is MarioSuperState || previousState is MarioFireState)
            {
                avatar.Y = avatar.Y + AvatarData.POWER_TRANSITION_ADJUST;
            }
            //avatar.numLives--;
            
            avatar.IsDead = true;
            Debug.WriteLine("Mario lost a life!");
            //Debug.WriteLine("Number of lives remaining: " + avatar.numLives);
            avatar.actionState.TransitioningTransition(new DyingTransition(avatar));
        }
    }
}
