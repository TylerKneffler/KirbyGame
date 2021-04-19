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
        private int livesLeft;
        private Score score;

        public KirbyHud(Camera camera, Texture2D texture, Vector2 location, Viewport viewport, Game1 game) : base(camera,texture,location,viewport)
        {
            factory = new HudFactory(game);
            font = factory.loadFont();
            livesLeft = 6;
            Initzialize();
        }

        public KirbyHud(Camera camera, Vector2 location, Viewport viewport, Game1 game) : base(camera, location, viewport)
        {
            factory = new HudFactory(game);
            font = factory.loadFont();
            livesLeft = 6;
            Initzialize();
        }

        public void Initzialize()
        {
            AddSprite(factory.createItem(hudType.NAME_KIRBY, new Vector2(48, 305)));
            AddSprite(factory.createItem(hudType.NAME_SCORE, new Vector2(48, 342)));
            AddSprite(factory.createItem(hudType.NAME_USES, new Vector2(385, 311)));
            AddSprite(factory.createItem(hudType.LIVES, new Vector2(385, 335)));

            for (int i = 0; i < livesLeft; i++)
            {
                AddSprite(factory.createItem(hudType.HP_COLOR, new Vector2(156 + 16* i, 300)));
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "X" + score.GetScore().ToString(), new Vector2(156, 342), Color.White);
        }

        public void DrawLives()
        {
            for (int i =0; i<livesLeft; i++)
            {
                AddSprite(factory.createItem(hudType.NAME_KIRBY, new Vector2(140+6*i, 305)));
            }
            for (int i = 0; i < livesLeft; i++)
            {
                AddSprite(factory.createItem(hudType.NAME_KIRBY, new Vector2(140 + 6 * i, 305)));
            }

        }

        public void LostHealth()
        {
            livesLeft--;
            RemoveSprite(factory.createItem(hudType.HP_COLOR, new Vector2(140 + 6 * livesLeft, 305)));
            AddSprite(factory.createItem(hudType.HP_EMPTY, new Vector2(140 + 6 * livesLeft, 305)));
        }

    }
}