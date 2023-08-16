using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.Characters;
using MyGame.Input;
using MyGame.interfaces;
using MyGame.Scenes.Levels;
using System;
using System.Collections.Generic;

namespace MyGame.Scenes.UI;

internal class MainMenu : UIScene
{

    public MainMenu(SceneManager sceneManager)
      : base(sceneManager)
    {
        Texture2D buttonTexture = sceneManager.Content.Load<Texture2D>("button");
        SpriteFont buttonFont = sceneManager.Content.Load<SpriteFont>("Font");
        Init(buttonTexture, buttonFont);
    }

    private void Init(Texture2D buttonTexture, SpriteFont buttonFont)
    {
        int middleOfScreen = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2;

        var background = new Background(sceneManager.Content, "Background_menu");
        background.Sprite.Scale = new Vector2(4, 4);
        AddUIComponent(background);

        var level1Button = new Button(buttonTexture, buttonFont)
        {
            Position = new Vector2(middleOfScreen - 150, 400),
            Text = "Level 1",
        };

        level1Button.Click += Level1Button_Click;

        var level2Button = new Button(buttonTexture, buttonFont)
        {
            Position = new Vector2(middleOfScreen - 150, 500),
            Text = "Level 2",
        };

        level2Button.Click += Level2Button_Click;


        Button quitGameButton = new Button(buttonTexture, buttonFont)
        {
            Position = new Vector2(middleOfScreen - 150, 600),
            Text = "Quit Game",
        };

        quitGameButton.Click += QuitGameButton_Click;

        AddUIComponent(level1Button);
        AddUIComponent(level2Button);
        AddUIComponent(quitGameButton);
    }

    private void Level1Button_Click(object sender, EventArgs e)
    {
        sceneManager.ChangeLevel(new Level1(sceneManager));
    }

    private void Level2Button_Click(object sender, EventArgs e)
    {
        sceneManager.ChangeLevel(new Level2(sceneManager));
    }

    private void QuitGameButton_Click(object sender, EventArgs e)
    {
        sceneManager.Exit();
    }
}

