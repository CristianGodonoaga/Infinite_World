using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
using SFML.System;

namespace Infinite_World
{
    /// <summary>
    /// Matrix which contains some of piece
    /// </summary>
    class Chunk : Transformable, Drawable
    {
        // Number of Tail in one Chunk (Wight, Height)
        public const int CHUNK_SIZE = 25;

        Tile[][] tiles;
        Vector2i chunkPos; // Position of Chunk in the matrix of the World

        public Chunk(Vector2i chunkPos)
        {
            // Set position of this Chunk
            this.chunkPos = chunkPos;
            Position = new Vector2f(chunkPos.X * CHUNK_SIZE * Tile.TILE_SIZE_X, chunkPos.Y * CHUNK_SIZE * Tile.TILE_SIZE_Y);

            // Create matrix of tails for this Chunk
            tiles = new Tile[CHUNK_SIZE][];
            for (int i = 0; i < CHUNK_SIZE; i++ )
            {
                tiles[i] = new Tile[CHUNK_SIZE];
            }            
        }

        // Set Tile in the Chunk
        public void SetTile(TileType type, int x, int y, Tile upTile, Tile downTile, Tile leftTile, Tile rightTile)
        {

            tiles[x][y] = new Tile(type, upTile, downTile, leftTile, rightTile);
            tiles[x][y].Position = new Vector2f(x * Tile.TILE_SIZE_X, y * Tile.TILE_SIZE_Y);
        }

        // Get Tile of the Chunk
        public Tile GetTile(int x, int y)
        {
            // Daca pozitia este inafara Chunk-ului
            if (x < 0 || y < 0 || x >= CHUNK_SIZE || y >= CHUNK_SIZE)
                return null;
            // Returnam Tile chiar daca e null
            return tiles[x][y];
        }

        // Draw all tailes inside of Chunk
        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform; // Desenarea relativ obiectului parinte

            // Drow tail
            for (int x = 0; x < CHUNK_SIZE; x++)
            {
                for (int y = 0; y < CHUNK_SIZE; y++)
                {
                    if (tiles[x][y] == null) // Daca nu a
                        continue;
                    target.Draw(tiles[x][y], states);
                }
            }
        }
    }
}
