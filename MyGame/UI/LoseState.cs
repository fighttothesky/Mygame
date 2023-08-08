using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.UI
{
    internal class LoseState : State
    {
        private List<Component> _components;

        public LoseState(Game1 game, GraphicsDevice graphicsDevice, ContentManager contentManager)
          : base(game, graphicsDevice, contentManager)
        {
            Texture2D buttonTexture = _content.Load<Texture2D>("button");
            SpriteFont buttonFont = _content.Load<SpriteFont>("Font");

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

            _components = new List<Component>()
              {
                backToMenu,
                quitGameButton,
              };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);
            // draw text "VICTORY" on screen
            spriteBatch.DrawString(_content.Load<SpriteFont>("Font"), "Lose", new Vector2(300, 100), Color.Red);


            spriteBatch.End();
        }

        public override void PostUpdate()
        {
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }

        private void BackToMenu_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
        }
        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }
    }
}
