using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using MyGame.Characters;
using MyGame.Input;
using MyGame.interfaces;
using MyGame.Terrain;
using MyGame.Collisions;
using System.Linq;

namespace MyGame.UI;
public class Level1State : State
{

    // TODO: make getter for gameObjects (combo dynamicPhysicsObjects and physicsObjects)
    private List<IGameObject> gameObjects;
    private List<IDynamicPhysicsObject> dynamicPhysicsObjects;
    private List<IPhysicsObject> physicsObjects;

    private SpriteFont font;

    public Level1State(Game1 game, GraphicsDevice graphicsDevice, ContentManager contentManager)
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
        cube1.Sprite.Position = new Vector2(400, 800);

        Cube cube2 = new Cube(contentManager);
        cube2.Sprite.Position = new Vector2(800, 900);

        Coin kiwi = new Coin(contentManager);
        kiwi.animationManager.SetPosition(new Vector2(500, 800));

        Spike spike1 = new Spike(contentManager);
        spike1.Sprite.Scale = new Vector2(4, 4);
        spike1.Sprite.Position = new Vector2(300, 940);
        
        Enemy1 enemy1 = new Enemy1(contentManager);
        enemy1.Sprite.Position = new Vector2(1000, 900);

        physicsObjects = new List<IPhysicsObject>
        {
            cube1,
            cube2,
            kiwi,
            spike1,
            enemy1,
        };

        int left = -100;
        int top = 1000;
        for (int i = 0; i < 10; i++)
        {
            Cube cube = new Cube(contentManager);
            cube.Sprite.Position = new Vector2(left + i * 250, top);
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

        // Show win state if 5 coins are collected in the hero's score
        if (physicsObjects.OfType<Hero>().First().Score >= 1)
        {
            _game.ChangeState(new WinState(_game, _graphicsDevice, _content));
        }

        // If hero lose is true, change to lose state
        if (physicsObjects.OfType<Hero>().First().Lose)
        {
            _game.ChangeState(new LoseState(_game, _graphicsDevice, _content));
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

