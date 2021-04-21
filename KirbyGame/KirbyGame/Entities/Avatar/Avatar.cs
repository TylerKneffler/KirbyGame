using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using System.Media;
using Microsoft.Xna.Framework.Audio;

namespace KirbyGame
{


    public class Avatar : Entity
    {
        public SwallowState swallowed;
        public Vector2 startingLocation;
        private KirbySpriteFactory factory;
        //public int numLives;
        private SoundEffect player;
        private int _damageTimer;
        private int _colorTimer;

        public event EventHandler<Collision> CollisionEvent;
        public event EventHandler KirbyHurt;
        public event EventHandler<Stats.ePower> PowerUpChange;

        public bool IsDead { get; set; }        
        public Avatar(Game1 game, Vector2 location) : base(Color.Yellow)
        {
            this.game = game;
            this.startingLocation = location;

            factory = new KirbySpriteFactory(this);

            swallowed = new EmptySwallowState(this);
            this.Sprite = factory.createSprite(swallowed, location, Sprite.eDirection.Right);

            //numLives = AvatarData.INIT_NUM_LIVES;
            IsDead = false;
            _colorTimer = 0;
            _damageTimer = 0;
        }

        public Avatar(Avatar avatar) : base(Color.Yellow)
        {
            this.game = avatar.game;
            this.startingLocation = new Vector2(avatar.X, avatar.Y);

            this.UpdateSprite();
        }
        

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            swallowed.Update(gameTime);
                
            DamageColorUpdate(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            swallowed.Draw(spriteBatch);
        }

        public bool canBreakBlocks()
        {
            return false;
        }
        public void MarioTeleport(Point final)
        {
            this.position = final;
        }
        public void StageChange(int stage) 
        {
            if(stage == 0)
            {
                this.X = 69 * 32;
                this.Y = 6;
                this.game.camera.Limits = new Rectangle(new Point(64 * 32 -16, -32), new Point(78 * 32, this.game.graphics.PreferredBackBufferHeight - 64));
            }
            if (stage == 1)
            {
                this.X = 145 * 32;
                this.Y = 6;
                this.game.camera.Limits = new Rectangle(new Point(144 * 32 - 18, -32), new Point(0, this.game.graphics.PreferredBackBufferHeight - 64));
            }
        } 
        /**
         * avatar commands (setting to small, reg, fire are done directly using state getters and setters and as such included in next section). 
         */
        #region Commands
        public void pressFloat()
        {
            swallowed.PressFloat();
        }

        public void releaseFloat()
        {
            swallowed.ReleaseFloat();
        }

        public void pressJump()
        {
            swallowed.PressJump();
        }
        public void pressDown()
        {
            swallowed.PressDown();
        }
        public void pressRight()
        {
            swallowed.PressRight();
        }
        public void pressLeft()
        {
            swallowed.PressLeft();
        }

        public void releaseJump()
        {

            swallowed.ReleaseJump();
        }

        public void releaseDown()
        {
            swallowed.ReleaseDown();
        }

        public void releaseRight()
        {
            swallowed.ReleaseRight();
        }

        public void releaseLeft()
        {
            swallowed.ReleaseLeft();
        }

        public void Trigger()
        {
            swallowed.Trigger();
        }

        public void releaseTrigger()
        {
            swallowed.ReleaseTrigger();
        }

        //not sure what this does 
        /*public void HitBlock()
        {
            swallowed.Down();
        }*/

        public void UpdateSprite()
        {
            this.Sprite = factory.createSprite(swallowed, new Vector2(X, Y), Sprite.Direction);
        }
        #endregion
        //UNTESTED
        public override void HandleCollision(Collision collision, Entity collider)
        {
            OnCollisionEvent(collision);
            Collision.Direction CollisionDirection = Collision.normalizeDirection(collision, this);
            if(collider is Block)
            {
                if(CollisionDirection is Collision.Direction.Up && !(((Block)collider).blocktype is HiddenBlock))
                {
                    velocity.Y = 0;
                    acceleration.Y = 0;
                    Y = collider.Y - this.BoundingBox.Height;
                    swallowed.HandleBlockCollision(collision);
                    game.player.PlayLandSound();
                }
                else if (CollisionDirection is Collision.Direction.Down && !(((Block)collider).blocktype is StairBlock))
                {
                    swallowed.HandleBlockCollision(collision);
                    if (((Block)collider).blocktype is HiddenBlock)
                    {
                        StageChange(((Block)collider).stage);
                    }
                    Y = collider.Y + collider.BoundingBox.Height;
                } else if (CollisionDirection is Collision.Direction.Right && !(((Block)collider).blocktype is StairBlock) && !(((Block)collider).blocktype is HiddenBlock))
                {
                    swallowed.HandleBlockCollision(collision);
                    X = collider.BoundingBox.Right;
                } else if (CollisionDirection is Collision.Direction.Left && !(((Block)collider).blocktype is StairBlock) && !(((Block)collider).blocktype is HiddenBlock))
                {
                    swallowed.HandleBlockCollision(collision);
                    X = collider.BoundingBox.Left - this.BoundingBox.Width;
                }

            }

            if (collider is EnemyTest)
            {
                
                TakeDamage();
                if (((EnemyTest)collider).enemytype is WhispyWoods || ((EnemyTest)collider).enemytype is DeadWhispyWoods)
                {
                    if (CollisionDirection is Collision.Direction.Up)
                    {
                        velocity.Y = 0;
                        acceleration.Y = 0;
                        Y = collider.Y - this.BoundingBox.Height;
                        swallowed.HandleBlockCollision(collision);
                        game.player.PlayLandSound();
                    }
                    else if (CollisionDirection is Collision.Direction.Down)
                    {

                        swallowed.HandleBlockCollision(collision);
                        Y = collider.Y + collider.BoundingBox.Height;
                    }
                    else if (CollisionDirection is Collision.Direction.Right)
                    {
                        swallowed.HandleBlockCollision(collision);
                        X = collider.BoundingBox.Right;
                    }
                    else if (CollisionDirection is Collision.Direction.Left)
                    {
                        swallowed.HandleBlockCollision(collision);
                        X = collider.BoundingBox.Left - this.BoundingBox.Width;
                    }
                }
                if (((EnemyTest)collider).enemytype is ShotzoTest)
                {

                    if (CollisionDirection is Collision.Direction.Up)
                    {
                        velocity.Y = 0;
                        acceleration.Y = 0;
                        Y = collider.Y - this.BoundingBox.Height;
                        swallowed.HandleBlockCollision(collision);
                        game.player.PlayLandSound();
                    }
                    else if (CollisionDirection is Collision.Direction.Down) 
                    { 

                        swallowed.HandleBlockCollision(collision);
                    Y = collider.Y + collider.BoundingBox.Height;
                    }
                    else if (CollisionDirection is Collision.Direction.Right)
                    {
                        swallowed.HandleBlockCollision(collision);
                        X = collider.BoundingBox.Right;
                    }
                    else if (CollisionDirection is Collision.Direction.Left)
                    {
                        swallowed.HandleBlockCollision(collision);
                        X = collider.BoundingBox.Left - this.BoundingBox.Width;
                    }
            
                }

                if (CollisionDirection is Collision.Direction.Up)
                {
                        Y = collider.Y - this.BoundingBox.Height;
                }
                else if (CollisionDirection is Collision.Direction.Down)
                {
                        Y = collider.Y + collider.BoundingBox.Height;
                }
                else if (CollisionDirection is Collision.Direction.Right)
                {
                        X = collider.BoundingBox.Right;
                }
                else if (CollisionDirection is Collision.Direction.Left)
                {
                        X = collider.BoundingBox.Left - this.BoundingBox.Width;
                }

            }
        }

        public void TakeDamage()
        {
            if(_damageTimer == 0) { 
            OnTakeDamage(EventArgs.Empty);
            this.ClearPowerUp();
            _damageTimer = 1000;
                }
        }

        public void ClearPowerUp()
        {
            swallowed.powerUp = null;
        }

        protected virtual void OnCollisionEvent(Collision collision)
        {
            CollisionEvent?.Invoke(this, collision);
        }

        public virtual void OnPowerUpChange(Stats.ePower power)
        {
            PowerUpChange?.Invoke(this, power);
        }

        public void DamageColorUpdate(GameTime gameTime)
        {
            //oh yeah there is a better way to do this but running out of time
            if (_damageTimer > 0)
            {
                _damageTimer -= gameTime.ElapsedGameTime.Milliseconds;
                _colorTimer -= gameTime.ElapsedGameTime.Milliseconds;
                if (_colorTimer < 0)
                {
                    if (Sprite.texture.currentColor == Color.White)
                    {
                        Sprite.texture.currentColor = Color.Magenta;
                    }
                    else
                    {
                        Sprite.texture.currentColor = Color.White;
                    }
                    _colorTimer = 250;
                }
                if (_damageTimer <= 0)
                {
                    _damageTimer = 0;
                    _colorTimer = 0;
                    Sprite.texture.currentColor = Color.White;
                }
            } else
            {
                Sprite.texture.currentColor = Color.White;
            }
        }

        public virtual void OnTakeDamage(EventArgs e)
        {
            KirbyHurt?.Invoke(this, e);
        }
    }
}
