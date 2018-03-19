using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
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

        public World()
        {
            chunks = new Chunk[WORLD_SIZE][];
            for (int i = 0; i < WORLD_SIZE; i++)
            {
                chunks[i] = new Chunk[WORLD_SIZE];
            }

            //@test
            chunks[0][0] = new Chunk();
        }

        public void Draw(RenderTarget target, RenderStates states) //Interface
        {
            //Drow Chunk
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


        Tile temp;//!!!!!!!!!!!!!!!!!!
        internal Tile GetTile(int pX, int pY)
        {

            return temp;
        }
    }
}
