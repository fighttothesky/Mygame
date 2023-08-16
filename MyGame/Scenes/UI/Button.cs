using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyGame.interfaces;
using System;

namespace MyGame.Scenes.UI
{
    public class Button : IGameObject
    {
        private MouseState currentMouse;
        private bool isHovering;
        private MouseState previousMouse;
        private Texture2D texture;
        private readonly SpriteFont font;

        public event EventHandler Click;
        public bool Clicked { get; private set; }

        private Vector2 position;
        public Vector2 Position { get => position; set => SetPosition(value); }

        public Text Text { get; private set; }

        public Button(Texture2D texture, SpriteFont font, string text)
        {
            this.texture = texture;
            this.font = font;
            SetText(text);
        }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            }
        }

        public void SetText(string text)
        {
            var x = Rectangle.X + Rectangle.Width / 2 - font.MeasureString(text).X / 2;
            var y = Rectangle.Y + Rectangle.Height / 2 - font.MeasureString(text).Y / 2;

            Text = new Text(text, font, new Vector2(x, y), Color.White);
            Text.IsCentered = false;
        }

        private void SetPosition(Vector2 position)
        {
            this.position = position;
            SetText(Text.Content);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var colour = Color.White;

            if (isHovering)
                colour = Color.Gray;

            spriteBatch.Draw(texture, Rectangle, colour);
            Text.Draw(spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            previousMouse = currentMouse;
            currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(currentMouse.X, currentMouse.Y, 1, 1);

            isHovering = false;

            if (mouseRectangle.Intersects(Rectangle))
            {
                isHovering = true;

                if (currentMouse.LeftButton == ButtonState.Released && previousMouse.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
        }
    }
}
