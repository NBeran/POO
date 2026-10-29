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
    internal class skeleton
    {
        private int x;
        private int y;
        private int speed;
        private int hp;
        private static int height = 85;
        private static int score = 150;
        private bool gauche = false;
        private bool droite = false;
        private bool bas = true;
        private int walktime = 0;
        private bool gaucheavant = false;
        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public int Speed { get => speed; set => speed = value; }
        public int Hp { get => hp; set => hp = value; }
        public static int Height { get => height; set => height = value; }
        public static int Score { get => score; set => score = value; }

        public skeleton(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.speed = 1;
            this.hp = 1;
        }
        public void Render(BufferedGraphics drawingSpace)
        {
            drawingSpace.Graphics.DrawImage(Resources.skeleton, x, y, height, height);
        }
        public void update(int interval)
        {
            if (y + height >= Config.limitheight)
            {
                if (GameSpace.Defense.Count >= 1)
                {
                    GameSpace.Defense[0].hit(false);
                }

            }
            else
            {
                if (gauche)
                {
                    walktime++;
                    if (x > 0)
                    {
                        x -= speed;
                    }
                    if (walktime >= 25)
                    {
                        gauche = false;
                        bas = true;
                        walktime = 0;
                    }


                }
                else if (droite)
                {
                    walktime++;
                    if (x < Config.AIRSPACEWIDTH - height)
                    {
                        x += speed;

                    }

                    if (walktime >= 25)
                    {
                        droite = false;
                        bas = true;
                        walktime = 0;
                    }

                }
                else if (bas)
                {
                    y += speed;
                    walktime++;
                    if (walktime >= 25)
                    {
                        walktime = 0;

                        if (gaucheavant)
                        {
                            droite = true;
                            bas = false;
                            gaucheavant = false;

                        }
                        else
                        {
                            gauche = true;
                            bas = false;
                            gaucheavant = true;
                        }
                    }
                }
            }
        }
    }
}
