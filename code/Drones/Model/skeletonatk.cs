using Drones.Properties;
using player;
using player.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drones.Model
{
    internal class skeletonatk
    {
        private int x;
        private int y;
        private int speed;
        private static int width = 45;
        private static int height = 45;
        public int X { get => x; private set => x = value; }
        public int Y { get => y; private set => y = value; }
        public int Speed { get => speed; private set => speed = value; }
        public static int Width { get => width; set => width = value; }
        public static int Height { get => height; set => height = value; }

        public skeletonatk(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.speed = 25;
        }


        public void Render(BufferedGraphics drawingSpace)
        {
            drawingSpace.Graphics.DrawImage(Resources.bone_atk, x, y, width, height);
        }
        public void update(int interval)
        {
            y += speed;

        }
    }
}
