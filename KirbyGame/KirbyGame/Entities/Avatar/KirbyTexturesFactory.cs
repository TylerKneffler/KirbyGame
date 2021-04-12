using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace KirbyGame
{
    class KirbyTexturesFactory
    {
        private Dictionary<int, TextureDetails> textureList;
        private Game1 game;

        public enum spriteType
        {
            EMPTY_IDLE, EMPTY_RUNNING, EMPTY_JUMPING, EMPTY_FLIPPING, EMPTY_FALLING, EMPTY_FLYING, EMPTY_SUCKING_POWER,
            FULL_IDLE, FULL_RUNNING, FULL_FLOATING, FULL_FLYING, FULL_EMPTYING
        }

        public KirbyTexturesFactory(Avatar kirby)
        {
            this.game = kirby.game;
            textureList = new Dictionary<int, TextureDetails>();
        }

        public TextureDetails createTextureDetails(ActionState action, SwallowState swallowed)
        {
            int type = (int)generateSpriteEnum(action, swallowed);
            if (!textureList.ContainsKey(type))
                loadTexture(type);
            return textureList[type];
        }

        private void loadTexture(int type)
        {
            spriteType currentState = (spriteType)type;
            switch (currentState)
            {
                case spriteType.EMPTY_IDLE:
                    {
                        TextureDetails details = new TextureDetails(game.Content.Load<Texture2D>("avatar"), new Rectangle(6, 24, 16, 16), 1);
                        details.AddFrame(new Rectangle(26, 24, 16, 16));
                        textureList.Add(type, details);
                        break;
                    }
                case spriteType.EMPTY_RUNNING:
                    {
                        TextureDetails details = new TextureDetails(game.Content.Load<Texture2D>("avatar"), new Rectangle(6, 24, 16, 16), 1);
                        details.AddFrame(new Rectangle(71, 24, 16, 16));
                        details.AddFrame(new Rectangle(91, 24, 16, 16));
                        details.AddFrame(new Rectangle(111, 24, 16, 16));
                        textureList.Add(type, details);
                        break;
                    }
                case spriteType.EMPTY_JUMPING:
                    {
                        TextureDetails details = new TextureDetails(game.Content.Load<Texture2D>("avatar"), new Rectangle(6, 49, 16, 16), 1);
                        textureList.Add(type, details);
                        break;
                    }
                case spriteType.EMPTY_FALLING:
                    {
                        TextureDetails details = new TextureDetails(game.Content.Load<Texture2D>("avatar"), new Rectangle(86, 49, 16, 16), 1);
                        textureList.Add(type, details);
                        break;
                    }
                case spriteType.EMPTY_FLIPPING:
                    {
                        TextureDetails details = new TextureDetails(game.Content.Load<Texture2D>("avatar"), new Rectangle(26, 49, 16, 16), 1);
                        details.AddFrame(new Rectangle(46, 49, 16, 16));
                        details.AddFrame(new Rectangle(66, 49, 16, 16));
                        textureList.Add(type, details);
                        break;
                    }
                case spriteType.EMPTY_FLYING:
                    {
                        TextureDetails details = new TextureDetails(game.Content.Load<Texture2D>("avatar"), new Rectangle(6, 83, 16, 16), 1);
                        details.AddFrame(new Rectangle(26, 83, 16, 16));
                        details.AddFrame(new Rectangle(46, 75, 16, 24));
                        details.AddFrame(new Rectangle(66, 75, 24, 24));
                        textureList.Add(type, details);
                        break;
                    }
                case spriteType.FULL_FLOATING:
                    {
                        TextureDetails details = new TextureDetails(game.Content.Load<Texture2D>("avatar"), new Rectangle(99, 75, 24, 24), 1);
                        details.AddFrame(new Rectangle(26, 83, 16, 16));
                        textureList.Add(type, details);
                        break;
                    }
                case spriteType.FULL_FLYING:
                    {
                        TextureDetails details = new TextureDetails(game.Content.Load<Texture2D>("avatar"), new Rectangle(99, 75, 24, 24), 1);
                        details.AddFrame(new Rectangle(26, 83, 16, 16));
                        details.Delay = details.Delay / 2;
                        textureList.Add(type, details);
                        break;
                    }

                default: break;
            }
        }

        public static spriteType generateSpriteEnum(ActionState action, SwallowState swallowed)
        {
            spriteType ret = 0;
            if(swallowed is EmptySwallowState)
            {
                if (action is IdleState)
                {
                    ret = spriteType.EMPTY_IDLE;
                } else if (action is RunningState)
                {
                    ret = spriteType.EMPTY_RUNNING;
                } else if (action is JumpingState)
                {
                    ret = spriteType.EMPTY_JUMPING;
                }
                else if (action is FallingState)
                {
                    ret = spriteType.EMPTY_FALLING;
                }
                else if (action is FlippingState)
                {
                    ret = spriteType.EMPTY_FLIPPING;
                }
                else if (action is FlyingState)
                {
                    ret = spriteType.EMPTY_FLYING;
                }
            }
            else if(swallowed is AirSwallowState)
            {
                if (action is IdleState)
                {
                    ret = spriteType.FULL_FLOATING;
                }
                else if (action is RunningState)
                {
                    ret = spriteType.FULL_FLOATING;
                }
                else if (action is JumpingState)
                {
                    ret = spriteType.FULL_FLYING;
                }
                else if (action is FlyingState)
                {
                    ret = spriteType.FULL_FLYING;
                }
            }
            return ret;
        }
    }
}
