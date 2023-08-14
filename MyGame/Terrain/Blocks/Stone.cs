using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace MyGame.Terrain.Blocks
{
    internal class Stone : Block
    {
        private const string TEXTURE_NAME = "Terrain";
        private readonly Rectangle TEXTURE_FRAME = new Rectangle(0, 0, 45, 45);
            
        public Stone(ContentManager contentManager) 
            : base(contentManager, TEXTURE_NAME)
        {
        }

        protected override Rectangle GetTextureFrame()
        {
            return TEXTURE_FRAME;   
        }
    }
}
