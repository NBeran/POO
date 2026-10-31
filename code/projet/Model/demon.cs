using player.Helpers;
using shoot_them_up.Model;
using shoot_them_up.Properties;

namespace player.Model
{
    public class demon : IMonster
    {
        //variables

        private int x;
        private int y;
        private int speed;
        private int hp;
        private static int height = 85;
        private static int score = 300;
        private bool left = false;
        private bool right = false;
        private bool down = true;
        private int walktime = 0;
        private bool lastleft = false;
        private bool alive;


        //getter setter pour les variables qui doivent être accessibles depuis l'extérieur de la classe
        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public int Speed { get => speed; set => speed = value; }
        public int Hp { get => hp; set => hp = value; }
        public static int Height { get => height; set => height = value; }
        public static int Score { get => score; set => score = value; }
        public bool Left { get => left; set => left = value; }
        public bool Right { get => right; set => right = value; }
        public bool Down { get => down; set => down = value; }
        public int Walktime { get => walktime; set => walktime = value; }
        public bool Lastleft { get => lastleft; set => lastleft = value; }
        public bool isalive { get => alive; set => alive = value; }
   




        //constructeur
        public demon(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.speed = 2;
            this.hp = 2;
            isalive = true;
        }

        //méthode pour dessiner le démon sur l'espace de dessin
        public void Render(BufferedGraphics drawingSpace)
        {
            drawingSpace.Graphics.DrawImage(Resources.demon, x, y, height, height);
        }

        //méthode pour mettre à jour la position du démon
        public void update(int interval)
        {

            //si le demon atteint la hauteur des defenses, il commences a les attaquer
            if (y + height >= Config.limitheight)
            {
                if (GameSpace.Defense.Count >= 1)
                {
                    GameSpace.Defense[0].hit(false);
                }

            }
            //se deplace a gauche puis en bas , puis a droite puis en bas, et ainsi de suite
            else
            {

                //marche a gauche puis change de dirrection vers le bas
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

                //marche a droite puis change de dirrection vers le bas
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
                //marche vers le bas puis change de dirrection vers la gauche ou la droite selon la derniere direction
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
                if ((x < 0 || x > Config.AIRSPACEWIDTH - skeleton.Height || y < 0 || y > Config.AIRSPACEHEIGHT - skeleton.Height) && isalive)
                {
                    isalive = false;
                }
            }
        }

    }
}
