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
using MyGame.UI;

namespace MyGame;

public class Game1 : Game
{
    private List<IDynamicPhysicsObject> dynamicPhysicsObjects;
    private List<IGameObject> gameObjects;
    private List<IPhysicsObject> physicsObjects;
    private SpriteBatch spriteBatch;
    //scene
    private GraphicsDeviceManager graphics;

    private State _currentState;

    private State _nextState;

    public void ChangeState(State state)
    {
        _nextState = state;
    }
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
        //menu
        _currentState = new MenuState(this, GraphicsDevice, Content);


        InitializeGameObjects(Content);
    }

    protected override void Initialize()
    {
        IsMouseVisible = true;

        base.Initialize();
    }

    private void InitializeGameObjects(ContentManager contentManager)
    {
        Hero hero = new Hero(contentManager, new KeyboardReader());
        hero.animationManager.SetPosition(new Vector2(0, 0));

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

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();

        Physics();

        // Menu
        if (_nextState != null)
        {
            _currentState = _nextState;

            _nextState = null;
        }

        _currentState.Update(gameTime);

        _currentState.PostUpdate(gameTime);

        //gameObjects.ForEach(gameObject => gameObject.Update(gameTime));
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

        //spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
        //gameObjects.ForEach(gameObject => gameObject.Draw(spriteBatch));
        //spriteBatch.End();

        _currentState.Draw(gameTime, spriteBatch);
        base.Draw(gameTime);
    }
}