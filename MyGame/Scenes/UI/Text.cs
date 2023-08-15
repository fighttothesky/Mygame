using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;
using MyGame.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Scenes.UI
{
    internal class Text : IGameObject
    {
        SpriteFont font;
        private readonly Vector2 position;
        private readonly Color colorText;

        public Text(string text, SpriteFont font, Vector2 position, Color colorText)
        {
            Content = text;
            this.font = font;
            this.position = position;
            this.colorText = colorText;
        }

        public string Content { get; set; }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, Content, position, colorText);
        }

        public void Update(GameTime gameTime)
        {
        }
    }
}
