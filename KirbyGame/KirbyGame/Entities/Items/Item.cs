using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace KirbyGame
{
    public abstract class Item : Entity
    {
        public Boolean isUsed;
        public Boolean justSpawned;
        
        public enum eItemType
        {
            ONE_UP_MUSHROOM,
            COIN,
            FIRE_FLOWER,
            STAR,
            SUPER_MUSHROOM,
            FLAG,
            POLE,
            POLETOP

        }
        public Item(Sprite sprite, Game1 game) : base(sprite)
        {
            boundingBoxSize.X = (int)(boundingBoxSize.X *1);
            boundingBoxSize.Y = (int)(boundingBoxSize.Y *1);
            isUsed = false;
            this.game = game;
            defaultColor = Color.Chartreuse;
            boundingColor = defaultColor; ;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!isUsed)
            {
                base.Draw(spriteBatch);
            }

        }

        public override void Update(GameTime gameTime)
        {
            if (!isUsed)
                base.Update(gameTime);
        }

        public override void HandleCollision(Collision collision, Entity collider)
        {
            
        }
    }
}
