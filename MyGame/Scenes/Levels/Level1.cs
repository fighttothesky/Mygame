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
public class Level1 : Level
{
    private Hero hero;

    public Level1(SceneManager sceneManager)
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
                { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,1 },
                { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6,7,7,7,10,10,10,10,10,10,10,10,10,10,10,10,10,10,1 },
                { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6,7,7,8,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
                { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6,7,7,8,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
                { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,9,10,10,11,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
                { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
                { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
                { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
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
        var background = new Background(sceneManager.Content, "Background_menu");
        background.Sprite.Scale = new Vector2(4, 4);
        backgroundObjects.Add(background);

        hero = new Hero(sceneManager.Content, GetInputReader());
        hero.animationManager.SetPosition(new Vector2(0, 600));
        AddDynamicPhysicsObject(hero);

        Snail enemy1 = new Snail(sceneManager.Content, 400);
        enemy1.animationManager.SetPosition(new Vector2(1400, 500));
        AddDynamicPhysicsObject(enemy1);

        Bird enemy2 = new Bird(sceneManager.Content, 500);
        enemy2.animationManager.SetPosition(new Vector2(650, 0));
        AddDynamicPhysicsObject(enemy2);

        Spike spike1 = new Spike(sceneManager.Content);
        spike1.Sprite.Scale = new Vector2(4, 4);
        spike1.Sprite.SpritePosition = new Vector2(425, 900);
        AddDynamicPhysicsObject(spike1);

        Spike spike2 = new Spike(sceneManager.Content);
        spike2.Sprite.Scale = new Vector2(4, 4);
        spike2.Sprite.SpritePosition = new Vector2(360, 900);
        AddDynamicPhysicsObject(spike2);

        Spike spike3 = new Spike(sceneManager.Content);
        spike3.Sprite.Scale = new Vector2(4, 4);
        spike3.Sprite.SpritePosition = new Vector2(1620, 960);
        AddDynamicPhysicsObject(spike3);

        Spike spike4 = new Spike(sceneManager.Content);
        spike4.Sprite.Scale = new Vector2(4, 4);
        spike4.Sprite.SpritePosition = new Vector2(1685, 960);
        AddDynamicPhysicsObject(spike4);

        Spike spike5 = new Spike(sceneManager.Content);
        spike5.Sprite.Scale = new Vector2(4, 4);
        spike5.Sprite.SpritePosition = new Vector2(600, 715);
        AddDynamicPhysicsObject(spike5);

        Spike spike6 = new Spike(sceneManager.Content);
        spike6.Sprite.Scale = new Vector2(4, 4);
        spike6.Sprite.SpritePosition = new Vector2(665, 715);
        AddDynamicPhysicsObject(spike6);

        // COINS
        Coin kiwi1 = new Coin(sceneManager.Content);
        kiwi1.animationManager.SetPosition(new Vector2(470, 750));
        AddDynamicPhysicsObject(kiwi1);

        Coin kiwi2 = new Coin(sceneManager.Content);
        kiwi2.animationManager.SetPosition(new Vector2(700, 500));
        AddDynamicPhysicsObject(kiwi2);

        Coin kiwi3 = new Coin(sceneManager.Content);
        kiwi3.animationManager.SetPosition(new Vector2(1100, 800));
        AddDynamicPhysicsObject(kiwi3);
    }

    public override void CheckEndConditions()
    {
        if (hero.IsRemoved())
        {
            sceneManager.ChangeLevel(new GameOver(sceneManager));
        }
        else if (hero.Score >= 3)
        {
            sceneManager.ChangeLevel(new Level2(sceneManager));
        }
    }
}
