using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MyGame.Collisions;
using MyGame.Enums;
using MyGame.interfaces;
using MyGame.Sprites;
using System.Collections.Generic;

namespace MyGame.Characters
{
    internal class Snail : IDynamicPhysicsObject, ISmartEnemy
    {
        // Does this enemy need forbidden directions?

        const int MAX_SINK_HEIGHT = 1;

        public Sprite Sprite { get; }

        private bool isDead = false;
        bool ISmartEnemy.IsDead { get => isDead; set => isDead = value; }

        public readonly AnimationManager animationManager;
        private readonly AnimationMover character;

        private SpriteAnimation walkAnimation;

        private List<Direction> forbiddenDirections;



        float distance;
        float oldDistance;

        Direction direction = Direction.NONE;

        public Snail(ContentManager contentManager, float newDistance)
        {
            CreateAnimations(contentManager);
            animationManager = new AnimationManager(walkAnimation, Vector2.One);
            character = new AnimationMover(animationManager);

            forbiddenDirections = new List<Direction>();

            distance = newDistance;
            oldDistance = distance;

            character.Speed = 2;
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

                if (collision.Direction.Left && collision.CollisionArea.Height > MAX_SINK_HEIGHT)
                {
                    forbiddenDirections.Add(Direction.LEFT);
                }
                if (collision.Direction.Right && collision.CollisionArea.Height > MAX_SINK_HEIGHT)
                {
                    forbiddenDirections.Add(Direction.RIGHT);
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
                direction = Direction.RIGHT;
            }
            else if (distance >= oldDistance)
            {
                direction = Direction.LEFT;
            }

            if (direction == Direction.RIGHT)
            {
                distance += character.Speed;
            }
            else if (direction == Direction.LEFT)
            {
                distance -= character.Speed;
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
            Texture2D walkSnail = contentManager.Load<Texture2D>("Snail_Walk");
            walkAnimation = new SpriteAnimation(walkSnail, 10, 1, 12);
            walkAnimation.SpritePosition = new Vector2(1, 1);
            walkAnimation.Scale = new Vector2(4, 4);
        }



        public void ApplyGravity()
        {
            // If not colliding with a floor, enemy is falling
            if (!forbiddenDirections.Contains(Direction.DOWN))
            {
                animationManager.SetCurrentAnimation(walkAnimation);
                character.Fall();
            }
        }
    }
}
