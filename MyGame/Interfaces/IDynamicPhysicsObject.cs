using MyGame.Collisions;
using System.Collections.Generic;

namespace MyGame.interfaces;

public interface IDynamicPhysicsObject : IPhysicsObject
{
    void HandleCollisions(List<Collision> collisions);
}
