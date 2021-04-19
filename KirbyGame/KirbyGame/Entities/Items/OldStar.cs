using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace KirbyGame
{
    class MarioStar : Item
    {
        private int initialY;
        private int Timer;

        public MarioStar(Sprite sprite, Game1 game) : base(sprite, game)
        {
            justSpawned = true;
            initialY = Y - 23;
            Timer = 0;
        }

        public override void Update(GameTime gameTime)
        {
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
                    base.acceleration.Y = 1;
                }
            }
            base.Update(gameTime);

            if (velocity.Y < 0)
            {
                Timer++;
                if (Timer > 50)
                {
                    Timer = 0;
                    velocity.Y = 1;
                }
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
                if (collision.CollisionDirection == Collision.Direction.Up && !(((Block)collider).blocktype is HiddenBlock) && !(((Block)collider).blocktype is BrokenBrickBlock) && !(((Block)collider).blocktype is Castle))
                {
                    velocity.Y = -1;
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
                    velocity.Y = 1;
                    base.acceleration.Y = 0;
                    Y = collider.BoundingBox.Bottom;
                }
            }
            else if (collider is Avatar)
            {
                //Also add to total lives, but that doesn't exist atm

                game.levelLoader.list.Remove(this);
            }
        }
        public void DetermineInitialDirection()
        {
            int marioX = game.mario.X;
            if (marioX < X)
            {
                velocity.X = 1;
            }
            else
            {
                velocity.X = -1;
            }
        }
    }
}
