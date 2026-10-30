
using player;
using player.Helpers;
using shoot_them_up.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace player.Model
{
    public class skeletonatk
    {

        //variables
        private int x;
        private int y;
        private int speed;
        private static int width = 45;
        private static int height = 45;

        //getter setter pour les variables qui doivent être accessibles depuis l'extérieur de la classe
        public int X { get => x; private set => x = value; }
        public int Y { get => y; private set => y = value; }
        public int Speed { get => speed; private set => speed = value; }
        public static int Width { get => width; private set => width = value; }
        public static int Height { get => height; private set => height = value; }


        //constructeur
        public skeletonatk(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.speed = 25;
        }


        //méthode pour dessiner l'attaque du squelette
        public void Render(BufferedGraphics drawingSpace)
        {
            drawingSpace.Graphics.DrawImage(Resources.bone_atk, x, y, width, height);
        }

        //méthode pour mettre à jour la position de l'attaque du squelette
        public void update(int interval)
        {
            y += speed;

        }
    }
}
