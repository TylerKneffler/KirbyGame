using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System;
using Microsoft.Xna.Framework.Audio;

namespace KirbyGame
{
    class EnemyTest : Entity
    {
        public EnemytypeTest enemytype;
        public int type;
        public Boolean seen = false;
        public Boolean dead = false;
        public Boolean updateNull = true;
        Sprite.eDirection direction;
        private SoundEffect player;


        public enum enemytypes
        {
            GOOMBA, KOOPA, KOOPA_SHELL, DEAD_KOOPA, DEAD_SHELL, DEAD_GOOMBA, SQUISH_GOOMBA, PARANA, DEAD_PARANA, SHOTZO, WADDLE_DEE, WADDLE_DOO, SIR_KIBBLE, APPLE,
            DEAD_WADDLE_DEE, DEAD_WADDLE_DOO, DEAD_SIR_KIBBLE, DEAD_APPLE, SUCK_WADDLE_DEE, SUCK_WADDLE_DOO, SUCK_SIR_KIBBLE, SUCK_APPLE , WHISPYWOODS, DEADWHISPYWOODS
        }

        public EnemyTest(enemytypes enemyType, Vector2 location, Game1 game)
        {

            this.type = (int)enemyType;
            this.game = game;
            seen = false;
            defaultColor = Color.Red;
            boundingColor = defaultColor;
            direction = Sprite.eDirection.Left;
            this.velocity.X = -1;
            this.velocity.Y = 0;

            if (enemyType == enemytypes.GOOMBA)
            {
                enemytype = new GoombaTest(this, location); 
            }
            else if (enemyType == enemytypes.KOOPA)
            {
                enemytype = new KoopaTest(this, location, direction); 
            }
            else if (enemyType == enemytypes.SHOTZO)
            {
                enemytype = new ShotzoTest(this, location, direction, game);
            }
            else if (enemyType == enemytypes.DEAD_PARANA)
            {
                enemytype = new DeadParana(this, location);
            }
            else if (enemyType == enemytypes.KOOPA_SHELL)
            {
                enemytype = new KoopaShellTest(this, location, direction); 
            }
            else if (enemyType == enemytypes.DEAD_KOOPA)
            {
                enemytype = new DeadKoopaTest(this, location); 
            }
            else if (enemyType == enemytypes.DEAD_SHELL)
            {
                enemytype = new DeadKoopaShellTest(this, location);
            }
            else if (enemyType == enemytypes.DEAD_GOOMBA)
            {
                enemytype = new DeadGoombaTest(this, location);
            }
            else if (enemyType == enemytypes.SQUISH_GOOMBA)
            {
                enemytype = new SquishGoombaTest(this, location);
            }
            else if (enemyType == enemytypes.WADDLE_DEE)
            {
                enemytype = new WaddleDeeTest(this, location);
            }
            else if (enemyType == enemytypes.WADDLE_DOO)
            {
                enemytype = new WaddleDooTest(this, location);
            }
            else if (enemyType == enemytypes.SIR_KIBBLE)
            {
                enemytype = new SirKibbleTest(this, location);
            }
            else if (enemyType == enemytypes.APPLE)
            {
                enemytype = new AppleTest(this, location);
            }
            else if (enemyType == enemytypes.WHISPYWOODS)
            {
                enemytype = new WhispyWoods(this, location);
            }

        }

        public void DeadGoombaStateChange()
        {
            if (type == 0)
            {
                type = 5;
                this.enemytype = new DeadGoombaTest(this, new Vector2(this.X, this.Y));
                this.dead = true;
            }
            this.player = this.game.Content.Load<SoundEffect>("SoundEffects/smb_stomp");
            this.player.Play();
        }

        public void SquishGoombaStateChange()
        {
            if (type == 0)
            {
                type = 6;
                this.enemytype = new SquishGoombaTest(this, new Vector2(this.X, this.Y));
            }
            this.player = this.game.Content.Load<SoundEffect>("SoundEffects/smb_stomp");
            this.player.Play();
        }
        public virtual void DeadKoopaStateChange()
        {
            if (type == 1)
            {
                type = 3;
                this.enemytype = new DeadKoopaTest(this, new Vector2(this.X, this.Y));
            }
            this.player = this.game.Content.Load<SoundEffect>("SoundEffects/smb_stomp");
            this.player.Play();
        }
        public virtual void DeadKoopaShellStateChange()
        {

            if (type == 2)
            {
                type = 4;
                this.enemytype = new DeadKoopaShellTest(this, new Vector2(this.X, this.Y));
            }
            this.player = this.game.Content.Load<SoundEffect>("SoundEffects/smb_stomp");
            this.player.Play();
        }

        public virtual void KoopaStateChange()
        {

            if (type == 1)
            {
                type = 2;
                this.enemytype = new KoopaShellTest(this, new Vector2(this.X, this.Y), this.direction);
                this.velocity.X = 0;
            }
            this.player = this.game.Content.Load<SoundEffect>("SoundEffects/smb_stomp");
            this.player.Play();
        }

        //Need a way to assign the sprite direction
        public virtual void ShellStateChange()
        {
 
            if (type == 2)
            {
                type = 1;
                this.enemytype = new KoopaTest(this, new Vector2(this.X, this.Y - 19), this.direction);
            }
        }

        public virtual void DeadParanaStateChange()
        {

            if (type == 7)
            {
                type = 8;
                this.velocity.Y = -4;
                this.enemytype = new DeadParana(this, new Vector2(this.X, this.Y - 40));
            }
        }

        //Handling all dead state changes in one method
        public virtual void DeadStateChange()
        {
            if(type == 10)
            {
                type = 14;
                this.enemytype = new DeadWaddleDeeTest(this, new Vector2(this.X, this.Y - 19));
            }
            else if(type == 11)
            {
                type = 15;
                this.enemytype = new DeadWaddleDooTest(this, new Vector2(this.X, this.Y - 19));
            }
            else if(type == 12)
            {
                type = 16;
                this.enemytype = new DeadSirKibbleTest(this, new Vector2(this.X, this.Y - 19));
            }
            else if(type == 13)
            {
                type = 17;
                this.enemytype = new DeadAppleTest(this, new Vector2(this.X, this.Y - 19));
            }
            else if (type == 22)
            {
                type = 23;
                this.enemytype = new DeadWhispyWoods(this, new Vector2(this.X, this.Y - 19));
            }
        }

        public virtual void SuckStateChange(int direction)
        {
            if (type == 10)
            {
                type = 18;
                this.enemytype = new SuckWaddleDeeTest(this, new Vector2(this.X, this.Y /*- 19*/), direction);
            }
            else if (type == 11)
            {
                type = 19;
                this.enemytype = new SuckWaddleDooTest(this, new Vector2(this.X, this.Y), direction);
            }
            else if (type == 12)
            {
                type = 20;
                this.enemytype = new SuckSirKibbleTest(this, new Vector2(this.X, this.Y), direction);
            }
            else if (type == 13)
            {
                type = 21;
                this.enemytype = new SuckAppleTest(this, new Vector2(this.X, this.Y), direction);
            }

        }


        public override void HandleCollision(Collision collision, Entity collider)
        {
            //Apple is special case, need to make it's velocity be -4 if it hits a block
            if (collider is Block && collision.CollisionDirection == Collision.Direction.Up)
            {
                if (collision.CollisionDirection == Collision.Direction.Up && !(((Block)collider).blocktype is HiddenBlock) &&
                    !(((Block)collider).blocktype is BrokenBrickBlock) && !(((Block)collider).blocktype is Castle))
                {
                    velocity.Y = 0;
                    acceleration.Y = 0;
                    Y = collider.BoundingBox.Top - this.BoundingBox.Height;
                }
                if(enemytype is AppleTest)
                {
                    velocity.X = -4;
                }

            } else
            {
                enemytype.HandleCollision(collision, collider);
            }

        }

        public override void Update(GameTime gameTime)
        {
            if (seen)
            {
                enemytype.Update(gameTime);
                base.Update(gameTime);
                if (velocity.Y == 0 && updateNull)
                {
                    acceleration.Y = 1;
                }
                else { 
                    acceleration.Y = AvatarData.GRAVITY;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!(enemytype is DeadKoopaTest || enemytype is DeadKoopaShellTest || enemytype is SquishGoombaTest))
            {
                base.Draw(spriteBatch);
            }
            else
            {
               enemytype.Draw(spriteBatch);    
            }
        }

        public int Points()
        {
            return enemytype.Points();
        }

    }
}
