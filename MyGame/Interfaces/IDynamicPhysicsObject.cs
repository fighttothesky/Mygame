using MyGame.Sprites;

namespace MyGame.interfaces;

internal interface IDynamicPhysicsObject : IPhysicsObject
{
    void ApplyGravity(AnimationManager animationManager);
}
