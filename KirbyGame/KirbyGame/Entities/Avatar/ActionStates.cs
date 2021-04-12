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
    abstract public class ActionState
    {
        public Avatar avatar;
        protected ActionState previousState;
        protected ActionState CurrentState { get { return avatar.actionState; } set { avatar.actionState = value; } }
        protected SoundEffect player;


        public ActionState(Avatar avatar)
        {
            this.avatar = avatar;
        }

        protected virtual void Enter(ActionState prevState)
        {
            //CurrentState = this;
            this.previousState = prevState;
            avatar.UpdateSprite();
        }

        protected virtual void Exit()
        {

        }
        public abstract void Left();
        public abstract void Right();
        public abstract void Up();
        public abstract void Down();

        public abstract void releaseLeft();
        public abstract void releaseRight();
        public abstract void releaseUp();
        public abstract void releaseDown();
        public abstract void Update(GameTime gameTime);
        public abstract void HandleBlockCollision(Collision collision);

        public void IdleTransition()
        {
            CurrentState.Exit();
            CurrentState = new IdleState(avatar);
            CurrentState.Enter(this);
        }


        public void RunningTransition()
        {
            CurrentState.Exit();
            CurrentState = new RunningState(avatar);
            CurrentState.Enter(this);
        }

        public void JumpingTransition()
        {
            CurrentState.Exit();
            CurrentState = new JumpingState(avatar);
            CurrentState.Enter(this);
        }

        public void FallingTransition()
        {
            CurrentState.Exit();
            CurrentState = new FallingState(avatar);
            CurrentState.Enter(this);
        }

        public void FlippingTransition()
        {
            CurrentState.Exit();
            CurrentState = new FlippingState(avatar);
            CurrentState.Enter(this);
        }

    }

    class IdleState : ActionState
    {
        public IdleState(Avatar avatar) : base(avatar)
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

        public override void releaseUp()
        {

        }

        public override void Right()
        {
            if (avatar.Sprite.Direction == Sprite.eDirection.Left)
                avatar.Sprite.Direction = Sprite.eDirection.Right;

            this.RunningTransition();
        }


        public override void Up()
        {
            this.JumpingTransition();
        }

        public override void Update(GameTime gameTime)
        {
        }

        protected override void Enter(ActionState prevState)
        {
            base.Enter(prevState);
            avatar.velocity = new Vector2();
            avatar.acceleration = new Vector2();
        }

    }



    class RunningState : ActionState
    {
        public RunningState(Avatar avatar) : base(avatar)
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

        public override void releaseUp()
        {
        }

        public override void Right()
        {
            if (avatar.Sprite.Direction == Sprite.eDirection.Left)
                this.IdleTransition();
        }

        public override void Up()
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

        protected override void Enter(ActionState prevState)
        {
            base.Enter(prevState);

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


            avatar.velocity.Y = 0;
            avatar.acceleration.Y = 0;

        }

        protected override void Exit()
        {
            base.Exit();
        }

    }

    class MarioTransitioningState : ActionState
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

        public override void releaseUp()
        {
        }

        public override void Right()
        {

        }

        public override void Up()
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

    }

    class JumpingState : ActionState
    {
        private int Timer;
        public JumpingState(Avatar avatar) : base(avatar)
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

        public override void releaseUp()
        {
            //this.FallingTransition();
        }

        public override void Right()
        {
            avatar.acceleration.X = AvatarData.DEFAULT_RUNNING_ACCELERATION;
        }

        public override void Up()
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
        protected override void Enter(ActionState prevState)
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
        protected override void Exit()
        {
            base.Exit();
        }


    }

    class FallingState : ActionState
    {
        public FallingState(Avatar avatar) : base(avatar)
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

        public override void releaseUp()
        {
        }

        public override void Right()
        {
            avatar.acceleration.X = AvatarData.DEFAULT_RUNNING_ACCELERATION;
        }

        public override void Up()
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

        protected override void Enter(ActionState prevState)
        {
            base.Enter(prevState);
            avatar.acceleration.Y = AvatarData.GRAVITY;
        }

        protected override void Exit()
        {
            base.Exit();
        }



    }


    class FlippingState : ActionState
    {
        private int Time;
        int textCount;
        public FlippingState(Avatar avatar) : base(avatar)
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

        public override void releaseUp()
        {
        }

        public override void Right()
        {
            avatar.acceleration.X = AvatarData.DEFAULT_RUNNING_ACCELERATION;
        }

        public override void Up()
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
            if(textCount == 0)
            {
                this.FallingTransition();
            }
        }

        protected override void Enter(ActionState prevState)
        {
            base.Enter(prevState);
            avatar.acceleration.Y = AvatarData.GRAVITY;
        }

        protected override void Exit()
        {
            base.Exit();
        }
    }
}
