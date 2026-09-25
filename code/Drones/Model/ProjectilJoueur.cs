using Drones.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Drones.Model
{
    internal class ProjectilJoueur
    {
        private int x;
        private int y;
        private int speed;

        public int X { get => x; private set => x = value; }
        public int Y { get => y; private set => y = value; }
        public int Speed { get => speed; private set => speed = value; }

        public ProjectilJoueur(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.speed = 20;
        }


        public void render (BufferedGraphics drawingSpace)   
        {
            drawingSpace.Graphics.DrawImage(Resources.attaque, x, y, 30, 60);
        }
        public void update (int interval)
        {
            y -= speed;
            if (y < -80)
            {
                throw new Exception();
            }
        }
    }
}
