using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace KirbyGame
{
    public class KirbyHud : Layer
    {
        public enum hudType
        {
            POWER_BEAM,
            POWER_CUTTER,
            POWER_NORMAL,
            POWER_NOTHING,
            HP_COLOR,
            HP_WHITE,
            HP_EMPTY,
            NAME_KIRBY,
            NAME_SCORE,
            NAME_USES,
            LIVES,
        }

        private HudFactory factory;
        readonly SpriteFont font;

        public KirbyHud(Camera camera, Vector2 location, Viewport viewport, Game1 game) : base(camera, location, viewport)
        {
            factory = new HudFactory(game);
            font = factory.loadFont();
            Initialize();
        }

        public void Initialize()
        {
            AddSprite(factory.createItem(hudType.NAME_KIRBY, new Vector2(48, 305)));
            AddSprite(factory.createItem(hudType.NAME_SCORE, new Vector2(48, 342)));
            AddSprite(factory.createItem(hudType.NAME_USES, new Vector2(385, 311)));
            AddSprite(factory.createItem(hudType.LIVES, new Vector2(385, 335)));

            for (int i = 0; i < 6; i++)
            {
                AddSprite(factory.createItem(hudType.HP_EMPTY, new Vector2(156 + 16 * i, 300)));
                AddSprite(factory.createItem(hudType.HP_COLOR, new Vector2(156 + 16 * i, 300)));
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

        }

        public override void Draw(SpriteBatch spriteBatch, Stats stats)
        {

            spriteBatch.Begin(/*samplerState: SamplerState.LinearWrap,*/transformMatrix: GetCamera().GetViewMatrix(Parallax));
            foreach (Sprite sprite in GetSpriteList())
            {
                if (sprite.zDepth != 0)
                {
                    int hp = stats.GetHealth();
                    for(int i = 6; i > hp; i--)
                    {
                        if(sprite.X == (156 + 16 * (i-1)))
                        {
                            sprite.SetVisibility(false);
                        }
                    }
                }

                sprite.DrawVisable(spriteBatch);
            }

            spriteBatch.DrawString(font, stats.GetScore().ToString(), new Vector2(156, 342), Color.Black);
            spriteBatch.DrawString(font, stats.GetLives().ToString(), new Vector2(415, 342), Color.Black);
            spriteBatch.DrawString(font, stats.GetHealth().ToString(), new Vector2(415, 320), Color.Black);
            spriteBatch.End();

            
        }
    }
}