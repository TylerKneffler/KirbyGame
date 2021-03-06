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
            avatar.game.player.PlayJumpSound();
            CurrentState.Enter(this);
        }

        public void ExpellingTransition()
        {
            CurrentState.Exit();
            CurrentState = new AirExpellingState(owner);
            avatar.game.player.PlaySpitSound();
            CurrentState.Enter(this);
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
            if(collision.CollisionDirection == Collision.Direction.Left || collision.CollisionDirection == Collision.Direction.Right)
            {
                avatar.velocity.X = 0;
                avatar.acceleration.X = 0;
            } else if (collision.CollisionDirection == Collision.Direction.Up)
            {
                avatar.velocity.Y = 0;
                avatar.acceleration.Y = 0;

            }

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
            avatar.velocity.Y = 2;
        }

        public override void Trigger()
        {
            this.ExpellingTransition();
        }

        public override void ReleaseTrigger()
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
            if (collision.CollisionDirection == Collision.Direction.Left || collision.CollisionDirection == Collision.Direction.Right)
            {
                avatar.velocity.X = 0;
                avatar.acceleration.X = 0;
            } else if (collision.CollisionDirection == Collision.Direction.Up || collision.CollisionDirection == Collision.Direction.Down)
            {
                avatar.velocity.Y = 0;
                avatar.acceleration.Y = 0;

            }

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

        public override void Trigger()
        {
            this.ExpellingTransition();
        }

        public override void ReleaseTrigger()
        {
        }
    }

    class AirExpellingState : AirActionState
    {
        bool finalFrameReached;
        public AirExpellingState(SwallowState owner) : base(owner)
        {
            finalFrameReached = false;
        }


        public override void Down()
        {
        }

        public override void HandleBlockCollision(Collision collision)
        {
            if (collision.CollisionDirection == Collision.Direction.Left || collision.CollisionDirection == Collision.Direction.Right)
            {
                avatar.velocity.X = 0;
                avatar.acceleration.X = 0;
            }
            else if (collision.CollisionDirection == Collision.Direction.Up || collision.CollisionDirection == Collision.Direction.Down)
            {
                avatar.velocity.Y = 0;
                avatar.acceleration.Y = 0;

            }

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

        public override void Enter(ActionState prevState)
        {
            base.Enter(prevState);
            avatar.acceleration.Y = AvatarData.GRAVITY;
            
        }

        public override void Float()
        {
        }

        public override void ReleaseFloat()
        {
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
            
            if(avatar.Sprite.texture.currentTexture == 1)
            {
                finalFrameReached = true;
            } else if(avatar.Sprite.texture.currentTexture == 0 && finalFrameReached)
            {
                owner.EmptyTransition();
            }
        }

        public override void Trigger()
        {
        }

        public override void ReleaseTrigger()
        {
        }
    }
}
