using Drones.Properties;
using player;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drones.Model
{
    public class bone_pile
    {
        public int x;                                 
        public int y;                                                         
        public int live = 3;
        public int height;
        public int width;
        public static int maxheight = 75;
        public static int maxwidth = 100;
        private int hitcounter;
        public int Height { get => height; private set => height = value; }
        public int Hitcounter { get => hitcounter; set => hitcounter = value; }

        public bone_pile(int x, int y)
        {
            this.x = x;
            this.y = y;
            height = maxheight;
            hitcounter = 0;
            width = maxwidth;
        }
        public void Render(BufferedGraphics drawingSpace)
        {

            drawingSpace.Graphics.DrawImage(Resources.bone_pile, x, y, width, height);
        }

        public void hit(bool player)
        {
            hitcounter++;
            height -= 10;
            width -= 10;
            if (hitcounter >= 5)
            {   
                if (player)
                {
                    GameSpace.Score -= 200;
                }
              
            }
        }
    }
}
