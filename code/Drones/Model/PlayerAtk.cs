

using shoot_them_up.Properties;

namespace player.Model
{
    public class PlayerAtk
    {
        //variables 
        private int x;
        private int y;
        private int speed;
        private static int width = 50;
        private static int height = 80;

        //getter setter pour les variables qui doivent être accessibles depuis l'extérieur de la classe
        public int X { get => x; private set => x = value; }
        public int Y { get => y; private set => y = value; }
        public int Speed { get => speed; private set => speed = value; }
        public static int Width { get => width; set => width = value; }
        public static int Height { get => height; set => height = value; }

        //constructeur
        public PlayerAtk(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.speed = 35;
        }


        //méthode pour dessiner l'attaque du joueur
        public void Render(BufferedGraphics drawingSpace)
        {
            drawingSpace.Graphics.DrawImage(Resources.attaque, x, y, width, height);
        }
        //méthode pour mettre à jour la position de l'attaque du joueur
        public void update(int interval)
        {
            y -= speed;

        }
    }
}
