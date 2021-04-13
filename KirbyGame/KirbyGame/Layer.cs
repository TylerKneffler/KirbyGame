﻿using KirbyGame;
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

        
    }
}