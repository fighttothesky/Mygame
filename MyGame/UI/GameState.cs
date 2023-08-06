using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyGame.Characters;
using MyGame.Input;
using MyGame.interfaces;
using MyGame.Terrain;
using MyGame.Collisions;

namespace MyGame.UI;
  public class GameState : State
{
    private List<IDynamicPhysicsObject> dynamicPhysicsObjects;
    private List<IGameObject> gameObjects;
    private List<IPhysicsObject> physicsObjects;

    public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager contentManager)
      : base(game, graphicsDevice, contentManager)
    {
        Hero hero = new Hero(contentManager, new KeyboardReader());
        hero.animationManager.SetPosition(new Vector2(500, 500));

        dynamicPhysicsObjects = new List<IDynamicPhysicsObject>
        {
            hero,
        };

        Cube cube1 = new Cube(contentManager);
        cube1.Sprite.Position = new Vector2(400, 800);

        Cube cube2 = new Cube(contentManager);
        cube2.Sprite.Position = new Vector2(800, 900);

        physicsObjects = new List<IPhysicsObject>
        {
            cube1,
            cube2,
        };

        int left = -100;
        int top = 1000;
        for (int i = 0; i < 10; i++)
        {
            Cube cube = new Cube(contentManager);
            cube.Sprite.Position = new Vector2(left + i * 250, top);
            cube.Sprite.Scale = new Vector2(4, 4);
            physicsObjects.Add(cube);
        }

        physicsObjects.AddRange(dynamicPhysicsObjects);

        gameObjects = new List<IGameObject>();
        gameObjects.AddRange(physicsObjects);

    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {

        spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
        gameObjects.ForEach(gameObject => gameObject.Draw(spriteBatch));
        spriteBatch.End();

    }

    public override void PostUpdate(GameTime gameTime)
    {

    }

    public override void Update(GameTime gameTime)
    {
        Physics();
        foreach (var gameObjects in gameObjects)
            gameObjects.Update(gameTime);
    }

    private void Physics()
    {
        foreach (IDynamicPhysicsObject dynamicPhysicsObject in dynamicPhysicsObjects)
        {
            List<Collision> collisions = new();

            foreach (IPhysicsObject other in physicsObjects)
            {
                if (dynamicPhysicsObject == other) continue;
                if (PixelCollision.IsColliding(dynamicPhysicsObject, other, out Collision collision))
                    collisions.Add(collision);
            }

            dynamicPhysicsObject.HandleCollisions(collisions);
            dynamicPhysicsObject.ApplyGravity();
        }
    }
}

