using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace KirbyGame
{
    public abstract class EnemytypeTest : IPointable
    {

        protected EnemyTest enemy;
        protected int points;
        


        public EnemytypeTest(EnemyTest enemy)
        {
            this.enemy = enemy;
            this.points = 100;
        }

        public virtual void Update(GameTime gameTime)
        {
        }
        public virtual void Draw(SpriteBatch spriteBatch) 
        {
        }

        public virtual void HandleCollision(Collision collision, Entity collider)
        {
        }

        public int Points()
        {
            return points;
        }

    }
}
