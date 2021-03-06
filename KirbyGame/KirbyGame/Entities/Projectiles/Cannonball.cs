using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace KirbyGame
{
    public class Cannonball : Entity, IProjectile
    {
        private bool explode;
        private int delay;
        private bool hurtKirby;
        public Cannonball(Sprite sprite, int direction, Game1 game) : base(sprite)
        {
            this.game = game;
            hurtKirby = true;
            defaultColor = Color.Yellow;
            boundingColor = defaultColor;
            explode = false;
            if (direction == 0)
            {
                velocity = new Vector2(-5, 0);

            }
            else
            {
                velocity = new Vector2(5, 0);
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
            if (delay > 15)
            {
                remove = true;
            }
            base.Update(gameTime);

        }

        public override void HandleCollision(Collision collision, Entity collider)
        {
            if (!explode)
            {
                collision.A.Timer = 180;
                collision.B.Timer = 180;
                collision.A.boundingColor = Color.Orange;
                collision.B.boundingColor = Color.Orange;
                Rectangle.Intersect(BoundingBox, collision.B.BoundingBox);
                Collision.Direction CollisionDirection = Collision.normalizeDirection(collision, this);
                if (collider is Block || collider is EnemyTest || collider is Avatar || collider is AirPuff || collider is Star)
                {
                    if (collider is Avatar && CollisionDirection == Collision.Direction.Left)
                    {
                        X = collision.B.BoundingBox.Left;
                    }
                    if (collider is Avatar && CollisionDirection == Collision.Direction.Right)
                    {
                        X = collision.B.BoundingBox.Right;
                    }
                    game.player.PlayExplosionSound();
                    explode = true;
                    velocity = new Vector2(0, 0);
                    Sprite.texture = new TextureDetails(game.Content.Load<Texture2D>("Explosion"), 3);
                }
            }
        }
    }
}
    
