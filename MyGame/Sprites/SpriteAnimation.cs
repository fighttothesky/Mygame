using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.interfaces;
using System.Collections.Generic;

namespace MyGame.Sprites;

public class SpriteAnimation : Sprite, IGameObject
{
    private readonly double fps;

    private readonly List<Rectangle> frames;
    private Rectangle currentFrame;
    private int rowCounter;
    private double secondRowCounter;

    public SpriteAnimation(Texture2D texture, int numberOfWidthSprites, int numberOfHeightSprites, double fps = 24) :
        base(texture)
    {
        this.fps = fps;
        frames = new List<Rectangle>();

        GetFramesFromTextureProperties(numberOfWidthSprites, numberOfHeightSprites);
    }

    // Convert image to List of AnimationFrame
    private void GetFramesFromTextureProperties(int numberOfWidthSprites, int numberOfHeightSprites)
    {
        int widthOfFrame = Texture.Width / numberOfWidthSprites;
        int heightOfFrame = Texture.Height / numberOfHeightSprites;

        for (int y = 0; y <= Texture.Height - heightOfFrame; y += heightOfFrame)
            for (int x = 0; x <= Texture.Width - widthOfFrame; x += widthOfFrame)
                frames.Add(new Rectangle(x, y, widthOfFrame, heightOfFrame));
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        currentFrame = frames[rowCounter];

        secondRowCounter += gameTime.ElapsedGameTime.TotalSeconds;

        if (secondRowCounter >= 1 / fps)
        {
            rowCounter++;
            secondRowCounter = 0;
        }

        if (rowCounter >= frames.Count) rowCounter = 0;
    }

    // Get the bounding rectangle of a part of the texture corresponding to the current frame in the animation
    protected override Rectangle GetFrame()
    {
        return currentFrame;
    }

    public void Flip()
    {
        effects = effects == SpriteEffects.None ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
    }

    public void SetFlipped(bool isFlipped)
    {
        effects = isFlipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
    }
}