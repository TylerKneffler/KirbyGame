using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KirbyGame
{
    class Pole : Item/*, IPointable*/
    {
        public Pole(Sprite sprite, Game1 game) : base(sprite, game)
        {

        }

        public override void HandleCollision(Collision collision, Entity collider)
        {
            if(collider is Avatar)
            {
                if (!(((Avatar)collider).actionState is MarioTransitioningState)) ;
                    //((Avatar)collider).actionState.TransitioningTransition(new FlagpoleTransition((Avatar)collider, (int)this.BoundingBox.Right));
            }
        }

        public int Points()
        {
            int height = this.game.mario.Y;
            int floorPos = 11*TileMap.CELL_SIZE;
            int mult = 2;
            int points = 100;
            if (height < floorPos - 128 * mult)
                points = 4000;
            else if (height < floorPos - 82 * mult)
                points = 2000;
            else if (height < floorPos - 58 * mult)
                points = 800;
            else if (height < floorPos - 18 * mult)
                points = 400;
            return points;
        }
    }
}
