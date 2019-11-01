using System.Collections.Generic;
using System.Linq;
using SFML.Graphics;

namespace Infinite_World.Views
{
    class WindowsStack
    {
        private static readonly Stack<RenderWindow> Stack;

        static WindowsStack()
        {
            Stack = new Stack<RenderWindow>();
        }

        public static void AddNewWindow(RenderWindow window)
        {
            if (Stack.Count > 0)
            {
                Stack.Last().SetVisible(false);
            }

            Stack.Push(window);
        }

        public static void CloseLastWindow()
        {
            if (Stack.Count > 0)
            {
                Stack.Pop().Close();
            }
            if (Stack.Count > 0)
            {
                Stack.Last().SetVisible(true);
            }
        }
    }
}