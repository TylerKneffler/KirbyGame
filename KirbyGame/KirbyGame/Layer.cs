using KirbyGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

public class Layer
{
    private Viewport _viewport;

    public Vector2 Parallax { get; set; }

    private readonly List<Sprite> _layerSprites;

    private readonly Camera _camera;

    //
    public Layer(Camera camera, Texture2D texture, Vector2 location, Viewport viewport)
    {
        _layerSprites = new List<Sprite>();
        _camera = camera;
        Parallax = Vector2.One;
        _viewport = viewport;
        _layerSprites.Add(new Sprite(texture, location, 1));
        location.X += texture.Width * 2;//multipy by 2 to get full texture width
        _layerSprites.Add(new Sprite(texture, location, 1));
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin(/*samplerState: SamplerState.LinearWrap,*/transformMatrix:_camera.GetViewMatrix(Parallax));
        foreach (Sprite sprite in _layerSprites)
            sprite.Draw(spriteBatch);
        spriteBatch.End();
    }

    public void Update()
    {

        for (int i = 0; i < _layerSprites.Count; i++)
        {
            float camDistTravRelToSprite = _camera.Position.X * Parallax.X;

            int index = i - 1;
            if (index < 0)
            {
                index = _layerSprites.Count - 1;
            }
            if (camDistTravRelToSprite + _viewport.Width > _layerSprites[i].location.X + _layerSprites[i].texture.size.X)//if right side of camera hits right edge of layer sprite
            {
                _layerSprites[index].location.X = _layerSprites[i].location.X + _layerSprites[i].texture.size.X;//move other layer sprite's left edge to current layer sprites right edge
            }
            else if (camDistTravRelToSprite < _layerSprites[i].location.X)//if left side of camera hits left edge of layer sprite
            {
                _layerSprites[index].location.X = _layerSprites[i].location.X - _layerSprites[i].texture.size.X;//move other layer sprite's right edge to current layer sprites left edge
            }
        }
    }
}