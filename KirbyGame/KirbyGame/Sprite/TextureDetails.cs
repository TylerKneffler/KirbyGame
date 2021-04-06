using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace BlankMonoGameProject
{
    public class TextureDetails
    {
        private Texture2D texture;

        private Rectangle textureLocation;
        private int maxFrames;
        private int currentFrame;
        private Point frameSize;
        private int Time;
        private int Delay;
        int mod;

        public Point size
        {
            get { return new Point(frameSize.X * mod, frameSize.Y * mod); }
        }

        public TextureDetails(Texture2D texture, int numFrames)
        {
            this.texture = texture;
            textureLocation = new Rectangle(new Point(0, 0), new Point(texture.Width, texture.Height));
            maxFrames = numFrames;
            currentFrame = 0;
            frameSize = new Point(textureLocation.Width / maxFrames, textureLocation.Height);
            Time = 0;
            Delay = SpriteData.DEFAULT_DELAY;
            mod = SpriteData.DEFAULT_SIZE_MOD;
        }

        public TextureDetails(Texture2D texture, Rectangle textureLocation, int numFrames)
        {
            this.texture = texture;
            this.textureLocation = textureLocation;
            maxFrames = numFrames;
            currentFrame = 0;
            frameSize = new Point(textureLocation.Width / maxFrames, textureLocation.Height);
            Time = 0;
            Delay = SpriteData.DEFAULT_DELAY;
            mod = SpriteData.DEFAULT_SIZE_MOD;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location, Sprite.eDirection direction)
        {
            Rectangle sourceRectangle = new Rectangle(textureLocation.X + frameSize.X * currentFrame, textureLocation.Y, frameSize.X, frameSize.Y);

            if (direction == Sprite.eDirection.Left)
                spriteBatch.Draw(texture, location, sourceRectangle, Color.White, 0, new Vector2(0, 0),mod, SpriteEffects.FlipHorizontally, 0);
            else
                spriteBatch.Draw(texture, location, sourceRectangle, Color.White, 0, new Vector2(0, 0),mod,  SpriteEffects.None, 0);
        }

        public void Update(GameTime gameTime)
        {
            if(maxFrames > 1)
            {
                Time += gameTime.ElapsedGameTime.Milliseconds;
                if (Time > Delay)
                {
                    Time -= Delay;
                    currentFrame++;
                }
                if (currentFrame == maxFrames)
                    currentFrame = 0;
            }          
        }

        public void SetCurrentFrame(int frame)
        {
            currentFrame = frame;
        }
    }

}
