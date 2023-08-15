using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.interfaces;
using MyGame.Scenes.Levels;
using System;
using System.Collections.Generic;

namespace MyGame.Scenes.UI;

internal class MainMenu : Scene
{
    private List<IGameObject> components;

    public MainMenu(SceneManager sceneManager)
      : base(sceneManager)
    {
        Texture2D buttonTexture = sceneManager.Content.Load<Texture2D>("button");
        SpriteFont buttonFont = sceneManager.Content.Load<SpriteFont>("Font");

        var level1Button = new Button(buttonTexture, buttonFont)
        {
            Position = new Vector2(300, 100),
            Text = "Level 1",
        };

        level1Button.Click += Level1Button_Click;

        var level2Button = new Button(buttonTexture, buttonFont)
        {
            Position = new Vector2(300, 200),
            Text = "Level 2",
        };

        level2Button.Click += Level2Button_Click;


        Button quitGameButton = new Button(buttonTexture, buttonFont)
        {
            Position = new Vector2(300, 300),
            Text = "Quit Game",
        };

        quitGameButton.Click += QuitGameButton_Click;

        components = new List<IGameObject>()
              {
                level1Button,
                level2Button,
                quitGameButton,
              };
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();

        foreach (var component in components)
            component.Draw(spriteBatch);

        spriteBatch.End();
    }


    private void Level1Button_Click(object sender, EventArgs e)
    {
        sceneManager.ChangeLevel(new Level1(sceneManager));
    }

    private void Level2Button_Click(object sender, EventArgs e)
    {
        sceneManager.ChangeLevel(new Level2(sceneManager));
    }

    public override void PostUpdate()
    {
    }

    public override void Update(GameTime gameTime)
    {
        foreach (var component in components)
            component.Update(gameTime);
    }

    private void QuitGameButton_Click(object sender, EventArgs e)
    {
        sceneManager.Exit();
    }
}

