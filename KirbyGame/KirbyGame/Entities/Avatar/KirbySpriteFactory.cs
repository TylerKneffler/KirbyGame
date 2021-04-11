using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace KirbyGame
{
    class KirbySpriteFactory
    {
        private KirbyTextureFactory textureFactory;
        private Avatar kirby;
        public KirbySpriteFactory(Avatar avatar)
        {
            textureFactory = new KirbyTextureFactory(avatar);
            this.kirby = avatar;
        }

        /*public Sprite createSprite(int type, Vector2 location, Sprite.eDirection direction)
        {
            return new Sprite(textureFactory.createTextureDetails(type), location, direction);
        }*/

        /*public Sprite createSprite(Vector2 location)
        {
            return createSprite((int)Avatar.marioState.SMALL_IDLE, location, Sprite.eDirection.Right);
        }*/
    }
}
