﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System;
using Microsoft.Xna.Framework.Audio;

namespace KirbyGame
{
    class EnemyTest : Entity/*, IPointable*/
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
            DEAD_WADDLE_DEE, DEAD_WADDLE_DOO, DEAD_SIR_KIBBLE, SUCK_WADDLE_DEE, SUCK_WADDLE_DOO, SUCK_SIR_KIBBLE, SUCK_APPLE
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


        public override void HandleCollision(Collision collision, Entity collider)
        {

            if (collider is Block && collision.CollisionDirection == Collision.Direction.Up && !(this.enemytype is ParanaTest))
            {
                if (collision.CollisionDirection == Collision.Direction.Up && !(((Block)collider).blocktype is HiddenBlock) &&
                    !(((Block)collider).blocktype is BrokenBrickBlock) && !(((Block)collider).blocktype is Castle))
                {
                    velocity.Y = 0;
                    acceleration.Y = 0;
                    Y = collider.BoundingBox.Top - this.BoundingBox.Height;
                }

            }   else 
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
            if (!(enemytype is DeadGoombaTest || enemytype is DeadKoopaTest || enemytype is DeadKoopaShellTest || enemytype is SquishGoombaTest))
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
