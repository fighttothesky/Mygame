using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MyGame.interfaces;
using MyGame.Sprites;

namespace MyGame.Terrain
{
    internal class Coin : IPhysicsObject
    {
        public Sprite Sprite { get; }
        public readonly AnimationManager animationManager;

        private SpriteAnimation idleAnimation;

        public bool isRemoved = false;

        public Coin(ContentManager contentManager)
        {
            CreateAnimations(contentManager);
            animationManager = new AnimationManager(idleAnimation, Vector2.One);

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            animationManager.Draw(spriteBatch);
        }

        public Sprite GetSprite()
        {
            return animationManager.CurrentAnimation;
        }

        public void Update(GameTime gameTime)
        {
            animationManager.Update(gameTime);
        }

        private void CreateAnimations(ContentManager contentManager)
        {
            Texture2D kiwi = contentManager.Load<Texture2D>("Kiwi");
            idleAnimation = new SpriteAnimation(kiwi, 17, 1, 12);
            idleAnimation.SpritePosition = new Vector2(1, 1);
            idleAnimation.Scale = new Vector2(4, 4);
        }
    }
}
