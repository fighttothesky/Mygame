using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyGame.Input;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using MyGame.Character;
using MyGame.Content.interfaces;

namespace MyGame
{
    public class Game1 : Game
    {
        private SpriteBatch spriteBatch;
        private List<IGameObject> gameObjects;

        public Game1()
        {
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            
            GraphicsDeviceManager graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            InitializeGameObjects(Content);
        }

        private void InitializeGameObjects(ContentManager contentManager)
        {
            gameObjects = new List<IGameObject>
            {
                new Hero(contentManager, new KeyboardReader()),
            };
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            gameObjects.ForEach(gameObject => gameObject.Update(gameTime));
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.BackToFront,null, SamplerState.PointClamp,null,null);
            gameObjects.ForEach(gameObject => gameObject.Draw(spriteBatch));
            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}