using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;


namespace Infinite_World
{
    class Content
    {
        public const string CONTENT_DIR = ".\\Content\\";
        public static Texture texTile0; //Textura de tip pamint

        //Load Resourse
        public static void Load()
        {
            texTile0 = new Texture(CONTENT_DIR + "Textures\\Tiles_0.png");
        }
    }
}