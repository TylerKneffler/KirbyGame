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
        private KirbyTexturesFactory textureFactory;
        private Avatar kirby;
        public KirbySpriteFactory(Avatar avatar)
        {
            textureFactory = new KirbyTexturesFactory(avatar);
            this.kirby = avatar;
        }
      
        public Sprite createSprite(SwallowState swallowed, Vector2 location, Sprite.eDirection direction)
        {
            TextureDetails details = textureFactory.createTextureDetails(swallowed.actionState, swallowed);
            details.AllFrames();
            Sprite sprite = new Sprite(details, location, direction);
            if (swallowed is FullSwallowState)
            {
                sprite.tint = Color.Pink;
            }
            return sprite;
        }

        /*public Sprite createSprite(Vector2 location)
        {
            return createSprite((int)KirbyTexturesFactory.spriteType.EMPTY_IDLE, location, Sprite.eDirection.Right);
        }*/
      


    }
}
