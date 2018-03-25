using SFML.Graphics;
using SFML.System;
/// <summary>
/// Сlass World is responsible for all the logic and drawing of the world
/// </summary>
namespace Infinite_World
{
    class World : Transformable, Drawable
    {
        //Number of Chunk (Wight, Height)
        public const int WORLD_SIZE = 5;
        Chunk[][] chunks;

        //Constructor
        public World()
        {
            chunks = new Chunk[WORLD_SIZE][];
            for (int i = 0; i < WORLD_SIZE; i++)
            {
                chunks[i] = new Chunk[WORLD_SIZE];
            }
        }

        //Generate new World
        public void GenerateWorld()
        {
            for (int x = 0; x < 50; x++)
                for (int y = 15; y <= 17; y++)
                    SetTile(TileType.GRASS, x, y);
            for (int x = 0; x < 50; x++)
                for (int y = 18; y <= 32; y++)
                    SetTile(TileType.GROUND, x, y);
            for (int x = 0; x < 1; x++)
                for (int y = 0; y <= 32; y++)
                    SetTile(TileType.GROUND, x, y);
            for (int x = 49; x < 50; x++)
                for (int y = 0; y <= 32; y++)
                    SetTile(TileType.GROUND, x, y);
        }

        public void SetTile(TileType type, int x, int y)
        {
            var chunk = GetChunk(x, y);
            var tilePos = GetTailPosFromChunk(x, y);

            // Find Neighbors
            Tile upTile = GetTile(x, y - 1);
            Tile downTile = GetTile(x, y + 1);
            Tile leftTile = GetTile(x - 1, y);
            Tile rightTile = GetTile(x + 1, y);

            chunk.SetTile(type, tilePos.X, tilePos.Y, upTile, downTile, leftTile, rightTile);
        }

        public Tile GetTile(int x, int y)
        {
            var chunk = GetChunk(x, y);
            if (chunk == null) // Daca Chank-ul nu a fost setat
                return null;

            var tilePos = GetTailPosFromChunk(x, y);
            return chunk.GetTile(tilePos.X, tilePos.Y);
        }

        public Chunk GetChunk(int x, int y)
        {
            int X = x / Chunk.CHUNK_SIZE;
            int Y = y / Chunk.CHUNK_SIZE;
            // @todo: Daca Player-ul poate ajunge in chank-ul -1 e problema
            if (X >= WORLD_SIZE || Y >= WORLD_SIZE || X < 0 || Y < 0)
                return null;

            if(chunks[X][Y] == null)
                chunks[X][Y] = new Chunk(new Vector2i(X, Y));

            return chunks[X][Y];
        }

        //Get Tail Position in the Chunk
        public Vector2i GetTailPosFromChunk(int x, int y)
        {
            int X = x / Chunk.CHUNK_SIZE;
            int Y = y / Chunk.CHUNK_SIZE;

            return new Vector2i(x - X * Chunk.CHUNK_SIZE, y - Y * Chunk.CHUNK_SIZE);
        }

        //Draw the World
        public void Draw(RenderTarget target, RenderStates states) //Interface
        {
            //Draw Chunk
            for (int x = 0; x < WORLD_SIZE; x++)
            {
                for (int y = 0; y < WORLD_SIZE; y++)
                {
                    if (chunks[x][y] == null)
                        continue;
                    target.Draw(chunks[x][y]);
                }
            }
        }

/*
        Tile temp;//!!!!!!!!!!!!!!!!!!
        internal Tile GetTile(int pX, int pY)
        {

            return temp;
        }
*/
    }
}
