using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KirbyGame
{
    class KirbyTextureFactory
    {
        private Dictionary<int, TextureDetails> textureList;
        private Game1 game;

        public KirbyTextureFactory(Avatar kirby)
        {
            this.game = kirby.game;
            textureList = new Dictionary<int, TextureDetails>();
        }

        /*public TextureDetails createTextureDetails(int type)
        {
            if (!textureList.ContainsKey(type))
                //loadTexture(type);
            return textureList[type];
        }*/


    }
}
