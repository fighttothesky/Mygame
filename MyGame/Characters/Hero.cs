using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MyGame.Collisions;
using MyGame.Enums;
using MyGame.interfaces;
using MyGame.Sprites;

namespace MyGame.Characters
{
    internal class Hero : IDynamicPhysicsObject
    {
        private IInputReader inputReader;
        private AnimationMover character;

        private AnimationManager animationManager;
        private SpriteAnimation idleAnimation;
        private SpriteAnimation walkAnimation;

        public Hero(ContentManager contentManager, IInputReader inputReader)
        {
            CreateAnimations(contentManager);
            animationManager = new AnimationManager(idleAnimation, Vector2.One);
            character = new AnimationMover(animationManager);
            character.Speed = 4;
            
            this.inputReader = inputReader;
        }

        private void CreateAnimations(ContentManager contentManager)
        {
            Texture2D radishIdle = contentManager.Load<Texture2D>("Radish_Idle");
            idleAnimation = new SpriteAnimation(radishIdle, 6, 1, 12);
            idleAnimation.Position = new Vector2(1, 1);
            idleAnimation.Scale = new Vector2(4, 4);
            
            Texture2D radishRun = contentManager.Load<Texture2D>("Radish_Run");
            walkAnimation = new SpriteAnimation(radishRun, 12, 1);
            walkAnimation.Position = new Vector2(1, 1);
            walkAnimation.Scale = new Vector2(4, 4);
        }

        public void Update(GameTime gameTime)
        {
            Direction direction = inputReader.ReadDirectionInput();

            if (direction == Direction.NONE)
            {
                animationManager.SetCurrentAnimation(idleAnimation);
            }
            else
            {
                character.Move(direction);
                animationManager.SetCurrentAnimation(walkAnimation);
            }
            
            animationManager.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            animationManager.Draw(spriteBatch);
        }

        public Sprite GetSprite()
        {
            return animationManager.CurrentAnimation;
        }

        public bool CollidesWith(IPhysicsObject other)
        {
            if (this == other) return false;
            
            bool collides = PixelCollision.IsColliding(this, other);
            return collides;
        }

        public void ApplyGravity(AnimationManager animationManager)
        {
            throw new System.NotImplementedException();
        }

        public Rectangle GetBoundingRectangle()
        {
            return animationManager.CurrentAnimation.GetBoundingRectangle();
        }
    }
}