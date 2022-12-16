using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.Animation;
using MyGame.Content.interfaces;
using MyGame.Input;
using MyGame.interfaces;
using SharpDX.Direct2D1;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace MyGame
{
    internal class Hero:IGameObject
    {
        Texture2D heroTexture;
        Animation.Animation animation;
        private Vector2 scale = new Vector2(4,4);
        private Vector2 origin = Vector2.Zero;
        private float rotation = 0;
        private float layerDepth = 0;

        //for movment
        private Vector2 position = new Vector2(1,1);
        private Vector2 speed = new Vector2(2, 2);
        //for keyboard
        private IInputReader inputReader;
        private Vector2 direction;


        //for mirror sprite
        private IInputReader mir;
        private SpriteEffects effects;

        public Hero(Texture2D texture, IInputReader inputReader)
        {
            heroTexture = texture;
            this.inputReader = inputReader;

            //mirror sprite
            mir = inputReader;


            animation = new Animation.Animation();
            animation.GetFramesFromTextureProperties(texture.Width, texture.Height, 12, 1);
        }
        public void Update(GameTime gameTime)
        {
            Move();
            animation.Update(gameTime);
        }

        public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(heroTexture, position, animation.CurrentFrame.SourceRactangle, Color.White, rotation, origin, scale, effects, layerDepth);
        }

        //for movment keyboard
        private void Move()
        {
            direction = inputReader.ReadInput();
            direction *= speed;
            position += direction;

            //mirror sprite
            effects = mir.ChooseEffect();
        }
        
    }
}
