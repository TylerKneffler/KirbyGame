using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KirbyGame
{
    class ItemFactory
    {
        private Game1 game;
        private Dictionary<Item.eItemType, TextureDetails> textureList;

        public ItemFactory(Game1 game)
        {
            this.game = game;
            textureList = new Dictionary<Item.eItemType, TextureDetails>();
        }

        public Item createItem(Item.eItemType type, Vector2 location)
        {
            Item ret = null;
            if (!textureList.ContainsKey(type))
                loadTexture(type);
            if (type == Item.eItemType.ONE_UP_MUSHROOM)
            {
                ret = new OneUpMushroom(new Sprite(textureList[type], location), game);
            }
            else if (type == Item.eItemType.SUPER_MUSHROOM)
            {
                ret = new SuperMushroom(new Sprite(textureList[type], location), game);
            }
            else if (type == Item.eItemType.COIN)
            {
                ret = new Coin(new Sprite(textureList[type], location), game);
            }
            else if (type == Item.eItemType.FIRE_FLOWER)
            {
                ret = new FireFlower(new Sprite(textureList[type], location), game);
            }
            else if (type == Item.eItemType.STAR)
            {
                ret = new MarioStar(new Sprite(textureList[type], location), game);
            }
            else if (type == Item.eItemType.POLE)
            {
                ret = new Pole(new Sprite(textureList[type], location), game);
            }
            else if (type == Item.eItemType.POLETOP)
            {
                ret = new PoleTop(new Sprite(textureList[type], location), game);
            }
            else if (type == Item.eItemType.FLAG)
            {
                ret = new Flag(new Sprite(textureList[type], location), game);
            }
            return ret;
        }

        public void loadTexture(Item.eItemType type)
        {
            switch (type)
            {
                case Item.eItemType.ONE_UP_MUSHROOM:
                    {
                        Texture2D oneUpMushroom = game.Content.Load<Texture2D>("1UpMushroom");
                        textureList.Add(type, new TextureDetails(oneUpMushroom, 1));
                        break;
                    }

                case Item.eItemType.SUPER_MUSHROOM:
                    {
                        Texture2D superMushroom = game.Content.Load<Texture2D>("SuperMushroom");
                        textureList.Add(type, new TextureDetails(superMushroom, 1));
                        break;
                    }
                case Item.eItemType.COIN:
                    {
                        Texture2D coin = game.Content.Load<Texture2D>("Coin");
                        textureList.Add(type, new TextureDetails(coin, 4));
                        break;
                    }
                case Item.eItemType.FIRE_FLOWER:
                    {
                        Texture2D fireFlower = game.Content.Load<Texture2D>("FireFlower");
                        textureList.Add(type, new TextureDetails(fireFlower, 4));
                        break;
                    }
                case Item.eItemType.STAR:
                    {
                        Texture2D Star = game.Content.Load<Texture2D>("Star");
                        textureList.Add(type, new TextureDetails(Star, 4));
                        break;
                    }
                case Item.eItemType.POLE:
                    {
                        Texture2D Pole = game.Content.Load<Texture2D>("Flagpole");
                        textureList.Add(type, new TextureDetails(Pole, 1));
                        break;
                    }
                case Item.eItemType.FLAG:
                    {
                        Texture2D Flag = game.Content.Load<Texture2D>("Flag");
                        textureList.Add(type, new TextureDetails(Flag, 1));
                        break;
                    }
                case Item.eItemType.POLETOP:
                    {
                        Texture2D PoleTop = game.Content.Load<Texture2D>("Flagtop");
                        textureList.Add(type, new TextureDetails(PoleTop, 1));
                        break;
                    }
            }
            
        }
    }
}
