using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.Characters;
using MyGame.Collisions;
using MyGame.interfaces;
using System.Collections.Generic;

namespace MyGame.Scenes.Levels
{
    public abstract class Level : Scene
    {
        private List<IDynamicPhysicsObject> dynamicPhysicsObjects = new List<IDynamicPhysicsObject>();
        private List<IPhysicsObject> otherPhysicsObjects = new List<IPhysicsObject>();

        protected Level(SceneManager sceneManager) : base(sceneManager)
        {

        }

        public void AddDynamicPhysicsObject(IDynamicPhysicsObject dynamicPhysicsObject)
        {
            dynamicPhysicsObjects.Add(dynamicPhysicsObject);
        }

        public void RemoveDynamicPhysicsObject(IDynamicPhysicsObject dynamicPhysicsObject)
        {
            dynamicPhysicsObjects.Remove(dynamicPhysicsObject);
        }

        public void AddPhysicsObject(IPhysicsObject physicsObject)
        {
            otherPhysicsObjects.Add(physicsObject);
        }

        public void RemovePhysicsObject(IPhysicsObject physicsObject)
        {
            otherPhysicsObjects.Remove(physicsObject);
        }

        // Combine all items of the 2 lists
        private List<IGameObject> Children()
        {
            var list = new List<IGameObject>();
            list.AddRange(dynamicPhysicsObjects);
            list.AddRange(otherPhysicsObjects);
            return list;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
            Children().ForEach(gameObject => gameObject.Draw(spriteBatch));
            spriteBatch.End();
        }

        public override void PostUpdate()
        {
            DeleteRemovedChildren();
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

                foreach (IPhysicsObject other in otherPhysicsObjects)
                {
                    if (dynamicPhysicsObject == other) continue;
                    if (PixelCollision.IsColliding(dynamicPhysicsObject, other, out Collision collision))
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
                    if (child is IDynamicPhysicsObject dynamic)
                    {
                        RemoveDynamicPhysicsObject(dynamic);
                    }
                    else
                    {
                        RemovePhysicsObject(child);
                    }
                }
            }
        }
    }
}
