using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace KirbyGame
{
    class EnemyFactoryTest
    {
        private Game1 game;

        public EnemyFactoryTest(Game1 game)
        {
            this.game = game;

        }

        public EnemyTest createEnemy(EnemyTest.enemytypes type, Vector2 location)
        {
            return new EnemyTest(type, location, game);
        }
    }
}
