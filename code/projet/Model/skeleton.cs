using player.Helpers;
using shoot_them_up.Model;
using shoot_them_up.Properties;

namespace player.Model
{
    public class skeleton : IMonster
    {
        //variables
        private int x;
        private int y;
        private int speed;
        private int hp;
        private static int height = 85;
        private static int score = 150;
        private bool left = false;
        private bool right = false;
        private bool down = true;
        private int walktime = 0;
        private bool lastleft = false;
        private bool alive;
        public bool isalive { get => alive;  set => alive = value; }

        //getter setter pour les variables qui doivent être accessibles depuis l'extérieur de la classe
        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public int Speed { get => speed; set => speed = value; }
        public int Hp { get => hp; set => hp = value; }
        public static int Height { get => height; set => height = value; }
        public static int Score { get => score; set => score = value; }
        public bool Down { get => down; set => down = value; }
        public bool Lastleft { get => lastleft; set => lastleft = value; }
        public bool Left { get => left; set => left = value; }
        public bool Right { get => right; set => right = value; }
        public int Walktime { get => walktime; set => walktime = value; }
        

        //constructeur
        public skeleton(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.speed = 2;
            this.hp = 1;
            isalive = true;
        }

        //méthode pour dessiner le squelette
        public void Render(BufferedGraphics drawingSpace)
        {
            drawingSpace.Graphics.DrawImage(Resources.skeleton, x, y, height, height);
        }

        //méthode pour mettre à jour la position du squelette
        public void update(int interval)
        {
            //si le squelette atteint la hauteur des defenses, il commence a mettre des dégats
            if (y + height >= Config.limitheight)
            {
                if (GameSpace.Defense.Count >= 1)
                {
                    GameSpace.Defense[0].hit(false);
                }

            }
            //se deplace a gauche, en bas, a droite, puis en bas, et recommence
            else
            {
                //marche a gauche puis en bas
                if (left)
                {
                    walktime++;
                    if (x > 0)
                    {
                        x -= speed;
                    }
                    if (walktime >= 25)
                    {
                        left = false;
                        down = true;
                        walktime = 0;
                    }


                }
                //marche a droite puis en bas
                else if (right)
                {
                    walktime++;
                    if (x < Config.AIRSPACEWIDTH - height)
                    {
                        x += speed;

                    }

                    if (walktime >= 25)
                    {
                        right = false;
                        down = true;
                        walktime = 0;
                    }

                }
                //marche en bas puis a gauche ou a droite selon la derniere direction
                else if (down)
                {
                    y += speed;
                    walktime++;
                    if (walktime >= 25)
                    {
                        walktime = 0;

                        if (lastleft)
                        {
                            right = true;
                            down = false;
                            lastleft = false;

                        }
                        else
                        {
                            left = true;
                            down = false;
                            lastleft = true;
                        }
                    }
                }
                if (x < 0 || x > Config.AIRSPACEWIDTH - skeleton.Height || y < 0 || y > Config.AIRSPACEHEIGHT - skeleton.Height)
                {
                    isalive = false;
                }
            }
        }
    }
}
