using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KirbyGame
{
	class Shell : Enemy
	{
		private Sprite.eDirection prevDirection;
		private Game1 game;
		private Texture2D texture;
		private int velocity;
		

		public Shell(Game1 game, Vector2 location, Sprite.eDirection direction) : base(sprite, game)
		{
			this.prevDirection = direction;
			this.game = game;
			this.location = location;
			this.texture = game.Content.Load<Texture2D>("shell");
			this.velocity = 0;
		}


	}
}
