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
        bool explode;
        int delay;
        public Fireball(Sprite sprite, int direction, Game1 game) : base(sprite)
        {
            this.game = game;
            defaultColor = Color.Yellow;
            boundingColor = defaultColor;
            explode = false;
            acceleration = new Vector2(0, 0);
            if (direction == 0)
            {
                velocity = new Vector2(-5, 0);

            }
            else
            {
                velocity = new Vector2(5, 0);
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
                remove = true;
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
            if (collider is Block || collider is  EnemyTest || collider is Avatar)
            {
               if (collider is Avatar && CollisionDirection == Collision.Direction.Left)
                {
                    X = collision.B.BoundingBox.Left;
                }
                if (collider is Avatar && CollisionDirection == Collision.Direction.Right)
                {
                    X = collision.B.BoundingBox.Right;
                }
                boundingBoxSize = new Point(0, 0);
                explode = true;
                velocity = new Vector2(0, 0);
                acceleration = new Vector2(0, 0);
                Sprite.texture = new TextureDetails(game.Content.Load<Texture2D>("Explosion"), 3);
            }        
        }
    }
}
