using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace KirbyGame
{
    class AvatarSpriteFactory : SpriteFactory
    {
        private Dictionary<int, TextureDetails> textureList;
        public AvatarSpriteFactory(Game1 game) : base(game)
        {
            textureList = new Dictionary<int, TextureDetails>();
        }

        public Sprite createSprite(int type, Vector2 location, int numFrames)
        {
            if (!textureList.ContainsKey(type))

            Sprite sprite = new Sprite()
            return sprite;
        }
    }
}
