using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MyGame.interfaces;
using MyGame.Sprites;

namespace MyGame.Terrain;
public class Spike : IPhysicsObject, IEnemy
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
}
