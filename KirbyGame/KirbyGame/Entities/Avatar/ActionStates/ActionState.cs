using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Microsoft.Xna.Framework.Audio;

namespace KirbyGame
{
    abstract public class ActionState
    {
        public Avatar avatar;
        public SwallowState owner;
        protected ActionState previousState;
        protected ActionState CurrentState { get { return owner.actionState; } set { owner.actionState = value; } }
        protected SoundEffect player;


        public ActionState(SwallowState owner)
        {
            this.owner = owner;
            this.avatar = owner.avatar;
        }

        public virtual void Enter(ActionState prevState)
        {
            //CurrentState = this;
            this.previousState = prevState;
            avatar.UpdateSprite();
        }

        public virtual void Exit()
        {

        }
        public abstract void Left();
        public abstract void Right();
        public abstract void Jump();
        public abstract void Down();
        public abstract void Float();

        public abstract void ReleaseFloat();

        public abstract void releaseLeft();
        public abstract void releaseRight();
        public abstract void releaseJump();
        public abstract void releaseDown();
        public abstract void Update(GameTime gameTime);
        public abstract void HandleBlockCollision(Collision collision);

    }
}
