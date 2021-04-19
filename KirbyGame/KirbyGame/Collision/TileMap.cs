using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.Collections;

namespace KirbyGame
{
    public class TileMap
    {
        public const int CELL_SIZE = 32;
        public int xBound;
        public int yBound;
        public List<Entity>[,] Map;
        public TileMap(int x, int y)
        {
            xBound = x/ CELL_SIZE;
            yBound = y/ CELL_SIZE;
            Map = new List<Entity>[xBound, yBound];
            for(int i = 0; i < xBound; i++)
            {
                for(int j = 0; j < yBound; j++)
                {
                    Map[i, j] = new List<Entity>();
                }
            }
        }

        public void Insert(Entity entity)
        {
            int startX = entity.X/ CELL_SIZE;
            int startY = entity.Y/ CELL_SIZE;
            int endX = (entity.X + entity.BoundingBox.Width -1) / CELL_SIZE;
            int endY = (entity.Y + entity.BoundingBox.Height -1) / CELL_SIZE;

            for (int x = startX; x <= endX; x++)
            {
                for (int y = startY; y <= endY; y++)
                {
                    if (inBounds(x, y))
                    {
                        /*if (Map[x, y] == null)
                            Map[x, y] = new List<Entity>();
*/                        Map[x, y].Add(entity);
                    }
                }
            }
        }

        public void Insert(List<Entity> entities)
        {
            foreach(Entity entity in entities)
            {
                this.Insert(entity);
            }
            //test
        }

        public void Clear()
        {
            for (int i = 0; i < xBound; i++)
            {
                for (int j = 0; j < yBound; j++)
                {
                    Map[i, j] = new List<Entity>();
                }
            }
        }

        //this method definitely needs to be optimizedc
        public void Remove(Entity entity)
        {
            /*if(entity.velocity.X != 0 || entity.velocity.Y != 0)
            {
                int startX = entity.X / CELL_SIZE;
                int startY = entity.Y / CELL_SIZE;
                int endX = (entity.X + entity.BoundingBox.Width - 1) / CELL_SIZE;
                int endY = (entity.Y + entity.BoundingBox.Height - 1) / CELL_SIZE;
                int adjStartX = (entity.X + (int)entity.velocity.X) / CELL_SIZE;
                int adjStartY = (entity.Y + (int)entity.velocity.Y) / CELL_SIZE;
                int adjEndX = (entity.X + entity.BoundingBox.Width - 1 + (int)entity.velocity.Y) / CELL_SIZE;
                int adjEndY = (entity.Y + entity.BoundingBox.Height - 1 + (int)entity.velocity.Y) / CELL_SIZE;

                for (int i = Math.Min(startX, adjStartX); i <= Math.Max(endX, adjEndX); i++)
                {
                    for (int j = Math.Min(startY, adjStartY); j <= Math.Max(endY, adjEndY); j++)
                    {
                        if (inBounds(i, j) && Map[i, j] != null && Map[i, j].Contains(entity))
                            Map[i, j].Remove(entity);
                    }
                }
            } else
            {
                for(int i = 0; i < xBound; i++)
                {
                    for(int j = 0; j < yBound; j++)
                    {
                        if (Map[i, j].Contains(entity))
                            Map[i, j].Remove(entity);
                    }
                }
            }*/
            for (int i = 0; i < xBound; i++)
            {
                for (int j = 0; j < yBound; j++)
                {
                    if (Map[i, j].Contains(entity))
                        Map[i, j].Remove(entity);
                }
            }

        }

        public void updateTileMap(List<Entity> entities)
        {
            foreach (Entity entity in entities.Where(e => e.velocity.X != 0.0 || e.velocity.Y != 0.0))
            {
                Remove(entity);
                Insert(entity);
            }
        }

        public Boolean inBounds(int x, int y)
        {
            return !(x < 0 || y < 0) && !(x >= xBound || y >= yBound);
        }
    }
}
