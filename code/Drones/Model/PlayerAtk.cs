using Drones.Properties;

namespace player.Model
{
    internal class PlayerAtk
    {
        private int x;
        private int y;
        private int speed;
        private static int width = 50;
        private static int height = 80; 
        public int X { get => x; private set => x = value; }
        public int Y { get => y; private set => y = value; }
        public int Speed { get => speed; private set => speed = value; }
        public static int Width { get => width; set => width = value; }
        public static int Height { get => height; set => height = value; }

        public PlayerAtk(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.speed = 35;
        }


        public void Render (BufferedGraphics drawingSpace)   
        {
            drawingSpace.Graphics.DrawImage(Resources.attaque, x, y, width, height);
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
