using Microsoft.Xna.Framework.Input;
using MyGame.interfaces;
using MyGame.Enum;

namespace MyGame.Input
{
    internal class KeyboardReader : IInputReader
    {
        private KeyboardState state;

        public Direction ReadDirectionInput()
        {
            state = Keyboard.GetState();
            
            if (state.IsKeyDown(Keys.Left))
            {
                return Direction.LEFT;
            }
            
            if (state.IsKeyDown(Keys.Right))
            {
                return Direction.RIGHT;
            }

            return Direction.NONE;
        }
    }
}
