using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MyGame.Scenes.UI;

namespace MyGame.Scenes
{
    public class SceneManager
    {
        public Scene CurrentLevel { get; private set; }

        public GraphicsDevice GraphicsDevice { get; }
        public ContentManager Content { get; }

        private Game game;

        public SceneManager(GraphicsDevice graphicsDevice, ContentManager Content, Game game)
        {
            this.Content = Content;
            this.GraphicsDevice = graphicsDevice;
            this.game = game;
        }

        public void Start()
        {
            CurrentLevel = new MainMenu(this);
        }

        public void ChangeLevel(Scene newLevel)
        {
            CurrentLevel = newLevel;
        }

        public void Exit()
        {
            game.Exit();
        }
    }
}
