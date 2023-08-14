using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
namespace MyGame.Terrain
{
    public class MakeTinyBlockSprite : Sprites.Sprite
    {
        // block is 15px x 15px

        const int BLOCKHEIGHT = 15;
        const int BLOCKWIDTH = 15;

        private Rectangle currentBlock;

        public List<Rectangle> blocks;

        public MakeTinyBlockSprite(Texture2D texture) : base(texture)
        {
            blocks = new List<Rectangle>();

            GetBlocksTexture();
        }

        // Convert image to List of blocks
        public void GetBlocksTexture()
        {
            // add block to list of 15x15 rectangles from the texture
            for (int i = 0; i < Texture.Width; i += BLOCKWIDTH)
            {
                for (int j = 0; j < Texture.Height; j += BLOCKHEIGHT)
                {
                    blocks.Add(new Rectangle(i, j, BLOCKWIDTH, BLOCKHEIGHT));
                }
            }   
        }
    }
}
