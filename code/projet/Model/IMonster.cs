using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shoot_them_up.Model
{
    public interface IMonster
    {
        bool isalive { get; set; }
        void Render(BufferedGraphics drawingspace);
        void update(int interval);
        int Y {  get; set; }
        int Speed { get; set; }
        int X { get; set; }
    }
}
