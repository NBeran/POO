using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drones
{
    internal class test
    {

    
    static void Main()
        {
            int posx = 0;
            int posy = 0;
            int speedx = 10;
            int speedy = 0;
            while (true)
            {
                Console.WriteLine("X");
                Thread.Sleep(100);
                posx += speedx;
                Console.SetCursorPosition(posx, posy);

            }

        }
    }
}
