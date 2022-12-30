using System;
using System.Collections.Generic;
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

        private List<Direction> forbiddenDirections;

        public Hero(ContentManager contentManager, IInputReader inputReader)
        {
            CreateAnimations(contentManager);
            animationManager = new AnimationManager(idleAnimation, Vector2.One);
            character = new AnimationMover(animationManager);
            character.Speed = 4;
            
            this.inputReader = inputReader;
            forbiddenDirections = new List<Direction>();
        }

        private void CreateAnimations(ContentManager contentManager)
        {
            Texture2D radishIdle = contentManager.Load<Texture2D>("Radish_Idle2");
            idleAnimation = new SpriteAnimation(radishIdle, 6, 1, 12);
            idleAnimation.Position = new Vector2(1, 1);
            idleAnimation.Scale = new Vector2(4, 4);
            
            Texture2D radishRun = contentManager.Load<Texture2D>("Radish_Run2");
            walkAnimation = new SpriteAnimation(radishRun, 12, 1);
            walkAnimation.Position = new Vector2(1, 1);
            walkAnimation.Scale = new Vector2(4, 4);
        }
        
        public void HandleCollisions(List<Collision> collisions)
        {
            forbiddenDirections = new List<Direction>();
            
            foreach (Collision collision in collisions)
            {
                forbiddenDirections.AddRange(collision.Directions);
                
                // Get collision dimensions
                int x = collision.CollisionArea.Width;
                int y = collision.CollisionArea.Height;

                // Axis to move is smallest value (tall = along x, wide = along y)
                // Value - 1 is value to push out while keeping collision (so won't move further in that direction)
                x = x < y ? x - 1 : 0;
                y = y > x ? y - 1 : 0;

                // No need to push out
                if (x == 0 && y == 0) return;

                // Invert value depending on which side the collision is on
                x = collision.Direction.Right ? -x : x;
                y = collision.Direction.Bottom ? -y : y;
                
                // Push out of collision
                character.MoveExact(new Vector2(x, y));
            }
        }
        
        public void ApplyGravity()
        {
            // If not colliding with a floor, hero is falling
            if (!forbiddenDirections.Contains(Direction.DOWN))
            {
                animationManager.SetCurrentAnimation(idleAnimation);
                character.Fall();
            }
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
    }
}