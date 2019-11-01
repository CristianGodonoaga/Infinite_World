using Infinite_World.Views;
using SFML.Graphics;

namespace Infinite_World
{
    class Game : BaseWindow
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
            player.StartPosition = new SFML.System.Vector2f(150, 10);
            player.Spawn();

            DebugRander.Enable = true;  
        }
        //Refrash la logica (fizica)
        public void Update()
        {
            player.Update();

        }
        //Desenarea jocului
        public void Drow()
        {
            Window.Draw(world);// Draw World
            Window.Draw(player);// Draw Player
            DebugRander.Draw(Window);
        }
        public void Start()
        {
            while (Window.IsOpen)
            {
                Window.DispatchEvents(); //Check input window msg
                this.Update();

                Window.Clear(Color.Black);

                //Desenarea
                this.Drow();

                Window.Display();
            }
        }
    }
}
