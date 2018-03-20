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
        GROUND,
        GRASS
    }

    class Tile : Transformable, Drawable
    {
        //Size of Tail (Wight, Height)
        public const int TILE_SIZE_X = 16;
        public const int TILE_SIZE_Y = 16;

        TileType type = TileType.NONE;
        RectangleShape rectShape; // O forma dreptunghiulara 'p/t element'

        // Neighbors
        Tile upTile = null;
        Tile downTile = null;
        Tile leftTile = null;
        Tile rightTile = null;

        // Constructor 
        public Tile(TileType type, Tile upTile, Tile downTile, Tile leftTile, Tile rightTile)
        {
            this.type = type;

            // Add/Update Neighbors 
            if(upTile != null)
            {
                this.upTile = upTile;
                this.upTile.downTile = this;
            }
            if (downTile != null)
            {
                this.downTile = downTile;
                this.downTile.downTile = this;
            }
            if (leftTile != null)
            {
                this.leftTile = leftTile;
                this.leftTile.downTile = this;
            }
            if (rightTile != null)
            {
                this.rightTile = rightTile;
                this.rightTile.downTile = this;
            }

            rectShape = new RectangleShape(new Vector2f(TILE_SIZE_X, TILE_SIZE_Y));

            switch (type)
            {
                case TileType.GROUND:
                    rectShape.Texture = Content.texTile0;
                    break;
                case TileType.GRASS:
                    rectShape.Texture = Content.texTile2;
                    break;
            }
            rectShape.TextureRect = GetTextureRect(1, 1);////////////////

            // Updates aspect of the tile in dependence on neighbors
            UpdateView();
        }

        // Updates aspect of the tile in dependence on neighbors
        public void UpdateView()
        {

        }

        // Get fragment of TEXTURE
        public IntRect GetTextureRect(int i, int j)
        {
            int x = i * TILE_SIZE_X + i * 2; // *2 pentru spatiu intre textura
            int y = j * TILE_SIZE_Y + j * 2;

            return new IntRect(x, y, TILE_SIZE_X, TILE_SIZE_Y);
        }

        // Draw Tail
        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;
            target.Draw(rectShape, states);
        }
    }
}
