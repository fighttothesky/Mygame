using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.interfaces;
using System;
using System.Collections.Generic;

namespace MyGame.Scenes.UI
{
    internal class GameOver : UIScene
    {

        public GameOver(SceneManager sceneManager)
      : base(sceneManager)
        {
            Texture2D buttonTexture = sceneManager.Content.Load<Texture2D>("button");
            SpriteFont font = sceneManager.Content.Load<SpriteFont>("Font");
            SpriteFont titleFont = sceneManager.Content.Load<SpriteFont>("TitleFont");

            Init(buttonTexture, font, titleFont);

        }

        private void Init(Texture2D buttonTexture, SpriteFont font, SpriteFont titleFont)
        {
            var background = new Background(sceneManager.Content, "Background_menu");
            background.Sprite.Scale = new Vector2(4, 4);
            AddUIComponent(background);

            int middleOfScreen = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2;

            Text text = new Text("GAME OVER", titleFont, new Vector2(middleOfScreen, 250), Color.Red);
            text.IsCentered = true;

            var backToMenu = new Button(buttonTexture, font, "Back to menu")
            {
                Position = new Vector2(middleOfScreen - 150, 400)
            };

            backToMenu.Click += BackToMenu_Click;

            Button quitGameButton = new Button(buttonTexture, font, "Quit Game")
            {
                Position = new Vector2(middleOfScreen - 150, 500)
            };

            quitGameButton.Click += QuitGameButton_Click;

            AddUIComponent(backToMenu);
            AddUIComponent(quitGameButton);
            AddUIComponent(text);
        }

        private void BackToMenu_Click(object sender, EventArgs e)
        {
            sceneManager.ChangeLevel(new MainMenu(sceneManager));
        }

        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            sceneManager.Exit();
        }
    }
}
