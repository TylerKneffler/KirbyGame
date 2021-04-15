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


    public class
        Avatar : Entity
    {
        public PowerState powerState;
        public ActionState actionState;
        private marioState combinedState;
        public Vector2 startingLocation;
        private MarioSpriteFactory factory;
        private CannonballFactory cannonBallFactory;
        //public int fireBallNum = 0;
        //public int numLives;
        private SoundEffect player;
       

        public bool IsDead { get; set; }

        //public event collisionPointsDelegate collideWithPoints;
        public enum marioState
        {
            SMALL_IDLE, SMALL_JUMPING, SMALL_RUNNING, SMALL_FALLING, SMALL_CROUCHING,
            SUPER_IDLE, SUPER_JUMPING, SUPER_RUNNING, SUPER_FALLING, SUPER_CROUCHING,
            FIRE_IDLE, FIRE_JUMPING, FIRE_RUNNING, FIRE_FALLING, FIRE_CROUCHING,
            DEAD
        }
        public Avatar(Game1 game, Vector2 location) : base(Color.Yellow)
        {
            this.game = game;
            this.startingLocation = location;

            factory = new MarioSpriteFactory(this);
            cannonBallFactory = new CannonballFactory(game);
            powerState = new MarioSmallState(this);
            actionState = new MarioIdleState(this);
            actionState.IdleTransition();
            powerState.SmallTransition();
            base.Sprite.Direction = Sprite.eDirection.Right;
            //numLives = AvatarData.INIT_NUM_LIVES;
            IsDead = false;
        }

        public Avatar(Avatar avatar) : base(Color.Yellow)
        {
            this.game = avatar.game;
            this.startingLocation = new Vector2(avatar.X, avatar.Y);

            factory = new MarioSpriteFactory(this);
            cannonBallFactory = new CannonballFactory(game);
            powerState = avatar.powerState;
            actionState = avatar.actionState;
            this.updateState();
        }
        

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            actionState.Update(gameTime);
            if (this.BoundingBox.Bottom > this.game.gameBounds.Y + TileMap.CELL_SIZE)
            {
                this.powerState.DeadTransition();
                //this.actionState.IdleTransition();
                this.Y = (int)this.game.gameBounds.Y +TileMap.CELL_SIZE- this.BoundingBox.Size.Y;
            }
            if (this.game.Hud.testTimer.TimeLeft == 0 && IsDead == false)
            {
                this.powerState.DeadTransition();
            }
            Debug.WriteLine(this.X);

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public bool canBreakBlocks()
        {
            return (!(powerState is MarioSmallState || powerState is MarioDeadState));
        }
        public void MarioTeleport(Point final)
        {
            this.position = final;
        }

        /**
         * avatar commands (setting to small, reg, fire are done directly using state getters and setters and as such included in next section). 
         */
        #region Commands
        public void pressUp()
        {
            actionState.Up();
        }
        public void pressDown()
        {
            actionState.Down();
        }
        public void pressRight()
        {
            actionState.Right();
        }
        public void pressLeft()
        {
            actionState.Left();
        }

        public void releaseUp()
        {

            actionState.releaseUp();
        }

        public void releaseDown()
        {
            actionState.releaseDown();
        }

        public void releaseRight()
        {
            actionState.releaseRight();
        }
        public void marioFireBall()
        {
            if (/*fireBallNum < 2 && */(combinedState == marioState.FIRE_CROUCHING || combinedState == marioState.FIRE_FALLING || combinedState == marioState.FIRE_IDLE || combinedState == marioState.FIRE_JUMPING || combinedState == marioState.FIRE_RUNNING))
            {
                if (Sprite.Direction == Sprite.eDirection.Right)
                {
                    //fireBallNum++;
                    game.levelLoader.list.Add(cannonBallFactory.CreateCannonball(new Vector2(base.position.X + 16, base.position.Y), (int)Sprite.Direction));
                }
                else
                {
                    //fireBallNum++;
                    game.levelLoader.list.Add(cannonBallFactory.CreateCannonball(new Vector2(base.position.X - 16, base.position.Y), (int)Sprite.Direction));
                }
            }
        }

        public void releaseLeft()
        {
            actionState.releaseLeft();
        }

        public void setStateSmall()
        {
            powerState.SmallTransition();
        }

        public void setStateSuper()
        {
            powerState.SuperTransition();
        }

        public void setStateFire()
        {
            powerState.FireTransition();
        }

        public void TakeDamage()
        {
            powerState.TakeDamage();
        }

        //not sure what this does 
        public void HitBlock()
        {
            actionState.Down();
        }

        #endregion
        //UNTESTED
        public override void HandleCollision(Collision collision, Entity collider)
        {
            Collision.Direction CollisionDirection = Collision.normalizeDirection(collision, this);
            if (!(this.actionState is MarioTransitioningState))
            {
                collision.A.Timer = AvatarData.BOUNDING_BOX_COLLISION_TIMER;
                collision.B.Timer = AvatarData.BOUNDING_BOX_COLLISION_TIMER;
                collision.A.boundingColor = Color.Orange;
                collision.B.boundingColor = Color.Orange;
                {

                }
                /*Debug.WriteLine("Avatar velocityX: "+velocity.X);
                Debug.WriteLine("Avatar velocityY: " + velocity.Y);
                Debug.WriteLine("Avatar Top: " + BoundingBox.Top);
                Debug.WriteLine("Block Bottom: " + collision.B.BoundingBox.Bottom);*/
                if (collider is Block && !(((Block)collider).blocktype is BrokenBrickBlock) && !(((Block)collider).blocktype is Castle))
                {
                    //Added to fix when Mario occasionaly gets stopped on a brick when walking. Haven't fully tested, but it seems to work better with this if statement!
                    if ((collision.CollisionDirection == Collision.Direction.Left || collision.CollisionDirection == Collision.Direction.Right) && collision.collidedFromTop)
                    {
                        collision.CollisionDirection = Collision.Direction.Up;
                        Debug.WriteLine("MARIO CHANGED!!");
                    }

                    if (CollisionDirection == Collision.Direction.Down)
                    {
                        actionState.HandleBlockCollision(collision);
                        Y = collider.BoundingBox.Bottom;
                    }
                    else if (CollisionDirection == Collision.Direction.Up && !(((Block)collider).blocktype is HiddenBlock))
                    {
                        if (((Block)collider).blocktype is PipeTeleportTop && this.actionState is MarioCrouchingState)
                        {
                            this.X = collider.X + AvatarData.PIPE_TRANSITION_UP_ADJUST;
                            Transition transition = new PipeTopTransition(this);
                            transition.teleport = ((Block)collider).blocktype.Teleportation;
                            this.actionState.TransitioningTransition(transition);
                        }else if (((Block)collider).blocktype is PipeTeleportTopBonus && this.actionState is MarioCrouchingState)
                        {
                            this.X = collider.X + AvatarData.PIPE_TRANSITION_UP_ADJUST;
                            Transition transition = new PipeTopTransitionBonus(this);
                            transition.teleport = ((Block)collider).blocktype.Teleportation;
                            this.actionState.TransitioningTransition(transition);
                        }else
                        {
                            actionState.HandleBlockCollision(collision);
                            velocity.Y = 0;
                            acceleration.Y = 0;
                            Y = collider.BoundingBox.Top - this.BoundingBox.Height;
                        }

                    }
                    else if (CollisionDirection == Collision.Direction.Right && !(((Block)collider).blocktype is HiddenBlock))
                    {
                         actionState.HandleBlockCollision(collision);
                         X = collider.BoundingBox.Right;
                    }
                    else if (CollisionDirection == Collision.Direction.Left && !(((Block)collider).blocktype is HiddenBlock))
                    {
                        if (((Block)collider).blocktype is PipeTeleportSide /*&& this.actionState is MarioCrouchingState */)
                        {
                            if (this.powerState is MarioSmallState)
                                this.Y = collider.Y + AvatarData.PIPE_TRANSITION_SIDE_ADJUST;
                            else
                                this.Y = collider.Y;

                            Transition transition = new PipeSideTransitionBonus(this);
                            transition.teleport = ((Block)collider).blocktype.Teleportation;
                            this.actionState.TransitioningTransition(transition);
                        }
                        else
                        {
                            actionState.HandleBlockCollision(collision);
                            X = collider.BoundingBox.Left - this.BoundingBox.Width;
                        }
                    }
                }
                else if (collider is Item)
                {
                    Console.WriteLine("colliderremove :" + collider.remove);

                    if (collider is SuperMushroom && this.powerState is MarioSmallState)
                    {
                        this.setStateSuper();
                    }
                    else if (collider is SuperMushroom && !(this.powerState is MarioSmallState))
                    {
                        //do nothing for now, eventually this will be add points 
                    }
                    else if (collider is FireFlower && !(this.powerState is MarioFireState))
                    {
                        this.setStateFire();
                    }
                    else if (collider is FireFlower && this.powerState is MarioFireState)
                    {
                        //do nothing for now, eventually this will add points
                    }
                    else if (collider is Coin)
                    {
                        //nothing for now, eventually will add to coin count
                    }
                    else if (collider is OneUpMushroom && game.levelLoader.list.Contains(collision.B) /*(collision.B is OneUpMushroom && collision.B.remove == false)*/)
                     
                    {

                        //numLives++;
                        //collider.remove = false;
                        //nothing for now, eventually will add to life count
                    }
                    else if (collider is Star)
                    {
                        //eventually will make mario temporarily invincible
                    }

                }
                /*else if(collision.B is Enemy)
                {
                    if(collision.CollisionDirection == Collision.Direction.Up)
                    {
                        this.actionState.IdleTransition();
                    } else
                    {
                        this.actionState.IdleTransition();
                        this.powerState.TakeDamage();
                    } 
                }*/

                //New enemy collision detection
                else if (collider is EnemyTest)
                {
                    //Colliding with enemy from above, kill enemy
                    if (CollisionDirection == Collision.Direction.Up && !(((EnemyTest)collider).enemytype is KoopaShellTest))
                    {

                        if (CollisionDirection == Collision.Direction.Up && (((EnemyTest)collider).enemytype is ParanaTest))
                        {
                            this.actionState.IdleTransition();
                            this.powerState.TakeDamage();
                        }
                        else
                        {
                            this.actionState.FallingTransition();
                            this.velocity.Y = AvatarData.ENEMY_BOUNCE_VELOCITY;
                        }
                    }
                    //If collision is from left right or down and it's not a koopa shell, take damage.
                    else if ((CollisionDirection == Collision.Direction.Left || CollisionDirection == Collision.Direction.Right ||
                        CollisionDirection == Collision.Direction.Down) && !(((EnemyTest)collider).enemytype is KoopaShellTest))
                    {
                        this.actionState.IdleTransition();
                        this.powerState.TakeDamage();
                    }
                    //If collision is from koopa shell and the direction isn't from above, AND the shell is moving: take damage
                    else if ((((EnemyTest)collider).enemytype is KoopaShellTest) && (collider.velocity.X != 0) && !(CollisionDirection == Collision.Direction.Up))
                    {
                        this.actionState.IdleTransition();
                        this.powerState.TakeDamage();
                    }
                    else if (((EnemyTest)collider).enemytype is KoopaShellTest && collider.velocity.X == 0 && CollisionDirection == Collision.Direction.Left)
                    {
                        X = collider.BoundingBox.Left - 18;
                        collider.X += 5;
                        collider.velocity.X = 5;
                        this.player = this.game.Content.Load<SoundEffect>("SoundEffects/smb_kick");
                        this.player.Play();
                    }
                    else if (((EnemyTest)collider).enemytype is KoopaShellTest && collider.velocity.X == 0 && CollisionDirection == Collision.Direction.Right)
                    {
                        X = collider.BoundingBox.Right + 18;
                        collider.X -= 5;
                        collider.velocity.X = -5;
                        this.player = this.game.Content.Load<SoundEffect>("SoundEffects/smb_kick");
                        this.player.Play();
                    }
                    else if (((EnemyTest)collider).enemytype is KoopaShellTest && collider.velocity.X != 0 && CollisionDirection == Collision.Direction.Up)
                    {
                        this.actionState.FallingTransition();
                        this.velocity.Y = AvatarData.ENEMY_BOUNCE_VELOCITY;
                        collider.velocity.X = 0;
                    }
                }
                else if (collider is Cannonball)
                {
                    this.TakeDamage();



                }
                
            }
            //if(collider is IPointable)
            //{
            //    this?.collideWithPoints((IPointable)collider, CollisionDirection);
            //}

        }
        //helper method to update combined state enum on state change
        public void updateState()
        {
            if(this.powerState is MarioSmallState)
            {
                if (this.actionState is MarioIdleState)
                {
                    this.combinedState = marioState.SMALL_IDLE;
                }
                else if (this.actionState is MarioJumpingState)
                {
                    this.combinedState = marioState.SMALL_JUMPING;
                }
                else if (this.actionState is MarioRunningState)
                {
                    this.combinedState = marioState.SMALL_RUNNING;
                }
                else if (this.actionState is MarioFallingState)
                {
                    this.combinedState = marioState.SMALL_FALLING;
                }
                else if (this.actionState is MarioCrouchingState)
                {
                    this.combinedState = marioState.SMALL_IDLE;
                } else if (this.actionState == null)
                {
                    this.combinedState = marioState.SMALL_IDLE;
                }
            } else if(this.powerState is MarioSuperState)
            {
                //Super States
                 if (this.actionState is MarioIdleState)
                {
                    this.combinedState = marioState.SUPER_IDLE;
                }
                else if (this.actionState is MarioJumpingState)
                {
                    this.combinedState = marioState.SUPER_JUMPING;
                }
                else if (this.actionState is MarioRunningState)
                {
                    this.combinedState = marioState.SUPER_RUNNING;
                }
                else if (this.actionState is MarioFallingState)
                {
                    this.combinedState = marioState.SUPER_FALLING;
                }
                else if (this.actionState is MarioCrouchingState)
                {
                    this.combinedState = marioState.SUPER_CROUCHING;
                }
            }
            else if(this.powerState is MarioFireState)
            {
                if (this.actionState is MarioIdleState)
                {
                    this.combinedState = marioState.FIRE_IDLE;
                }
                else if (this.actionState is MarioJumpingState)
                {
                    this.combinedState = marioState.FIRE_JUMPING;
                }
                else if (this.actionState is MarioRunningState)
                {
                    this.combinedState = marioState.FIRE_RUNNING;
                }
                else if (this.actionState is MarioFallingState)
                {
                    this.combinedState = marioState.FIRE_FALLING;
                }
                else if (this.actionState is MarioCrouchingState)
                {
                    this.combinedState = marioState.FIRE_CROUCHING;
                }
            }
            else if (this.powerState is MarioDeadState)
            {
                this.combinedState = marioState.DEAD;
            }

            if (Sprite != null)
            {
                Sprite = factory.createSprite((int)combinedState, new Vector2(Sprite.X, Sprite.Y), Sprite.Direction);
            } else
            {
                Sprite = factory.createSprite((int)combinedState, startingLocation , Sprite.eDirection.Right);
            }
        }
    }
}
