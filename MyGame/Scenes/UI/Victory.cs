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
            Text text = new Text("YOU WIN!", font, new Vector2(300, 100), Color.White);

            Button quitGameButton = new Button(buttonTexture, font)
            {
                Position = new Vector2(300, 300),
                Text = "Quit Game",
            };

            quitGameButton.Click += QuitGameButton_Click;

            components = new List<IGameObject>()
              {
                quitGameButton,
              };
        }

        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            sceneManager.Exit();
        }
    }
}
