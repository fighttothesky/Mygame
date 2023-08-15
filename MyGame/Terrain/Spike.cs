using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MyGame.Characters;
using MyGame.Collisions;
using MyGame.interfaces;
using MyGame.Sprites;
using System.Collections.Generic;

namespace MyGame.Terrain;
public class Spike : IEnemy, IDynamicPhysicsObject
{
    public Sprite Sprite { get; }

    public Spike(ContentManager contentManager)
    {
        Texture2D texture = contentManager.Load<Texture2D>("Spikes");
        Sprite = new Sprite(texture);
    }

    public void Update(GameTime gameTime)
    {
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        Sprite.Draw(spriteBatch);
    }

    public Sprite GetSprite()
    {
        return Sprite;
    }

    public void HandleCollisions(List<Collision> collisions)
    {
        foreach (Collision collision in collisions)
        {
            if (collision.Other is Hero hero)
            {
                hero.Remove();
            }
        }
    }
}
