using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyGame.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Input
{
    internal class KeyboardReader : IInputReader, IMovable
    {
        private KeyboardState state;

        public Vector2 ReadInput()
        {
            state = Keyboard.GetState();
            Vector2 direction = Vector2.Zero;


            if (state.IsKeyDown(Keys.Left))
            {
                direction.X -= 1;
            }
            if (state.IsKeyDown(Keys.Right))
            {
                direction.X += 1;
            }
            return direction;
        }


        //mirror sprite
        SpriteEffects effect = SpriteEffects.FlipHorizontally;
        public SpriteEffects ChooseEffect()
        {

            if (state.IsKeyDown(Keys.Left))
            {
                effect = SpriteEffects.None;
            }
            if (state.IsKeyDown(Keys.Right))
            {
                effect = SpriteEffects.FlipHorizontally;
            }

            return effect;
        }
    }
}
