using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyGame.Input;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using MyGame.Characters;
using MyGame.interfaces;
using MyGame.Terrain;

namespace MyGame
{
    public class Game1 : Game
    {
        private SpriteBatch spriteBatch;
        private List<IGameObject> gameObjects;
        private List<IPhysicsObject> physicsObjects;

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
            Hero hero = new Hero(contentManager, new KeyboardReader());
            
            Cube cube1 = new Cube(contentManager);
            cube1.Sprite.Position = new Vector2(200, 100);
            
            Cube cube2 = new Cube(contentManager);
            cube2.Sprite.Position = new Vector2(0, 400);
            cube2.Sprite.Scale = new Vector2(4, 4);

            physicsObjects = new List<IPhysicsObject>
            {
                hero,
                cube1,
                cube2
            };

            gameObjects = new List<IGameObject>();
            gameObjects.AddRange(physicsObjects);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            gameObjects.ForEach(gameObject => gameObject.Update(gameTime));
            physicsObjects.ForEach(physicsObject1 => physicsObjects.ForEach(physicsObject2 => physicsObject1.CollidesWith(physicsObject2)));
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