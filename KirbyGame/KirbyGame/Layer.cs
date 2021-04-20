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
    }

    public Layer(Camera camera, Vector2 location, Viewport viewport)
    {
        _layerSprites = new List<Sprite>();
        _camera = camera;
        Parallax = Vector2.One;
        _viewport = viewport;
    }

    public void AddSprite(Sprite sprite)
    {
        _layerSprites.Add(sprite);
    }

    public Camera GetCamera()
    {
        return _camera;
    }

    public List<Sprite> GetSpriteList()
    {
        return _layerSprites;
    }


    public void RemoveSprite(Sprite sprite)
    {
        _layerSprites.Remove(sprite);
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin(/*samplerState: SamplerState.LinearWrap,*/transformMatrix:_camera.GetViewMatrix(Parallax));
        foreach (Sprite sprite in _layerSprites)
            sprite.Draw(spriteBatch);
        spriteBatch.End();
    }

    public virtual void Draw(SpriteBatch spriteBatch, Stats stat)
    {

    }

    public void Update(GameTime gameTime)
    {
        foreach (Sprite sprite in _layerSprites)
            sprite.Update(gameTime);
    }
}