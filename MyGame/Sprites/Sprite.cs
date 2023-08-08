using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MyGame.interfaces;

namespace MyGame.Sprites;

public class Sprite : IGameObject
{

    protected SpriteEffects effects;

    // Location
    public Vector2 Scale { get; set; } = Vector2.One;
    public Vector2 Origin { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 Position2 { get; set; } = new Vector2(0,0);
    public float Rotation { get; set; }
    public bool Debug { get; set; } = true;
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
        if (Debug) spriteBatch.DrawRectangle(GetBoundingRectangle(), Color.OrangeRed);
    }

    // Get the bounding rectangle of the texture (no scaling applied)
    protected virtual Rectangle GetFrame()
    {
        return new Rectangle((int)Position2.X, (int)Position2.Y, Texture.Width, Texture.Height);
    }

    // Get the bounding rectangle of the texture (scaling applied)
    public Rectangle GetBoundingRectangle()
    {
        return new Rectangle(Position.ToPoint(), GetFrame().Size * Scale.ToPoint());
    }

    public void SetPosition(Vector2 position)
    {
        Position = position;
    }
}