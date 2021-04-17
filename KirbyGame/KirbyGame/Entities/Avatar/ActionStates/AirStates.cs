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
    public abstract class AirActionState : ActionState
    {
        public AirActionState(SwallowState owner) : base(owner)
        {

        }

        public void FloatingTransition()
        {
            CurrentState.Exit();
            CurrentState = new AirFloatingState(owner);
            CurrentState.Enter(this);
        }

        public void FlappingTransition()
        {
            CurrentState.Exit();
            CurrentState = new AirFlyingState(owner);
            CurrentState.Enter(this);
        }

        public override void Update(GameTime gameTime)
        {
            avatar.velocity.X = MathHelper.Clamp(avatar.velocity.X, -AvatarData.MAX_FLOAT_SPEED, AvatarData.MAX_FLOAT_SPEED);
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
    }

    class AirFloatingState : AirActionState
    {
        public AirFloatingState(SwallowState owner) : base(owner)
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
            avatar.acceleration.X = -AvatarData.DEFAULT_FLOATING_ACCELERATION;
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
            avatar.acceleration.X = AvatarData.DEFAULT_FLOATING_ACCELERATION;
        }


        public override void Jump()
        {
            this.FlappingTransition();
        }

        public override void Enter(ActionState prevState)
        {
            base.Enter(prevState);
            avatar.velocity.Y = 2;
            avatar.acceleration.Y = 0;
        }

        public override void Float()
        {
            this.FlappingTransition();
        }

        public override void ReleaseFloat()
        {

        }
    }

    class AirFlyingState : AirActionState
    {
        public AirFlyingState(SwallowState owner) : base(owner)
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
            avatar.acceleration.X = -AvatarData.DEFAULT_FLOATING_ACCELERATION;
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
            this.FloatingTransition();
        }

        public override void Right()
        {
            avatar.acceleration.X = AvatarData.DEFAULT_FLOATING_ACCELERATION;
        }


        public override void Jump()
        {
        }

        public override void Enter(ActionState prevState)
        {
            base.Enter(prevState);
            avatar.velocity.Y = -3;
            avatar.acceleration.Y = 0;
        }

        public override void Float()
        {
        }

        public override void ReleaseFloat()
        {
            this.FloatingTransition();
        }
    }
}
