using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KirbyGame
{
    public class Checkpoints
    {
        Game1 game;
        private List<int> respawnPoints;
        private Avatar mario;
        public Vector2 currentRespawn;

        public Checkpoints(Avatar mario, Game1 game)
        {
            this.game = game;
            currentRespawn = new Vector2(4 * TileMap.CELL_SIZE, 10*TileMap.CELL_SIZE);
            this.mario = mario;
            respawnPoints = new List<int>();
            respawnPoints.Add(1920);
            respawnPoints.Add(3360);
            respawnPoints.Add(4730);
            respawnPoints.Add(6600);
        }
        
        public void Update()
        {
            foreach(int spawnPoint in respawnPoints)
            {
                if(spawnPoint > currentRespawn.X && mario.X > spawnPoint)
                {
                    currentRespawn = new Vector2(spawnPoint, mario.Y);
                }
            }
        }

        public void resetFromCheckpoint()
        {
            game.map.Remove(mario);
            mario.X = (int)currentRespawn.X;
            mario.Y = (int)currentRespawn.Y;
            //mario.actionState.FallingTransition();
            game.map.Insert(mario);
        }
    }
}
