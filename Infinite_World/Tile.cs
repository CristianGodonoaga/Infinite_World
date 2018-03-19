using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
using SFML.System;

namespace Infinite_World
{
    enum TileType 
    {
        NONE,
        GROUND
    }

    class Tile : Transformable, Drawable
    {
        //Size of Tail (Wight, Height)
        public const int TILE_SIZE = 16;

        TileType type = TileType.GROUND;
        RectangleShape rectShape;

        public Tile()
        {
            rectShape = new RectangleShape(new Vector2f(TILE_SIZE, TILE_SIZE));

            switch (type)
            {
                case TileType.GROUND:
                    rectShape.Texture = Content.texTile0;
                    rectShape.TextureRect = new IntRect(0, 0, TILE_SIZE, TILE_SIZE);
                    break;
            }

        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;

            target.Draw(rectShape, states);
        }
    }
}
