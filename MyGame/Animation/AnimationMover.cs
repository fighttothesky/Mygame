using Microsoft.Xna.Framework;
using MyGame.Enum;
using MyGame.interfaces;

namespace MyGame.Animation;

public class AnimationMover : IMovable
{
    private readonly AnimationManager animationManager;
    private Direction currentDirection;

    public float Speed { get; set; }

    public AnimationMover(AnimationManager animationManager, Direction currentDirection = Direction.NONE, float speed = 0)
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
        if (newDirection != Direction.UP && newDirection != Direction.DOWN && newDirection != currentDirection)
        {
            animationManager.Flip();
        }
        currentDirection = newDirection;

        // Calculate movement
        Vector2 movement = Vector2.Zero;
        switch (newDirection)
        {
            case Direction.RIGHT: movement = new Vector2(1, 0); break;
            case Direction.LEFT: movement = new Vector2(-1, 0); break;
        }
        movement *= Speed;
        
        // Move in direction
        animationManager.SetPosition(animationManager.Position + movement);
    }
}