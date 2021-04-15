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

            swallowed = new FullSwallowState(this);
            this.Sprite = factory.createSprite(swallowed, location, Sprite.eDirection.Right);

            //numLives = AvatarData.INIT_NUM_LIVES;
            IsDead = false;
        }

        public Avatar(Avatar avatar) : base(Color.Yellow)
        {
            this.game = avatar.game;
            this.startingLocation = new Vector2(avatar.X, avatar.Y);

            this.UpdateSprite();
        }
        

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            swallowed.Update(gameTime);


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
            swallowed.PressFloat();
        }

        public void releaseFloat()
        {
            swallowed.ReleaseFloat();
        }

        public void pressJump()
        {
            swallowed.PressJump();
        }
        public void pressDown()
        {
            swallowed.PressDown();
        }
        public void pressRight()
        {
            swallowed.PressRight();
        }
        public void pressLeft()
        {
            swallowed.PressLeft();
        }

        public void releaseJump()
        {

            swallowed.ReleaseJump();
        }

        public void releaseDown()
        {
            swallowed.ReleaseDown();
        }

        public void releaseRight()
        {
            swallowed.ReleaseRight();
        }

        public void releaseLeft()
        {
            swallowed.ReleaseLeft();
        }

        //not sure what this does 
        /*public void HitBlock()
        {
            swallowed.Down();
        }*/
        
        public void UpdateSprite()
        {
            this.Sprite = factory.createSprite(swallowed, new Vector2(X, Y), Sprite.Direction);
        }
        #endregion
        //UNTESTED
        public override void HandleCollision(Collision collision, Entity collider)
        {
            Collision.Direction CollisionDirection = Collision.normalizeDirection(collision, this);
            swallowed.HandleBlockCollision(collision);
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
    }
}
