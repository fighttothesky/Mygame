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
        private double secondCounter = 0;

        //private Vector2 positie;
        public Hero(Texture2D texture)
        {
            heroTexture = texture;
            animation = new Animation.Animation();
            animation.GetFramesFromTextureProperties(texture.Width, texture.Height, 13, 1);
        }
        public void Update(GameTime gameTime)
        {
            animation.Update(gameTime);
        }

        public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(heroTexture, new Vector2(0, 0), animation.CurrentFrame.SourceRactangle, Color.White, rotation, origin, scale, SpriteEffects.None, layerDepth);
        
            //positie = new Vector2(0,0);

            //spriteBatch.Draw(heroTexture, positie, animation.CurrentFrame.SourceRactangle, Color.White, rotation, origin, scale, SpriteEffects.None, layerDepth);
        }

        //private void Move()
        //{
        //}
        
    }
}
