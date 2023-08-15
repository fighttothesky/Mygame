using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MyGame.Collisions;
using MyGame.Enums;
using MyGame.interfaces;
using MyGame.Sprites;
using MyGame.Terrain;
using System.Collections.Generic;
using System.Linq;

namespace MyGame.Characters;

internal class Hero : IDynamicPhysicsObject, IRemovable, IGravityObject
{
    const int MAX_SINK_HEIGHT = 4;

    public readonly AnimationManager animationManager;
    private readonly AnimationMover character;
    private readonly IInputReader inputReader;

    private List<Rectangle> currentCollisions;
    private List<Rectangle> currentIntersections;

    private List<Direction> forbiddenDirections;
    private SpriteAnimation idleAnimation;
    private SpriteAnimation walkAnimation;

    public int Score { get; private set; } = 0;

    public bool Debug { get; set; } = true;

    // lose if hero touches a enemy
    private bool lose;

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
            if (collision.Other is IDynamicPhysicsObject) continue;

            if (collision.Direction.Bottom)
            {
                forbiddenDirections.Add(Direction.DOWN);
            }
            if (collision.Direction.Top)
            {
                forbiddenDirections.Add(Direction.UP);
                forbiddenDirections.Add(Direction.RIGHT_UP);
                forbiddenDirections.Add(Direction.LEFT_UP);
            }


            if (collision.Direction.Left && collision.CollisionArea.Height > MAX_SINK_HEIGHT)
            {
                forbiddenDirections.Add(Direction.LEFT);
                forbiddenDirections.Add(Direction.LEFT_UP);
            }
            if (collision.Direction.Right && collision.CollisionArea.Height > MAX_SINK_HEIGHT)
            {
                forbiddenDirections.Add(Direction.RIGHT);
                forbiddenDirections.Add(Direction.RIGHT_UP);
            }
        }
    }

    public void ApplyGravity()
    {
        Direction direction = inputReader.ReadDirectionInput();

        // If not colliding with a floor, hero is falling
        if (!forbiddenDirections.Contains(Direction.DOWN) && direction != Direction.UP && direction != Direction.RIGHT_UP && direction != Direction.LEFT_UP)
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
                spriteBatch.DrawRectangle(intersection, Color.Green);
            }
        }
    }

    public Sprite GetSprite()
    {
        return animationManager.CurrentAnimation;
    }

    public void AddScore()
    {
        Score++;
    }

    private void CreateAnimations(ContentManager contentManager)
    {
        Texture2D radishIdle = contentManager.Load<Texture2D>("Radish_Idle2");
        idleAnimation = new SpriteAnimation(radishIdle, 6, 1, 12);
        idleAnimation.SpritePosition = new Vector2(1, 1);
        idleAnimation.Scale = new Vector2(4, 4);

        Texture2D radishRun = contentManager.Load<Texture2D>("Radish_Run2");
        walkAnimation = new SpriteAnimation(radishRun, 12, 1);
        walkAnimation.SpritePosition = new Vector2(1, 1);
        walkAnimation.Scale = new Vector2(4, 4);
    }

    public bool IsRemoved()
    {
        return lose;
    }

    public void Remove()
    {
        lose = true;
    }
}