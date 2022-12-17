using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MyGame.Enum;

namespace MyGame.interfaces
{
    internal interface IInputReader
    {
        Direction ReadDirectionInput();
        
    }
}
