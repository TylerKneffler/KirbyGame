using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace KirbyGame
{
    public class LazerProjectile : Entity, IProjectile
    {
        private bool explode;
        private int delay;
        private int direction;
        private int timer;
        private Vector2 location;
        public bool hurtKirby;
        public LazerProjectile(Sprite sprite, Vector2 location, int direction, bool hurtKirby, Game1 game) : base(sprite)
        {
            this.game = game;
            defaultColor = Color.Yellow;
            boundingColor = defaultColor;
            this.direction = direction;
            this.location = location;
            explode = false;
            this.hurtKirby = hurtKirby;
            timer = 20;
            if (direction == 0)
            {
                velocity = new Vector2(-2, (float)1);
                acceleration = new Vector2((float).5, (float).5);

            }
            else
            {
                velocity = new Vector2(2, (float)1);
                acceleration = new Vector2((float)-.5, (float).5);
            }
        }

        public bool canHurtKirby()
        {
            return this.hurtKirby;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            if (explode)
            {
                delay++;
            }
            if(delay > 10 || timer == 0)
            {
                this.game.levelLoader.list.Remove(this);
                this.game.map.Remove(this);

            }
            if (direction == 1)
            {
                acceleration.X += (float).01 * (X - location.X);
            }
            else
            {
                acceleration.X -= (float).01 * (location.X - X);
            }
            acceleration.Y -= (float).001 * (Y - (location.Y));
            base.Update(gameTime);
            timer--;
        }

        public override void HandleCollision(Collision collision, Entity collider)
        {
            collision.A.Timer = 180;
            collision.B.Timer = 180;
            collision.A.boundingColor = Color.Orange;
            collision.B.boundingColor = Color.Orange;
            Rectangle.Intersect(BoundingBox, collision.B.BoundingBox);
            Collision.Direction CollisionDirection = Collision.normalizeDirection(collision, this);
            if (collider is Block || collider is EnemyTest || collider is Avatar)
            {
                if (collider is Avatar && CollisionDirection == Collision.Direction.Left)
                {
                    X = collision.B.BoundingBox.Left;
                }
                if (collider is Avatar && CollisionDirection == Collision.Direction.Right)
                {
                    X = collision.B.BoundingBox.Right;
                }
                velocity = new Vector2(0, 0);

                explode = true;
            }
        }
    }
}
    
