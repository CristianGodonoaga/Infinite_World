using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infinite_World;
using SFML;
using SFML.Graphics;
using SFML.Window;

namespace Infinite_World
{
    class Program
    {
        static RenderWindow win;

        public static RenderWindow Window { get { return win; } }
        public static Game Game { private set; get; }

        static void Main(string[] args)
        {
            win = new RenderWindow(new SFML.Window.VideoMode(800, 600), "Infinite World");
            //win = new RenderWindow(new VideoMode(800, 600), "Infinite World", Styles.Titlebar, new ContextSettings());
            win.SetVerticalSyncEnabled(true);

            #region SetControls
            win.Closed += Win_Closed; //Metoda la inchiderea win
            win.Resized += Win_Resized; //Metoda la redemensionarea win
            //win.GainedFocus += Win_GainedFocus;
            //win.LostFocus += Win_LostFocus;
            //win.KeyPressed += Win_KeyPressed;
            win.KeyReleased += Win_KeyReleased;
            //win.MouseButtonPressed += Win_MouseButtonPressed;
            //win.MouseButtonReleased += Win_MouseButtonReleassed;
            //win.MouseMoved += Win_MouseMoved;
            //win.MouseEntered += Win_MouseEntered;
            //win.MouseLeft += Win_MouseLeft;
            //win.SetActive(true);
            //win.SetFramerateLimit(60);
            #endregion
            Content.Load();//Load content "resurse"

            Game = new Game();
            while (win.IsOpen)
            {
                win.DispatchEvents(); //Check input window msg
                Game.Update();

                win.Clear(Color.Black);

                //Desenarea
                Game.Drow();

                win.Display();
            }
        }

        private static void Win_Resized(object sender, SizeEventArgs e)
        {
            win.SetView(new View(new FloatRect(0, 0, e.Width, e.Height)));
        }

        private static void Win_Closed(object sender, EventArgs e)
        {
            win.Close();
        }

        private static void Win_KeyReleased(object sender, KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Escape)
                win.Close();
        }
    }
}