using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MyGame.Characters;
using MyGame.Collisions;
using MyGame.interfaces;
using MyGame.Sprites;
using System.Collections.Generic;

namespace MyGame.Terrain
{
    internal class Coin : IDynamicPhysicsObject, IRemovable
    {
        public Sprite Sprite { get; }
        public readonly AnimationManager animationManager;

        private SpriteAnimation idleAnimation;

        private bool isRemoved = false;

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

        public void HandleCollisions(List<Collision> collisions)
        {
            foreach (Collision collision in collisions)
            {
                if (collision.Other is Hero hero)
                {
                    hero.AddScore();
                    isRemoved = true;
                }
            }
        }

        public bool IsRemoved()
        {
            return isRemoved;
        }

        public void Remove()
        {
            isRemoved = true;
        }
    }
}
