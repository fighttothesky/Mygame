using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.interfaces;
using MyGame.Scenes.UI;

namespace MyGame.Scenes
{
    public abstract class Scene : IGameObject
    {
        protected SceneManager sceneManager;

        public Scene(SceneManager sceneManager)
        {
            this.sceneManager = sceneManager;
        }

        public abstract void Update(GameTime gameTime);

        public abstract void PostUpdate();

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}