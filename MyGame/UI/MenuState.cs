using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.interfaces;
using MyGame.Enums;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using System.ComponentModel;
using System;

namespace MyGame.UI;

public class MenuState : State
{
        private List<Component> _components;

        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager contentManager)
          : base(game, graphicsDevice, contentManager)
        {
            Texture2D buttonTexture = _content.Load<Texture2D>("button");
            SpriteFont buttonFont = _content.Load<SpriteFont>("Font");

            var level1Button = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 100),
                Text = "Level 1",
            };

            level1Button.Click += NewGameButton_Click;

            var level2Button = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 200),
                Text = "Level 2",
            };

            level2Button.Click += NewGameButton_Click;


        Button quitGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 300),
                Text = "Quit Game",
            };

            quitGameButton.Click += QuitGameButton_Click;

            _components = new List<Component>()
              {
                level1Button,
                level2Button,
                quitGameButton,
              };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }


        private void NewGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
        }

        public override void PostUpdate(GameTime gameTime)
        {
            // remove sprites if they're not needed
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }

        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }
    }

