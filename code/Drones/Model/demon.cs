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
    internal class demon
    {
        private int x;
        private int y;
        private int speed;
        private int hp;
        private static int height = 85;
        private static int score = 300;
        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public int Speed { get => speed; set => speed = value; }
        public int Hp { get => hp; set => hp = value; }
        public static int Height { get => height; set => height = value; }
        public static int Score { get => score; set => score = value; }

        public demon(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.speed = 1;
            this.hp = 2;
        }
        public void Render(BufferedGraphics drawingSpace)
        {
            drawingSpace.Graphics.DrawImage(Resources.demon, x, y, height, height);
        }
        public void update(int interval)
        {
            
            if (y + height >= Config.limitheight)
           {
                 if (GameSpace.Defense.Count >= 1)
                {
                    GameSpace.Defense[0].hit(false);
                }
                else
                {
                    throw new Exception("le joueur s'est fait tué");
                }
           }
            else
            {
                y += speed;
            }
        }
    }
}
