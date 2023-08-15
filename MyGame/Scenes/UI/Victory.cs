using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MyGame.interfaces;
using System;
using System.Collections.Generic;

namespace MyGame.Scenes.UI
{
    internal class Victory : Scene
    {
        private List<IGameObject> components;

        public Victory(SceneManager sceneManager)
            : base(sceneManager)
        {
            Texture2D buttonTexture = sceneManager.Content.Load<Texture2D>("button");
            SpriteFont buttonFont = sceneManager.Content.Load<SpriteFont>("Font");

            Button quitGameButton = new Button(buttonTexture, buttonFont)
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

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            foreach (var component in components)
                component.Draw(spriteBatch);
            // draw text "VICTORY" on screen
            spriteBatch.DrawString(sceneManager.Content.Load<SpriteFont>("Font"), "VICTORY", new Vector2(300, 100), Color.White);


            spriteBatch.End();
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
}
