using System.Collections.Generic;
using MyGame.Collisions;
using MyGame.Sprites;

namespace MyGame.interfaces;

internal interface IDynamicPhysicsObject : IPhysicsObject
{
    void HandleCollisions(List<Collision> collisions);
    
    void ApplyGravity();
}
