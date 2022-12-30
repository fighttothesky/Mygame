using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyGame.Characters;
using MyGame.Collisions;
using MyGame.Input;
using MyGame.interfaces;
using MyGame.Terrain;

namespace MyGame;

public class Game1 : Game
{
    private List<IDynamicPhysicsObject> dynamicPhysicsObjects;
    private List<IGameObject> gameObjects;
    private List<IPhysicsObject> physicsObjects;
    private SpriteBatch spriteBatch;

    public Game1()
    {
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        GraphicsDeviceManager graphics = new(this);
        graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        InitializeGameObjects(Content);
    }

    private void InitializeGameObjects(ContentManager contentManager)
    {
        Hero hero = new(contentManager, new KeyboardReader());

        dynamicPhysicsObjects = new List<IDynamicPhysicsObject>
        {
            hero
        };

        Cube cube1 = new(contentManager);
        cube1.Sprite.Position = new Vector2(200, 100);

        Cube cube2 = new(contentManager);
        cube2.Sprite.Position = new Vector2(0, 400);
        cube2.Sprite.Scale = new Vector2(4, 4);

        physicsObjects = new List<IPhysicsObject>
        {
            cube1,
            cube2
        };
        physicsObjects.AddRange(dynamicPhysicsObjects);

        gameObjects = new List<IGameObject>();
        gameObjects.AddRange(physicsObjects);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();

        Physics();

        gameObjects.ForEach(gameObject => gameObject.Update(gameTime));
        base.Update(gameTime);
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

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        spriteBatch.Begin(SpriteSortMode.BackToFront, null, SamplerState.PointClamp);
        gameObjects.ForEach(gameObject => gameObject.Draw(spriteBatch));
        spriteBatch.End();

        base.Draw(gameTime);
    }
}