using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;

namespace Infinite_World
{
    class DebugRander
    {
        public static bool Enable = false;

        static List<Drawable> objects = new List<Drawable>();

        public static void AddRectangle(float x, float y, float w, float h, Color color)
        {
            if (!Enable)
                return;
            var obj = new RectangleShape(new Vector2f(w, h));
            obj.Position = new Vector2f(x, y);
            obj.FillColor = Color.Transparent;
            obj.OutlineColor = color;
            obj.OutlineThickness = 1;
            objects.Add(obj);
        }

        // Supraincarcarea metodei AddRectangle cu FloatRect inegru
        public static void AddRectangle(FloatRect rect, Color color)
        {
            AddRectangle(rect.Left, rect.Top, rect.Width, rect.Height, color);
        }

        public static void Draw(RenderTarget target)
        {
            if (!Enable)
                return;

            foreach(var obj in objects)
            {
                target.Draw(obj);
            }

            objects.Clear();
        }
    }
}
