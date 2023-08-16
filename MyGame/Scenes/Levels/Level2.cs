using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.Characters;
using MyGame.Collisions;
using MyGame.Input;
using MyGame.interfaces;
using MyGame.Scenes.UI;
using MyGame.Terrain;
using System.Collections.Generic;

namespace MyGame.Scenes.Levels;
public class Level2 : Level
{
    private Hero hero;

    public Level2(SceneManager sceneManager)
      : base(sceneManager)
    {
        InitBoard();
        InitEntities(sceneManager);
    }
    // Make the gameboard
    private void InitBoard()
    {
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
                { 1,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,1 },
                { 1,7,10,10,10,10,10,10,10,10,7,7,7,7,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,7,1 },
                { 1,8,0,0,0,0,0,0,0,0,6,7,7,8,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6,1 },
                { 1,8,0,0,0,0,0,0,0,0,9,10,10,11,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6,1 },
                { 1,8,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6,1 },
                { 1,8,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6,1 },
                { 1,8,0,0,0,0,0,0,3,5,0,0,0,0,0,0,3,5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6,1 },
                { 1,8,0,0,0,0,0,0,6,8,0,0,0,0,0,0,6,8,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6,1 },
                { 1,8,0,0,0,0,0,0,6,7,4,4,4,4,4,4,7,8,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6,1 },
                { 1,8,0,0,0,0,0,0,9,10,10,10,10,10,10,10,10,11,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6,1 },
                { 1,8,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,4,4,4,4,4,4,4,4,7,1 },
                { 1,8,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,4,7,7,7,7,7,7,7,7,7,7,1 },
                { 1,11,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6,7,7,7,7,7,7,7,7,7,7,7,1 },
                { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,4,7,7,7,7,7,7,7,7,7,7,7,7,1 },
                { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6,7,7,7,7,7,7,7,7,7,7,7,7,7,1 },
                { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,4,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7 },
                { 1,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,1 },
                { 1,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,1 },
        };

        for (int l = 0; l < gameboard.GetLength(0); l++)
        {
            for (int c = 0; c < gameboard.GetLength(1); c++)
            {
                if (gameboard[l, c] == 1)
                {
                    Block block2 = new Block(sceneManager.Content, Enums.BlockType.EARTH_TOP);
                    block2.SpritePosition = new Vector2(c * 15 * 4 - 15 * 4, l * 15 * 4 - 15 * 4);
                    block2.Scale = new Vector2(4, 4);
                    AddPhysicsObject(block2);
                }
                if (gameboard[l, c] == 2)
                {
                    Block block = new Block(sceneManager.Content, Enums.BlockType.EARTH_CENTER);
                    block.SpritePosition = new Vector2(c * 15 * 4 - 15 * 4, l * 15 * 4 - 15 * 4);
                    block.Scale = new Vector2(4, 4);
                    AddPhysicsObject(block);
                }
                if (gameboard[l, c] == 3)
                {
                    Block block2 = new Block(sceneManager.Content, Enums.BlockType.STONE_TOP_LEFTCORNER);
                    block2.SpritePosition = new Vector2(c * 15 * 4 - 15 * 4, l * 15 * 4 - 15 * 4);
                    block2.Scale = new Vector2(4, 4);
                    AddPhysicsObject(block2);
                }

                if (gameboard[l, c] == 4)
                {
                    Block block2 = new Block(sceneManager.Content, Enums.BlockType.STONE_TOP);
                    block2.SpritePosition = new Vector2(c * 15 * 4 - 15 * 4, l * 15 * 4 - 15 * 4);
                    block2.Scale = new Vector2(4, 4);
                    AddPhysicsObject(block2);
                }
                if (gameboard[l, c] == 5)
                {
                    Block block2 = new Block(sceneManager.Content, Enums.BlockType.STONE_TOP_RIGHTCORNER);
                    block2.SpritePosition = new Vector2(c * 15 * 4 - 15 * 4, l * 15 * 4 - 15 * 4);
                    block2.Scale = new Vector2(4, 4);
                    AddPhysicsObject(block2);
                }
                if (gameboard[l, c] == 6)
                {
                    Block block2 = new Block(sceneManager.Content, Enums.BlockType.STONE_LEFT);
                    block2.SpritePosition = new Vector2(c * 15 * 4 - 15 * 4, l * 15 * 4 - 15 * 4);
                    block2.Scale = new Vector2(4, 4);
                    AddPhysicsObject(block2);
                }
                if (gameboard[l, c] == 7)
                {
                    Block block2 = new Block(sceneManager.Content, Enums.BlockType.STONE_CENTER);
                    block2.SpritePosition = new Vector2(c * 15 * 4 - 15 * 4, l * 15 * 4 - 15 * 4);
                    block2.Scale = new Vector2(4, 4);
                    AddPhysicsObject(block2);
                }
                if (gameboard[l, c] == 8)
                {
                    Block block2 = new Block(sceneManager.Content, Enums.BlockType.STONE_RIGHT);
                    block2.SpritePosition = new Vector2(c * 15 * 4 - 15 * 4, l * 15 * 4 - 15 * 4);
                    block2.Scale = new Vector2(4, 4);
                    AddPhysicsObject(block2);
                }
                if (gameboard[l, c] == 9)
                {
                    Block block2 = new Block(sceneManager.Content, Enums.BlockType.STONE_BOTTOM_LEFTCORNER);
                    block2.SpritePosition = new Vector2(c * 15 * 4 - 15 * 4, l * 15 * 4 - 15 * 4);
                    block2.Scale = new Vector2(4, 4);
                    AddPhysicsObject(block2);
                }
                if (gameboard[l, c] == 10)
                {
                    Block block2 = new Block(sceneManager.Content, Enums.BlockType.STONE_BOTTOM);
                    block2.SpritePosition = new Vector2(c * 15 * 4 - 15 * 4, l * 15 * 4 - 15 * 4);
                    block2.Scale = new Vector2(4, 4);
                    AddPhysicsObject(block2);
                }
                if (gameboard[l, c] == 11)
                {
                    Block block2 = new Block(sceneManager.Content, Enums.BlockType.STONE_BOTTOM_RIGHTCORNER);
                    block2.SpritePosition = new Vector2(c * 15 * 4 - 15 * 4, l * 15 * 4 - 15 * 4);
                    block2.Scale = new Vector2(4, 4);
                    AddPhysicsObject(block2);
                }
            }
        }
    }

    private void InitEntities(SceneManager sceneManager)
    {
        var background = new Background(sceneManager.Content, "Stone_Background");
        background.Sprite.Scale = new Vector2(4, 4);
        backgroundObjects.Add(background);

        // COINS
        Coin kiwi1 = new Coin(sceneManager.Content);
        kiwi1.animationManager.SetPosition(new Vector2(650, 350));
        AddDynamicPhysicsObject(kiwi1);

        Coin kiwi2 = new Coin(sceneManager.Content);
        kiwi2.animationManager.SetPosition(new Vector2(1700, 450));
        AddDynamicPhysicsObject(kiwi2);

        // Hero
        hero = new Hero(sceneManager.Content, GetInputReader());
        hero.animationManager.SetPosition(new Vector2(0, 800));
        AddDynamicPhysicsObject(hero);
        kiwi1.Attach(hero);
        kiwi2.Attach(hero);

        // Enemies
        Snail enemy1 = new Snail(sceneManager.Content, 400);
        enemy1.animationManager.SetPosition(new Vector2(1700, 500));
        AddDynamicPhysicsObject(enemy1);

        Bird enemy2 = new Bird(sceneManager.Content, 600);
        enemy2.animationManager.SetPosition(new Vector2(200, 150));
        AddDynamicPhysicsObject(enemy2);


        // Add spikes
        for (int i = 0; i < 2; i++)
        {
            Spike spike1 = new Spike(sceneManager.Content);
            spike1.Sprite.Scale = new Vector2(4, 4);
            spike1.Sprite.SpritePosition = new Vector2(420 + (i * 65), 895);
            AddDynamicPhysicsObject(spike1);

            Spike spike2 = new Spike(sceneManager.Content);
            spike2.Sprite.Scale = new Vector2(4, 4);
            spike2.Sprite.effects = SpriteEffects.FlipVertically;
            spike2.Sprite.SpritePosition = new Vector2(420 + (i * 65), 600);
            AddDynamicPhysicsObject(spike2);

            Spike spike3 = new Spike(sceneManager.Content);
            spike3.Sprite.Scale = new Vector2(4, 4);
            spike3.Sprite.effects = SpriteEffects.FlipVertically;
            spike3.Sprite.SpritePosition = new Vector2(650 + (i * 65), 240);
            AddDynamicPhysicsObject(spike3);
        }

    }

    public override void CheckEndConditions()
    {
        if (hero.IsRemoved())
        {
            sceneManager.ChangeLevel(new GameOver(sceneManager));
        }
        else if (hero.Score >= 2)
        {
            sceneManager.ChangeLevel(new Victory(sceneManager));
        }
    }
}
