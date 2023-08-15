using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MyGame.Enums;
using MyGame.interfaces;
using MyGame.Sprites;

namespace MyGame.Terrain
{
    internal class Block : Sprite, IPhysicsObject
    {
        private const int BLOCK_SIZE = 15;
        private const string TEXTURE_NAME = "Terrain";
        public Rectangle Frame { get; private set; }

        public Block(ContentManager contentManager, BlockType block = BlockType.STONE_TOP)
            : base(contentManager.Load<Texture2D>(TEXTURE_NAME))
        {
            FramePosition = Frame.Location.ToVector2();

            // Draw different block depending on block type
            switch (block)
            {
                case BlockType.STONE_TOP:
                    SetTopMiddleStone();
                    break;
                case BlockType.STONE_TOP_LEFTCORNER:
                    SetTopLeftStone();
                    break;
                case BlockType.STONE_TOP_RIGHTCORNER:
                    SetTopRightStone();
                    break;
                case BlockType.STONE_LEFT:
                    SetLeftStone();
                    break;
                case BlockType.STONE_RIGHT:
                    SetRightStone();
                    break;
                case BlockType.STONE_CENTER:
                    SetMiddleStone();
                    break;
                case BlockType.STONE_BOTTOM_LEFTCORNER:
                    SetBottomLeftStone();
                    break;
                case BlockType.STONE_BOTTOM_RIGHTCORNER:
                    SetBottomRightStone();
                    break;
                case BlockType.STONE_BOTTOM:
                    SetBottomMiddleStone();
                    break;

                case BlockType.EARTH_TOP:
                    SetTopEarth();
                    break;
                case BlockType.EARTH_CENTER:
                    SetMiddleEarth();
                    break;
                default:
                    SetTopMiddleStone();
                    break;
            }

        }


        // For Stone
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
            Frame = new Rectangle(BLOCK_SIZE * 2, 0, BLOCK_SIZE, BLOCK_SIZE);
        }

        public void SetLeftStone()
        {
            Frame = new Rectangle(0, BLOCK_SIZE, BLOCK_SIZE, BLOCK_SIZE);
        }
        public void SetMiddleStone()
        {
            Frame = new Rectangle(BLOCK_SIZE, BLOCK_SIZE, BLOCK_SIZE, BLOCK_SIZE);
        }
        public void SetRightStone()
        {
            Frame = new Rectangle(BLOCK_SIZE * 2, BLOCK_SIZE, BLOCK_SIZE, BLOCK_SIZE);
        }
        public void SetBottomLeftStone()
        {
            Frame = new Rectangle(0, BLOCK_SIZE * 2, BLOCK_SIZE, BLOCK_SIZE);
        }
        public void SetBottomMiddleStone()
        {
            Frame = new Rectangle(BLOCK_SIZE, BLOCK_SIZE * 2, BLOCK_SIZE, BLOCK_SIZE);
        }
        public void SetBottomRightStone()
        {
            Frame = new Rectangle(BLOCK_SIZE * 2, BLOCK_SIZE * 2, BLOCK_SIZE, BLOCK_SIZE);
        }

        // For earth
        public void SetTopEarth()
        {
            Frame = new Rectangle(BLOCK_SIZE * 7, 0, BLOCK_SIZE, BLOCK_SIZE);
        }

        public void SetMiddleEarth()
        {
            Frame = new Rectangle(BLOCK_SIZE * 7, BLOCK_SIZE * 2, BLOCK_SIZE, BLOCK_SIZE);
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
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, SpritePosition, Frame, Color.White, Rotation, Origin, Scale, effects, 0);

            // For debugging
            //if (Debug) spriteBatch.DrawRectangle(GetBoundingRectangle(), Color.OrangeRed);
        }

        public Sprite GetSprite()
        {
            return this;
        }
    }
}
