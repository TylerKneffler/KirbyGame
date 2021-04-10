 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace KirbyGame
{

    class BlockImg
    {
        public Texture2D FloorBlock;
        public Texture2D QuestionBlock;
        public Texture2D UsedBlock;
        public Texture2D StairBlock;
        public Texture2D HiddenBlock;
        public Texture2D BrickBlock;
        public Texture2D BrokenBrickpiece;
        public Texture2D BrokenBrickBlock;

        public void load(Game game)
        {

            Texture2D floorBlock = game.Content.Load<Texture2D>("Floor_Block");
            Texture2D questionBlock = game.Content.Load<Texture2D>("Question_Block");
            Texture2D usedBlock = game.Content.Load<Texture2D>("UsedQuestion_Block");
            Texture2D stairBlock = game.Content.Load<Texture2D>("Pyramid_Block");
            Texture2D hiddenBlock = game.Content.Load<Texture2D>("Hidden_Block");
            Texture2D brickBlock = game.Content.Load<Texture2D>("Brick_Block");
            Texture2D brokenBrickpiece = game.Content.Load<Texture2D>("Brokenbrick_Block");
            Texture2D brokenBrickBlock = game.Content.Load<Texture2D>("Broken Block");

            FloorBlock = floorBlock;
            QuestionBlock = questionBlock;
            UsedBlock = usedBlock;
            StairBlock = stairBlock;
            HiddenBlock = hiddenBlock;
            BrickBlock = brickBlock;
            BrokenBrickpiece = brokenBrickpiece;
            BrokenBrickBlock = brokenBrickBlock;
        }

    }
}
