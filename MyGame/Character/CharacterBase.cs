using Microsoft.Xna.Framework;
using MyGame.Animation;
using MyGame.Enum;

namespace MyGame.Character;

public class CharacterBase
{
    public float Speed { get; set; }

    private Direction currentDirection;

    public CharacterBase(Direction currentDirection = Direction.NONE, float speed = 0)
    {
        this.currentDirection = currentDirection;
        Speed = speed;
    }

    public void Move(Direction newDirection, AnimationManager animationManager)
    {
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
        animationManager.Position += movement;
    }

    public void Stop()
    {
        currentDirection = Direction.NONE;
    }
}