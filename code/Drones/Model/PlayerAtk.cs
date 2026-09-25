using Drones.Properties;

namespace player.Model
{
    internal class PlayerAtk
    {
        private int x;
        private int y;
        private int speed;

        public int X { get => x; private set => x = value; }
        public int Y { get => y; private set => y = value; }
        public int Speed { get => speed; private set => speed = value; }

        public PlayerAtk(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.speed = 30;
        }


        public void render (BufferedGraphics drawingSpace)   
        {
            drawingSpace.Graphics.DrawImage(Resources.attaque, x, y, 40, 70);
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
