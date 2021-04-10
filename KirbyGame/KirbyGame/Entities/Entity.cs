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
    public abstract class Entity : Isprite
    {
        private Sprite sprite;
        public Point boundingBoxSize;
        public const int maxVelocity = 4;
        public bool remove = false;
     
        //anchor
        public Point position 
        {
            get {
                return new Point((int)Sprite.X, (int)Sprite.Y); 
            }
            set
            {
                Sprite.X = value.X;
                Sprite.Y = value.Y;
            }
        }

        public int Timer;

        public int X { get { return (int)Sprite.X; } set { Sprite.X = value; } }
        public int Y { get { return (int)Sprite.Y; } set { Sprite.Y = value; } }
        public Vector2 velocity;
        public Vector2 acceleration;
        //what does mask do
        //it did nothing, it was a test from long ago so I removed it! - Dennis
        public Color boundingColor;
        public Color defaultColor;

        public Game1 game;

        public Sprite Sprite
        {
            set { 
                sprite = value;
                boundingBoxSize = sprite.texture.size;
            }
            get { return sprite; }
        }
        
        public Entity(Sprite sprite)
        {
            this.sprite = sprite;
            position = new Point((int)sprite.X, (int)sprite.Y);
            velocity = new Vector2();
            acceleration = new Vector2();
            boundingBoxSize = Sprite.texture.size;
            boundingColor = defaultColor;
            Timer = 0;
        }

        public Entity()
        {
            velocity = new Vector2();
            acceleration = new Vector2();
            Timer = 0;
        }

        public Entity(Color defColor)
        {
            velocity = new Vector2();
            acceleration = new Vector2();
            defaultColor = defColor;
            boundingColor = defaultColor;
            Timer = 0;
        }

        public virtual Rectangle BoundingBox { get { return new Rectangle(new Point((int)Sprite.X + (Sprite.texture.size.X-boundingBoxSize.X)/2, (int)Sprite.Y + (Sprite.texture.size.Y - boundingBoxSize.Y) / 2), boundingBoxSize); } }
        
        public virtual void Update(GameTime gameTime)
        {
            velocity += acceleration;
            velocity.X = MathHelper.Clamp(velocity.X, -maxVelocity, maxVelocity);
            sprite.X += (int)velocity.X;
            sprite.Y += (int)velocity.Y;
            sprite.Update(gameTime);
            if (Timer > 0)
            {
                Timer -= gameTime.ElapsedGameTime.Milliseconds;
                if (Timer < 0)
                {
                    Timer = 0;
                    boundingColor = defaultColor;
                }
            }
        }
        Texture2D _texture = null;
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch);
            if (game.boundingBoxToggle)
            {
                if (_texture == null)
                    _texture = new Texture2D(game.GraphicsDevice, 1, 1);
                _texture.SetData(new Color[] { boundingColor });

                spriteBatch.Draw(_texture, new Rectangle(BoundingBox.Left, BoundingBox.Top, BoundingBox.Width, 1), boundingColor);
                spriteBatch.Draw(_texture, new Rectangle(BoundingBox.Right, BoundingBox.Top, 1, BoundingBox.Height), boundingColor);
                spriteBatch.Draw(_texture, new Rectangle(BoundingBox.Left, BoundingBox.Bottom, BoundingBox.Width, 1), boundingColor);
                spriteBatch.Draw(_texture, new Rectangle(BoundingBox.Left, BoundingBox.Top, 1, BoundingBox.Height), boundingColor);
            }
            
        }

        public abstract void HandleCollision(Collision collision, Entity collider);

        #region Collision
        public bool TouchingTopOfEntity(Entity entity)
        {
            return this.BoundingBox.Bottom + this.velocity.X > entity.BoundingBox.Top &&
              this.BoundingBox.Top < entity.BoundingBox.Top &&
              this.BoundingBox.Right > entity.BoundingBox.Left &&
              this.BoundingBox.Left < this.BoundingBox.Right;
        }
        public bool TouchingBottomOfEntity(Entity entity)
        {
            return this.BoundingBox.Top + this.velocity.X > entity.BoundingBox.Bottom &&
              this.BoundingBox.Bottom < entity.BoundingBox.Bottom &&
              this.BoundingBox.Right > entity.BoundingBox.Left &&
              this.BoundingBox.Left < this.BoundingBox.Right;
        }
        public bool TouchingLeftOfEntity(Entity entity)
        {
            return this.BoundingBox.Right + this.velocity.X > entity.BoundingBox.Left &&
              this.BoundingBox.Left < entity.BoundingBox.Left &&
              this.BoundingBox.Bottom > entity.BoundingBox.Top &&
              this.BoundingBox.Top < this.BoundingBox.Bottom;
        }

        public bool TouchingRightOfEntity(Entity entity)
        {
            return this.BoundingBox.Left + this.velocity.X > entity.BoundingBox.Right &&
              this.BoundingBox.Right < entity.BoundingBox.Right &&
              this.BoundingBox.Bottom > entity.BoundingBox.Top &&
              this.BoundingBox.Top < this.BoundingBox.Bottom;
        }
       
        #endregion

        public Entity ShallowCopy()
        {
            return (Entity)this.MemberwiseClone();
        }
    }
}
