using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KirbyGame
{
    class EnemyFactory
    {
        private Dictionary<Enemy.eEnemyType, TextureDetails> textureList;
        private Game1 game;
        
        public EnemyFactory(Game1 game)
        {
            this.game = game;
            textureList = new Dictionary<Enemy.eEnemyType, TextureDetails>();
        }

        public Enemy createEnemy(Enemy.eEnemyType type, Vector2 location)
        {
            Enemy ret = null;
            if (!textureList.ContainsKey(type))
                loadTexture(type);
            if(type == Enemy.eEnemyType.GOOMBA)
            {
                ret = new Goomba(new Sprite(textureList[type], location), game);
            } else if(type == Enemy.eEnemyType.KOOPA)
            {
                ret = new Koopa(new Sprite(textureList[type], location), game);
            } 
            return ret;
        }

        private void loadTexture(Enemy.eEnemyType type)
        {
            switch (type)
            {
                case Enemy.eEnemyType.GOOMBA:
                    {
                        Texture2D goomba = game.Content.Load<Texture2D>("goomba");
                        textureList.Add(type, new TextureDetails(goomba, new Rectangle(new Point(0, 0), new Point(32, 16)), 2));
                        break;
                    }
                case Enemy.eEnemyType.KOOPA:
                    {
                        Texture2D koopa = game.Content.Load<Texture2D>("koopa");
                        textureList.Add(type, new TextureDetails(koopa, new Rectangle(new Point(30, 0), new Point(58, 24)), 2));
                        break;
                    }
            }
        }


    }
}
