using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace KirbyGame
{
    public class Collision
    {
        public delegate void PointsEventHandler(object source, Collision collision);

        public event PointsEventHandler AddPoints;

        private int CELL_SIZE = 32;
        public bool collidedFromLeft;
        public bool collidedFromRight;
        public bool collidedFromTop;
        public bool collidedFromBottom;
        public enum Direction
        {
            Left,
            Right,
            Up,
            Down
        }

        public Entity A;
        public Entity B;
        public double Time;
        public Direction CollisionDirection;

        public Collision()
        {

        }
        public Collision(Entity a, Entity b)
        {
            A = a;
            B = b;
        }
        
        /*
         * Useless method: does however hold a way to calculate what grid spots a sprite is currently occupying/will occupy
         */
        public Rectangle Mask2(Entity entity)
        {
            
            Vector2 velocity = entity.velocity;

            int x1 = (int)(entity.Sprite.X / TileMap.CELL_SIZE);
            int y1 = (int)(entity.Sprite.Y / TileMap.CELL_SIZE);
            int x2 = (int)(entity.Sprite.X + entity.Sprite.texture.size.X / TileMap.CELL_SIZE);
            int y2 = (int)(entity.Sprite.Y + entity.Sprite.texture.size.Y / TileMap.CELL_SIZE);

            int xVelocity = (int)(velocity.X / TileMap.CELL_SIZE);
            int yVelocity = (int)(velocity.Y / TileMap.CELL_SIZE);

            int x1Swept = Math.Min(x1, x1 + xVelocity);
            int x2Swept = Math.Max(x2, x2 + xVelocity);
            int y1Swept = Math.Min(y1, y1 + yVelocity);
            int y2Swept = Math.Max(y2, y2 + yVelocity);

            Rectangle mask = new Rectangle(x1Swept, y1Swept, x2Swept-x1Swept, y2Swept-y1Swept);
            return mask;
        }

        public bool CheckCollision(Entity a, Entity b)
        {
            return a.BoundingBox.Intersects(b.BoundingBox);    
        }

        public bool GetCollisionData()
        {

            bool collided = false;
            Rectangle velocityAdjustA = new Rectangle(new Point(this.A.BoundingBox.X + (int)this.A.velocity.X, this.A.BoundingBox.Y + (int)this.A.velocity.Y), this.A.boundingBoxSize);
            Rectangle velocityAdjustB = new Rectangle(new Point(this.B.BoundingBox.X + (int)this.B.velocity.X, this.B.BoundingBox.Y + (int)this.B.velocity.Y), this.B.boundingBoxSize);
            //Rectangle intersect = Rectangle.Intersect(velocityAdjustA, velocityAdjustB);
            //added +1, for some reason workds
            collidedFromLeft = this.A.BoundingBox.Right <= this.B.BoundingBox.Left + 1 && velocityAdjustA.Right + 1 >= velocityAdjustB.Left;
            collidedFromRight = this.A.BoundingBox.Left + 1 >= this.B.BoundingBox.Right && velocityAdjustA.Left <= velocityAdjustB.Right + 1;
            collidedFromTop = this.A.BoundingBox.Bottom <= this.B.BoundingBox.Top + 1 && velocityAdjustA.Bottom + 1 >= velocityAdjustB.Top;
            collidedFromBottom = this.A.BoundingBox.Top + 1 >= this.B.BoundingBox.Bottom && velocityAdjustA.Top <= velocityAdjustB.Bottom + 1;


            if (velocityAdjustA.Intersects(velocityAdjustB) && (collidedFromTop || collidedFromRight || collidedFromLeft || collidedFromBottom ))
            {
                collided = true;


                /*if (A is Avatar)
                    Debug.WriteLine("Avatar Perspective");
                else if (A is Block)
                    Debug.WriteLine("Block Perspective");

                Debug.WriteLine("A Previous Top: " + this.A.BoundingBox.Top);
                Debug.WriteLine("B Previous Bottom: " + this.B.BoundingBox.Bottom);
                Debug.WriteLine("A Adjusted Top: " + velocityAdjustA.Top);
                Debug.WriteLine("B adjusted Bottom: " + velocityAdjustB.Bottom);
                Debug.WriteLine("A Previous Right: " + this.A.BoundingBox.Right);
                Debug.WriteLine("B Previous Left: " + this.B.BoundingBox.Left);
                Debug.WriteLine("A Adjusted Right: " + velocityAdjustA.Right);
                Debug.WriteLine("B adjusted Left: " + velocityAdjustB.Left);
                Debug.WriteLine("A bounding box width: "+ this.A.BoundingBox.Width);

                Debug.WriteLine("Collision?: " + collided);
                Debug.WriteLine("Left: "+collidedFromLeft);
                Debug.WriteLine("Right: " + collidedFromRight);
                Debug.WriteLine("Top: " + collidedFromTop);
                Debug.WriteLine("Bottom: " + collidedFromBottom);*/





                if (collidedFromLeft)
                {
                    double leftTime = Math.Abs(((double)this.B.BoundingBox.Left - this.A.BoundingBox.Right) / ((int)this.A.velocity.X - (int)this.B.velocity.X));
                    if (collidedFromTop)
                    {
                        Time = Math.Min(Math.Abs(((double)this.B.BoundingBox.Top - this.A.BoundingBox.Bottom) / ((int)this.A.velocity.Y - (int)this.B.velocity.Y)), leftTime);
                        if (Time == leftTime)
                        {
                            CollisionDirection = Direction.Left;
                        }
                        else
                        {
                            CollisionDirection = Direction.Up;
                        }
                    }
                    else if (collidedFromBottom)
                    {
                        Time = Math.Min(Math.Abs(((double)this.B.BoundingBox.Bottom - this.A.BoundingBox.Top) / ((int)this.A.velocity.Y-(int)this.B.velocity.Y)), leftTime);
                        if(Time == leftTime)
                        {
                            CollisionDirection = Direction.Left;
                        }
                        else
                        {
                            CollisionDirection = Direction.Down;
                        }
                    }
                    else
                    {
                        Time = leftTime;
                        CollisionDirection = Direction.Left;
                    }

                }
                else if (collidedFromRight)
                {

                    double rightTime = Math.Abs(((double)this.B.BoundingBox.Right - this.A.BoundingBox.Left) / ((int)this.A.velocity.X - (int)this.B.velocity.X));
                    if (collidedFromTop)
                    {
                        double topTime = Math.Abs(((double)this.B.BoundingBox.Top - this.A.BoundingBox.Bottom) / (((int)this.A.velocity.Y) - (int)this.B.velocity.Y));
                        Time = Math.Min(topTime, rightTime);
                        if (Time == rightTime)
                        {
                            CollisionDirection = Direction.Right;
                        }
                        else
                        {
                            CollisionDirection = Direction.Up;
                        }
                    }
                    else if (collidedFromBottom)
                    {
                        Time = Math.Min(Math.Abs(((double)this.B.BoundingBox.Bottom - this.A.BoundingBox.Top) / ((int)this.A.velocity.Y - (int)this.B.velocity.Y)), rightTime);
                        if (Time == rightTime)
                        {
                            CollisionDirection = Direction.Right;
                        }
                        else
                        {
                            CollisionDirection = Direction.Down;
                        }
                    }
                    else
                    {
                        Time = rightTime;
                        CollisionDirection = Direction.Right;
                    }
                }
                else if (collidedFromTop)
                {
                    Time = ((double)this.B.BoundingBox.Top - this.A.BoundingBox.Bottom) / ((int)this.A.velocity.Y - (int)this.B.velocity.Y);
                    CollisionDirection = Direction.Up;
                }
                else if (collidedFromBottom)
                {
                    Time = ((double)this.B.BoundingBox.Bottom - this.A.BoundingBox.Top) / ((int)this.A.velocity.Y - (int)this.B.velocity.Y);
                    CollisionDirection = Direction.Down;
                }

            }

            return collided;
        }

        public static void PotentialCollisions(List<Entity> entities, TileMap tileMap)
        {
            int debugCounter = 1;
            List<Collision> collisions = new List<Collision>();
            foreach (Entity entity in entities.Where(e => e.velocity.X != 0.0 || e.velocity.Y != 0.0))
            {

                int startX = entity.X / TileMap.CELL_SIZE;
                int startY = entity.Y / TileMap.CELL_SIZE;
                int endX = (entity.X + entity.BoundingBox.Width - 1) / TileMap.CELL_SIZE;
                int endY = (entity.Y + entity.BoundingBox.Height - 1) / TileMap.CELL_SIZE;
                /*Debug.WriteLine("Checking for a collision");
                Debug.WriteLine("startX: " + startX);
                Debug.WriteLine("startY: " + startY);
                Debug.WriteLine("endX: " + endX);
                Debug.WriteLine("endY: " + endY);*/

                for (int i = startX-1; i <= endX+1; i++)
                {
                    for (int j = startY-1; j <= endY+1; j++)
                    {

                        if (tileMap.inBounds(i, j) && tileMap.Map[i, j] != null)
                        {
                            foreach (Entity collider in tileMap.Map[i, j])
                            {
                                if(collider != entity)
                                {

                                    Collision collision2 = new Collision(entity, collider);
                                    if (collision2.GetCollisionData())
                                    {
                                        bool Add = true;
                                        foreach (Collision listCollision in collisions)
                                        {
                                            if ((listCollision.A == collision2.A && listCollision.B == collision2.B) || (listCollision.A == collision2.B && listCollision.B == collision2.A))
                                                Add = false;

                                        }
                                        if(Add)
                                            collisions.Add(collision2);
                                        //Debug.WriteLine("Collision # " + debugCounter + ": A - " + entity + " B - " + entities[j]);
                                        debugCounter++;
                                    }
                                }
                                
                            }
  
                        }
                    }
                }

                //for (int j = 0; j < entities.Count(); j++)
                //{
                //    Collision collision = new Collision(entity, entities[j]);
                //    if (collision.GetCollisionData())
                //    {
                //        collisions.Add(collision);
                //        Debug.WriteLine("Collision # " + debugCounter + ": A - " + entity + " B - " + entities[j]);
                //        debugCounter++;
                //    }

                //    //collision = new Collision(entities[j], entity);
                //    //if (collision.GetCollisionData())
                //    //{
                //    //    collisions.Add(collision);
                //    //    Debug.WriteLine("Collision # " + debugCounter + ": A - " + entities[j] + " B - " + entity);
                //    //    debugCounter++;
                //    //}
                //}
            }
            while (collisions.Any())
            {
                collisions.Sort((x, y) => x.Time.CompareTo(y.Time));
                double firstCollide = collisions[0].Time;
                int index = 0;
                while (0 < collisions.Count() && firstCollide == collisions[index].Time)
                {
                    if(collisions[index].A is Avatar && collisions[index].B is Block && collisions[index].CollisionDirection == Collision.Direction.Down)
                    {
                        //
                    }
                    // Handle the collision and update entities
                    collisions[index].A.HandleCollision(collisions[index], collisions[index].B);
                    collisions[index].B.HandleCollision(collisions[index], collisions[index].A);
                    //collisions[index].AddPoints += collisions[index].A.game.points.OnAddPoints;
                    //Notify
                    collisions[index].OnAddPoints(collisions[index]);

                    //collisions[index].AddPoints -= collisions[index].A.game.points.OnAddPoints;

                    collisions.RemoveAt(index);
                }
                List<Collision> updatedCollisions = new List<Collision>();
                foreach(Collision collision in collisions)
                {
                    if (collision.GetCollisionData())
                    {
                        updatedCollisions.Add(collision);
                    }
                }
                collisions = updatedCollisions;
            }
        }
        public static Collision.Direction normalizeDirection(Collision collision, Entity entity)
        {
            if (collision.A == entity)
            {
                return collision.CollisionDirection;
            }
            else if (collision.CollisionDirection == Collision.Direction.Up)
            {
                return Collision.Direction.Down;
            }
            else if (collision.CollisionDirection == Collision.Direction.Down)
            {
                return Collision.Direction.Up;
            }
            else if (collision.CollisionDirection == Collision.Direction.Left)
            {
                return Collision.Direction.Right;
            }
            else
            {
                return Collision.Direction.Left;
            }
        }

        protected virtual void OnAddPoints(Collision collision)
        {
            if(AddPoints != null)
            {
                AddPoints(this, collision);
            }
        }
    }

}
