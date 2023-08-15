using MyGame.Collisions;
using System.Collections.Generic;

namespace MyGame.interfaces;

internal interface IDynamicPhysicsObject : IPhysicsObject
{
    void HandleCollisions(List<Collision> collisions);

    void ApplyGravity();
}
