using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace KirbyGame
{
    class MarioTexturesFactory
    {
        private Dictionary<int, TextureDetails> textureList;
        private Game1 game;

        public MarioTexturesFactory(Avatar mario)
        {
            this.game = mario.game;
            textureList = new Dictionary<int, TextureDetails>();
        }

        public TextureDetails createTextureDetails(int type)
        {
            if (!textureList.ContainsKey(type))
                loadTexture(type);
            return textureList[type];
        }

        private void loadTexture(int type)
        {
            Avatar.marioState currentState = (Avatar.marioState)type;
            switch (currentState)
            {
                case Avatar.marioState.SMALL_IDLE:
                    {
                        TextureDetails details = new TextureDetails(game.Content.Load<Texture2D>("avatar"), new Rectangle(6, 24, 16, 16), 1);
                        details.AddFrame(new Rectangle(26, 24, 16, 16));
                        textureList.Add(type, details);

                        break;
                    }
                case Avatar.marioState.SUPER_IDLE:
                    {
                        textureList.Add(type, new TextureDetails(game.Content.Load<Texture2D>("mario/marioIdleSuper"), 1));
                        break;
                    }
                case Avatar.marioState.FIRE_IDLE:
                    {
                        textureList.Add(type, new TextureDetails(game.Content.Load<Texture2D>("mario/marioIdleFire"), 1));
                        break;
                    }
                case Avatar.marioState.SMALL_RUNNING:
                    {
                        textureList.Add(type, new TextureDetails(game.Content.Load<Texture2D>("mario/marioRunningSmall"), 3));
                        break;
                    }
                case Avatar.marioState.SUPER_RUNNING:
                    {
                        textureList.Add(type, new TextureDetails(game.Content.Load<Texture2D>("mario/marioRunningSuper"), 3));
                        break;
                    }
                case Avatar.marioState.FIRE_RUNNING:
                    {
                        textureList.Add(type, new TextureDetails(game.Content.Load<Texture2D>("mario/marioRunningFire"), 3));
                        break;
                    }
                case Avatar.marioState.SMALL_JUMPING:
                    {
                        textureList.Add(type, new TextureDetails(game.Content.Load<Texture2D>("mario/marioJumpingSmall"), 1));
                        break;
                    }
                case Avatar.marioState.SUPER_JUMPING:
                    {
                        textureList.Add(type, new TextureDetails(game.Content.Load<Texture2D>("mario/marioJumpingSuper"), 1));
                        break;
                    }
                case Avatar.marioState.FIRE_JUMPING:
                    {
                        textureList.Add(type, new TextureDetails(game.Content.Load<Texture2D>("mario/marioJumpingFire"), 1));
                        break;
                    }
                case Avatar.marioState.SMALL_FALLING:
                    {
                        textureList.Add(type, new TextureDetails(game.Content.Load<Texture2D>("mario/marioJumpingSmall"), 1));
                        break;
                    }
                case Avatar.marioState.SUPER_FALLING:
                    {
                        textureList.Add(type, new TextureDetails(game.Content.Load<Texture2D>("mario/marioJumpingSuper"), 1));
                        break;
                    }
                case Avatar.marioState.FIRE_FALLING:
                    {
                        textureList.Add(type, new TextureDetails(game.Content.Load<Texture2D>("mario/marioJumpingFire"), 1));
                        break;
                    }
                case Avatar.marioState.SMALL_CROUCHING:
                    {
                        textureList.Add(type, new TextureDetails(game.Content.Load<Texture2D>("mario/marioIdleSmall"), 1));
                        break;
                    }
                case Avatar.marioState.SUPER_CROUCHING:
                    {
                        textureList.Add(type, new TextureDetails(game.Content.Load<Texture2D>("mario/marioCrouchingSuper"), 1));
                        break;
                    }
                case Avatar.marioState.FIRE_CROUCHING:
                    {
                        textureList.Add(type, new TextureDetails(game.Content.Load<Texture2D>("mario/marioCrouchingFire"), 1));
                        break;
                    }
                case Avatar.marioState.DEAD:
                    {
                        textureList.Add(type, new TextureDetails(game.Content.Load<Texture2D>("mario/marioDead"), 1));
                        break;
                    }
                default: break;
            }
        }
    }
}
