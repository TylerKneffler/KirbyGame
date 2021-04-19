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
            AIR_SPITTING, FULL_SPITTING,
            FULL_IDLE, FULL_RUNNING, FULL_FLOATING, FULL_FLYING, FULL_EMPTYING, FULL_JUMPING, FULL_FALLING
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
                        details.AddFrame(new Rectangle(127, 75, 24, 24));
                        textureList.Add(type, details);
                        break;
                    }
                case spriteType.FULL_FLYING:
                    {
                        TextureDetails details = new TextureDetails(game.Content.Load<Texture2D>("avatar"), new Rectangle(99, 75, 24, 24), 1);
                        details.AddFrame(new Rectangle(127, 75, 24, 24));
                        details.Delay = details.Delay / 2;
                        textureList.Add(type, details);
                        break;
                    }
                case spriteType.FULL_JUMPING:
                    {
                        TextureDetails details = new TextureDetails(game.Content.Load<Texture2D>("avatar"), new Rectangle(96, 107, 24, 24), 1);
                        details.AddFrame(new Rectangle(124, 107, 24, 24));
                        details.AddFrame(new Rectangle(152, 107, 24, 24));
                        textureList.Add(type, details);
                        break;
                    }
                case spriteType.FULL_FALLING:
                    {
                        TextureDetails details = new TextureDetails(game.Content.Load<Texture2D>("avatar"), new Rectangle(96, 107, 24, 24), 1);
                        textureList.Add(type, details);
                        break;
                    }
                case spriteType.FULL_IDLE:
                    {
                        TextureDetails details = new TextureDetails(game.Content.Load<Texture2D>("avatar"), new Rectangle(63, 107, 24, 24), 1);
                        textureList.Add(type, details);
                        break;
                    }
                case spriteType.EMPTY_SUCKING_POWER:
                    {
                        TextureDetails details = new TextureDetails(game.Content.Load<Texture2D>("avatar"), new Rectangle(26, 107, 16, 24), 1);
                        textureList.Add(type, details);
                        break;
                    }
                case spriteType.AIR_SPITTING:
                    {
                        TextureDetails details = new TextureDetails(game.Content.Load<Texture2D>("avatar"), new Rectangle(160, 75, 24, 24), 1);
                        details.AddFrame(new Rectangle(188, 75, 16, 24));
                        textureList.Add(type, details);
                        break;
                    }
                case spriteType.FULL_SPITTING:
                    {
                        TextureDetails details = new TextureDetails(game.Content.Load<Texture2D>("avatar"), new Rectangle(185, 107, 24, 24), 1);
                        details.AddFrame(new Rectangle(213, 107, 24, 24));
                        details.AddFrame(new Rectangle(241, 107, 16, 24));
                        textureList.Add(type, details);
                        break;
                    }

                default: break;
            }
        }

        public static spriteType generateSpriteEnum(ActionState action, SwallowState swallowed)
        {
            spriteType ret = 0;
            if (swallowed is EmptySwallowState)
            {
                if (action is EmptyIdleState)
                {
                    ret = spriteType.EMPTY_IDLE;
                } else if (action is EmptyRunningState)
                {
                    ret = spriteType.EMPTY_RUNNING;
                } else if (action is EmptyJumpingState)
                {
                    ret = spriteType.EMPTY_JUMPING;
                }
                else if (action is EmptyFallingState)
                {
                    ret = spriteType.EMPTY_FALLING;
                }
                else if (action is EmptyFlippingState)
                {
                    ret = spriteType.EMPTY_FLIPPING;
                }
                else if (action is EmptyFlyingState)
                {
                    ret = spriteType.EMPTY_FLYING;
                } else if(action is EmptySuckingState)
                {
                    ret = spriteType.EMPTY_SUCKING_POWER;
                }
            } else if (swallowed is AirSwallowState)
            {
                if (action is AirFloatingState)
                {
                    ret = spriteType.FULL_FLOATING;
                } else if (action is AirFlyingState)
                {
                    ret = spriteType.FULL_FLYING;
                } else if(action is AirExpellingState)
                {
                    ret = spriteType.AIR_SPITTING;
                }
            } else if(swallowed is FullSwallowState)
            {
                if(action is FullIdleState)
                {
                    ret = spriteType.FULL_IDLE;
                } else if(action is FullRunningState)
                {
                    //Running and Jumping Sprites are the same
                    ret = spriteType.FULL_JUMPING;
                } else if(action is FullJumpingState)
                {
                    ret = spriteType.FULL_JUMPING;
                }
                else if (action is FullFallingState)
                {
                    ret = spriteType.FULL_FALLING;
                }
            }
            return ret;
        }
    }
}
