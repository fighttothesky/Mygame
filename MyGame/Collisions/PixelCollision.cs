using Microsoft.Xna.Framework;
using MyGame.interfaces;
using MyGame.Sprites;

namespace MyGame.Collisions;

public static class PixelCollision
{
    public static bool IsColliding(IPhysicsObject physicsObjectA, IPhysicsObject physicsObjectB,
        out Collision collision)
    {
        collision = null;

        Sprite a = physicsObjectA.GetSprite();
        Sprite b = physicsObjectB.GetSprite();

        // No intersection if bounding boxes do not intersect
        if (!a.GetBoundingRectangle().Intersects(b.GetBoundingRectangle())) return false;

        Rectangle intersection = Rectangle.Intersect(a.GetBoundingRectangle(), b.GetBoundingRectangle());
        collision = new Collision(physicsObjectA, physicsObjectB, intersection);
        return collision.HasCollidingPixels();
    }
}