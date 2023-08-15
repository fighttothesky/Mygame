using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyGame.Scenes;

namespace MyGame;

public class Game : Microsoft.Xna.Framework.Game
{
    private SpriteBatch spriteBatch;
    SceneManager sceneManager;

    public Game()
    {
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        GraphicsDeviceManager graphics = new(this);
        graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        sceneManager = new SceneManager(GraphicsDevice, Content, this);
        sceneManager.Start();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();

        sceneManager.CurrentLevel.Update(gameTime);
        sceneManager.CurrentLevel.PostUpdate();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        sceneManager.CurrentLevel.Draw(gameTime, spriteBatch);
    }
}