using Microsoft.Xna.Framework;
using MyGame.interfaces;

namespace MyGame.Collisions;

public static class PixelCollision
{
    public static bool IsColliding(IPhysicsObject objectA, IPhysicsObject objectB, out Collision collision)
    {
        collision = null;
        
        // No intersection if intersection has a size of 0
        Rectangle intersection = Rectangle.Intersect(objectA.GetSprite().GetBoundingRectangle(), objectB.GetSprite().GetBoundingRectangle());
        if (intersection.Size == Point.Zero) return false;
        
        // Get pixel collision
        collision = new Collision(objectA, objectB, intersection);
        return collision.HasCollidingPixels();
    }
}