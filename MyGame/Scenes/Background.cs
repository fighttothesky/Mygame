using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MyGame.Characters;
using MyGame.Collisions;
using MyGame.interfaces;
using MyGame.Sprites;
using System.Collections.Generic;

namespace MyGame.Scenes
{
    internal class Background : IGameObject
    {
        public Sprite Sprite { get; }

        public Background(ContentManager contentManager, string backgroundTexture)
        {
            Texture2D texture = contentManager.Load<Texture2D>(backgroundTexture);
            Sprite = new Sprite(texture);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch);
        }
        public void Update(GameTime gameTime)
        {
        }


    }
}
