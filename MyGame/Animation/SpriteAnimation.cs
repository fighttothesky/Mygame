using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.Content.interfaces;

namespace MyGame.Animation
{
    public class SpriteAnimation : IGameObject
    {
        // Location
        public Vector2 Scale { get; set; }
        public Vector2 Origin { get; set; }
        public Vector2 Position { get; set; } 
        public float Rotation { get; set; }

        public Rectangle CurrentFrame { get; set; }
        private readonly List<Rectangle> frames;
        private int rowCounter;
        private double secondRowCounter;
        private readonly double fps;

        private SpriteEffects effects;
        private readonly Texture2D texture;

        public SpriteAnimation(Texture2D texture, int numberOfWidthSprites, int numberOfHeightSprites, double fps = 24)
        {
            this.fps = fps;
            frames = new List<Rectangle>();
            effects = SpriteEffects.None;
            this.texture = texture;

            GetFramesFromTextureProperties(numberOfWidthSprites, numberOfHeightSprites);
        }
        
        // Convert image to List of AnimationFrame
        private void GetFramesFromTextureProperties(int numberOfWidthSprites, int numberOfHeightSprites)
        {
            int widthOfFrame = texture.Width / numberOfWidthSprites;
            int heightOfFrame = texture.Height / numberOfHeightSprites;

            for (int y = 0; y <= texture.Height - heightOfFrame; y+= heightOfFrame)
            {
                for (int x = 0; x <= texture.Width - widthOfFrame; x+= widthOfFrame)
                {
                    frames.Add(new Rectangle(x,y,widthOfFrame,heightOfFrame));
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            CurrentFrame = frames[rowCounter];

            secondRowCounter += gameTime.ElapsedGameTime.TotalSeconds;

            if(secondRowCounter >= 1 / fps)
            {
                rowCounter++;
                secondRowCounter = 0;
            }

            if (rowCounter >= frames.Count)
            {
                rowCounter = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, CurrentFrame, Color.White, Rotation,
                Origin, Scale, effects, 0);
        }

        public void Flip()
        {
            effects = (effects == SpriteEffects.None) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
        }

        public void SetFlipped(bool isFlipped)
        {
            effects = isFlipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
        }
    }
}
