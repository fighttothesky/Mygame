using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MyGame.interfaces;
using MyGame.Sprites;


namespace MyGame.Terrain.Blocks
{
    abstract class Block : Sprite, IPhysicsObject
    {
        public Block(ContentManager contentManager, string textureName)
            : base(contentManager.Load<Texture2D>(textureName))
        {
            FramePosition = GetTextureFrame().Location.ToVector2();
        }

        protected abstract Rectangle GetTextureFrame();

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, SpritePosition, GetTextureFrame(), Color.White, Rotation, Origin, Scale, effects, 0);

            // For debugging
            if (Debug) spriteBatch.DrawRectangle(GetBoundingRectangle(), Color.OrangeRed);
        }

        public Sprite GetSprite()
        {
            return this;
        }
    }
}
