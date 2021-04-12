using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using System.Media;
using Microsoft.Xna.Framework.Audio;

namespace KirbyGame
{


    public class Avatar : Entity
    {
        public SwallowState swallowed;
        public ActionState actionState;
        public Vector2 startingLocation;
        private KirbySpriteFactory factory;
        //public int numLives;
        private SoundEffect player;

        public bool IsDead { get; set; }        
        public Avatar(Game1 game, Vector2 location) : base(Color.Yellow)
        {
            this.game = game;
            this.startingLocation = location;

            factory = new KirbySpriteFactory(this);
            actionState = new IdleState(this);
            swallowed = new EmptySwallowState(this);
            this.Sprite = factory.createSprite(actionState, swallowed, location, Sprite.eDirection.Right);

            //numLives = AvatarData.INIT_NUM_LIVES;
            IsDead = false;
        }

        public Avatar(Avatar avatar) : base(Color.Yellow)
        {
            this.game = avatar.game;
            this.startingLocation = new Vector2(avatar.X, avatar.Y);
            actionState = avatar.actionState;
            this.UpdateSprite();
        }
        

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            actionState.Update(gameTime);

            Debug.WriteLine(this.X);

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public bool canBreakBlocks()
        {
            return false;
        }
        public void MarioTeleport(Point final)
        {
            this.position = final;
        }

        /**
         * avatar commands (setting to small, reg, fire are done directly using state getters and setters and as such included in next section). 
         */
        #region Commands
        public void pressFloat()
        {
            actionState.Float();
        }

        public void releaseFloat()
        {
            actionState.ReleaseFloat();
        }

        public void pressJump()
        {
            actionState.Jump();
        }
        public void pressDown()
        {
            actionState.Down();
        }
        public void pressRight()
        {
            actionState.Right();
        }
        public void pressLeft()
        {
            actionState.Left();
        }

        public void releaseJump()
        {

            actionState.releaseJump();
        }

        public void releaseDown()
        {
            actionState.releaseDown();
        }

        public void releaseRight()
        {
            actionState.releaseRight();
        }

        public void releaseLeft()
        {
            actionState.releaseLeft();
        }

        //not sure what this does 
        public void HitBlock()
        {
            actionState.Down();
        }
        
        public void UpdateSprite()
        {
            this.Sprite = factory.createSprite(actionState, swallowed, new Vector2(X, Y), Sprite.Direction);
        }
        #endregion
        //UNTESTED
        public override void HandleCollision(Collision collision, Entity collider)
        {
            Collision.Direction CollisionDirection = Collision.normalizeDirection(collision, this);
            actionState.HandleBlockCollision(collision);
            if(collider is Block)
            {
                if(CollisionDirection is Collision.Direction.Up)
                {
                    velocity.Y = 0;
                    acceleration.Y = 0;
                    Y = collider.Y - this.BoundingBox.Height;
                }
            }
        }
        public void updateSprite()
        {
            this.Sprite = factory.createSprite(actionState, swallowed, new Vector2(X, Y), Sprite.Direction);
        }
    }
}
