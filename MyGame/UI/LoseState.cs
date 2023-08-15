using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MyGame.interfaces;
using System;
using System.Collections.Generic;

namespace MyGame.UI
{
    internal class LoseState : State
    {
        private List<IGameObject> components;

        public LoseState(Game game, GraphicsDevice graphicsDevice, ContentManager contentManager)
          : base(game, graphicsDevice, contentManager)
        {
            Texture2D buttonTexture = content.Load<Texture2D>("button");
            SpriteFont buttonFont = content.Load<SpriteFont>("Font");

            var backToMenu = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 200),
                Text = "Back to menu",
            };

            backToMenu.Click += BackToMenu_Click;

            Button quitGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 300),
                Text = "Quit Game",
            };

            quitGameButton.Click += QuitGameButton_Click;

            components = new List<IGameObject>()
              {
                backToMenu,
                quitGameButton,
              };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            foreach (var component in components)
                component.Draw(spriteBatch);
            // draw text "VICTORY" on screen
            spriteBatch.DrawString(content.Load<SpriteFont>("Font"), "Lose", new Vector2(300, 100), Color.Red);


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

        private void BackToMenu_Click(object sender, EventArgs e)
        {
            game.ChangeState(new MenuState(game, graphicsDevice, content));
        }
        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            game.Exit();
        }
    }
}
