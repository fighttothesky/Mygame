using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.interfaces;

namespace MyGame.Scenes.UI
{
    public class Text : IGameObject
    {
        private SpriteFont font;
        private readonly Vector2 position;
        private readonly Color colorText;

        public bool IsCentered { get; set; }
        public string Content { get; set; }

        public Text(string text, SpriteFont font, Vector2 position, Color colorText)
        {
            Content = text;
            this.font = font;
            this.position = position;
            this.colorText = colorText;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var drawPosition = position;

            if (IsCentered)
            {
                var x = position.X - (font.MeasureString(Content).X / 2);
                var y = position.Y - (font.MeasureString(Content).Y / 2);
                drawPosition = new Vector2(x, y);
            }

            spriteBatch.DrawString(font, Content, drawPosition, colorText);
        }

        public void Update(GameTime gameTime)
        {
        }
    }
}
