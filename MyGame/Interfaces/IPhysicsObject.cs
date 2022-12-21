using MyGame.Sprites;

namespace MyGame.interfaces;

public interface IPhysicsObject : IGameObject
{
    Sprite GetSprite();
}
