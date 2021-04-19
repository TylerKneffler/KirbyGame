using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System;
using System.Media;
using Microsoft.Xna.Framework.Audio;

namespace KirbyGame
{
    class Block : Entity
    {
        public Blocktype blocktype;
        public int type;


        private ItemFactory itemFactory;
        private EnemyFactoryTest enemyFactory;

        public int coins;
        public int item;
        public int enemy;
        public int stage;

        public Point anchor;
        public Boolean bumping;

        private SoundEffect player;


        public enum blocktypes
        {
            FLOOR, QUESTION, USED, STAIR, HIDDEN, BRICK ,BRICKBlUE, FLOORBLUE, BROKEN_BRICK, PIPETOP, PIPEMIDDLE, PIPESIDWAYS, PIPEMIDDLESIDWAYS, PIPERIGHTSIDE, PIPEROT, CASTLE, PIPETELEPORTTOP, PIPETELEPORTSIDE, PIPETELEPORTTOPBONUS
        }

        public Block(blocktypes blockType, Vector2 location, int coins, int item, int enemy, Game1 game)
        {
            itemFactory = new ItemFactory(game);
            enemyFactory = new EnemyFactoryTest(game);
            this.coins = coins;
            this.type = (int)blockType;
            anchor = new Point((int)location.X, (int) location.Y);
            this.item = item;
            this.enemy = enemy;
            this.game = game;
            bumping = false;
            defaultColor = Color.Blue;
            boundingColor = defaultColor;

            if (blockType == blocktypes.FLOOR)
            {
                blocktype = new FloorBlock(this, location); 
            }
            else if (blockType == blocktypes.QUESTION)
            {
                blocktype = new QuestionBlock(this, location); 
            }
            else if (blockType == blocktypes.USED)
            {
                blocktype = new UsedBlock(this, location); 
            }
            else if (blockType == blocktypes.STAIR)
            {
                blocktype = new StairBlock(this, location); 
            }
            else if (blockType == blocktypes.HIDDEN)
            {
                blocktype = new HiddenBlock(this, location); 
            }
            else if (blockType == blocktypes.BRICK)
            {
                blocktype = new BrickBlock(this, location); 
            }
            else if (blockType == blocktypes.BROKEN_BRICK)
            {
                blocktype = new BrokenBrickBlock(this, location);
            }
            else if (blockType == blocktypes.PIPETOP)
            {
                blocktype = new PipeTop(this, location);
            }
            else if (blockType == blocktypes.PIPESIDWAYS)
            {
                blocktype = new PipeSideways(this, location);
            }
            else if (blockType == blocktypes.PIPEMIDDLE)
            {
                blocktype = new PipeMiddle(this, location);
            }
            else if (blockType == blocktypes.PIPEMIDDLESIDWAYS)
            {
                blocktype = new PipeMiddleSideways(this, location);
            }
            else if (blockType == blocktypes.PIPEROT)
            {
                blocktype = new PipeRot(this, location);
            }
            else if (blockType == blocktypes.PIPERIGHTSIDE)
            {
                blocktype = new PipeRightSide(this, location);
            }
            else if (blockType == blocktypes.CASTLE)
            {
                blocktype = new Castle(this, location);
            }
            else if (blockType == blocktypes.PIPETELEPORTTOP)
            {
                blocktype = new PipeTeleportTop(this, location);
                
            }
            else if (blockType == blocktypes.PIPETELEPORTSIDE)
            {
                blocktype = new PipeTeleportSide(this, location);

            }
            else if (blockType == blocktypes.PIPETELEPORTTOPBONUS)
            {
                blocktype = new PipeTeleportTopBonus(this, location);

            }
            else if (blockType == blocktypes.BRICKBlUE)
            {
                blocktype = new BrickBlockBlue(this, location);

            }
            else if (blockType == blocktypes.FLOORBLUE)
            {
                blocktype = new FloorBlockBlue(this, location);

            }

        }

        public virtual void QuestionStateChange()
        {
            //Debug.WriteLine("change is occuring" + coins);
            if (type == 1 && coins == 1)
            {
                type = 2;
                this.blocktype = new UsedBlock(this, new Vector2(this.X, this.Y));
            }
            if (item != 0)
            {
                this.ReleaseItem();
                item = 0;
            }
            coins--;
            blocktype.Bump();
            this.player = this.game.Content.Load<SoundEffect>("SoundEffects/smb_bump");
            this.player.Play();

        }

        public virtual void ReleaseItem()
        {
            game.levelLoader.list.Add(itemFactory.createItem((Item.eItemType)item-1, new Vector2(this.anchor.X, this.anchor.Y-16)));
            this.player = this.game.Content.Load<SoundEffect>("SoundEffects/smb_powerup_appears");
            //this.player.Play();
        //}
            if (base.boundingBoxSize.X > 32)
            {
                game.levelLoader.list.Add(itemFactory.createItem((Item.eItemType)item - 1, new Vector2(this.anchor.X + base.boundingBoxSize.X/4 , this.anchor.Y - 16)));
            }
            else
            {
                game.levelLoader.list.Add(itemFactory.createItem((Item.eItemType)item - 1, new Vector2(this.anchor.X, this.anchor.Y - 16)));
            }
        }

        public virtual void ReleaseEnemie()
        {
            EnemyTest me = enemyFactory.createEnemy((EnemyTest.enemytypes)enemy - 1, new Vector2(this.anchor.X, this.anchor.Y - 40));
            me.velocity.Y = -3;
            game.levelLoader.list.Add(me);
            game.map.Insert(me);
        }
        
        public virtual void HiddenStateChange()
        {
            
            game.player.PlayEnterSound();
            game.player.IsInTransition = true;
            System.Threading.Thread.Sleep(1000);
            game.player.IsInTransition = false;
            /*
            if (type == 4)
            {
                type = 1;
                this.blocktype = new QuestionBlock(this, new Vector2(this.X, this.Y));
            }
            this.player.Play();
            */
        }

        public virtual void BrickStateChange(Avatar mario)
        {
            //Debug.WriteLine("change is occuring" + coins);
            if (type != 6 && mario.canBreakBlocks())
            {
                type = 6;
                this.blocktype = new BrokenBrickBlock(this, new Vector2(this.X, this.Y));
                this.player = this.game.Content.Load<SoundEffect>("SoundEffects/smb_breakblock");
                this.player.Play();
            }
            else
            {
                if (item != 0)
                {
                    this.ReleaseItem();
                    item = 0;
                }
                blocktype.Bump();
                this.player = this.game.Content.Load<SoundEffect>("SoundEffects/smb_bump");
                this.player.Play();
            }
        }

        public override void HandleCollision(Collision collision, Entity collider)
        {
            //Debug.WriteLine("Block bounding box (X) before handle: " + this.BoundingBox.X);
            //Debug.WriteLine("Block bounding box (Y) before handle: " + this.BoundingBox.Y);
            //Debug.WriteLine("Collision direction: " + collision.CollisionDirection);
            if (collision.CollisionDirection == Collision.Direction.Down && this.blocktype is QuestionBlock)
            {
                this.QuestionStateChange();
            } else if (collision.CollisionDirection == Collision.Direction.Down && this.blocktype is HiddenBlock)
            {
                this.HiddenStateChange();
            } else if (collision.CollisionDirection == Collision.Direction.Down && this.blocktype is BrickBlock && collider is Avatar)
            {
                this.BrickStateChange((Avatar)collider);
            }
            //Debug.WriteLine("Block bounding box (X) AFTER handle: " + this.BoundingBox.X);
            //Debug.WriteLine("Block bounding box (Y) AFTER handle: " + this.BoundingBox.Y);
            //Debug.WriteLine("Block bounding box (SIZE) AFTER handle: " + this.BoundingBox.Size);
        }



        public override void Update(GameTime gameTime)
        {
            blocktype.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!(blocktype is BrokenBrickBlock))
            {
                base.Draw(spriteBatch);
            }
            else
            {
                blocktype.Draw(spriteBatch);
                
            }
        }

    }
}
