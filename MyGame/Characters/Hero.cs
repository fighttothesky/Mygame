using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MyGame.Collisions;
using MyGame.Enums;
using MyGame.interfaces;
using MyGame.Sprites;
using MyGame.Terrain;

namespace MyGame.Characters;

internal class Hero : IDynamicPhysicsObject
{
    const int MAX_SINK_HEIGHT = 1;

    public readonly AnimationManager animationManager;
    private readonly AnimationMover character;
    private readonly IInputReader inputReader;

    private List<Rectangle> currentCollisions;
    private List<Rectangle> currentIntersections;

    private List<Direction> forbiddenDirections;
    private SpriteAnimation idleAnimation;
    private SpriteAnimation walkAnimation;

    // lose if hero touches a enemy
    public bool Lose = false;

    public int Score = 0;

    public bool Debug { get; set; } = true;

    public Hero(ContentManager contentManager, IInputReader inputReader)
    {
        CreateAnimations(contentManager);
        animationManager = new AnimationManager(idleAnimation, Vector2.One);
        character = new AnimationMover(animationManager);
        character.Speed = 3;

        this.inputReader = inputReader;
        forbiddenDirections = new List<Direction>();
    }

    public void HandleCollisions(List<Collision> collisions)
    {
        forbiddenDirections = new List<Direction>();
        currentCollisions = collisions.Select(c => c.CollisionArea).ToList();
        currentIntersections = collisions.Select(c => c.IntersectionArea).ToList();

        foreach (Collision collision in collisions)
        {

            // Remove coin is intersecting with the hero
            if (collision.Other is Coin coin)
            {
                Score++;
                coin.isRemoved = true;
            }

            if (collision.Other is IEnemy)
            {
                Lose = true;
            }

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

        if (direction == Direction.NONE || forbiddenDirections.Contains(direction))
        {
            animationManager.SetCurrentAnimation(idleAnimation);
        }
        else if (direction == Direction.UP)
        {
            character.Move(direction);
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

        // For debugging
        if (Debug)
        {
            foreach (Rectangle collision in currentCollisions)
            {
                spriteBatch.DrawRectangle(collision, Color.Blue);
            }

            foreach (Rectangle intersection in currentIntersections)
            {
                spriteBatch.DrawRectangle(intersection, Color.LightBlue);
            }
        }
    }

    public Sprite GetSprite()
    {
        return animationManager.CurrentAnimation;
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
}