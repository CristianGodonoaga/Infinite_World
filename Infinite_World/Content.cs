using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;

/// <summary>
/// Contain all src
/// </summary>
namespace Infinite_World
{
    class Content
    {
        private const string CONTENT_DIR = ".\\Content\\";
        public static Texture texTile0; //Textura de tip pamint "GROUND"
        public static Texture texTile2; //Textura de tip pamint "GRASS"

        //Load Resourse
        public static void Load()
        {
            texTile0 = new Texture(CONTENT_DIR + "Textures\\Tiles_0.png");
            texTile2 = new Texture(CONTENT_DIR + "Textures\\Tiles_2.png");
        }
    }
}