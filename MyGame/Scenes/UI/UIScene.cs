using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Scenes.UI
{
    internal abstract class UIScene : Scene
    {
        private List<IGameObject> UIComponents = new List<IGameObject>();
        protected UIScene(SceneManager sceneManager) : base(sceneManager)
        {
        }

        public void AddUIComponent(IGameObject gameObject)
        {
            UIComponents.Add(gameObject);
        }

        public void RemoveUIComponent(IGameObject gameObject)
        {
            UIComponents.Remove(gameObject);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            foreach (var uiComponent in UIComponents)
                uiComponent.Draw(spriteBatch);

            spriteBatch.End();
        }

        public override void PostUpdate()
        {
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var uiComponent in UIComponents)
                uiComponent.Update(gameTime);
        }
    }
}
