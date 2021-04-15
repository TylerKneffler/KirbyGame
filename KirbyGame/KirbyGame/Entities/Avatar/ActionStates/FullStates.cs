﻿using System;
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
    public abstract class FullActionState : ActionState
    {
        public FullActionState(SwallowState owner) : base(owner)
        {

        }

        public void IdleTransition()
        {
            CurrentState.Exit();
            CurrentState = new FullIdleState(owner);
            CurrentState.Enter(this);
        }


        public void RunningTransition()
        {
            CurrentState.Exit();
            CurrentState = new FullRunningState(owner);
            CurrentState.Enter(this);
        }

        public void JumpingTransition()
        {
            CurrentState.Exit();
            CurrentState = new FullJumpingState(owner);
            CurrentState.Enter(this);
        }
    }

    class FullIdleState : FullActionState
    {
        public FullIdleState(SwallowState owner) : base(owner)
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
        }

        public override void Enter(ActionState prevState)
        {
            base.Enter(prevState);
            avatar.velocity = new Vector2();
            avatar.acceleration = new Vector2();
        }

        public override void Float()
        {
        }

        public override void ReleaseFloat()
        {

        }
    }

    class FullRunningState : FullActionState
    {
        public FullRunningState(SwallowState owner) : base(owner)
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
                //this.FallingTransition();
            }
            else if (Math.Abs(avatar.acceleration.X) == 0)
            {
                avatar.velocity.X = avatar.velocity.X * AvatarData.AVATAR_FRICTION;
                if (Math.Abs(avatar.velocity.X) < AvatarData.AVATAR_STOPPING_VELOCITY)
                    this.IdleTransition();
            }
            //avatar.velocity.Y = 1;
            if (avatar.velocity.X < 0 && avatar.Sprite.Direction == Sprite.eDirection.Right)
                avatar.Sprite.Direction = Sprite.eDirection.Left;
            else if (avatar.velocity.X > 0 && avatar.Sprite.Direction == Sprite.eDirection.Left)
                avatar.Sprite.Direction = Sprite.eDirection.Right;
        }

        public override void Enter(ActionState prevState)
        {
            base.Enter(prevState);
            avatar.velocity.Y = 0;
            avatar.acceleration.Y = 0;

        }

        public override void Exit()
        {
            base.Exit();
        }
        public override void Float()
        {
        }

        public override void ReleaseFloat()
        {

        }

    }

    class FullJumpingState : FullActionState
    {
        private int Timer;
        public FullJumpingState(SwallowState owner) : base(owner)
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
                Timer = 0;
                avatar.acceleration.Y = AvatarData.GRAVITY;
            }
            else if (collision.CollisionDirection != Collision.Direction.Up)
            {
                this.RunningTransition();
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
                    avatar.acceleration.Y = AvatarData.GRAVITY;
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
        }

        public override void ReleaseFloat()
        {

        }


    }
}
