using Microsoft.Xna.Framework;
using MyGame.Enums;
using MyGame.interfaces;

namespace MyGame.Sprites;

public class AnimationMover : IMovable
{
    private readonly AnimationManager animationManager;
    private Direction currentDirection;
    private string lookingDirection = "left";

    public float Speed { get; set; }

    public AnimationMover(AnimationManager animationManager, Direction currentDirection = Direction.NONE,
        float speed = 0)
    {
        this.animationManager = animationManager;
        this.currentDirection = currentDirection;
        Speed = speed;
    }

    public void Move(Direction newDirection)
    {
        // This is for Stop to handle
        if (newDirection == Direction.NONE) return;

        // Face direction
        if (newDirection != Direction.UP
            && newDirection != currentDirection
            && ((newDirection == Direction.LEFT || newDirection == Direction.LEFT_UP) && lookingDirection == "right"
            || (newDirection == Direction.RIGHT || newDirection == Direction.RIGHT_UP) && lookingDirection == "left")
        )
        {
            animationManager.Flip();
        }
        currentDirection = newDirection;

        // Calculate movement
        Vector2 movement = Vector2.Zero;
        switch (newDirection)
        {
            case Direction.RIGHT_UP:
                movement = new Vector2(1, -2);
                lookingDirection = "right";
                break;
            case Direction.LEFT_UP:
                movement = new Vector2(-1, -2);
                lookingDirection = "left";
                break;
            case Direction.RIGHT:
                movement = new Vector2(1, 0);
                lookingDirection = "right";
                break;
            case Direction.LEFT:
                movement = new Vector2(-1, 0);
                lookingDirection = "left";
                break;
            case Direction.DOWN:
                movement = new Vector2(0, 2);
                break;
            case Direction.UP:
                movement = new Vector2(0, -2);
                break;
        }

        movement *= Speed;
        // Move in direction
        animationManager.SetPosition(animationManager.Position + movement);
    }

    // This method only moves the sprite in the given direction. It doesn't take rotation or speed into account.
    public void MoveExact(Vector2 direction)
    {
        animationManager.SetPosition(animationManager.Position + direction);
    }

    public void Fall()
    {
        const float GRAVITY = 9.81f;

        Vector2 velocity = new Vector2(0, 1) * GRAVITY;
        animationManager.SetPosition(animationManager.Position + velocity);
    }
}