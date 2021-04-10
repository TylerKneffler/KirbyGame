using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System;

namespace KirbyGame
{
    class FloorBlock : Blocktype
    {
        public FloorBlock(Block block, Vector2 location) : base(block)
        {
            this.block.Sprite = new Sprite(new TextureDetails(this.block.game.Content.Load<Texture2D>("Floor_Block"), 1), location);
        }
    }

    class QuestionBlock : Blocktype
    {
        public QuestionBlock(Block block, Vector2 location) : base(block)
        {
            this.block.Sprite = new Sprite(new TextureDetails(this.block.game.Content.Load<Texture2D>("Question_Block"), new Rectangle(0, 0, 48, 16), 3), location);
        }
        
    }
    class UsedBlock : Blocktype
    {
        public UsedBlock(Block block, Vector2 location) : base(block)
        {
            this.block.Sprite = new Sprite(new TextureDetails(this.block.game.Content.Load<Texture2D>("UsedQuestion_Block"), 1), location);
        }
    }

    class StairBlock : Blocktype
    {
        public StairBlock(Block block, Vector2 location) : base(block)
        {
            this.block.Sprite = new Sprite(new TextureDetails(this.block.game.Content.Load<Texture2D>("Pyramid_Block"), 1), location);
        }
    }
    class HiddenBlock : Blocktype
    {
        public HiddenBlock(Block block, Vector2 location) : base(block)
        {
            this.block.Sprite = new Sprite(new TextureDetails(this.block.game.Content.Load<Texture2D>("Hidden_Block"), 1), location);
        }

    }
    class BrickBlock : Blocktype
    {
        public BrickBlock(Block block, Vector2 location) : base(block)
        {
            this.block.Sprite = new Sprite(new TextureDetails(this.block.game.Content.Load<Texture2D>("Brick_block"), 1), location);
        }
    }
    class BrickBlockBlue : Blocktype
    {
        public BrickBlockBlue(Block block, Vector2 location) : base(block)
        {
            this.block.Sprite = new Sprite(new TextureDetails(this.block.game.Content.Load<Texture2D>("BrickBlockBlue"), 1), location);
        }
    }
    class FloorBlockBlue : Blocktype
    {
        public FloorBlockBlue(Block block, Vector2 location) : base(block)
        {
            this.block.Sprite = new Sprite(new TextureDetails(this.block.game.Content.Load<Texture2D>("FloorBlockBlue"), 1), location);
        }
    }
    class PipeTop : Blocktype
    {
        public PipeTop(Block block, Vector2 location) : base(block)
        {
            this.block.Sprite = new Sprite(new TextureDetails(this.block.game.Content.Load<Texture2D>("PipeTop"), 1), location);
        }
       
    }
    class PipeTeleportTop : Blocktype
    {
        public PipeTeleportTop(Block block, Vector2 location) : base(block)
        {
            this.block.Sprite = new Sprite(new TextureDetails(this.block.game.Content.Load<Texture2D>("PipeTop"), 1), location);
        }
    }
    class PipeTeleportTopBonus : Blocktype
    {
        public PipeTeleportTopBonus(Block block, Vector2 location) : base(block)
        {
            this.block.Sprite = new Sprite(new TextureDetails(this.block.game.Content.Load<Texture2D>("PipeTop"), 1), location);
        }
    }
    class PipeTeleportSide : Blocktype
    {
        public PipeTeleportSide(Block block, Vector2 location) : base(block)
        {
            this.block.Sprite = new Sprite(new TextureDetails(this.block.game.Content.Load<Texture2D>("PipeSideways"), 1), location);
        }
    }
    class PipeMiddle : Blocktype
    {
        public PipeMiddle(Block block, Vector2 location) : base(block)
        {
            this.block.Sprite = new Sprite(new TextureDetails(this.block.game.Content.Load<Texture2D>("PipeMiddle"), 1), location);
        }
    }
    class PipeSideways : Blocktype
    {
        public PipeSideways(Block block, Vector2 location) : base(block)
        {
            this.block.Sprite = new Sprite(new TextureDetails(this.block.game.Content.Load<Texture2D>("PipeSideways"), 1), location);
        }
    }
    class PipeMiddleSideways : Blocktype
    {
        public PipeMiddleSideways(Block block, Vector2 location) : base(block)
        {
            this.block.Sprite = new Sprite(new TextureDetails(this.block.game.Content.Load<Texture2D>("PipeMiddleSideways"), 1), location);
        }
    }
    class PipeRightSide : Blocktype
    {
        public PipeRightSide(Block block, Vector2 location) : base(block)
        {
            this.block.Sprite = new Sprite(new TextureDetails(this.block.game.Content.Load<Texture2D>("PipeRightside"), 1), location);
        }
    }
    class PipeRot : Blocktype
    {
        public PipeRot(Block block, Vector2 location) : base(block)
        {
            this.block.Sprite = new Sprite(new TextureDetails(this.block.game.Content.Load<Texture2D>("PipeRot"), 1), location);
        }
    }
    class Castle : Blocktype
    {
        public Castle(Block block, Vector2 location) : base(block)
        {
            this.block.Sprite = new Sprite(new TextureDetails(this.block.game.Content.Load<Texture2D>("Castle1"), 1), location);
        }
    }

    class BrokenBrickBlock : Blocktype
    {
        public bool broke = true;

        public Vector2 piece1;
        public Vector2 piece2;
        public Vector2 piece3;
        public Vector2 piece4;

        Point Velocity1;
        Point Velocity2;
        Point Velocity3;
        Point Velocity4;

        int Time = 0;
        int Delay = 200;
        int CurrentFrameNum = 0;

        Point FrameOrigin = new Point(0,0);
        Point CurrentFrame = new Point(0,0);
        Point SheetSize = new Point(1,2);
        Point FrameSize;
        Vector2 location;
        Texture2D texture;


        Random rnd = new Random();

        int gravity = 1;
        public BrokenBrickBlock(Block block, Vector2 location) : base(block)
        {
            this.location = location;
            piece1 = new Vector2(location.X + 7, location.Y + 7);
            piece2 = new Vector2(location.X + 7, location.Y - 7);
            piece3 = new Vector2(location.X - 7, location.Y + 7);
            piece4 = new Vector2(location.X - 7, location.Y - 7);

            Velocity1.X = rnd.Next(-20, 20);
            Velocity1.Y = rnd.Next(-20, 20);
            Velocity2.X = rnd.Next(-20, 20);
            Velocity2.Y = rnd.Next(-20, 20);
            Velocity3.X = rnd.Next(-20, 20);
            Velocity3.Y = rnd.Next(-20, 20);
            Velocity4.X = rnd.Next(-20, 20);
            Velocity4.Y = rnd.Next(-20, 20);

            texture = this.block.game.Content.Load<Texture2D>("Brokenbrick_block");
            FrameSize.X = texture.Width/SheetSize.X;
            FrameSize.Y = texture.Height/SheetSize.Y;
            this.block.boundingBoxSize = new Point();
            this.block.position = new Point();

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2(piece1.X, piece1.Y),
            new Rectangle(FrameOrigin.X + CurrentFrame.X * FrameSize.X, FrameOrigin.Y + CurrentFrame.Y * FrameSize.Y, FrameSize.X, FrameSize.Y),
            Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0);

            spriteBatch.Draw(texture, new Vector2(piece2.X, piece2.Y),
            new Rectangle(FrameOrigin.X + CurrentFrame.X * FrameSize.X, FrameOrigin.Y + CurrentFrame.Y * FrameSize.Y, FrameSize.X, FrameSize.Y),
            Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0);

            spriteBatch.Draw(texture, new Vector2(piece3.X, piece3.Y),
            new Rectangle(FrameOrigin.X + CurrentFrame.X * FrameSize.X, FrameOrigin.Y + CurrentFrame.Y * FrameSize.Y, FrameSize.X, FrameSize.Y),
            Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0);

            spriteBatch.Draw(texture, new Vector2(piece4.X, piece4.Y),
            new Rectangle(FrameOrigin.X + CurrentFrame.X * FrameSize.X, FrameOrigin.Y + CurrentFrame.Y * FrameSize.Y, FrameSize.X, FrameSize.Y),
            Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0);
        }

        public override void Update(GameTime gameTime)
        {
            Time += gameTime.ElapsedGameTime.Milliseconds;
            if(broke == true)
            {
                Time += gameTime.ElapsedGameTime.Milliseconds;
                if (Time > Delay)
                {
                    Time -= Delay;
                    CurrentFrameNum += 1;
                    CurrentFrame.X += 1;

                    if (CurrentFrame.X >= SheetSize.X)
                    {
                        CurrentFrame.X = 0;
                        CurrentFrame.Y += 1;
                        if (CurrentFrame.Y >= SheetSize.Y)
                        {
                            CurrentFrame.Y = 0;
                            CurrentFrameNum = 0;
                        }
                    }
                }

                Velocity();
            }
            else
            {
                Time -= Delay;
            }
        }

        public virtual void Velocity()
        {
            piece1.X += Velocity1.X;
            Velocity1.Y += gravity;
            piece1.Y += Velocity1.Y;

            piece2.X += Velocity2.X;
            Velocity2.Y += gravity;
            piece2.Y += Velocity2.Y;

            piece3.X += Velocity3.X;
            Velocity3.Y += gravity;
            piece3.Y += Velocity3.Y;

            piece4.X += Velocity4.X;
            Velocity4.Y += gravity;
            piece4.Y += Velocity4.Y;
        }
    }
    
}
