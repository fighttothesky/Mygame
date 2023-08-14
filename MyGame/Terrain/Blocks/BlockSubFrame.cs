using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MyGame.Collisions;
using MyGame.Enums;
using MyGame.interfaces;
using MyGame.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Terrain.Blocks
{
    internal class BlockSubFrame : Block
    {
        private const int BLOCK_SIZE = 15;
        private const string TEXTURE_NAME = "Terrain";
        public Rectangle Frame { get; private set; }

        public BlockSubFrame(ContentManager contentManager, BlockType block = BlockType.GREY_TOP ) : base(contentManager, TEXTURE_NAME)
        {
            // Draw different block depending on block type
            switch (block)
            {
                case BlockType.GREY_TOP:
                    SetTopMiddleStone();
                    break;
                case BlockType.GREY_LEFT_CORNER:
                    SetTopLeftStone();
                    break;
                case BlockType.GREY_RIGHT_CORNER:
                    SetTopRightStone();
                    break;
                default:
                    SetTopMiddleStone();
                    break;
            }   

        }

        public void SetTopLeftStone()
        {
            Frame = new Rectangle(0, 0, BLOCK_SIZE, BLOCK_SIZE);
        }

        public void SetTopMiddleStone()
        {
            Frame = new Rectangle(BLOCK_SIZE, 0, BLOCK_SIZE, BLOCK_SIZE);
        }
        public void SetTopRightStone()
        {
            Frame = new Rectangle(BLOCK_SIZE*2, 0, BLOCK_SIZE, BLOCK_SIZE);
        }

        protected override Rectangle GetTextureFrame()
        {
            return Frame;
        }

        protected override Rectangle GetFrame()
        {
            return new Rectangle((int)FramePosition.X, (int)FramePosition.Y, BLOCK_SIZE, BLOCK_SIZE);
        }


        // Get the bounding rectangle of the texture (scaling applied)
        public override Rectangle GetBoundingRectangle()
        {
            return new Rectangle(SpritePosition.ToPoint(), GetFrame().Size * Scale.ToPoint());
        }
    }
}
