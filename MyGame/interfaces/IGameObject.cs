using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Content.interfaces
{
    internal interface IGameObject
    {
        void Update();
        void Draw(SpriteBatch spriteBatch);
    }
}
