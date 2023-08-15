using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame.UI
{
    public abstract class State
    {
        protected ContentManager content;

        protected GraphicsDevice graphicsDevice;

        protected Game game;

        public State(Game game, GraphicsDevice graphicsDevice, ContentManager content)
        {
            this.game = game;

            this.graphicsDevice = graphicsDevice;

            this.content = content;
        }

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public abstract void PostUpdate();

        public abstract void Update(GameTime gameTime);
    }
}