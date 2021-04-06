using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace KirbyGame
{
    public class Sprite : Isprite
    {
        public enum eDirection
        {
            Left,
            Right
        }
        private eDirection direction;
        public TextureDetails texture;
        public Vector2 location;//made public

        public Sprite(TextureDetails texture, Vector2 location, eDirection direction)
        {
            this.texture = texture;
            this.location = location;
            this.direction = direction;
        }

        public Sprite(TextureDetails texture, Vector2 location)
        {
            this.texture = texture;
            this.location = location;
            this.direction = eDirection.Right;
        }

        public Sprite(Texture2D texture, Vector2 location, int numFrames)
        {
            this.direction = eDirection.Right;
            this.location = location;
            this.texture = new TextureDetails(texture, numFrames);
        }

        public Sprite(Texture2D texture, Rectangle textureLocation, Vector2 location, int numFrames)
        {
            this.direction = eDirection.Right;
            this.location = location;
            this.texture = new TextureDetails(texture, textureLocation, numFrames);
        }

        public float X
        {
            get { return location.X; }
            set { location = new Vector2(value, Y); }
        }

        public float Y
        {
            get { return location.Y; }
            set { location = new Vector2(X, value); }
        }

        public eDirection Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        public void Update(GameTime gameTime)
        {
            texture.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            texture.Draw(spriteBatch, location, direction);     
        }
    }
}
