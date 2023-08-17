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
        SpriteFont titleFont = sceneManager.Content.Load<SpriteFont>("TitleFont");
        Init(buttonTexture, buttonFont, titleFont);
    }

    private void Init(Texture2D buttonTexture, SpriteFont buttonFont, SpriteFont titleFont)
    {
        var background = new Background(sceneManager.Content, "Background_menu");
        background.Sprite.Scale = new Vector2(4, 4);
        AddUIComponent(background);

        int middleOfScreen = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2;
        Text text = new Text("THE FLYING RADISHMAN", titleFont, new Vector2(middleOfScreen, 250), Color.White);
        text.IsCentered = true;

        var level1Button = new Button(buttonTexture, buttonFont, "Level 1")
        {
            Position = new Vector2(middleOfScreen - 150, 400)
        };

        level1Button.Click += Level1Button_Click;

        var level2Button = new Button(buttonTexture, buttonFont, "Level 2")
        {
            Position = new Vector2(middleOfScreen - 150, 500)
        };

        level2Button.Click += Level2Button_Click;


        Button quitGameButton = new Button(buttonTexture, buttonFont, "Quit Game")
        {
            Position = new Vector2(middleOfScreen - 150, 600)
        };

        quitGameButton.Click += QuitGameButton_Click;

        AddUIComponent(text);
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

