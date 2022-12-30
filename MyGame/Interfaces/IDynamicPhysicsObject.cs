using System.Collections.Generic;
using MyGame.Collisions;

namespace MyGame.interfaces;

internal interface IDynamicPhysicsObject : IPhysicsObject
{
    void HandleCollisions(List<Collision> collisions);

    void ApplyGravity();
}
