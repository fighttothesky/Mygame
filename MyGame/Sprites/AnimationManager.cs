using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.interfaces;

namespace MyGame.Sprites;

public class AnimationManager : IGameObject
{
    private bool flipped;

    public Vector2 Position { get; private set; }
    public SpriteAnimation CurrentAnimation { get; private set; }

    public bool isRemoved => false;

    public AnimationManager(SpriteAnimation startingAnimation, Vector2 startingPosition)
    {
        Position = startingPosition;
        CurrentAnimation = startingAnimation;
    }

    public void Update(GameTime gameTime)
    {
        CurrentAnimation.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        CurrentAnimation.Draw(spriteBatch);
    }

    public void SetPosition(Vector2 position)
    {
        Position = position;
        CurrentAnimation.Position = position;
    }

    public void SetCurrentAnimation(SpriteAnimation newAnimation)
    {
        newAnimation.Position = Position;
        newAnimation.SetFlipped(flipped);

        CurrentAnimation = newAnimation;
    }

    public void Flip()
    {
        flipped = !flipped;
        CurrentAnimation.Flip();
    }
}