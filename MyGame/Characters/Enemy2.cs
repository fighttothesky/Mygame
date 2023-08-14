using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MyGame.Collisions;
using MyGame.Enums;
using MyGame.interfaces;
using MyGame.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Characters
{
    internal class Enemy2 : IDynamicPhysicsObject, ISmartEnemy
    {
        // Does this enemy need forbidden directions?

        const int MAX_SINK_HEIGHT = 1;

        private bool isDead = false;
        bool ISmartEnemy.IsDead { get => isDead; set => isDead = value; }

        public Sprite Sprite { get; }

        public readonly AnimationManager animationManager;
        private readonly AnimationMover character;

        private SpriteAnimation movingAnimation;

        private List<Direction> forbiddenDirections;

        public bool IsDead = false;

        float distance;
        float oldDistance;

        Direction direction = Direction.NONE;

        public Enemy2(ContentManager contentManager, float newDistanceUp)
        {
            CreateAnimations(contentManager);
            animationManager = new AnimationManager(movingAnimation, Vector2.One);
            character = new AnimationMover(animationManager);

            forbiddenDirections = new List<Direction>();

            distance = newDistanceUp;
            oldDistance = distance;

            character.Speed = 1;
        }

        public void HandleCollisions(List<Collision> collisions)
        {
            forbiddenDirections = new List<Direction>();

            foreach (Collision collision in collisions)
            {
                if (collision.Direction.Bottom)
                {
                    forbiddenDirections.Add(Direction.DOWN);
                }
                if (collision.Direction.Top)
                {
                    forbiddenDirections.Add(Direction.UP);
                }

            }
        }

        public Sprite GetSprite()
        {
            return animationManager.CurrentAnimation;
        }

        public void Update(GameTime gameTime)
        {

            if (forbiddenDirections.Contains(direction) || distance <= 0)
            {
                direction = Direction.UP;
            }
            else if (distance >= oldDistance)
            {
                direction = Direction.DOWN;
            }

            if (direction == Direction.UP)
            {
                distance += 6;
            }
            else if (direction == Direction.DOWN)
            {
                distance -= 6;
            }

            character.Move(direction);



            animationManager.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            animationManager.Draw(spriteBatch);
        }

        private void CreateAnimations(ContentManager contentManager)
        {
            Texture2D movingBird = contentManager.Load<Texture2D>("FatBird_Idle");
            movingAnimation = new SpriteAnimation(movingBird, 8, 1, 12);
            movingAnimation.SpritePosition = new Vector2(1, 1);
            movingAnimation.Scale = new Vector2(4, 4);
        }



        public void ApplyGravity()
        {
            
        }
    }
}

