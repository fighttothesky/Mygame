using Microsoft.Xna.Framework.Input;
using MyGame.Enums;
using MyGame.interfaces;
using System;

namespace MyGame.Input;

class KeyboardReader : IInputReader
{
    public static KeyboardReader Instance = new KeyboardReader();

    protected KeyboardReader()
    {
    }

    private KeyboardState state;

    public Direction ReadDirectionInput()
    {
        state = Keyboard.GetState();

        if (state.IsKeyDown(Keys.Up) && state.IsKeyDown(Keys.Left)) return Direction.LEFT_UP;

        if (state.IsKeyDown(Keys.Up) && state.IsKeyDown(Keys.Right)) return Direction.RIGHT_UP;

        if (state.IsKeyDown(Keys.Left)) return Direction.LEFT;

        if (state.IsKeyDown(Keys.Right)) return Direction.RIGHT;

        if (state.IsKeyDown(Keys.Up)) return Direction.UP;

        return Direction.NONE;
    }
}