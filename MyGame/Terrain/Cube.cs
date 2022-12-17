using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MyGame.interfaces;
using MyGame.Sprites;

namespace MyGame.Terrain;

public class Cube: IPhysicsObject
{
    public Sprite Sprite { get; }

    public Cube(ContentManager contentManager)
    {
        Texture2D texture = contentManager.Load<Texture2D>("Purple");
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

    public bool CollidesWith(IPhysicsObject other)
    {
        return GetSprite().GetBoundingRectangle().Intersects(other.GetSprite().GetBoundingRectangle());
    }
}