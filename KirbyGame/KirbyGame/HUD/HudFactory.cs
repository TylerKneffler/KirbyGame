using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KirbyGame
{
    class HudFactory
    {
        private Game1 game;
        private Dictionary<KirbyHud.hudType, TextureDetails> textureList;

        public HudFactory(Game1 game)
        {
            this.game = game;
            textureList = new Dictionary<KirbyHud.hudType, TextureDetails>();
        }

        public Sprite createItem(KirbyHud.hudType type, Vector2 location)
        {
            Sprite ret = null;
            if (!textureList.ContainsKey(type))
                loadTexture(type);

            ret = new Sprite(textureList[type], location);

            return ret;
        }

        public void loadTexture(KirbyHud.hudType type)
        {
            switch (type)
            {
                case KirbyHud.hudType.POWER_BEAM:
                    {
                        Texture2D beamPower = game.Content.Load<Texture2D>("HUD/Beam_power");
                        textureList.Add(type, new TextureDetails(beamPower, 1));
                        break;
                    }
                case KirbyHud.hudType.POWER_CUTTER:
                    {
                        Texture2D cutterPower = game.Content.Load<Texture2D>("HUD/Cutter_power");
                        textureList.Add(type, new TextureDetails(cutterPower, 1));
                        break;
                    }

                case KirbyHud.hudType.POWER_NORMAL:
                    {
                        Texture2D normalPower = game.Content.Load<Texture2D>("HUD/Normal_power");
                        textureList.Add(type, new TextureDetails(normalPower, 1));
                        break;
                    }
                case KirbyHud.hudType.POWER_NOTHING:
                    {
                        Texture2D nothingPower = game.Content.Load<Texture2D>("HUD/Nothing_power");
                        textureList.Add(type, new TextureDetails(nothingPower, 1));
                        break;
                    }
                case KirbyHud.hudType.HP_COLOR:
                    {
                        Texture2D hpColor = game.Content.Load<Texture2D>("HUD/HP_color");
                        textureList.Add(type, new TextureDetails(hpColor, 1));
                        break;
                    }
                case KirbyHud.hudType.HP_WHITE:
                    {
                        Texture2D hpWhite = game.Content.Load<Texture2D>("HUD/HP_white");
                        textureList.Add(type, new TextureDetails(hpWhite, 1));
                        break;
                    }
                case KirbyHud.hudType.HP_EMPTY:
                    {
                        Texture2D hpEmpty = game.Content.Load<Texture2D>("HUD/HP_empty");
                        textureList.Add(type, new TextureDetails(hpEmpty, 1));
                        break;
                    }
                case KirbyHud.hudType.NAME_KIRBY:
                    {
                        Texture2D kirbyName = game.Content.Load<Texture2D>("HUD/Kirby_hp_name");
                        textureList.Add(type, new TextureDetails(kirbyName, 1));
                        break;
                    }
                case KirbyHud.hudType.NAME_SCORE:
                    {
                        Texture2D scoreName = game.Content.Load<Texture2D>("HUD/Kirby_score_name");
                        textureList.Add(type, new TextureDetails(scoreName, 1));
                        break;
                    }
                case KirbyHud.hudType.NAME_USES:
                    {
                        Texture2D usesName = game.Content.Load<Texture2D>("HUD/Uses_left");
                        textureList.Add(type, new TextureDetails(usesName, 2));
                        break;
                    }
                case KirbyHud.hudType.LIVES:
                    {
                        Texture2D lives = game.Content.Load<Texture2D>("HUD/Kirby_lives");
                        textureList.Add(type, new TextureDetails(lives, 3));
                        break;
                    }
            }

        }
        public SpriteFont loadFont()
        {
            return game.Content.Load<SpriteFont>("Kirby_font");
        }
    }
}
