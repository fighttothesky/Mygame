using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.interfaces;
using System;
using System.Collections.Generic;

namespace MyGame.Scenes.UI
{
    internal class GameOver : UIScene
    {
        private List<IGameObject> components;

        public GameOver(SceneManager sceneManager)
      : base(sceneManager)
        {
            Texture2D buttonTexture = sceneManager.Content.Load<Texture2D>("button");
            SpriteFont font = sceneManager.Content.Load<SpriteFont>("Font");

            Init(buttonTexture, font);

        }

        private void Init(Texture2D buttonTexture, SpriteFont font)
        {
            Text text = new Text("GAME OVER", font, new Vector2(300, 100), Color.Red);

            var backToMenu = new Button(buttonTexture, font)
            {
                Position = new Vector2(300, 200),
                Text = "Back to menu",
            };

            backToMenu.Click += BackToMenu_Click;

            Button quitGameButton = new Button(buttonTexture, font)
            {
                Position = new Vector2(300, 300),
                Text = "Quit Game",
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
