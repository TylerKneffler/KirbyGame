using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace KirbyGame
{
    public class TextureDetails
    {
        private Texture2D texture;

        public List<Rectangle> textureLocations;
        public int currentTexture;

        private Point frameSize;
        private int Time;
        public int Delay;
        public Color currentColor;
        int mod;
        bool oneFrameMode;

        public Point size
        {
            get { return new Point(frameSize.X * mod, frameSize.Y * mod); }
        }

        public TextureDetails(Texture2D texture, int numFrames)
        {
            this.texture = texture;
            textureLocations = new List<Rectangle>();
            currentTexture = 0;            
            for(int i = 0; i < numFrames; i++)
            {
                textureLocations.Add(new Rectangle(texture.Width/numFrames*i, 0, texture.Width/numFrames, texture.Height));
            }

            frameSize = textureLocations[0].Size;
            Time = 0;
            Delay = SpriteData.DEFAULT_DELAY;
            mod = SpriteData.DEFAULT_SIZE_MOD;
            currentColor = Color.White;
            oneFrameMode = false;
        }

        public TextureDetails(Texture2D texture, Rectangle textureLocation, int numFrames)
        {
            this.texture = texture;
            textureLocations = new List<Rectangle>();
            currentTexture = 0;
            for (int i = 0; i < numFrames; i++)
            {
                textureLocations.Add(new Rectangle(textureLocation.X + textureLocation.Width / numFrames * i, textureLocation.Y, textureLocation.Width / numFrames, textureLocation.Height));
            }

            frameSize = textureLocations[0].Size;
            Time = 0;
            Delay = SpriteData.DEFAULT_DELAY;
            mod = SpriteData.DEFAULT_SIZE_MOD;
            currentColor = Color.White;
            oneFrameMode = false;
        }

        public void AddFrame(Rectangle textureLocation)
        {
            textureLocations.Add(textureLocation);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location, Sprite.eDirection direction)
        {

            if (direction == Sprite.eDirection.Left)
                spriteBatch.Draw(texture, location, textureLocations[currentTexture], currentColor, 0, new Vector2(0, 0),mod, SpriteEffects.FlipHorizontally, 0);
            else
                spriteBatch.Draw(texture, location, textureLocations[currentTexture], currentColor, 0, new Vector2(0, 0),mod,  SpriteEffects.None, 0);
        }

        public void DrawDepth(SpriteBatch spriteBatch, Vector2 location, Sprite.eDirection direction, float depth)
        {

            if (direction == Sprite.eDirection.Left)
                spriteBatch.Draw(texture, location, textureLocations[currentTexture], currentColor, 0, new Vector2(0, 0), mod, SpriteEffects.FlipHorizontally, depth);
            else
                spriteBatch.Draw(texture, location, textureLocations[currentTexture], currentColor, 0, new Vector2(0, 0), mod, SpriteEffects.None, depth);
        }

        public void Update(GameTime gameTime)
        {
            /*if(maxFrames > 1)
            {
                Time += gameTime.ElapsedGameTime.Milliseconds;
                if (Time > Delay)
                {
                    Time -= Delay;
                    currentFrame++;
                }
                if (currentFrame == maxFrames)
                    currentFrame = 0;
            }*/
            if (textureLocations.Count > 1 && !oneFrameMode)
            {
                Time += gameTime.ElapsedGameTime.Milliseconds;
                if (Time > Delay)
                {
                    Time -= Delay;
                    currentTexture++;
                }
                if (currentTexture == textureLocations.Count)
                    currentTexture = 0;
                frameSize = textureLocations[currentTexture].Size;
            }
        }

        public void SetCurrentFrame(int frame)
        {
            currentTexture = frame;
        }

        public void SetOnlyFrame(int frame)
        {
            oneFrameMode = true;
            currentTexture = frame;
        }

        public void AllFrames()
        {
            oneFrameMode = false;
            currentTexture = 0;
        }
    }

}
