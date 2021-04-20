using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace KirbyGame
{
    public class Boomerang : Entity, IProjectile
    {
        private int delay;
        public bool hurtKirby;
        private bool explode;
        private int startingLoc;
        private int direction;
        public Boomerang(Sprite sprite, int direction, Game1 game, bool canHurtKirby) : base(sprite)
        {
            this.game = game;
            this.hurtKirby = canHurtKirby;
            this.startingLoc = this.position.X;
            this.direction = direction;
            explode = false;
            defaultColor = Color.Yellow;
            boundingColor = defaultColor;

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
            if(this.direction == 0)
            {
                if(this.position.X < this.startingLoc - 100)
                {
                    this.velocity.X *= -1;
                }
            }else if(this.direction != 0)
            {
                if(this.position.X > this.startingLoc + 100)
                {
                    this.velocity.X *= -1;
                }
            }

            base.Update(gameTime);

        }

        public override void HandleCollision(Collision collision, Entity collider)
        {
            collision.A.Timer = 180;
            collision.B.Timer = 180;
            collision.A.boundingColor = Color.Orange;
            collision.B.boundingColor = Color.Orange;
            Rectangle.Intersect(BoundingBox, collision.B.BoundingBox);
            Collision.Direction CollisionDirection = Collision.normalizeDirection(collision, this);

            if (collider is Block && hurtKirby == false || (collider is EnemyTest && hurtKirby == false) || (collider is Avatar && hurtKirby == true) )
            {
                if (collider is Avatar && CollisionDirection == Collision.Direction.Left)
                {
                    X = collision.B.BoundingBox.Left;
                }
                if (collider is Avatar && CollisionDirection == Collision.Direction.Right)
                {
                    X = collision.B.BoundingBox.Right;
                }
                explode = true;
                velocity = new Vector2(0, 0);
                Sprite.texture = new TextureDetails(game.Content.Load<Texture2D>("Explosion"), 3);
            }
            else if(collider is Avatar && hurtKirby == false)
            {
                remove = true;
            }
            else if(collider is EnemyTest && ((EnemyTest)collider).enemytype is SirKibbleTest && hurtKirby == true)
            {
                remove = true;
            }
        }
    }
}
