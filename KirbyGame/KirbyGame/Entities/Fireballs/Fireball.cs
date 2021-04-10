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
    public class Fireball : Entity
    {
        Random rand = new Random();
        bool explode;
        int delay;
        public Fireball(Sprite sprite, int direction, Game1 game) : base(sprite)
        {
            this.game = game;
            defaultColor = Color.Yellow;
            boundingColor = defaultColor;
            explode = false;
            base.acceleration = new Vector2(0, 1);
            if (direction == 0)
            {
                base.velocity = new Vector2(-5, -2);

            }
            else
            {
                base.velocity = new Vector2(5, -2);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if (explode)
            {
                delay++;
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (explode && delay == 15)
            {
                base.remove = true;
            }
            base.Update(gameTime);
        }

        public override void HandleCollision(Collision collision, Entity collider)
        {
            collision.A.Timer = 180;
            collision.B.Timer = 180;
            collision.A.boundingColor = Color.Orange;
            collision.B.boundingColor = Color.Orange;
            Rectangle intersection = Rectangle.Intersect(this.BoundingBox, collision.B.BoundingBox);
            Collision.Direction CollisionDirection = Collision.normalizeDirection(collision, this);
            if (collider is Block)
            {
                if (CollisionDirection == Collision.Direction.Down && !(((Block)collider).blocktype is BrokenBrickBlock) && !(((Block)collider).blocktype is Castle))
                {
                    base.velocity.Y = 0;
                    Y = this.BoundingBox.Bottom;
                }
                else if (CollisionDirection == Collision.Direction.Up && !(((Block)collider).blocktype is BrokenBrickBlock) && !(((Block)collider).blocktype is Castle))
                {
                    base.velocity.Y = base.velocity.Y * -1;
                    Y = this.BoundingBox.Top - this.BoundingBox.Height + 1;
                }
                else if (CollisionDirection == Collision.Direction.Right && !collision.collidedFromTop && !collision.collidedFromBottom && !(((Block)collider).blocktype is HiddenBlock) && !(((Block)collider).blocktype is BrokenBrickBlock) && !(((Block)collider).blocktype is Castle))
                {
                    if (rand.Next(1, 3) == 2)
                    {

                        base.velocity.X = base.velocity.X * -1;
                        X = this.BoundingBox.Right;
                    }
                    else
                    {
                        explode = true;
                        base.velocity = new Vector2(0, 0);
                        base.acceleration = new Vector2(0, 0);
                        base.boundingBoxSize = new Point(0, 0);
                        this.Sprite.texture = new TextureDetails(game.Content.Load<Texture2D>("Explosion"), 3);
                    }
                }
               else  if (CollisionDirection == Collision.Direction.Left && !collision.collidedFromTop && !collision.collidedFromBottom && !(((Block)collider).blocktype is HiddenBlock) && !(((Block)collider).blocktype is BrokenBrickBlock) && !(((Block)collider).blocktype is Castle))
                {
                    if (rand.Next(1, 3) == 2)
                    {

                        base.velocity.X = base.velocity.X * -1;
                        X = collider.BoundingBox.Left;
                    }
                    else
                    {
                        explode = true;
                        base.velocity = new Vector2(0, 0);
                        base.acceleration = new Vector2(0, 0);
                        base.boundingBoxSize = new Point(0, 0);
                        this.Sprite.texture = new TextureDetails(game.Content.Load<Texture2D>("Explosion"), 3);
                    }

                }
            }
            else if (collider is EnemyTest)
            {
                explode = true;
                base.velocity = new Vector2(0, 0);
                base.acceleration = new Vector2(0, 0);

                base.boundingBoxSize = new Point(0, 0);
                this.Sprite.texture = new TextureDetails(game.Content.Load<Texture2D>("Explosion"), 3);
            }
            else if (collider is Avatar && CollisionDirection == Collision.Direction.Left)
            {
                explode = true;
                base.velocity = new Vector2(0, 0);
                base.acceleration = new Vector2(0, 0);
                base.boundingBoxSize = new Point(0, 0);
                base.X = collision.B.BoundingBox.Left;
                this.Sprite.texture = new TextureDetails(game.Content.Load<Texture2D>("Explosion"), 3);
            }
            else if (collider is Avatar && CollisionDirection == Collision.Direction.Right)
            {
                explode = true;
                base.velocity = new Vector2(0, 0);
                base.acceleration = new Vector2(0, 0);
                base.boundingBoxSize = new Point(0, 0);
                base.X = collision.B.BoundingBox.Right;
                this.Sprite.texture = new TextureDetails(game.Content.Load<Texture2D>("Explosion"), 3);
            }
        }
    }
}
