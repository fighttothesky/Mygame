using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MyGame.Animation;
using MyGame.Character;
using MyGame.Content.interfaces;
using MyGame.Enum;
using MyGame.interfaces;

namespace MyGame
{
    internal class Hero : IGameObject
    {
        private IInputReader inputReader;
        private CharacterBase character;

        private AnimationManager animationManager;
        private SpriteAnimation idleAnimation;
        private SpriteAnimation walkAnimation;

        public Hero(ContentManager contentManager, IInputReader inputReader)
        {
            CreateAnimations(contentManager);
            animationManager = new AnimationManager(idleAnimation, Vector2.One);
            character = new CharacterBase();
            character.Speed = 4;
            
            this.inputReader = inputReader;
        }

        private void CreateAnimations(ContentManager contentManager)
        {
            Texture2D radishIdle = contentManager.Load<Texture2D>("Radish_Idle");
            idleAnimation = new SpriteAnimation(radishIdle, 12, 1);
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
                character.Stop();
                animationManager.CurrentAnimation = idleAnimation;
            }
            else
            {
                character.Move(direction, animationManager);
                animationManager.CurrentAnimation = walkAnimation;
            }
            
            animationManager.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            animationManager.Draw(spriteBatch);
        }
    }
}