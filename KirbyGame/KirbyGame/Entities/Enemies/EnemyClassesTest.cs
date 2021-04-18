using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using Microsoft.Xna.Framework.Audio;

namespace KirbyGame
{
    class WaddleDeeTest : EnemytypeTest
    {
        public WaddleDeeTest(EnemyTest enemy, Vector2 location) : base(enemy)
        {
            this.enemy.Sprite = new Sprite(new TextureDetails(this.enemy.game.Content.Load<Texture2D>("WaddleDee"), 2), location);
        }

        public override void HandleCollision(Collision collision, Entity collider)
        {
            if(collider is Avatar /* or is projectile */)
            {
                this.enemy.DeadStateChange();
            }
            else if(collider is Block && (collision.CollisionDirection == Collision.Direction.Right || collision.CollisionDirection == Collision.Direction.Left))
            {
                this.enemy.velocity.X = this.enemy.velocity.X * -1;
            }
            //else if(collider is Succ)
            // this.enemy.SuckStateChange(getdirection (check collision for left or right);
        }
    }

    class WaddleDooTest : EnemytypeTest
    {
        public WaddleDooTest(EnemyTest enemy, Vector2 location) : base(enemy)
        {
            this.enemy.Sprite = new Sprite(new TextureDetails(this.enemy.game.Content.Load<Texture2D>("WaddleDoo"), 2), location);
        }

        public override void HandleCollision(Collision collision, Entity collider)
        {
            if (collider is Avatar /*or is projectile */)
            {
                this.enemy.DeadStateChange();
            }
            else if (collider is Block && (collision.CollisionDirection == Collision.Direction.Right || collision.CollisionDirection == Collision.Direction.Left))
            {
                this.enemy.velocity.X = this.enemy.velocity.X * -1;
            }
            //else if(collider is Succ)
        }
    }

    class SirKibbleTest : EnemytypeTest
    {
        public SirKibbleTest(EnemyTest enemy, Vector2 location) : base(enemy)
        {
            this.enemy.Sprite = new Sprite(new TextureDetails(this.enemy.game.Content.Load<Texture2D>("SirKibble"), 2), location);
            this.enemy.velocity.X = 0;
        }
        public override void HandleCollision(Collision collision, Entity collider)
        {
            if (collider is Avatar /* or is projectile */)
            {
                this.enemy.DeadStateChange();
            }
            else if (collider is Block && (collision.CollisionDirection == Collision.Direction.Right || collision.CollisionDirection == Collision.Direction.Left))
            {
                this.enemy.velocity.X = this.enemy.velocity.X * -1;
            }
            //else if(collider is Succ)
        }

        public override void Update(GameTime gameTime)
        {
            //check if Kirby in range, then throw boomerang on CD
        }
    }

    class AppleTest : EnemytypeTest
    {
        
        private bool hitGround = false;
        public AppleTest(EnemyTest enemy, Vector2 location) : base(enemy)
        {
            this.enemy.Sprite = new Sprite(new TextureDetails(this.enemy.game.Content.Load<Texture2D>("Apple"), 2), location);
            this.enemy.seen = true;
            this.enemy.velocity.X = 0;
        }

        public override void HandleCollision(Collision collision, Entity collider)
        {
            if (collider is Avatar /* or is projectile */)
            {
                this.enemy.DeadStateChange();
            }
            //else if(collider is Succ)
        }

        public override void Update(GameTime gameTime)
        {

        }
    }

    //Next 4 classes are for when enemies run into kirby or are hit by a projectile.
    class DeadWaddleDeeTest : EnemytypeTest
    {
        public Vector2 location;
        Texture2D texture;
        private int maxFrames;
        private int currentFrame;
        private Point frameSize;
        private int Time;
        private int Delay;

        public DeadWaddleDeeTest(EnemyTest enemy, Vector2 location) : base(enemy)
        {
            texture = this.enemy.game.Content.Load<Texture2D>("WaddleDee");
            maxFrames = 2;
            currentFrame = 0;
            frameSize = new Point(texture.Width / maxFrames, texture.Height);
            Time = 0;
            Delay = 200;
            this.location = location;
            this.enemy.acceleration = new Vector2(0, 1);
            this.enemy.velocity.Y = 1;

            this.enemy.boundingBoxSize = new Point();
            this.enemy.position = new Point();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRectangle = new Rectangle(frameSize.X * currentFrame, 0, frameSize.X, frameSize.Y);
            spriteBatch.Draw(texture, location, sourceRectangle, Color.White, 180, new Vector2(0, 0), 2, SpriteEffects.FlipHorizontally, 0);
        }

        public override void Update(GameTime gameTime)
        {
            if (maxFrames > 1)
            {
                Time += gameTime.ElapsedGameTime.Milliseconds;
                if (Time > Delay)
                {
                    Time -= Delay;
                    currentFrame++;
                }
                if (currentFrame == maxFrames)
                    currentFrame = 0;
            }
            location.Y += this.enemy.velocity.Y;
        }
    }

    class DeadWaddleDooTest : EnemytypeTest
    {
        public Vector2 location;
        Texture2D texture;
        private int maxFrames;
        private int currentFrame;
        private Point frameSize;
        private int Time;
        private int Delay;

        public DeadWaddleDooTest(EnemyTest enemy, Vector2 location) : base(enemy)
        {
            texture = this.enemy.game.Content.Load<Texture2D>("WaddleDoo");
            maxFrames = 2;
            currentFrame = 0;
            frameSize = new Point(texture.Width / maxFrames, texture.Height);
            Time = 0;
            Delay = 200;
            this.location = location;
            this.enemy.acceleration = new Vector2(0, 1);
            this.enemy.velocity.Y = 1;

            this.enemy.boundingBoxSize = new Point();
            this.enemy.position = new Point();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRectangle = new Rectangle(frameSize.X * currentFrame, 0, frameSize.X, frameSize.Y);
            spriteBatch.Draw(texture, location, sourceRectangle, Color.White, 180, new Vector2(0, 0), 2, SpriteEffects.FlipHorizontally, 0);
        }

        public override void Update(GameTime gameTime)
        {
            if (maxFrames > 1)
            {
                Time += gameTime.ElapsedGameTime.Milliseconds;
                if (Time > Delay)
                {
                    Time -= Delay;
                    currentFrame++;
                }
                if (currentFrame == maxFrames)
                    currentFrame = 0;
            }
            location.Y += this.enemy.velocity.Y;
        }
    }

    class DeadSirKibbleTest : EnemytypeTest
    {
        public Vector2 location;
        Texture2D texture;
        private int maxFrames;
        private int currentFrame;
        private Point frameSize;
        private int Time;
        private int Delay;

        public DeadSirKibbleTest(EnemyTest enemy, Vector2 location) : base(enemy)
        {
            texture = this.enemy.game.Content.Load<Texture2D>("SirKibble");
            maxFrames = 2;
            currentFrame = 0;
            frameSize = new Point(texture.Width / maxFrames, texture.Height);
            Time = 0;
            Delay = 200;
            this.location = location;
            this.enemy.acceleration = new Vector2(0, 1);
            this.enemy.velocity.Y = 1;

            this.enemy.boundingBoxSize = new Point();
            this.enemy.position = new Point();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRectangle = new Rectangle(frameSize.X * currentFrame, 0, frameSize.X, frameSize.Y);
            spriteBatch.Draw(texture, location, sourceRectangle, Color.White, 180, new Vector2(0, 0), 2, SpriteEffects.FlipHorizontally, 0);
        }

        public override void Update(GameTime gameTime)
        {
            if (maxFrames > 1)
            {
                Time += gameTime.ElapsedGameTime.Milliseconds;
                if (Time > Delay)
                {
                    Time -= Delay;
                    currentFrame++;
                }
                if (currentFrame == maxFrames)
                    currentFrame = 0;
            }
            location.Y += this.enemy.velocity.Y;
        }
    }
    class DeadAppleTest : EnemytypeTest
    {
        public Vector2 location;
        Texture2D texture;
        private int maxFrames;
        private int currentFrame;
        private Point frameSize;
        private int Time;
        private int Delay;

        public DeadAppleTest(EnemyTest enemy, Vector2 location) : base(enemy)
        {
            texture = this.enemy.game.Content.Load<Texture2D>("Apple");
            maxFrames = 2;
            currentFrame = 0;
            frameSize = new Point(texture.Width / maxFrames, texture.Height);
            Time = 0;
            Delay = 200;
            this.location = location;
            this.enemy.acceleration = new Vector2(0, 1);
            this.enemy.velocity.Y = 1;

            this.enemy.boundingBoxSize = new Point();
            this.enemy.position = new Point();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRectangle = new Rectangle(frameSize.X * currentFrame, 0, frameSize.X, frameSize.Y);
            spriteBatch.Draw(texture, location, sourceRectangle, Color.White, 180, new Vector2(0, 0), 2, SpriteEffects.FlipHorizontally, 0);
        }

        public override void Update(GameTime gameTime)
        {
            if (maxFrames > 1)
            {
                Time += gameTime.ElapsedGameTime.Milliseconds;
                if (Time > Delay)
                {
                    Time -= Delay;
                    currentFrame++;
                }
                if (currentFrame == maxFrames)
                    currentFrame = 0;
            }
            location.Y += this.enemy.velocity.Y;
        }
    }

    //These classes are for when an enemy gets sucked into Kirby. direction will be 1 for left, 2 for right.
    class SuckWaddleDeeTest : EnemytypeTest
    {
        public Vector2 location;
        Texture2D texture;
        private int maxFrames;
        private int currentFrame;
        private Point frameSize;
        private int Time;
        private int Delay;
        private int Direction;
        private int check = 0;

        public SuckWaddleDeeTest(EnemyTest enemy, Vector2 location, int direction) : base(enemy)
        {
            texture = this.enemy.game.Content.Load<Texture2D>("WaddleDee");
            maxFrames = 2;
            currentFrame = 0;
            frameSize = new Point(texture.Width / maxFrames, texture.Height);
            Time = 0;
            Delay = 200;
            this.location = location;
            this.Direction = direction;
            

            if (Direction == 1)
            {
                this.enemy.acceleration = new Vector2(-1, 0);
                this.enemy.velocity.X = -1;

            }
            else if(Direction == 2)
            {
                this.enemy.acceleration = new Vector2(1, 0);
                this.enemy.velocity.X = 1;
            }

            this.enemy.boundingBoxSize = new Point();
            this.enemy.position = new Point();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            while (check < 50)
            {
                Rectangle sourceRectangle = new Rectangle(frameSize.X * currentFrame, 0, frameSize.X, frameSize.Y);
                spriteBatch.Draw(texture, location, sourceRectangle, Color.White, 180, new Vector2(0, 0), 2, SpriteEffects.None, 0);
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (maxFrames > 1 && check < 50)
            {
                Time += gameTime.ElapsedGameTime.Milliseconds;
                if (Time > Delay)
                {
                    Time -= Delay;
                    currentFrame++;
                }
                if (currentFrame == maxFrames)
                    currentFrame = 0;
            }
            location.X += this.enemy.velocity.X;
            check++;
        }
    }

    class SuckWaddleDooTest : EnemytypeTest
    {
        public Vector2 location;
        Texture2D texture;
        private int maxFrames;
        private int currentFrame;
        private Point frameSize;
        private int Time;
        private int Delay;
        private int Direction;
        private int check = 0;

        public SuckWaddleDooTest(EnemyTest enemy, Vector2 location, int direction) : base(enemy)
        {
            texture = this.enemy.game.Content.Load<Texture2D>("WaddleDee");
            maxFrames = 2;
            currentFrame = 0;
            frameSize = new Point(texture.Width / maxFrames, texture.Height);
            Time = 0;
            Delay = 200;
            this.location = location;
            this.Direction = direction;


            if (Direction == 1)
            {
                this.enemy.acceleration = new Vector2(-1, 0);
                this.enemy.velocity.X = -1;

            }
            else if (Direction == 2)
            {
                this.enemy.acceleration = new Vector2(1, 0);
                this.enemy.velocity.X = 1;
            }

            this.enemy.boundingBoxSize = new Point();
            this.enemy.position = new Point();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            while (check < 50)
            {
                Rectangle sourceRectangle = new Rectangle(frameSize.X * currentFrame, 0, frameSize.X, frameSize.Y);
                spriteBatch.Draw(texture, location, sourceRectangle, Color.White, 180, new Vector2(0, 0), 2, SpriteEffects.None, 0);
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (maxFrames > 1 && check < 50)
            {
                Time += gameTime.ElapsedGameTime.Milliseconds;
                if (Time > Delay)
                {
                    Time -= Delay;
                    currentFrame++;
                }
                if (currentFrame == maxFrames)
                    currentFrame = 0;
            }
            location.X += this.enemy.velocity.X;
            check++;
        }
    }

    class SuckSirKibbleTest : EnemytypeTest
    {
        public Vector2 location;
        Texture2D texture;
        private int maxFrames;
        private int currentFrame;
        private Point frameSize;
        private int Time;
        private int Delay;
        private int Direction;
        private int check = 0;

        public SuckSirKibbleTest(EnemyTest enemy, Vector2 location, int direction) : base(enemy)
        {
            texture = this.enemy.game.Content.Load<Texture2D>("SirKibble");
            maxFrames = 2;
            currentFrame = 0;
            frameSize = new Point(texture.Width / maxFrames, texture.Height);
            Time = 0;
            Delay = 200;
            this.location = location;
            this.Direction = direction;


            if (Direction == 1)
            {
                this.enemy.acceleration = new Vector2(-1, 0);
                this.enemy.velocity.X = -1;

            }
            else if (Direction == 2)
            {
                this.enemy.acceleration = new Vector2(1, 0);
                this.enemy.velocity.X = 1;
            }

            this.enemy.boundingBoxSize = new Point();
            this.enemy.position = new Point();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            while (check < 50)
            {
                Rectangle sourceRectangle = new Rectangle(frameSize.X * currentFrame, 0, frameSize.X, frameSize.Y);
                spriteBatch.Draw(texture, location, sourceRectangle, Color.White, 180, new Vector2(0, 0), 2, SpriteEffects.None, 0);
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (maxFrames > 1 && check < 50)
            {
                Time += gameTime.ElapsedGameTime.Milliseconds;
                if (Time > Delay)
                {
                    Time -= Delay;
                    currentFrame++;
                }
                if (currentFrame == maxFrames)
                    currentFrame = 0;
            }
            location.X += this.enemy.velocity.X;
            check++;
        }
    }
    class SuckAppleTest : EnemytypeTest
    {
        public Vector2 location;
        Texture2D texture;
        private int maxFrames;
        private int currentFrame;
        private Point frameSize;
        private int Time;
        private int Delay;
        private int Direction;
        private int check = 0;

        public SuckAppleTest(EnemyTest enemy, Vector2 location, int direction) : base(enemy)
        {
            texture = this.enemy.game.Content.Load<Texture2D>("SirKibble");
            maxFrames = 2;
            currentFrame = 0;
            frameSize = new Point(texture.Width / maxFrames, texture.Height);
            Time = 0;
            Delay = 200;
            this.location = location;
            this.Direction = direction;


            if (Direction == 1)
            {
                this.enemy.acceleration = new Vector2(-1, 0);
                this.enemy.velocity.X = -1;

            }
            else if (Direction == 2)
            {
                this.enemy.acceleration = new Vector2(1, 0);
                this.enemy.velocity.X = 1;
            }

            this.enemy.boundingBoxSize = new Point();
            this.enemy.position = new Point();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            while (check < 50)
            {
                Rectangle sourceRectangle = new Rectangle(frameSize.X * currentFrame, 0, frameSize.X, frameSize.Y);
                spriteBatch.Draw(texture, location, sourceRectangle, Color.White, 180, new Vector2(0, 0), 2, SpriteEffects.None, 0);
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (maxFrames > 1 && check < 50)
            {
                Time += gameTime.ElapsedGameTime.Milliseconds;
                if (Time > Delay)
                {
                    Time -= Delay;
                    currentFrame++;
                }
                if (currentFrame == maxFrames)
                    currentFrame = 0;
            }
            location.X += this.enemy.velocity.X;
            check++;
        }
    }

    class GoombaTest : EnemytypeTest
    {
        public GoombaTest(EnemyTest enemy, Vector2 location) : base(enemy)
        {
            this.enemy.Sprite = new Sprite(new TextureDetails(this.enemy.game.Content.Load<Texture2D>("goomba"), 2), location);
            base.points = 100;
        }

        public override void HandleCollision(Collision collision, Entity collider)
        {
            Collision.Direction CollisionDirection = Collision.normalizeDirection(collision, this.enemy);
            //Avatar collision
            if (collider is Avatar)
            {
                if (CollisionDirection == Collision.Direction.Down)
                {
                    this.enemy.SquishGoombaStateChange();

                }
            }

            //Block collision
            else if (collider is Block)
            {
                if (CollisionDirection == Collision.Direction.Left)
                {
                    this.enemy.velocity.X = this.enemy.velocity.X * -1;

                }
                else if (CollisionDirection == Collision.Direction.Right)
                {
                    this.enemy.velocity.X = this.enemy.velocity.X * -1;

                }
            }
            //Enemies NOT Shell
            else if (collider is EnemyTest && !(((EnemyTest)collider).enemytype is KoopaShellTest))
            {
                if (CollisionDirection == Collision.Direction.Left)
                {
                    this.enemy.velocity.X = this.enemy.velocity.X * -1;
                }
                else if (CollisionDirection == Collision.Direction.Right)
                {
                    this.enemy.velocity.X = this.enemy.velocity.X * -1;
                }
            }

            //Enemies Shell
            else if (collider is EnemyTest && ((EnemyTest)collider).enemytype is KoopaShellTest)
            {
                if (collider.velocity.X == 0 && CollisionDirection == Collision.Direction.Left)
                {
                    Debug.WriteLine("Collision Detected!");
                    this.enemy.velocity.X = this.enemy.velocity.X * -1;
                }
                else if (collider.velocity.X == 0 && CollisionDirection == Collision.Direction.Right)
                {

                    this.enemy.velocity.X = this.enemy.velocity.X * -1;
                }
                else if (collider.velocity.X != 0 && CollisionDirection == Collision.Direction.Left)
                {
                    this.enemy.DeadGoombaStateChange();
                }
                else if (collider.velocity.X != 0 && CollisionDirection == Collision.Direction.Right)
                {
                    this.enemy.DeadGoombaStateChange();
                }
            }

            //Fireball Collision
            else if (collider is Cannonball)
            {
                this.enemy.DeadGoombaStateChange();
            }
        }

    }

    class ParanaTest : EnemytypeTest
    {
        public int timer = 0;
        int startY;
        public ParanaTest(EnemyTest enemy, Vector2 location) : base(enemy)
        {
            this.enemy.Sprite = new Sprite(new TextureDetails(this.enemy.game.Content.Load<Texture2D>("parana"), 2), location);
            base.points = 100;
            enemy.velocity.X = 0;
            enemy.updateNull = false;
            enemy.velocity.Y = -1;
            startY = enemy.Y;
        }

        public override void Update(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime.Milliseconds;
            enemy.acceleration.Y = 0;
            if (enemy.seen)
            {
                base.Update(gameTime);

                if (enemy.Y < startY - 40)
                {
                    enemy.Y = startY - 40;
                }
                if (enemy.Y > startY)
                {
                    enemy.Y = startY;
                }
                if (timer > 5000)
                    if (enemy.game.levelLoader.getMario().X - enemy.X > 50 || enemy.game.levelLoader.getMario().X - enemy.X < -50)
                    {
                        {
                            if (enemy.velocity.Y == -1)
                                enemy.velocity.Y = 1;
                            else if (enemy.velocity.Y == 1)
                            {
                                enemy.velocity.Y = -1;
                            }
                            timer = 0;
                        }
                    }
            }
        }



        public override void HandleCollision(Collision collision, Entity collider)
        {
            Collision.Direction CollisionDirection = Collision.normalizeDirection(collision, this.enemy);

            //Enemies NOT Shell
            if (collider is EnemyTest && !(((EnemyTest)collider).enemytype is KoopaShellTest))
            {
                if (CollisionDirection == Collision.Direction.Left)
                {
                    this.enemy.velocity.X = this.enemy.velocity.X * -1;
                }
                else if (CollisionDirection == Collision.Direction.Right)
                {
                    this.enemy.velocity.X = this.enemy.velocity.X * -1;
                }
            }

            //Enemies Shell
            else if (collider is EnemyTest && ((EnemyTest)collider).enemytype is KoopaShellTest)
            {
                if (collider.velocity.X == 0 && CollisionDirection == Collision.Direction.Left)
                {
                    Debug.WriteLine("Collision Detected!");
                    this.enemy.velocity.X = this.enemy.velocity.X * -1;
                }
                else if (collider.velocity.X == 0 && CollisionDirection == Collision.Direction.Right)
                {

                    this.enemy.velocity.X = this.enemy.velocity.X * -1;
                }
                else if (collider.velocity.X != 0 && CollisionDirection == Collision.Direction.Left)
                {
                    this.enemy.DeadParanaStateChange();
                }
                else if (collider.velocity.X != 0 && CollisionDirection == Collision.Direction.Right)
                {
                    this.enemy.DeadParanaStateChange();
                }
            }

            //Fireball Collision
            else if (collider is Cannonball)
            {
                this.enemy.DeadParanaStateChange();
            }
        }

    }

    class KoopaTest : EnemytypeTest
    {

        public KoopaTest(EnemyTest enemy, Vector2 location, Sprite.eDirection direction) : base(enemy)
        {
            this.enemy.Sprite = new Sprite(new TextureDetails(this.enemy.game.Content.Load<Texture2D>("koopaleft"), 2), location);
            this.enemy.velocity.X = -1;
            base.points = 100;

        }
        public override void HandleCollision(Collision collision, Entity collider)
        {
            Collision.Direction CollisionDirection = Collision.normalizeDirection(collision, this.enemy);
            //Avatar collision
            if (collider is Avatar)
            {
                if (CollisionDirection == Collision.Direction.Down)
                {
                    this.enemy.KoopaStateChange();
                }
            }

            //Block collision - possibly have to mirror the sheet?
            else if (collider is Block)
            {
                if (CollisionDirection == Collision.Direction.Left)
                {
                    this.enemy.velocity.X = this.enemy.velocity.X * -1;
                    if (this.enemy.Sprite.Direction == Sprite.eDirection.Left)
                    {
                        this.enemy.Sprite.Direction = Sprite.eDirection.Right;
                    }
                    else
                    {
                        this.enemy.Sprite.Direction = Sprite.eDirection.Left;
                    }
                }
                else if (CollisionDirection == Collision.Direction.Right)
                {
                    this.enemy.velocity.X = this.enemy.velocity.X * -1;
                    if (this.enemy.Sprite.Direction == Sprite.eDirection.Left)
                    {
                        this.enemy.Sprite.Direction = Sprite.eDirection.Right;
                    }
                    else
                    {
                        this.enemy.Sprite.Direction = Sprite.eDirection.Left;
                    }
                }
            }
            //Enemies NOT Shell - possibly have to mirror the sheet?
            else if (collider is EnemyTest && !(((EnemyTest)collider).enemytype is KoopaShellTest))
            {
                if (CollisionDirection == Collision.Direction.Left)
                {

                    this.enemy.velocity.X = this.enemy.velocity.X * -1;
                    if (this.enemy.Sprite.Direction == Sprite.eDirection.Left)
                    {
                        this.enemy.Sprite.Direction = Sprite.eDirection.Right;
                    }
                    else
                    {
                        this.enemy.Sprite.Direction = Sprite.eDirection.Left;
                    }
                }
                else if (CollisionDirection == Collision.Direction.Right)
                {

                    this.enemy.velocity.X = this.enemy.velocity.X * -1;
                    if (this.enemy.Sprite.Direction == Sprite.eDirection.Left)
                    {
                        this.enemy.Sprite.Direction = Sprite.eDirection.Right;
                    }
                    else
                    {
                        this.enemy.Sprite.Direction = Sprite.eDirection.Left;
                    }
                }
            }

            //Enemies Shell
            else if (collider is EnemyTest && ((EnemyTest)collider).enemytype is KoopaShellTest)
            {
                if (collider.velocity.X == 0 && CollisionDirection == Collision.Direction.Left)
                {
                    this.enemy.velocity.X = this.enemy.velocity.X * -1;
                    if (this.enemy.Sprite.Direction == Sprite.eDirection.Left)
                    {
                        this.enemy.Sprite.Direction = Sprite.eDirection.Right;
                    }
                    else
                    {
                        this.enemy.Sprite.Direction = Sprite.eDirection.Left;
                    }
                }
                else if (collider.velocity.X == 0 && CollisionDirection == Collision.Direction.Right)
                {
                    this.enemy.velocity.X = this.enemy.velocity.X * -1;
                    if (this.enemy.Sprite.Direction == Sprite.eDirection.Left)
                    {
                        this.enemy.Sprite.Direction = Sprite.eDirection.Right;
                    }
                    else
                    {
                        this.enemy.Sprite.Direction = Sprite.eDirection.Left;
                    }
                }
                else if (collider.velocity.X != 0 && CollisionDirection == Collision.Direction.Left)
                {
                    this.enemy.DeadKoopaStateChange();
                }
                else if (collider.velocity.X != 0 && CollisionDirection == Collision.Direction.Right)
                {
                    this.enemy.DeadKoopaStateChange();
                }
            }
            else if (collider is Cannonball)
            {
                this.enemy.DeadKoopaStateChange();
            }
        }
    }
    class KoopaShellTest : EnemytypeTest
    {

        private int timer = 0;
        private int secondTimer = 0;


        public KoopaShellTest(EnemyTest enemy, Vector2 location, Sprite.eDirection direction) : base(enemy)
        {
            this.enemy.Sprite = new Sprite(new TextureDetails(this.enemy.game.Content.Load<Texture2D>("koopashell"), 1), location);
            this.enemy.Sprite.Direction = direction;
            this.enemy.Sprite.location.Y += 19;
        }

        public override void HandleCollision(Collision collision, Entity collider)
        {
            Collision.Direction CollisionDirection = Collision.normalizeDirection(collision, this.enemy);
            //Avatar collisions
            if (collider is Avatar)
            {
                Debug.WriteLine("Velocity is " + this.enemy.velocity.X);
                if (CollisionDirection == Collision.Direction.Right && this.enemy.velocity.X == 0)
                {
                    Debug.WriteLine("Increasing velocity");
                    base.enemy.velocity.X = -2;
                }
                else if (CollisionDirection == Collision.Direction.Left && base.enemy.velocity.X == 0)
                {
                    Debug.WriteLine("Increasing velocity");
                    base.enemy.velocity.X = 2;
                }
                else if (CollisionDirection == Collision.Direction.Up)
                {
                    base.enemy.velocity.X = 0;
                }
            }

            //Block collisions
            else if (collider is Block)
            {
                if (CollisionDirection == Collision.Direction.Left)
                {
                    this.enemy.velocity.X = this.enemy.velocity.X * -1;
                }
                else if (CollisionDirection == Collision.Direction.Right)
                {
                    this.enemy.velocity.X = this.enemy.velocity.X * -1;
                }
            }

            //Fireball Collisions
            else if (collider is Cannonball)
            {
                this.enemy.DeadKoopaShellStateChange();
            }

        }

        public override void Update(GameTime gameTime)
        {
            if (timer > 500 && this.enemy.velocity.X == 0)
            {
                this.enemy.Sprite = new Sprite(new TextureDetails(this.enemy.game.Content.Load<Texture2D>("koopaemerging"), 1), new Vector2(this.enemy.X, this.enemy.Y - 1));
                if (secondTimer > 150)
                {
                    this.enemy.ShellStateChange();
                    this.enemy.velocity.X = -1;
                }
                secondTimer++;
                if (this.enemy.velocity.X != 0)
                {
                    timer = 0;
                    secondTimer = 0;
                }
            }
            timer++;
            if (this.enemy.velocity.X != 0)
            {
                timer = 0;
            }
        }
    }

    class DeadKoopaTest : EnemytypeTest
    {
        public Vector2 location;
        Texture2D texture;
        private int maxFrames;
        private int currentFrame;
        private Point frameSize;
        private int Time;
        private int Delay;


        public DeadKoopaTest(EnemyTest enemy, Vector2 location) : base(enemy)
        {
            texture = this.enemy.game.Content.Load<Texture2D>("koopaleft");
            maxFrames = 2;
            currentFrame = 0;
            frameSize = new Point(texture.Width / maxFrames, texture.Height);
            Time = 0;
            Delay = 200;
            this.location = location;
            this.enemy.acceleration = new Vector2(0, 1);
            this.enemy.velocity.Y = 1;

            this.enemy.boundingBoxSize = new Point();
            this.enemy.position = new Point();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRectangle = new Rectangle(frameSize.X * currentFrame, 0, frameSize.X, frameSize.Y);
            spriteBatch.Draw(texture, location, sourceRectangle, Color.White, 180, new Vector2(0, 0), 2, SpriteEffects.FlipHorizontally, 0);
        }

        public override void Update(GameTime gameTime)
        {
            if (maxFrames > 1)
            {
                Time += gameTime.ElapsedGameTime.Milliseconds;
                if (Time > Delay)
                {
                    Time -= Delay;
                    currentFrame++;
                }
                if (currentFrame == maxFrames)
                    currentFrame = 0;
            }
            location.Y += this.enemy.velocity.Y;
        }

    }

    class DeadKoopaShellTest : EnemytypeTest
    {
        public Vector2 location;
        Texture2D texture;
        private int maxFrames;
        private int currentFrame;
        private Point frameSize;
        private int Time;
        private int Delay;


        public DeadKoopaShellTest(EnemyTest enemy, Vector2 location) : base(enemy)
        {
            texture = this.enemy.game.Content.Load<Texture2D>("koopashell");
            maxFrames = 2;
            currentFrame = 0;
            frameSize = new Point(texture.Width / maxFrames, texture.Height);
            Time = 0;
            Delay = 200;
            this.location = location;
            this.enemy.acceleration = new Vector2(0, 1);
            this.enemy.velocity.Y = 1;

            this.enemy.boundingBoxSize = new Point();
            this.enemy.position = new Point();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRectangle = new Rectangle(frameSize.X * currentFrame, 0, frameSize.X, frameSize.Y);
            spriteBatch.Draw(texture, location, sourceRectangle, Color.White, 180, new Vector2(0, 0), 2, SpriteEffects.FlipHorizontally, 0);
        }

        public override void Update(GameTime gameTime)
        {
            if (maxFrames > 1)
            {
                Time += gameTime.ElapsedGameTime.Milliseconds;
                if (Time > Delay)
                {
                    Time -= Delay;
                    currentFrame++;
                }
                if (currentFrame == maxFrames)
                    currentFrame = 0;
            }
            location.Y += this.enemy.velocity.Y;
        }

    }
    class DeadGoombaTest : EnemytypeTest
    {

        public Vector2 location;
        Texture2D texture;
        private int maxFrames;
        private int currentFrame;
        private Point frameSize;
        private int Time;
        private int Delay;


        public DeadGoombaTest(EnemyTest enemy, Vector2 location) : base(enemy)
        {
            texture = this.enemy.game.Content.Load<Texture2D>("goomba");
            maxFrames = 2;
            currentFrame = 0;
            frameSize = new Point(texture.Width / maxFrames, texture.Height);
            Time = 0;
            Delay = 200;
            this.location = location;
            this.enemy.acceleration = new Vector2(0, 1);
            this.enemy.velocity.Y = 1;

            this.enemy.boundingBoxSize = new Point();
            this.enemy.position = new Point();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRectangle = new Rectangle(frameSize.X * currentFrame, 0, frameSize.X, frameSize.Y);
            spriteBatch.Draw(texture, location, sourceRectangle, Color.White, 180, new Vector2(0, 0), 2, SpriteEffects.FlipHorizontally, 0);
        }

        public override void Update(GameTime gameTime)
        {
            if (maxFrames > 1)
            {
                Time += gameTime.ElapsedGameTime.Milliseconds;
                if (Time > Delay)
                {
                    Time -= Delay;
                    currentFrame++;
                }
                if (currentFrame == maxFrames)
                    currentFrame = 0;
            }
            location.Y += this.enemy.velocity.Y;
        }

    }

    class DeadParana : EnemytypeTest
    {

        public Vector2 location;
        Texture2D texture;
        private int maxFrames;
        private int currentFrame;
        private Point frameSize;
        private int Time;
        private int Delay;


        public DeadParana(EnemyTest enemy, Vector2 location) : base(enemy)
        {
            texture = this.enemy.game.Content.Load<Texture2D>("parana");
            maxFrames = 2;
            currentFrame = 0;
            frameSize = new Point(texture.Width / maxFrames, texture.Height);
            Time = 0;
            Delay = 200;
            this.location = location;
            this.enemy.acceleration = new Vector2(0, 1);
            this.enemy.velocity.Y = 1;

            this.enemy.boundingBoxSize = new Point();
            this.enemy.position = new Point();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRectangle = new Rectangle(frameSize.X * currentFrame, 0, frameSize.X, frameSize.Y);
            spriteBatch.Draw(texture, location, sourceRectangle, Color.White, 180, new Vector2(0, 0), 2, SpriteEffects.FlipHorizontally, 0);
        }

        public override void Update(GameTime gameTime)
        {
            if (maxFrames > 1)
            {
                Time += gameTime.ElapsedGameTime.Milliseconds;
                if (Time > Delay)
                {
                    Time -= Delay;
                    currentFrame++;
                }
                if (currentFrame == maxFrames)
                    currentFrame = 0;
            }
            location.Y += this.enemy.velocity.Y;
        }

    }

    class SquishGoombaTest : EnemytypeTest
    {
        public Vector2 location;
        Texture2D texture;
        private int maxFrames;
        private int currentFrame;
        private Point frameSize;
        private int Time;
        private int Delay;
        private int Timer = 0;


        public SquishGoombaTest(EnemyTest enemy, Vector2 location) : base(enemy)
        {
            texture = this.enemy.game.Content.Load<Texture2D>("squishgoomba");
            maxFrames = 1;
            currentFrame = 0;
            frameSize = new Point(texture.Width / maxFrames, texture.Height);
            Time = 0;
            Delay = 200;
            this.location.Y = location.Y + 16;
            this.location.X = location.X;
            this.enemy.velocity.X = 0;
            this.enemy.acceleration = new Vector2(0, 0);
            this.enemy.velocity.Y = 0;

            this.enemy.boundingBoxSize = new Point();
            this.enemy.position = new Point();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            if (Timer < 50)
            {
                Rectangle sourceRectangle = new Rectangle(frameSize.X * currentFrame, 0, frameSize.X, frameSize.Y);
                spriteBatch.Draw(texture, location, sourceRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0);
                Timer++;
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (maxFrames > 1)
            {
                Time += gameTime.ElapsedGameTime.Milliseconds;
                if (Time > Delay)
                {
                    Time -= Delay;
                    currentFrame++;
                }
                if (currentFrame == maxFrames)
                    currentFrame = 0;
            }
        }





    }
    class ShotzoTest : EnemytypeTest
    {
        private int delay = 0;
        private SoundEffect player;
        private Vector2 _location;
        private readonly CannonballFactory cannonballFactory;
        private bool right = true;
        public ShotzoTest(EnemyTest enemy, Vector2 location, Sprite.eDirection direction,Game1 game) : base(enemy)
        {
            enemy.velocity.X = 0;
            cannonballFactory = new CannonballFactory(game);
            _location = location;
            this.enemy.Sprite = new Sprite(new TextureDetails(this.enemy.game.Content.Load<Texture2D>("shotzoright"), 1), _location);
            Follow();
        }

        public override void Update(GameTime gameTime)
        {
            Follow();
            enemy.acceleration.Y = 0; 
            base.Update(gameTime);
            int displacement = (int)(enemy.game.levelLoader.getMario().position.X + enemy.game.levelLoader.getMario().boundingBoxSize.X / 2 - _location.X + enemy.Sprite.texture.size.X / 2);
            Console.WriteLine("dis" + (enemy.game.levelLoader.getMario().position.X + enemy.game.levelLoader.getMario().boundingBoxSize.X / 2 - _location.X + enemy.Sprite.texture.size.X / 2));
            if (displacement < 300 && displacement > -300)
            {
                delay++;
                if (delay > 100)
                {
                    if (right)
                    {
                        enemy.game.levelLoader.list.Add(cannonballFactory.CreateCannonball(new Vector2(_location.X+ enemy.Sprite.texture.size.X , _location.Y + 10), (int)Sprite.eDirection.Right));
                    }
                    else
                    {
                        enemy.game.levelLoader.list.Add(cannonballFactory.CreateCannonball(new Vector2(_location.X , _location.Y + 10), (int)Sprite.eDirection.Left));
                    }
                    this.player = this.enemy.game.Content.Load<SoundEffect>("SoundEffects/50 - Gunshot");
                    this.player.Play();
                    delay = 0;
                }
            }
        }
        private void Follow()
        {
            if (enemy.game.levelLoader.getMario().position.X > _location.X + enemy.Sprite.texture.size.X / 2)
            {
                this.enemy.Sprite = new Sprite(new TextureDetails(this.enemy.game.Content.Load<Texture2D>("shotzoright"), 1), _location);
                right = true;
            }
            else
            {
                this.enemy.Sprite = new Sprite(new TextureDetails(this.enemy.game.Content.Load<Texture2D>("shotzoleft"), 1), _location);
                right = false;
            }
        }
        public override void HandleCollision(Collision collision, Entity collider)
        {
            Collision.Direction CollisionDirection = Collision.normalizeDirection(collision, this.enemy);
            //Avatar collision
            if (collider is Avatar)
            {
                if (CollisionDirection == Collision.Direction.Down)
                {
                    this.enemy.SquishGoombaStateChange();

                }
            }
        }
    }
}