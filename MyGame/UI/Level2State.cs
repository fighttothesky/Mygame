using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MyGame.Characters;
using MyGame.Collisions;
using MyGame.Input;
using MyGame.interfaces;
using MyGame.Terrain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.UI
{
    internal class Level2State : State
    {

        // TODO: make getter for gameObjects (combo dynamicPhysicsObjects and physicsObjects)
        private List<IGameObject> gameObjects;
        private List<IDynamicPhysicsObject> dynamicPhysicsObjects;
        private List<IPhysicsObject> physicsObjects;

        private SpriteFont font;

        public Level2State(Game1 game, GraphicsDevice graphicsDevice, ContentManager contentManager)
          : base(game, graphicsDevice, contentManager)
        {
            font = _content.Load<SpriteFont>("Font");

            Hero hero = new Hero(contentManager, new KeyboardReader());
            hero.animationManager.SetPosition(new Vector2(0, 0));

            dynamicPhysicsObjects = new List<IDynamicPhysicsObject>
        {
            hero,
        };

            Cube cube1 = new Cube(contentManager);
            cube1.Sprite.SpritePosition = new Vector2(400, 800);

            Cube cube2 = new Cube(contentManager);
            cube2.Sprite.SpritePosition = new Vector2(800, 900);

            Coin kiwi = new Coin(contentManager);
            kiwi.animationManager.SetPosition(new Vector2(200, 800));

            physicsObjects = new List<IPhysicsObject>
        {
            kiwi,
        };

            int left = -100;
            int top = 1000;
            for (int i = 0; i < 10; i++)
            {
                Cube cube = new Cube(contentManager);
                cube.Sprite.SpritePosition = new Vector2(left + i * 250, top);
                cube.Sprite.Scale = new Vector2(4, 4);
                physicsObjects.Add(cube);
            }

            physicsObjects.AddRange(dynamicPhysicsObjects);

            gameObjects = new List<IGameObject>();
            gameObjects.AddRange(physicsObjects);

            PostUpdate();

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
            gameObjects.ForEach(gameObject => gameObject.Draw(spriteBatch));

            int fontY = 10;
            int i = 0;

            foreach (var physicsObject in physicsObjects)
            {
                if (physicsObject is Hero)
                    spriteBatch.DrawString(font, string.Format("Points: " + ((Hero)physicsObject).Score, ++i, ((Hero)physicsObject)), new Vector2(10, fontY += 20), Color.Black);
            }

            spriteBatch.End();

        }

        public override void PostUpdate()
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                // check if game object is a coin
                if (gameObjects[i] is Coin coin)
                {
                    // check if coin is removed
                    if (coin.isRemoved)
                    {
                        // remove coin from game objects
                        gameObjects.RemoveAt(i--);
                        physicsObjects.Remove(coin);
                    }
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            Physics();
            foreach (var gameObjects in gameObjects)
                gameObjects.Update(gameTime);

        }

        private void Physics()
        {
            foreach (IDynamicPhysicsObject dynamicPhysicsObject in dynamicPhysicsObjects)
            {
                List<Collision> collisions = new();

                foreach (IPhysicsObject other in physicsObjects)
                {
                    if (dynamicPhysicsObject == other) continue;
                    if (PixelCollision.IsColliding(dynamicPhysicsObject, other, out Collision collision))
                        collisions.Add(collision);
                }

                dynamicPhysicsObject.HandleCollisions(collisions);
                dynamicPhysicsObject.ApplyGravity();
            }
        }
    }
}
