using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.Characters;
using MyGame.Collisions;
using MyGame.Input;
using MyGame.interfaces;
using System.Collections.Generic;

namespace MyGame.Scenes.Levels
{
    public abstract class Level : Scene
    {
        private List<IDynamicPhysicsObject> dynamicPhysicsObjects = new List<IDynamicPhysicsObject>();
        private List<IPhysicsObject> otherPhysicsObjects = new List<IPhysicsObject>();
        public List<IGameObject> backgroundObjects = new List<IGameObject>();

        protected Level(SceneManager sceneManager) : base(sceneManager)
        {

        }

        public void AddDynamicPhysicsObject(IDynamicPhysicsObject dynamicPhysicsObject)
        {
            dynamicPhysicsObjects.Add(dynamicPhysicsObject);
        }

        public void AddPhysicsObject(IPhysicsObject physicsObject)
        {
            otherPhysicsObjects.Add(physicsObject);
        }

        public void RemoveChild(IPhysicsObject gameObject)
        {
            if (gameObject is IDynamicPhysicsObject dynamicPhysicsObject)
            {
                dynamicPhysicsObjects.Remove(dynamicPhysicsObject);
            }
            else if (gameObject is IPhysicsObject physicsObject)
            {
                otherPhysicsObjects.Remove(physicsObject);
            }
        }

        public IInputReader GetInputReader()
        {
            return KeyboardReader.Instance;
        }

        // Combine all items of the 2 lists
        private List<IGameObject> Children()
        {
            var list = new List<IGameObject>();
            list.AddRange(otherPhysicsObjects);
            list.AddRange(dynamicPhysicsObjects);
            return list;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
            backgroundObjects.ForEach(gameObject => gameObject.Draw(spriteBatch));
            Children().ForEach(gameObject => gameObject.Draw(spriteBatch));
            spriteBatch.End();
        }

        public override void PostUpdate()
        {
            DeleteRemovedChildren();
            CheckEndConditions();
        }

        public abstract void CheckEndConditions();

        public override void Update(GameTime gameTime)
        {
            Physics();
            foreach (var gameObjects in Children())
                gameObjects.Update(gameTime);
        }

        private void Physics()
        {
            foreach (IDynamicPhysicsObject dynamicPhysicsObject in dynamicPhysicsObjects)
            {
                List<Collision> collisions = new();

                foreach (IPhysicsObject child in Children())
                {
                    if (dynamicPhysicsObject == child) continue;
                    if (PixelCollision.IsColliding(dynamicPhysicsObject, child, out Collision collision))
                        collisions.Add(collision);
                }

                dynamicPhysicsObject.HandleCollisions(collisions);
                if (dynamicPhysicsObject is IGravityObject gravityObject)
                {
                    gravityObject.ApplyGravity();
                }
            }
        }

        public void DeleteRemovedChildren()
        {
            foreach (IPhysicsObject child in Children())
            {
                if (child is IRemovable removable && removable.IsRemoved() && removable is not Hero)
                {
                    RemoveChild(child);
                }
            }
        }
    }
}
