using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.interfaces;

namespace MyGame.Sprites;

public class Sprite : IGameObject
{
    // Location
    public Vector2 Scale { get; set; } = Vector2.One;
    public Vector2 Origin { get; set; }
    public Vector2 Position { get; set; } 
    public float Rotation { get; set; }
    
    protected SpriteEffects effects;
    public Texture2D Texture { get; }

    public Sprite(Texture2D texture)
    {
        effects = SpriteEffects.None;
        Texture = texture;
    }
    
    public virtual void Update(GameTime gameTime)
    {
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Texture, Position, GetFrame(), Color.White, Rotation, Origin, Scale, effects, 0);
        
        // For debugging
        // MonoGame.Extended.ShapeExtensions.DrawRectangle(spriteBatch, GetBoundingRectangle(), Color.Red);
    }

    // Get the bounding rectangle of the texture (no scaling applied)
    protected virtual Rectangle GetFrame()
    {
        return new Rectangle(Position.ToPoint(), new Point(Texture.Width, Texture.Height));
    }
    
    // Get the bounding rectangle of the texture (scaling applied)
    public Rectangle GetBoundingRectangle()
    {
        return new Rectangle(Position.ToPoint(), GetFrame().Size * Scale.ToPoint());
    }
}