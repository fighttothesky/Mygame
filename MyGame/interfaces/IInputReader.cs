using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.interfaces
{
    internal interface IInputReader
    {
        Vector2 ReadInput();


        //mirror sprite
        SpriteEffects ChooseEffect();
    }
}
