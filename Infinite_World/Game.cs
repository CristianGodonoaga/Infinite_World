using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Infinite_World
{
    class Game
    {
        World world; //Lumea
        Player player; //Jucator

        public Game()
        {
            //Crearea lumei noi
            world = new World();
            world.GenerateWorld();

            //Crearea jucatorului
            player = new Player(world);
            player.StartPosition = new SFML.System.Vector2f(300, 150);
            player.Spawn();
        }
        //Refrash la logica
        public void Update()
        {
            player.Update();

        }
        //Desenarea jocului
        public void Drow()
        {
            Program.Window.Draw(world);
            Program.Window.Draw(player);
        }
    }
}
