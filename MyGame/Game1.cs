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
    //private List<IDynamicPhysicsObject> dynamicPhysicsObjects;
    //private List<IGameObject> gameObjects;
    //private List<IPhysicsObject> physicsObjects;
    private SpriteBatch spriteBatch;
    //scene
    private GraphicsDeviceManager graphics;

    private State currentState;

    private State nextState;

    public void ChangeState(State state)
    {
        nextState = state;
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
        currentState = new MenuState(this, GraphicsDevice, Content);


        //InitializeGameObjects(Content);
    }

    protected override void Initialize()
    {

        base.Initialize();
    }


    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();


        // Menu
        if (nextState != null)
        {
            currentState = nextState;

            nextState = null;
        }

        currentState.Update(gameTime);

        currentState.PostUpdate();

        //gameObjects.ForEach(gameObject => gameObject.Update(gameTime));
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        currentState.Draw(gameTime, spriteBatch);
        //base.Draw(gameTime);
    }
}