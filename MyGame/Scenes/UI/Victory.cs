using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.interfaces;
using System;
using System.Collections.Generic;

namespace MyGame.Scenes.UI
{
    internal class Victory : UIScene
    {
        private List<IGameObject> components;

        public Victory(SceneManager sceneManager)
            : base(sceneManager)
        {
            Texture2D buttonTexture = sceneManager.Content.Load<Texture2D>("button");
            SpriteFont font = sceneManager.Content.Load<SpriteFont>("Font");
            Init(buttonTexture, font);
        }

        private void Init(Texture2D buttonTexture, SpriteFont font)
        {
            var background = new Background(sceneManager.Content, "Background_menu");
            background.Sprite.Scale = new Vector2(4, 4);
            AddUIComponent(background);

            int middleOfScreen = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2;
            Text text = new Text("YOU WIN!", font, new Vector2(middleOfScreen - 40, 350), Color.White);

            Button quitGameButton = new Button(buttonTexture, font)
            {
                Position = new Vector2(middleOfScreen - 150, 400),
                Text = "Quit Game",
            };

            quitGameButton.Click += QuitGameButton_Click;

            components = new List<IGameObject>()
              {
                quitGameButton,
              };

            AddUIComponent(quitGameButton);
            AddUIComponent(text);
        }

        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            sceneManager.Exit();
        }
    }
}
