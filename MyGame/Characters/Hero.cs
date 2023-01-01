using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MyGame.Collisions;
using MyGame.Enums;
using MyGame.interfaces;
using MyGame.Sprites;

namespace MyGame.Characters;

internal class Hero : IDynamicPhysicsObject
{
    public readonly AnimationManager animationManager;
    private readonly AnimationMover character;
    private readonly IInputReader inputReader;

    private List<Direction> forbiddenDirections;
    private SpriteAnimation idleAnimation;
    private SpriteAnimation walkAnimation;

    private Rectangle currentCollision;
    
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
        currentCollision = new Rectangle();

        foreach (Collision collision in collisions)
        {
            // Prevent further movement in the directions of the collision
            forbiddenDirections.AddRange(collision.Directions);

            // Get collision dimensions
            int x = collision.CollisionArea.Width;
            int y = collision.CollisionArea.Height;

            // Axis to move is smallest value (tall = along x, wide = along y)
            x = x < collision.CollisionArea.Height ? x : 0;
            y = y < collision.CollisionArea.Width ? y : 0;

            if (x < 2 && y < 2)
            {
                return;
            }

            // Invert value depending on which side the collision is on
            x = collision.Direction.Right ? -x : x;
            y = collision.Direction.Bottom ? -y : y;

            // Push out
            character.MoveExact(new Vector2(x, y));

            currentCollision = collision.CollisionArea;
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
        // For debugging
        if (Debug) spriteBatch.DrawRectangle(currentCollision, Color.Red);
        
        animationManager.Draw(spriteBatch);
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