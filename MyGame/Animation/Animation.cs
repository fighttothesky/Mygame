﻿using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MyGame.Animation
{
    public class Animation
    {
        public AnimationFrame CurrentFrame { get; set; }

        private List<AnimationFrame> frames;

        private int counter;
        private double secondCounter=0;

        public Animation()
        {
            frames = new List<AnimationFrame>();
        }

        public void AddFrame(AnimationFrame animtionFr)
        {
            frames.Add(animtionFr);
            CurrentFrame = frames[0];

        }

        public void Update(GameTime gameTime)
        {
            CurrentFrame = frames[counter];

            secondCounter += gameTime.ElapsedGameTime.TotalSeconds;
            int fps = 24;

            if(secondCounter>= 1d / fps)
            {
                counter++;
                secondCounter = 0;
            }

            if (counter >= frames.Count)
            {
                counter = 0;
            }
        }

        public void GetFramesFromTextureProperties(int width, int height, int numberOfWidthSprites, int numberOfHeightSprites)
        {
            int widthOfFrame = width / numberOfWidthSprites;
            int heightOfFrame = height / numberOfHeightSprites;

            for (int y = 0; y <= height - heightOfFrame; y+= heightOfFrame)
            {
                for (int x = 0; x <= width - widthOfFrame; x+= widthOfFrame)
                {
                    frames.Add(new AnimationFrame(new Rectangle(x,y,widthOfFrame,heightOfFrame)));
                }
            }
        }
    }
}
