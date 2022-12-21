using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MyGame.Enums;
using MyGame.interfaces;

namespace MyGame.Collisions;

public class Collision
{
    public IPhysicsObject This { get; }
    public IPhysicsObject Other { get; }
    public Rectangle CollisionArea { get; }
    
    public CollisionDirection Direction { get; }
    
    public List<Direction> Directions { get; }

    public Collision(IPhysicsObject thisObject, IPhysicsObject otherObject, Rectangle collisionArea)
    {
        This = thisObject;
        Other = otherObject;
        CollisionArea = collisionArea;
        Direction = GetCollisionDirections(This, Other);

        Directions = new List<Direction>();
        if (Direction.Left) Directions.Add(Enums.Direction.LEFT);
        if (Direction.Right) Directions.Add(Enums.Direction.RIGHT);
        if (Direction.Top) Directions.Add(Enums.Direction.UP);
        if (Direction.Bottom) Directions.Add(Enums.Direction.DOWN);
    }

    private static CollisionDirection GetCollisionDirections(IPhysicsObject thisObject, IPhysicsObject otherObject)
    {
        Rectangle boundsA = thisObject.GetSprite().GetBoundingRectangle();
        Rectangle boundsB = otherObject.GetSprite().GetBoundingRectangle();

        CollisionDirection collisionDirection = new CollisionDirection();
        collisionDirection.Left = boundsB.Left < boundsA.Left && boundsA.Left < boundsB.Right;
        collisionDirection.Right = boundsB.Left < boundsA.Right && boundsA.Right < boundsB.Right;
        collisionDirection.Top = boundsB.Top < boundsA.Top && boundsA.Top < boundsB.Bottom;
        collisionDirection.Bottom = boundsB.Top < boundsA.Bottom && boundsA.Bottom < boundsB.Bottom;

        return collisionDirection;
    }
}
