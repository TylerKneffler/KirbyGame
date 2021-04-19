using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Microsoft.Xna.Framework.Audio;

namespace KirbyGame
{

    public abstract class EmptyActionState : ActionState
    {
        public EmptyActionState(SwallowState owner) : base(owner)
        {

        }

        public void IdleTransition()
        {
            CurrentState.Exit();
            CurrentState = new EmptyIdleState(owner);
            CurrentState.Enter(this);
        }


        public void RunningTransition()
        {
            CurrentState.Exit();
            CurrentState = new EmptyRunningState(owner);
            CurrentState.Enter(this);
        }

        public void JumpingTransition()
        {
            CurrentState.Exit();
            CurrentState = new EmptyJumpingState(owner);
            CurrentState.Enter(this);
        }

        public void FallingTransition()
        {
            CurrentState.Exit();
            CurrentState = new EmptyFallingState(owner);
            CurrentState.Enter(this);
        }

        public void FlippingTransition()
        {
            CurrentState.Exit();
            CurrentState = new EmptyFlippingState(owner);
            CurrentState.Enter(this);
        }

        public void FlyingTransition()
        {
            CurrentState.Exit();
            CurrentState = new EmptyFlyingState(owner);
            CurrentState.Enter(this);
        }

        public void SuckingTransition()
        {
            CurrentState.Exit();
            CurrentState = new EmptySuckingState(owner);
            avatar.game.player.PlaySuckStartSound();
            CurrentState.Enter(this);
        }
    }
    class EmptyIdleState : EmptyActionState
    {
        public EmptyIdleState(SwallowState owner) : base(owner)
        {

        }


        public override void Down()
        {
        }

        public override void HandleBlockCollision(Collision collision)
        {

        }

        public override void Left()
        {
            if (avatar.Sprite.Direction == Sprite.eDirection.Right)
                avatar.Sprite.Direction = Sprite.eDirection.Left;

            this.RunningTransition();

        }

        public override void releaseDown()
        {

        }

        public override void releaseLeft()
        {

        }

        public override void releaseRight()
        {

        }

        public override void releaseJump()
        {

        }

        public override void Right()
        {
            if (avatar.Sprite.Direction == Sprite.eDirection.Left)
                avatar.Sprite.Direction = Sprite.eDirection.Right;

            this.RunningTransition();
        }


        public override void Jump()
        {
            this.JumpingTransition();
        }

        public override void Update(GameTime gameTime)
        {
            if(avatar.velocity.Y > 0)
            {
                this.FallingTransition();
            }
            avatar.velocity.Y = 1;
        }

        public override void Enter(ActionState prevState)
        {
            base.Enter(prevState);
            avatar.velocity = new Vector2();
            avatar.acceleration = new Vector2();
        }

        public override void Float()
        {
            this.FlyingTransition();
        }

        public override void ReleaseFloat()
        {
            
        }

        public override void Trigger()
        {
            this.SuckingTransition();
        }

        public override void ReleaseTrigger()
        {

        }
    }



    class EmptyRunningState : EmptyActionState
    {
        public EmptyRunningState(SwallowState owner) : base(owner)
        {

        }

        public override void Down()
        {

        }

        public override void HandleBlockCollision(Collision collision)
        {
            if (collision.CollisionDirection == Collision.Direction.Left || collision.CollisionDirection == Collision.Direction.Right)
            {
                this.IdleTransition();
            }
            else if (collision.CollisionDirection == Collision.Direction.Up)
            {
                /*avatar.velocity.Y = 0;
                avatar.acceleration.Y = 0;*/
            }
        }

        public override void Left()
        {
            if (avatar.Sprite.Direction == Sprite.eDirection.Right)
                this.IdleTransition();
        }

        public override void releaseDown()
        {
        }

        public override void releaseLeft()
        {
            avatar.acceleration.X = 0;
        }

        public override void releaseRight()
        {
            avatar.acceleration.X = 0;
        }

        public override void releaseJump()
        {
        }

        public override void Right()
        {
            if (avatar.Sprite.Direction == Sprite.eDirection.Left)
                this.IdleTransition();
        }

        public override void Jump()
        {
            this.JumpingTransition();
        }

        public override void Update(GameTime gameTime)
        {
            if (avatar.velocity.Y > 0)
            {
                this.FallingTransition();
            }
            else if (Math.Abs(avatar.acceleration.X) == 0)
            {
                avatar.velocity.X = avatar.velocity.X * AvatarData.AVATAR_FRICTION;
                if (Math.Abs(avatar.velocity.X) < AvatarData.AVATAR_STOPPING_VELOCITY)
                    this.IdleTransition();
            }
            avatar.velocity.Y = 1;
            if (avatar.velocity.X < 0 && avatar.Sprite.Direction == Sprite.eDirection.Right)
                avatar.Sprite.Direction = Sprite.eDirection.Left;
            else if (avatar.velocity.X > 0 && avatar.Sprite.Direction == Sprite.eDirection.Left)
                avatar.Sprite.Direction = Sprite.eDirection.Right;
        }

        public override void Enter(ActionState prevState)
        {
            base.Enter(prevState);

            if (!(previousState is EmptyFallingState) && !(previousState is EmptyJumpingState) && !(previousState is EmptyFlippingState))
            {
                if (avatar.Sprite.Direction == Sprite.eDirection.Right)
                {
                    avatar.acceleration.X = AvatarData.DEFAULT_RUNNING_ACCELERATION;
                    if (avatar.velocity.X < AvatarData.INIT_RUN_VELOCITY)
                        avatar.velocity.X = AvatarData.INIT_RUN_VELOCITY;
                }
                else
                {
                    avatar.acceleration.X = -AvatarData.DEFAULT_RUNNING_ACCELERATION;
                    if (avatar.velocity.X > -AvatarData.INIT_RUN_VELOCITY)
                        avatar.velocity.X = -AvatarData.INIT_RUN_VELOCITY;
                }
            }


            avatar.velocity.Y = 0;
            avatar.acceleration.Y = 0;

        }

        public override void Exit()
        {
            base.Exit();
        }
        public override void Float()
        {
            this.FlyingTransition();
        }

        public override void ReleaseFloat()
        {

        }

        public override void Trigger()
        {
            this.SuckingTransition();
        }

        public override void ReleaseTrigger()
        {

        }

    }

    /*class MarioTransitioningState : ActionState
    {
        private Transition transition;
        public MarioTransitioningState(Avatar avatar, Transition transition) : base(avatar)
        {
            this.transition = transition;
        }

        public override void Down()
        {
        }

        public override void HandleBlockCollision(Collision collision)
        {

        }

        public override void Left()
        {

        }

        public override void releaseDown()
        {
        }

        public override void releaseLeft()
        {
        }

        public override void releaseRight()
        {
        }

        public override void releaseJump()
        {
        }

        public override void Right()
        {

        }

        public override void Jump()
        {
        }

        public override void Update(GameTime gameTime)
        {
            transition.Update(gameTime);
        }

        protected override void Enter(ActionState prevState)
        {
            this.previousState = prevState;
            avatar.velocity = new Vector2();
            avatar.acceleration = new Vector2();
            transition.Enter();
        }

        public override void Float()
        {
            this.FlyingTransition();
        }

        public override void ReleaseFloat()
        {

        }

    }*/

    class EmptyJumpingState : EmptyActionState
    {
        private int Timer;
        public EmptyJumpingState(SwallowState owner) : base(owner)
        {
            Timer = AvatarData.JUMP_MAX_TIME;
        }

        public override void Down()
        {
        }

        public override void HandleBlockCollision(Collision collision)
        {
            if (collision.CollisionDirection == Collision.Direction.Down)
            {
                //this.FallingTransition();
                avatar.velocity.Y = 0;
            }
            else if (collision.CollisionDirection != Collision.Direction.Up)
            {
                avatar.velocity.X = 0;
                avatar.acceleration.X = 0;
            }
        }

        public override void Left()
        {
            avatar.acceleration.X = -AvatarData.DEFAULT_RUNNING_ACCELERATION;
        }

        public override void releaseDown()
        {
        }

        public override void releaseLeft()
        {
            avatar.acceleration.X = 0;
        }

        public override void releaseRight()
        {
            avatar.acceleration.X = 0;
        }

        public override void releaseJump()
        {
            //add flip transition
            //this.FallingTransition();
        }

        public override void Right()
        {
            avatar.acceleration.X = AvatarData.DEFAULT_RUNNING_ACCELERATION;
        }

        public override void Jump()
        {
        }

        public override void Update(GameTime gameTime)
        {

            if (Math.Abs(avatar.acceleration.X) == 0)
            {
                avatar.velocity.X = avatar.velocity.X * AvatarData.AVATAR_FRICTION;
                if (Math.Abs(avatar.velocity.X) < AvatarData.AVATAR_STOPPING_VELOCITY)
                    avatar.velocity.X = 0;
            }

            if (avatar.velocity.X < 0 && avatar.Sprite.Direction == Sprite.eDirection.Right)
                avatar.Sprite.Direction = Sprite.eDirection.Left;
            else if (avatar.velocity.X > 0 && avatar.Sprite.Direction == Sprite.eDirection.Left)
                avatar.Sprite.Direction = Sprite.eDirection.Right;
            if (Timer > 0)
            {
                Timer -= gameTime.ElapsedGameTime.Milliseconds;
                if (Timer < 0)
                {
                    Timer = 0;
                    this.FlippingTransition();
                }
            }

        }
        public override void Enter(ActionState prevState)
        {
            base.Enter(prevState);
            avatar.velocity.Y = AvatarData.INIT_JUMP_VELOCITY;
            /*if (this.avatar.powerState is MarioSmallState)
            {
                this.player = this.avatar.game.Content.Load<SoundEffect>("SoundEffects/smb_jump-small");
            }
            else
            {
                this.player = this.avatar.game.Content.Load<SoundEffect>("SoundEffects/smb_jump-super");
            }
            this.player.Play();*/
        }
        public override void Exit()
        {
            base.Exit();
        }

        public override void Float()
        {
            this.FlyingTransition();
        }

        public override void ReleaseFloat()
        {

        }

        public override void Trigger()
        {
            this.SuckingTransition();
        }

        public override void ReleaseTrigger()
        {

        }

    }

    class EmptyFallingState : EmptyActionState
    {
        public EmptyFallingState(SwallowState owner) : base(owner)
        {

        }

        public override void Down()
        {
        }

        public override void HandleBlockCollision(Collision collision)
        {
            if (collision.CollisionDirection == Collision.Direction.Up)
            {
                if (Math.Abs(avatar.velocity.X) == 0)
                {
                    this.IdleTransition();
                }
                else
                {
                    this.RunningTransition();
                }
            }
            else if (collision.CollisionDirection == Collision.Direction.Down)
            {
                avatar.velocity.Y = 0;
            }
            else
            {
                avatar.velocity.X = 0;
                avatar.acceleration.X = 0;
            }
        }

        public override void Left()
        {
            avatar.acceleration.X = -AvatarData.DEFAULT_RUNNING_ACCELERATION;
        }

        public override void releaseDown()
        {
        }

        public override void releaseLeft()
        {
            avatar.acceleration.X = 0;
        }

        public override void releaseRight()
        {
            avatar.acceleration.X = 0;
        }

        public override void releaseJump()
        {
        }

        public override void Right()
        {
            avatar.acceleration.X = AvatarData.DEFAULT_RUNNING_ACCELERATION;
        }

        public override void Jump()
        {
        }

        public override void Update(GameTime gameTime)
        {
            if (Math.Abs(avatar.acceleration.X) == 0)
            {
                avatar.velocity.X = avatar.velocity.X * AvatarData.AVATAR_FRICTION;
                if (Math.Abs(avatar.velocity.X) < AvatarData.AVATAR_STOPPING_VELOCITY)
                    avatar.velocity.X = 0;
            }

            if (avatar.velocity.X < 0 && avatar.Sprite.Direction == Sprite.eDirection.Right)
                avatar.Sprite.Direction = Sprite.eDirection.Left;
            else if (avatar.velocity.X > 0 && avatar.Sprite.Direction == Sprite.eDirection.Left)
                avatar.Sprite.Direction = Sprite.eDirection.Right;
        }

        public override void Enter(ActionState prevState)
        {
            base.Enter(prevState);
            avatar.acceleration.Y = AvatarData.GRAVITY;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Float()
        {
            this.FlyingTransition();
        }

        public override void ReleaseFloat()
        {

        }

        public override void Trigger()
        {
            this.SuckingTransition();
        }

        public override void ReleaseTrigger()
        {

        }



    }


    class EmptyFlippingState : EmptyActionState
    {
        private int Time;
        int textCount;
        public EmptyFlippingState(SwallowState owner) : base(owner)
        {
            Time = 0;
            textCount = 3;
        }

        public override void Down()
        {
        }

        public override void HandleBlockCollision(Collision collision)
        {
            if (collision.CollisionDirection == Collision.Direction.Up)
            {
                if (Math.Abs(avatar.velocity.X) == 0)
                {
                    this.IdleTransition();
                }
                else
                {
                    this.RunningTransition();
                }
            }
            else if (collision.CollisionDirection == Collision.Direction.Down)
            {
                avatar.velocity.Y = 0;
            }
            else
            {
                avatar.velocity.X = 0;
                avatar.acceleration.X = 0;
            }
        }

        public override void Left()
        {
            avatar.acceleration.X = -AvatarData.DEFAULT_RUNNING_ACCELERATION;
        }

        public override void releaseDown()
        {
        }

        public override void releaseLeft()
        {
            avatar.acceleration.X = 0;
        }

        public override void releaseRight()
        {
            avatar.acceleration.X = 0;
        }

        public override void releaseJump()
        {
        }

        public override void Right()
        {
            avatar.acceleration.X = AvatarData.DEFAULT_RUNNING_ACCELERATION;
        }

        public override void Jump()
        {
        }

        public override void Update(GameTime gameTime)
        {
            if (Math.Abs(avatar.acceleration.X) == 0)
            {
                avatar.velocity.X = avatar.velocity.X * AvatarData.AVATAR_FRICTION;
                if (Math.Abs(avatar.velocity.X) < AvatarData.AVATAR_STOPPING_VELOCITY)
                    avatar.velocity.X = 0;
            }

            if (avatar.velocity.X < 0 && avatar.Sprite.Direction == Sprite.eDirection.Right)
                avatar.Sprite.Direction = Sprite.eDirection.Left;
            else if (avatar.velocity.X > 0 && avatar.Sprite.Direction == Sprite.eDirection.Left)
                avatar.Sprite.Direction = Sprite.eDirection.Right;
            Time += gameTime.ElapsedGameTime.Milliseconds;
            if (Time > SpriteData.DEFAULT_DELAY)
            {
                Time -= SpriteData.DEFAULT_DELAY;
                textCount--;
            }
            if (textCount == 0)
            {
                this.FallingTransition();
            }
        }

        public override void Enter(ActionState prevState)
        {
            base.Enter(prevState);
            avatar.acceleration.Y = AvatarData.GRAVITY;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Float()
        {
            this.FlyingTransition();
        }

        public override void ReleaseFloat()
        {

        }

        public override void Trigger()
        {
            this.SuckingTransition();
        }

        public override void ReleaseTrigger()
        {

        }

    }
    class EmptyFlyingState : EmptyActionState
    {
        private int Time;
        int textCount;
        bool check;
        public EmptyFlyingState(SwallowState owner) : base(owner)
        {
            check = false;
            Time = 0;
            textCount = 4;
        }

        public override void Down()
        {
        }

        public override void HandleBlockCollision(Collision collision)
        {

        }

        public override void Left()
        {
            avatar.acceleration.X = -AvatarData.DEFAULT_RUNNING_ACCELERATION;
        }

        public override void releaseDown()
        {
        }

        public override void releaseLeft()
        {
            avatar.acceleration.X = 0;
        }

        public override void releaseRight()
        {
            avatar.acceleration.X = 0;
        }

        public override void releaseJump()
        {
        }

        public override void Right()
        {
            avatar.acceleration.X = AvatarData.DEFAULT_RUNNING_ACCELERATION;
        }

        public override void Jump()
        {
        }

        public override void Update(GameTime gameTime)
        {
            if (Math.Abs(avatar.acceleration.X) == 0)
            {
                avatar.velocity.X = avatar.velocity.X * AvatarData.AVATAR_FRICTION;
                if (Math.Abs(avatar.velocity.X) < AvatarData.AVATAR_STOPPING_VELOCITY)
                    avatar.velocity.X = 0;
            }

            if (avatar.velocity.X < 0 && avatar.Sprite.Direction == Sprite.eDirection.Right)
                avatar.Sprite.Direction = Sprite.eDirection.Left;
            else if (avatar.velocity.X > 0 && avatar.Sprite.Direction == Sprite.eDirection.Left)
                avatar.Sprite.Direction = Sprite.eDirection.Right;
            Time += gameTime.ElapsedGameTime.Milliseconds;
            if (Time > SpriteData.DEFAULT_DELAY)
            {
                Time -= SpriteData.DEFAULT_DELAY;
                textCount--;
            }
            if (textCount == 0)
            {
                owner.AirTransition();
            } else if (textCount == 2 && !check)
            {
                avatar.Y = avatar.Y-12;
                check = true;
            }
        }

        public override void Enter(ActionState prevState)
        {
            base.Enter(prevState);
            avatar.velocity.Y = -3;
            avatar.acceleration.Y = 0;
        }

        public override void Exit()
        {
            base.Exit();
            //avatar.swallowed.AirTransition();
        }

        public override void Float()
        {
        }

        public override void ReleaseFloat()
        {
        }

        public override void Trigger()
        {
        }

        public override void ReleaseTrigger()
        {

        }
    }

    public class EmptySuckingState : EmptyActionState
    {
        private int delay = 10;
        public EmptySuckingState(SwallowState owner) : base(owner)
        {
        }

        public override void Enter(ActionState prevState)
        {
            base.Enter(prevState);
            avatar.velocity = new Vector2();
            avatar.acceleration = new Vector2();
            avatar.velocity.Y = -3;
            avatar.acceleration.Y = AvatarData.GRAVITY/2;
        }

        public override void Down()
        {
        }

        public override void Float()
        {
        }

        public override void HandleBlockCollision(Collision collision)
        {
            //test
        }

        public override void Jump()
        {
        }

        public override void Left()
        {
        }

        public override void releaseDown()
        {
        }

        public override void ReleaseFloat()
        {
        }

        public override void releaseJump()
        {
        }

        public override void releaseLeft()
        {
        }

        public override void releaseRight()
        {
        }

        public override void ReleaseTrigger()
        {
            this.IdleTransition();
            avatar.game.player.StopSuckStartSound();
            avatar.game.player.StopSuckProggressSound();

        }

        public override void Right()
        {
        }

        public override void Trigger()
        {
            //test
        }

        public override void Update(GameTime gameTime)
        {
            avatar.velocity.Y = 1;
            delay++;
            if (delay >75)
            { 
                avatar.game.player.PlaySuckInProgressSound();
            }

        }
    }
}
