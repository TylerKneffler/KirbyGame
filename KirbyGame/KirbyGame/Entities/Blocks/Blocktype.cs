using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.Media;

namespace KirbyGame
{
    abstract class Blocktype
    {
        protected Block block;
        public Point Teleportation;
        public Blocktype(Block block)
        {
            this.block = block;
            //each instance of blockType sets the sprite as well

        }

        public virtual void Update(GameTime gameTime)
        {
            if (block.bumping)
            {
                if (block.velocity.Y < 0 && (block.position.Y - block.anchor.Y) < -5)
                    block.velocity.Y = -block.velocity.Y;
                else if(block.velocity.Y > 0 && (block.anchor.Y - block.position.Y) > 0)
                {
                    block.velocity.Y = 0;
                    block.position = block.anchor;
                    block.bumping = false;
                }
            }
        }
        public virtual void Draw(SpriteBatch spriteBatch) { 
        
        }

        public void Bump()
        {
            block.bumping = true;
            block.velocity.Y = -1;
        }

    }
}
