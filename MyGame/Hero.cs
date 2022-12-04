using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.Animation;
using MyGame.Content.interfaces;
using SharpDX.Direct2D1;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Hero:IGameObject
    {
        Texture2D heroTexture;
        Animation.Animation animation;
        private Vector2 scale = new Vector2(4,4);
        private Vector2 origin = Vector2.Zero;
        private float rotation = 0;
        private float layerDepth = 0;
        //private Rectangle _partRectangle;
        //private int moveAside_X = 0;
        public Hero(Texture2D texture)
        {
            heroTexture = texture;
            animation = new Animation.Animation();
            animation.GetFramesFromTextureProperties(texture.Width, texture.Height, 13, 1);
        }
        public void Update()
        {
            animation.Update();
        }

        public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(heroTexture, new Vector2(20, 20), animation.CurrentFrame.SourceRactangle, Color.White, rotation, origin, scale, SpriteEffects.None, layerDepth);
        }
        
    }
}
