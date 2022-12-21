using System;
using Microsoft.Xna.Framework;
using MyGame.interfaces;
using MyGame.Sprites;

namespace MyGame.Collisions;

public static class PixelCollision
{
    public static bool IsColliding(IPhysicsObject physicsObjectA, IPhysicsObject physicsObjectB, out Collision collision)
    {
        collision = null;
        
        Sprite a = physicsObjectA.GetSprite();
        Sprite b = physicsObjectB.GetSprite();
        
        // No intersection if bounding boxes do not intersect
        if (!a.GetBoundingRectangle().Intersects(b.GetBoundingRectangle()))
        {
            return false;
        }
        
        Rectangle intersection = Rectangle.Intersect(a.GetBoundingRectangle(), b.GetBoundingRectangle());
        Vector2 scale = a.Scale.X * a.Scale.Y > b.Scale.X * b.Scale.Y ? a.Scale : b.Scale;

        Color[] pixelsA = GetLocalIntersectionPixels(a, intersection, scale);
        Color[] pixelsB = GetLocalIntersectionPixels(b, intersection, scale);

        for (int i = 0; i < pixelsA.Length && i < pixelsB.Length; i++)
        {
            if (pixelsA[i].A != 0 && pixelsB[i].A != 0)
            {
                collision = new Collision(physicsObjectA, physicsObjectB, intersection);
                return true;
            }
        }

        return false;
    }

    private static Color[] GetLocalIntersectionPixels(Sprite sprite, Rectangle globalIntersection, Vector2 scale)
    {
        Rectangle localIntersection = new Rectangle(globalIntersection.Location - sprite.Position.ToPoint(), globalIntersection.Size);
        localIntersection.Location /= scale.ToPoint();
        localIntersection.Size /= scale.ToPoint();
        localIntersection.Size = new Point(Math.Max(localIntersection.Size.X, 1), Math.Max(localIntersection.Size.Y, 1));
        
        int pixelCount = localIntersection.Size.X * localIntersection.Size.Y;
        Color[] pixels = new Color[pixelCount];
        sprite.Texture.GetData(0, localIntersection, pixels, 0, pixelCount);

        return pixels;
    }
}