using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MyGame.Characters;
using MyGame.Collisions;
using MyGame.Input;
using MyGame.interfaces;
using MyGame.Terrain;
using MyGame.Terrain.Blocks;
using System.Collections.Generic;
using System.Linq;

namespace MyGame.UI;
public class Level2State : State
{

    // TODO: make getter for gameObjects (combo dynamicPhysicsObjects and physicsObjects)
    private List<IGameObject> gameObjects;
    private List<IDynamicPhysicsObject> dynamicPhysicsObjects;
    private List<IPhysicsObject> physicsObjects;

    private SpriteFont font;


    // 1 -> earth top
    // 2 -> earth middle
    // 3 -> Stone top left
    // 4 -> Stone top middle
    // 5 -> Stone top right
    // 6 -> Stone middle left
    // 7 -> Stone middle middle
    // 8 -> Stone middle right
    // 9 -> Stone bottom left
    // 10 -> Stone bottom middle
    // 11 -> Stone bottom right
    int[,] gameboard = new int[,]
    {
            { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,1 },
            { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6,7,7,7,10,10,10,10,10,10,10,10,10,10,10,10,10,10,1 },
            { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6,7,7,8,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
            { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6,7,7,8,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
            { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,9,10,10,11,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
            { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
            { 1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
            { 1,0,0,0,0,0,0,0,0,2,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
            { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,4,5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
            { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6,7,8,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
            { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6,7,7,4,4,4,4,4,4,4,4,4,5,0,0,0,0,0,1 },
            { 1,0,0,0,0,0,0,0,0,0,0,0,0,3,4,7,7,7,7,7,7,7,7,7,7,7,7,11,0,0,0,0,0,1 },
            { 1,0,0,0,0,1,1,0,0,0,0,0,0,6,7,7,7,7,7,10,10,10,10,10,10,10,11,0,0,0,0,0,0,1 },
            { 1,0,0,0,0,2,2,0,0,0,0,3,4,7,7,7,7,7,8,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
            { 1,1,1,1,1,2,2,0,0,0,0,6,7,7,7,7,7,7,8,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
            { 1,2,2,2,2,2,2,0,0,3,4,7,7,7,7,7,7,7,8,0,0,0,0,0,0,0,0,0,0,0,3,4,4,1 },
            { 1,2,2,2,2,2,2,1,1,6,7,7,7,7,7,7,7,7,7,4,4,4,4,4,4,4,4,5,0,0,6,7,7,1 },
            { 1,2,2,2,2,2,2,2,2,6,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,4,4,7,7,7,1 },
    };

    public Level2State(Game game, GraphicsDevice graphicsDevice, ContentManager contentManager)
      : base(game, graphicsDevice, contentManager)
    {
        font = content.Load<SpriteFont>("Font");

        Hero hero = new Hero(contentManager, new KeyboardReader());
        hero.animationManager.SetPosition(new Vector2(0, 0));

        Enemy1 enemy1 = new Enemy1(contentManager, 400);
        enemy1.animationManager.SetPosition(new Vector2(1400, 500));

        Enemy2 enemy2 = new Enemy2(contentManager, 500);
        enemy2.animationManager.SetPosition(new Vector2(650, 0));


        dynamicPhysicsObjects = new List<IDynamicPhysicsObject>
        {
            hero,
            enemy1,
            enemy2,
        };

        Spike spike1 = new Spike(contentManager);
        spike1.Sprite.Scale = new Vector2(4, 4);
        spike1.Sprite.SpritePosition = new Vector2(420, 900);

        Spike spike2 = new Spike(contentManager);
        spike2.Sprite.Scale = new Vector2(4, 4);
        spike2.Sprite.SpritePosition = new Vector2(360, 900);


        Spike spike3 = new Spike(contentManager);
        spike3.Sprite.Scale = new Vector2(4, 4);
        spike3.Sprite.SpritePosition = new Vector2(1620, 960);

        Spike spike4 = new Spike(contentManager);
        spike4.Sprite.Scale = new Vector2(4, 4);
        spike4.Sprite.SpritePosition = new Vector2(1680, 960);



        // COINS
        Coin kiwi1 = new Coin(contentManager);
        kiwi1.animationManager.SetPosition(new Vector2(100, 500));

        Coin kiwi2 = new Coin(contentManager);
        kiwi2.animationManager.SetPosition(new Vector2(300, 100));

        Coin kiwi3 = new Coin(contentManager);
        kiwi3.animationManager.SetPosition(new Vector2(1100, 800));

        physicsObjects = new List<IPhysicsObject>
        {
            spike1,
            spike2,
            spike3,
            spike4,
            kiwi1,
            kiwi2,
            kiwi3,
        };

        // Make the gameboard
        for (int l = 0; l < gameboard.GetLength(0); l++)
        {
            for (int c = 0; c < gameboard.GetLength(1); c++)
            {
                if (gameboard[l, c] == 1)
                {
                    Block block2 = new Block(contentManager, Enums.BlockType.EARTH_TOP);
                    block2.SpritePosition = new Vector2((c * 15 * 4) - 15 * 4, (l * 15 * 4) - 15 * 4);
                    block2.Scale = new Vector2(4, 4);
                    physicsObjects.Add(block2);
                }
                if (gameboard[l, c] == 2)
                {
                    Block block = new Block(contentManager, Enums.BlockType.EARTH_CENTER);
                    block.SpritePosition = new Vector2((c * 15 * 4) - 15 * 4, (l * 15 * 4) - 15 * 4);
                    block.Scale = new Vector2(4, 4);
                    physicsObjects.Add(block);
                }
                if (gameboard[l, c] == 3)
                {
                    Block block2 = new Block(contentManager, Enums.BlockType.STONE_TOP_LEFTCORNER);
                    block2.SpritePosition = new Vector2((c * 15 * 4) - 15 * 4, (l * 15 * 4) - 15 * 4);
                    block2.Scale = new Vector2(4, 4);
                    physicsObjects.Add(block2);
                }

                if (gameboard[l, c] == 4)
                {
                    Block block2 = new Block(contentManager, Enums.BlockType.STONE_TOP);
                    block2.SpritePosition = new Vector2((c * 15 * 4) - 15 * 4, (l * 15 * 4) - 15 * 4);
                    block2.Scale = new Vector2(4, 4);
                    physicsObjects.Add(block2);
                }
                if (gameboard[l, c] == 5)
                {
                    Block block2 = new Block(contentManager, Enums.BlockType.STONE_TOP_RIGHTCORNER);
                    block2.SpritePosition = new Vector2((c * 15 * 4) - 15 * 4, (l * 15 * 4) - 15 * 4);
                    block2.Scale = new Vector2(4, 4);
                    physicsObjects.Add(block2);
                }
                if (gameboard[l, c] == 6)
                {
                    Block block2 = new Block(contentManager, Enums.BlockType.STONE_LEFT);
                    block2.SpritePosition = new Vector2((c * 15 * 4) - 15 * 4, (l * 15 * 4) - 15 * 4);
                    block2.Scale = new Vector2(4, 4);
                    physicsObjects.Add(block2);
                }
                if (gameboard[l, c] == 7)
                {
                    Block block2 = new Block(contentManager, Enums.BlockType.STONE_CENTER);
                    block2.SpritePosition = new Vector2((c * 15 * 4) - 15 * 4, (l * 15 * 4) - 15 * 4);
                    block2.Scale = new Vector2(4, 4);
                    physicsObjects.Add(block2);
                }
                if (gameboard[l, c] == 8)
                {
                    Block block2 = new Block(contentManager, Enums.BlockType.STONE_RIGHT);
                    block2.SpritePosition = new Vector2((c * 15 * 4) - 15 * 4, (l * 15 * 4) - 15 * 4);
                    block2.Scale = new Vector2(4, 4);
                    physicsObjects.Add(block2);
                }
                if (gameboard[l, c] == 9)
                {
                    Block block2 = new Block(contentManager, Enums.BlockType.STONE_BOTTOM_LEFTCORNER);
                    block2.SpritePosition = new Vector2((c * 15 * 4) - 15 * 4, (l * 15 * 4) - 15 * 4);
                    block2.Scale = new Vector2(4, 4);
                    physicsObjects.Add(block2);
                }
                if (gameboard[l, c] == 10)
                {
                    Block block2 = new Block(contentManager, Enums.BlockType.STONE_BOTTOM);
                    block2.SpritePosition = new Vector2((c * 15 * 4) - 15 * 4, (l * 15 * 4) - 15 * 4);
                    block2.Scale = new Vector2(4, 4);
                    physicsObjects.Add(block2);
                }
                if (gameboard[l, c] == 11)
                {
                    Block block2 = new Block(contentManager, Enums.BlockType.STONE_BOTTOM_RIGHTCORNER);
                    block2.SpritePosition = new Vector2((c * 15 * 4) - 15 * 4, (l * 15 * 4) - 15 * 4);
                    block2.Scale = new Vector2(4, 4);
                    physicsObjects.Add(block2);
                }
            }
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

            // check if game object is a Enemy1
            if (gameObjects[i] is ISmartEnemy smartEnemy)
            {
                // check if smartEnemy is removed
                if (smartEnemy.IsDead)
                {
                    // remove smartEnemy from game objects
                    gameObjects.RemoveAt(i--);
                    physicsObjects.Remove((IDynamicPhysicsObject)smartEnemy);
                }
            }
        }
        // Show win state if 3 coins are collected in the hero's score
        if (physicsObjects.OfType<Hero>().First().Score >= 3)
        {
            game.ChangeState(new WinState(game, graphicsDevice, content));
        }

        // If hero lose is true, change to lose state
        if (physicsObjects.OfType<Hero>().First().Lose)
        {
            game.ChangeState(new LoseState(game, graphicsDevice, content));
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

