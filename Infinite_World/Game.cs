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
            Program.Window.Draw(world);// Draw World
            Program.Window.Draw(player);// Draw Player
            DebugRander.Draw(Program.Window);
        }
    }
}
