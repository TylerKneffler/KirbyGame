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
            this.enemy.Sprite = new Sprite(new TextureDetails(this.enemy.game.Content.Load<Texture2D>("WaddleDeeFixed"), 2), location);
        }

        public override void HandleCollision(Collision collision, Entity collider)
        {
            if(collider is Avatar || (collider is Boomerang && ((Boomerang)collider).hurtKirby == false) || collider is Star || collider is AirPuff)
            {
                this.enemy.DeadStateChange();
            }
            else if(collider is Block && (collision.CollisionDirection == Collision.Direction.Right || collision.CollisionDirection == Collision.Direction.Left))
            {
                this.enemy.velocity.X = this.enemy.velocity.X * -1;

                if (this.enemy.Sprite.Direction == Sprite.eDirection.Right)
                {
                    this.enemy.Sprite.Direction = Sprite.eDirection.Left;
                }
                else
                {
                    this.enemy.Sprite.Direction = Sprite.eDirection.Right;
                }
            }
            else if (collider is SuckUp)
            {
                this.enemy.SuckStateChange((int)Collision.normalizeDirection(collision, collider));
            }
        }
    }

    class WaddleDooTest : EnemytypeTest
    {
        private LazerProjectileFactory factory;
        private int cooldown;
        private int delay;
        private float tempX;
        
        public WaddleDooTest(EnemyTest enemy, Vector2 location) : base(enemy)
        {
            this.enemy.Sprite = new Sprite(new TextureDetails(this.enemy.game.Content.Load<Texture2D>("WaddleDooFixed"), 2), location);
            factory = new LazerProjectileFactory(enemy.game);
            cooldown = 0;
            delay = 0;
        }

        public override void HandleCollision(Collision collision, Entity collider)
        {
            if (collider is Avatar || (collider is Boomerang && ((Boomerang)collider).hurtKirby == false) || collider is Star || collider is AirPuff)
            {
                this.enemy.DeadStateChange();
            }
            else if (collider is Block && (collision.CollisionDirection == Collision.Direction.Right || collision.CollisionDirection == Collision.Direction.Left))
            {
                this.enemy.velocity.X = this.enemy.velocity.X * -1;

                if (this.enemy.Sprite.Direction == Sprite.eDirection.Right)
                {
                    this.enemy.Sprite.Direction = Sprite.eDirection.Left;
                }
                else
                {
                    this.enemy.Sprite.Direction = Sprite.eDirection.Right;
                }
            }
            else if (collider is SuckUp)
            {
                this.enemy.SuckStateChange((int)Collision.normalizeDirection(collision, collider));
            }
        }

        public override void Update(GameTime gameTime)
        {
            int rangeCheck = enemy.game.levelLoader.getMario().position.X;

            base.Update(gameTime);
            if (this.enemy.position.X - rangeCheck <= 100 && this.enemy.position.X - rangeCheck <= 100 && cooldown == 0)
            {
                tempX = enemy.velocity.X;
                this.enemy.velocity.X = 0;
                if (this.enemy.position.X - rangeCheck <= 200 && this.enemy.position.X - rangeCheck >= 0 && cooldown == 0)
                {
                    Attack();
                    cooldown = 400;
                    delay = 30;
                }
                cooldown = 100;
                delay = 30;
            }
            if (cooldown != 0)
            {
                cooldown--;
                if (delay == 0)
                {
                    this.enemy.Sprite = new Sprite(new TextureDetails(this.enemy.game.Content.Load<Texture2D>("WaddleDooFixed"), 2), this.enemy.Sprite.location);
                    this.enemy.velocity.X = tempX;
                }
                delay--;
            }

        }
        private void Attack()
        {
            this.enemy.Sprite = new Sprite(new TextureDetails(this.enemy.game.Content.Load<Texture2D>("WaddleDooFixed"), new Rectangle(new Point(16, 0), new Point(16, 16)), 1), this.enemy.Sprite.location);
            this.enemy.velocity.X = 0;
            if (this.enemy.Sprite.Direction == Sprite.eDirection.Right)
            {
                enemy.game.levelLoader.list.Add(factory.CreateLazerProjectile(new Vector2((this.enemy.position.X - 16),(this.enemy.position.Y - 0)), 0, true));
                enemy.game.levelLoader.list.Add(factory.CreateLazerProjectile(new Vector2((this.enemy.position.X - 32), (this.enemy.position.Y - 16)), 0, true));
                enemy.game.levelLoader.list.Add(factory.CreateLazerProjectile(new Vector2((this.enemy.position.X - 48), (this.enemy.position.Y - 32)), 0, true));
                enemy.game.levelLoader.list.Add(factory.CreateLazerProjectile(new Vector2((this.enemy.position.X - 64), (this.enemy.position.Y - 48)), 0, true));
            }
            else
            {
                enemy.game.levelLoader.list.Add(factory.CreateLazerProjectile(new Vector2((this.enemy.position.X + 48), (this.enemy.position.Y - 0)), 0, true));
                enemy.game.levelLoader.list.Add(factory.CreateLazerProjectile(new Vector2((this.enemy.position.X + 64), (this.enemy.position.Y - 16)), 0, true));
                enemy.game.levelLoader.list.Add(factory.CreateLazerProjectile(new Vector2((this.enemy.position.X + 80), (this.enemy.position.Y - 32)), 0, true));
                enemy.game.levelLoader.list.Add(factory.CreateLazerProjectile(new Vector2((this.enemy.position.X + 96), (this.enemy.position.Y - 48)), 0, true));
            }
        }
    }

    class SirKibbleTest : EnemytypeTest
    {
        private BoomerangFactory factory;
        private int cooldown;
        private int delay;
        public SirKibbleTest(EnemyTest enemy, Vector2 location) : base(enemy)
        {
            this.enemy.Sprite = new Sprite(new TextureDetails(this.enemy.game.Content.Load<Texture2D>("SirKibbleFixed"), 2), location);
            this.enemy.velocity.X = 0;
            factory = new BoomerangFactory(this.enemy.game);
            cooldown = 0;
            delay = 0;
        }
        public override void HandleCollision(Collision collision, Entity collider)
        {
            if (collider is Avatar || (collider is Boomerang && ((Boomerang)collider).hurtKirby == false) || collider is Star || collider is AirPuff)
            {
                this.enemy.DeadStateChange();
            }
            else if (collider is Block && (collision.CollisionDirection == Collision.Direction.Right || collision.CollisionDirection == Collision.Direction.Left))
            {
                this.enemy.velocity.X = this.enemy.velocity.X * -1;

                if(this.enemy.Sprite.Direction == Sprite.eDirection.Right)
                {
                    this.enemy.Sprite.Direction = Sprite.eDirection.Left;
                }
                else
                {
                    this.enemy.Sprite.Direction = Sprite.eDirection.Right;
                }
                
            }

            else if (collider is SuckUp)
            {
                this.enemy.SuckStateChange((int)Collision.normalizeDirection(collision, collider));
            }
        }

        public override void Update(GameTime gameTime)
        {
            int rangeCheck = enemy.game.levelLoader.getMario().position.X;

            base.Update(gameTime);
            if(this.enemy.position.X - rangeCheck <= 200 && this.enemy.position.X - rangeCheck >= 0 && cooldown == 0)
            {
                throwCutter();
                cooldown = 400;
                delay = 30;
            }
            if(cooldown != 0)
            {
                cooldown--;
                if(delay == 0)
                {
                    this.enemy.Sprite = new Sprite(new TextureDetails(this.enemy.game.Content.Load<Texture2D>("SirKibbleFixed"), 2), this.enemy.Sprite.location);
                }
                delay--;
            }
            
        }

        private void throwCutter()
        {
            this.enemy.Sprite = new Sprite(new TextureDetails(this.enemy.game.Content.Load<Texture2D>("SirKibbleThrowFixed"), 1), this.enemy.Sprite.location);
            this.enemy.velocity.X = 0;
            enemy.game.levelLoader.list.Add(factory.CreateBoomerang(new Vector2(this.enemy.position.X, this.enemy.position.Y), 0, true));
        }
    }

    class AppleTest : EnemytypeTest
    {
        
        private bool hitGround = false;
        public AppleTest(EnemyTest enemy, Vector2 location) : base(enemy)
        {
            this.enemy.Sprite = new Sprite(new TextureDetails(this.enemy.game.Content.Load<Texture2D>("AppleFixed"), 4), location);
            this.enemy.seen = true;
            this.enemy.velocity.X = 0;
        }

        public override void HandleCollision(Collision collision, Entity collider)
        {
            if (collider is Avatar || (collider is Boomerang && ((Boomerang)collider).hurtKirby == false) || collider is Star || collider is AirPuff)
            {
                this.enemy.DeadStateChange();
            }
            else if (collider is SuckUp)
            {
                this.enemy.SuckStateChange((int)Collision.normalizeDirection(collision, collider));
            }
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
            texture = this.enemy.game.Content.Load<Texture2D>("WaddleDeeFixed");
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
            texture = this.enemy.game.Content.Load<Texture2D>("WaddleDooFixed");
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
            texture = this.enemy.game.Content.Load<Texture2D>("SirKibbleFixed");
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
            texture = this.enemy.game.Content.Load<Texture2D>("AppleFixed");
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
            texture = this.enemy.game.Content.Load<Texture2D>("WaddleDeeFixed");
            maxFrames = 2;
            currentFrame = 0;
            frameSize = new Point(texture.Width / maxFrames, texture.Height);
            Time = 0;
            Delay = 200;
            this.location = location;
            this.Direction = direction;
            

            if (Direction == 0)
            {
                this.enemy.acceleration = new Vector2(-1, 0);
                this.enemy.velocity.X = -1;

            }
            else if(Direction == 1)
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
            texture = this.enemy.game.Content.Load<Texture2D>("WaddleDooFixed");
            maxFrames = 2;
            currentFrame = 0;
            frameSize = new Point(texture.Width / maxFrames, texture.Height);
            Time = 0;
            Delay = 200;
            this.location = location;
            this.Direction = direction;


            if (Direction == 0)
            {
                this.enemy.acceleration = new Vector2(-1, 0);
                this.enemy.velocity.X = -1;

            }
            else if (Direction == 1)
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
            texture = this.enemy.game.Content.Load<Texture2D>("SirKibbleFixed");
            maxFrames = 2;
            currentFrame = 0;
            frameSize = new Point(texture.Width / maxFrames, texture.Height);
            Time = 0;
            Delay = 200;
            this.location = location;
            this.Direction = direction;


            if (Direction == 0)
            {
                this.enemy.acceleration = new Vector2(-1, 0);
                this.enemy.velocity.X = -1;

            }
            else if (Direction == 1)
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
            texture = this.enemy.game.Content.Load<Texture2D>("AppleFixed");
            maxFrames = 2;
            currentFrame = 0;
            frameSize = new Point(texture.Width / maxFrames, texture.Height);
            Time = 0;
            Delay = 200;
            this.location = location;
            this.Direction = direction;


            if (Direction == 0)
            {
                this.enemy.acceleration = new Vector2(-1, 0);
                this.enemy.velocity.X = -1;

            }
            else if (Direction == 1)
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
                    //Debug.WriteLine("Collision Detected!");
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
                    //Debug.WriteLine("Collision Detected!");
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
                //Debug.WriteLine("Velocity is " + this.enemy.velocity.X);
                if (CollisionDirection == Collision.Direction.Right && this.enemy.velocity.X == 0)
                {
                   // Debug.WriteLine("Increasing velocity");
                    base.enemy.velocity.X = -2;
                }
                else if (CollisionDirection == Collision.Direction.Left && base.enemy.velocity.X == 0)
                {
                    //Debug.WriteLine("Increasing velocity");
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
        private Vector2 _location;
        private readonly CannonballFactory cannonballFactory;

        private bool right = true;
        public ShotzoTest(EnemyTest enemy, Vector2 location,Game1 game) : base(enemy)
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
            int displacement = (int)(enemy.game.levelLoader.getMario().position.X - _location.X - 5) ;
            Console.WriteLine("shotzo" + displacement);


            if (displacement < 300 && displacement > -300)
            {
                delay++;
                if (delay > 100)
                {
                    if (right)
                    {
                        enemy.game.levelLoader.list.Add(cannonballFactory.CreateCannonball(new Vector2(_location.X + enemy.Sprite.texture.size.X, _location.Y + 9), (int)Sprite.eDirection.Right));

                    }
                    else
                    {
                        enemy.game.levelLoader.list.Add(cannonballFactory.CreateCannonball(new Vector2(_location.X, _location.Y + 9), (int)Sprite.eDirection.Left));
                    }
                    enemy.game.player.PlayShotSound();
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
        }
    }

    class WhispyWoods : EnemytypeTest
    {
        private EnemyFactoryTest factory;
        private int cooldown;
        private int delay;
        public int life = 5;

        Random rnd = new Random();

        public WhispyWoods(EnemyTest enemy, Vector2 location) : base(enemy)
        {
            this.enemy.Sprite = new Sprite(new TextureDetails(this.enemy.game.Content.Load<Texture2D>("WhispyWoods"),new Rectangle(new Point(0,0),new Point(24,80)), 1), location);
            this.enemy.velocity.X = 0;
            factory = new EnemyFactoryTest(this.enemy.game);
            cooldown = 500;
            delay = 0;
            
        }
        public override void HandleCollision(Collision collision, Entity collider)
        {
            if ((collider is Boomerang && ((Boomerang)collider).hurtKirby == false) || (collider is LazerProjectile && ((LazerProjectile)collider).hurtKirby == false) || collider is Star || collider is AirPuff && delay == 0)
            {
                this.enemy.Sprite = new Sprite(new TextureDetails(this.enemy.game.Content.Load<Texture2D>("WhispyWoods"), new Rectangle(new Point(24, 0), new Point(24, 80)), 1), new Vector2(enemy.X, enemy.Y));
                TakeDamage();
                delay = 10;
            }
        }

        public override void Update(GameTime gameTime)
        {
            int rangeCheck = enemy.game.levelLoader.getMario().position.X;
            
            this.enemy.acceleration.Y = 0;
            this.enemy.velocity.Y = 0;
            base.Update(gameTime);
            if (cooldown == 10)
            {
                this.enemy.Sprite = new Sprite(new TextureDetails(this.enemy.game.Content.Load<Texture2D>("WhispyWoods"), new Rectangle(new Point(24, 0), new Point(24, 80)), 1), new Vector2(enemy.X, enemy.Y));
            }
            if (cooldown == 0 && this.enemy.position.X - rangeCheck <= 600 && this.enemy.position.X - rangeCheck >= -600)
            {
                Attack();
                Attack();
                Attack(); 
                this.enemy.Sprite = new Sprite(new TextureDetails(this.enemy.game.Content.Load<Texture2D>("WhispyWoods"), new Rectangle(new Point(0, 0), new Point(24, 80)), 1), new Vector2(enemy.X, enemy.Y));
                cooldown = 200;
            }
            cooldown--;
            delay--;
        }


        public void TakeDamage()
        {
            life--;
            if (life <= 0)
            {
                this.enemy.DeadStateChange();
            }
        }

        public void Attack()
        {
            this.enemy.game.levelLoader.list.Add(factory.createEnemy(EnemyTest.enemytypes.APPLE, new Vector2(this.enemy.X - rnd.Next(50,400), this.enemy.Y - rnd.Next(100, 200))));
        }
    }

    class DeadWhispyWoods : EnemytypeTest
    {

        Random rnd = new Random();

        public DeadWhispyWoods(EnemyTest enemy, Vector2 location) : base(enemy)
        {
            this.enemy.Sprite = new Sprite(new TextureDetails(this.enemy.game.Content.Load<Texture2D>("WhispyWoods"), new Rectangle(new Point(92, 0), new Point(24, 80)), 1), location);
            this.enemy.velocity.X = 0;

        }

        public override void Update(GameTime gameTime)
        {
            int rangeCheck = enemy.game.levelLoader.getMario().position.X;

            this.enemy.acceleration.Y = 0;
            this.enemy.velocity.Y = 0;
            base.Update(gameTime);
        }


    }
}