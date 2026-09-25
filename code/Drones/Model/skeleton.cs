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

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public int Speed { get => speed; set => speed = value; }
        public int Hp { get => hp; set => hp = value; }

        public skeleton(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.speed = 5;
            this.hp = 1;
        }


    }
}
