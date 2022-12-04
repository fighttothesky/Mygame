using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Animation
{
    public class AnimationFrame
    {
        public Rectangle SourceRactangle { get; set; }

        public AnimationFrame(Rectangle rectangle)
        {
            SourceRactangle = rectangle;
        }
    }
}
