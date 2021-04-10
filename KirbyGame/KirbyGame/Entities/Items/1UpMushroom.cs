using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace KirbyGame
{
    class OneUpMushroom : Item
    {
        private int initialY;
        private SoundEffect player;

        public OneUpMushroom(Sprite sprite, Game1 game) : base(sprite, game)
        {
            justSpawned = true;
            initialY = Y - 23;
            remove = false;

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (justSpawned)
            {
                if (Y > initialY)
                {
                    velocity.Y = -1;
                }
                else
                {
                    velocity.Y = 0;
                    justSpawned = false;
                    DetermineInitialDirection();
                }
            }
            else if (velocity.Y == 0)
            {
                velocity.Y = 1;
            }
            else if (velocity.Y > 0)
            {
                acceleration.Y = .3F;
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
        public override void HandleCollision(Collision collision, Entity collider)
        {
            if (collider is Block)
            {

                if ((collision.CollisionDirection == Collision.Direction.Left || collision.CollisionDirection == Collision.Direction.Right) && collision.collidedFromTop)
                {
                    collision.CollisionDirection = Collision.Direction.Up;
                    Debug.WriteLine("CHANGED!!");
                }

                if (collision.CollisionDirection == Collision.Direction.Up && !(((Block)collider).blocktype is HiddenBlock) && !(((Block)collider).blocktype is BrokenBrickBlock) && !(((Block)collider).blocktype is Castle))
                {
                    velocity.Y = 0;
                    base.acceleration.Y = 0;
                    Y = collider.BoundingBox.Top - this.BoundingBox.Height;
                    Debug.WriteLine("Collided from above!!");
                }
                else if (collision.CollisionDirection == Collision.Direction.Left && !(((Block)collider).blocktype is HiddenBlock) && !(((Block)collider).blocktype is BrokenBrickBlock) && !(((Block)collider).blocktype is Castle))
                {
                    this.velocity.X = this.velocity.X * -1;
                    Debug.WriteLine("Collided from left!!");
                }
                else if (collision.CollisionDirection == Collision.Direction.Right && !(((Block)collider).blocktype is HiddenBlock) && !(((Block)collider).blocktype is BrokenBrickBlock) && !(((Block)collider).blocktype is Castle))
                {
                    this.velocity.X = this.velocity.X * -1;
                    Debug.WriteLine("Collided from right!!");
                }
                else if (collision.CollisionDirection == Collision.Direction.Down && !(((Block)collider).blocktype is HiddenBlock) && !(((Block)collider).blocktype is BrokenBrickBlock) && !(((Block)collider).blocktype is Castle))
                {
                    Debug.WriteLine("This should never print!");
                }
            }
            else if (collider is Avatar)
            {
                //Also add to total lives, but that doesn't exist atm
                //Y += 2 * boundingBoxSize.Y;
                //isUsed = true;
                this.player = this.game.Content.Load<SoundEffect>("SoundEffects/smb_1-up");
                this.player.Play();
                this.boundingBoxSize = new Point();
                this.position = new Point();
                game.levelLoader.list.Remove(this);
                

                //remove = true;
            }
        }
        public void DetermineInitialDirection()
        {
            int marioX = game.mario.X;
            if (marioX < X)
            {
                velocity.X = 2;
            }
            else
            {
                velocity.X = -2;
            }
        }
    }
}
