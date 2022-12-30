using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MyGame.Enums;
using MyGame.interfaces;
using MyGame.Sprites;

namespace MyGame.Collisions;

public class Collision
{
    public IPhysicsObject This { get; }
    public IPhysicsObject Other { get; }

    public Rectangle CollisionArea { get; }
    public List<Point> CollidingPixels { get; }
    public Vector2 Scale { get; }

    public CollisionDirection Direction { get; }
    public List<Direction> Directions { get; }

    public Collision(IPhysicsObject thisObject, IPhysicsObject otherObject, Rectangle collisionArea)
    {
        This = thisObject;
        Other = otherObject;

        Direction = FindCollisionDirections(This, Other);
        Directions = CollisionDirectionsAsList();

        Scale = CalculateScale();
        List<Point> thisPixels = GetFilledPixels(This.GetSprite(), collisionArea);
        List<Point> otherPixels = GetFilledPixels(Other.GetSprite(), collisionArea);
        CollidingPixels = thisPixels.FindAll(point => otherPixels.Contains(point));
        CollisionArea = GetPixelCollisionArea();
    }

    private static CollisionDirection FindCollisionDirections(IPhysicsObject thisObject, IPhysicsObject otherObject)
    {
        Rectangle boundsA = thisObject.GetSprite().GetBoundingRectangle();
        Rectangle boundsB = otherObject.GetSprite().GetBoundingRectangle();

        CollisionDirection collisionDirection = new();
        collisionDirection.Left = boundsB.Left < boundsA.Left && boundsA.Left < boundsB.Right;
        collisionDirection.Right = boundsB.Left < boundsA.Right && boundsA.Right < boundsB.Right;
        collisionDirection.Top = boundsB.Top < boundsA.Top && boundsA.Top < boundsB.Bottom;
        collisionDirection.Bottom = boundsB.Top < boundsA.Bottom && boundsA.Bottom < boundsB.Bottom;

        return collisionDirection;
    }

    private List<Direction> CollisionDirectionsAsList()
    {
        List<Direction> directions = new();
        if (Direction.Left) directions.Add(Enums.Direction.LEFT);
        if (Direction.Right) directions.Add(Enums.Direction.RIGHT);
        if (Direction.Top) directions.Add(Enums.Direction.UP);
        if (Direction.Bottom) directions.Add(Enums.Direction.DOWN);
        return directions;
    }
    
    private Vector2 CalculateScale()
    {
        return This.GetSprite().Scale.X * This.GetSprite().Scale.Y > Other.GetSprite().Scale.X * Other.GetSprite().Scale.Y
            ? This.GetSprite().Scale
            : Other.GetSprite().Scale;
    }
    
    private List<Point> GetFilledPixels(Sprite sprite, Rectangle globalIntersection)
    {
        Rectangle localIntersection = new(globalIntersection.Location - sprite.Position.ToPoint(), globalIntersection.Size);
        localIntersection.Location /= Scale.ToPoint();
        localIntersection.Size /= Scale.ToPoint();
        localIntersection.Size = new Point(Math.Max(localIntersection.Size.X, 1), Math.Max(localIntersection.Size.Y, 1));

        var pixelCount = localIntersection.Size.X * localIntersection.Size.Y;

        Color[] flatPixels = new Color[pixelCount];
        sprite.Texture.GetData(0, localIntersection, flatPixels, 0, pixelCount);

        List<Point> collidingPixels = new();
        for (var i = 0; i < pixelCount; i++)
        {
            // Transparent pixels aren't interesting for collisions
            if (flatPixels[i].A == 0) continue;

            var x = (int) Math.Round(i * Scale.X % localIntersection.Size.X);
            var y = (int) Math.Round(i * Scale.Y / localIntersection.Size.X);
            collidingPixels.Add(new Point(x, y));
        }

        return collidingPixels.Distinct().ToList();
    }

    public bool HasCollidingPixels()
    {
        return CollidingPixels.Count > 0;
    }

    public Rectangle GetPixelCollisionArea()
    {
        if (!HasCollidingPixels()) return new Rectangle();

        var x = CollidingPixels.Select(point => point.X).Min();
        var y = CollidingPixels.Select(point => point.Y).Min();
        Point position = new(x, y);

        var width = CollidingPixels.Select(point => point.X).Max() - x;
        var height = CollidingPixels.Select(point => point.Y).Max() - y;
        Point size = new(width, height);

        return new Rectangle(position, size);
    }
}
