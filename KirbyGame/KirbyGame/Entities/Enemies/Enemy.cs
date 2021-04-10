using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace KirbyGame
{
    public abstract class Enemy: Entity
    {
        public enum eEnemyType
        {
            KOOPA,
            GOOMBA,
        }
        public bool isDead;
        public bool seen;

        public Enemy(Sprite sprite, Game1 game) : base(sprite)
        {
            this.game = game;
            boundingBoxSize.X = (int)(boundingBoxSize.X * .8);
            boundingBoxSize.Y = (int)(boundingBoxSize.Y * .8);
            isDead = false;
            seen = true;
            defaultColor = Color.Red;
            boundingColor = defaultColor;
            this.velocity.X = -6;
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            if(seen)
            { 
                base.Update(gameTime);
            }
            else
            {
                //code to check if seen
            }
        }

        public override void HandleCollision(Collision collision, Entity collider)
        {
            if (collision.CollisionDirection == Collision.Direction.Up)  
            {
                isDead = true;
                boundingBoxSize = new Point();
            }
            else if((collision.CollisionDirection == Collision.Direction.Left && (collider is Block || collider is Enemy)))
            {
                this.velocity.X = this.velocity.X * -1;
                Sprite.Direction = Sprite.eDirection.Right;
            }
            else if((collision.CollisionDirection == Collision.Direction.Right && (collider is Block || collider is Enemy)))
            {
                this.velocity.X = this.velocity.X * -1;
                Sprite.Direction = Sprite.eDirection.Left;
            }
            else if (collision.CollisionDirection == Collision.Direction.Down && collider is Block)
            {
                Y = collider.BoundingBox.Bottom;
            }
        }
    }
}
