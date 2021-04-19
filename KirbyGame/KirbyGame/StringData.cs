using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace KirbyGame
{
    public class StringData
    {
        SpriteFont _font;
        string _text;
        Vector2 _location;
        Color _color;
        public StringData(SpriteFont font, string text, Vector2 location, Color color)
        {
            _font = font;
            _text = text;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font, _text, _location, _color);
        }
    }
}
