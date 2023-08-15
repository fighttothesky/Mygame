using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame.Scenes
{
    public abstract class Scene
    {
        protected SceneManager sceneManager;

        public Scene(SceneManager sceneManager)
        {
            this.sceneManager = sceneManager;
        }

        public abstract void Update(GameTime gameTime);

        public abstract void PostUpdate();

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}