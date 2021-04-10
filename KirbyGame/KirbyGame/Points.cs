using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace KirbyGame
{

    public class Points
    {
        public Hud hud;
        private Boolean flagPointsCollected;

        public Points(Hud hud)
        {
            this.hud = hud;
            flagPointsCollected = false;
        }

        public void OnAddPoints(object source, Collision collision)
        {
            //Avatar + Item Collisions
            if ((collision.A is Avatar || collision.B is Avatar) && (collision.A is Item || collision.B is Item))
            {
                if(collision.A is SuperMushroom || collision.B is SuperMushroom)
                {
                    hud.pointTotal += 1000;
                }
                else if(collision.A is FireFlower || collision.B is FireFlower)
                {
                    hud.pointTotal += 1000;
                }
                else if(collision.A is Coin || collision.B is Coin)
                {
                    hud.coinTotal++;
                    if(hud.coinTotal > 99)
                    {
                        if (hud.NumberOfLives < 99)
                        {
                            hud.NumberOfLives++;
                            hud.testLives.Add();
                        }
                        hud.coinTotal = 1;
                    }
                }
                else if(collision.A is OneUpMushroom || collision.B is OneUpMushroom)
                {
                    if (hud.NumberOfLives < 99)
                    {
                        hud.NumberOfLives++;
                        hud.testLives.Add();
                    }
                } else if((collision.B is Pole || collision.A is Pole || collision.A is Flag || collision.B is Flag)&& !flagPointsCollected)
                {
                    int height = collision.A.Y;
                    int floorPos = 11 * TileMap.CELL_SIZE;
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
                    hud.pointTotal += points;
                    flagPointsCollected = true;
                } else if((collision.A is PoleTop || collision.B is PoleTop)&& !flagPointsCollected)
                {
                    flagPointsCollected = true;
                    hud.NumberOfLives++;
                    hud.testLives.Add();
                }
            }
            //Avatar + Enemy Collisions
            else if (collision.A is Avatar && collision.B is EnemyTest)
            {
                Collision.Direction normalizedDirection = Collision.normalizeDirection(collision, collision.B);
                //Mario lands on Goomba's head
                if (((EnemyTest)collision.B).enemytype is SquishGoombaTest)
                {
                    hud.pointTotal += 100;
                }

            }
            else if (collision.A is EnemyTest && collision.B is Avatar)
            {
                Collision.Direction normalizedDirection = Collision.normalizeDirection(collision, collision.A);
                if (((EnemyTest)collision.A).enemytype is SquishGoombaTest)
                {
                    hud.pointTotal += 100;
                }
            }
            //Fireball + Enemy Collisions
            else if ((collision.A is Fireball || collision.B is Fireball) && (collision.A is EnemyTest || collision.B is EnemyTest))
            {
                hud.pointTotal += 100;
            }
            //Shell + Enemy Collisions
            else if (collision.A is EnemyTest && collision.B is EnemyTest)
            {
                if(((EnemyTest)collision.A).enemytype is KoopaShellTest || ((EnemyTest)collision.B).enemytype is KoopaShellTest)
                {
                    if(((EnemyTest)collision.A).enemytype is KoopaShellTest && collision.A.velocity.X != 0)
                    {
                        hud.pointTotal += 100;
                    }
                    else if(((EnemyTest)collision.B).enemytype is KoopaShellTest && collision.A.velocity.X != 0)
                    {
                        hud.pointTotal += 100;
                    }
                }
            }
        }
    }

}
