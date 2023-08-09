using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MyGame.interfaces;
using MyGame.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Characters
{
    internal class Enemy1 : IPhysicsObject, IEnemy
    {
        public Sprite Sprite { get; }
        public readonly AnimationManager animationManager;

        private SpriteAnimation idleAnimation;

        public bool IsDead = false;

        public Enemy1(ContentManager contentManager)
        {
            CreateAnimations(contentManager);
            animationManager = new AnimationManager(idleAnimation, Vector2.One);

        }

        public Sprite GetSprite()
        {
            return animationManager.CurrentAnimation;
        }

        public void Update(GameTime gameTime)
        {
            animationManager.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        private void CreateAnimations(ContentManager contentManager)
        {
            Texture2D idelSnail = contentManager.Load<Texture2D>("Snail_Idle");
            idleAnimation = new SpriteAnimation(idelSnail, 15, 1, 12);
            idleAnimation.Position = new Vector2(1, 1);
            idleAnimation.Scale = new Vector2(4, 4);
        }
    }
}
