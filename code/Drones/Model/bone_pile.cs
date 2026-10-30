
using player;
using shoot_them_up.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace player.Model
{
    public class bone_pile
    {
        //variables
        public int x;                                 
        public int y;                                                         
        public int live = 3;
        public int height;
        public int width;
        public static int maxheight = 75;
        public static int maxwidth = 100;
        private int hitcounter;
        //getter setter pour les variables qui doivent être accessibles depuis l'extérieur de la classe
        public int Height { get => height; private set => height = value; }
        public int Hitcounter { get => hitcounter; set => hitcounter = value; }

        //constructeur
        public bone_pile(int x, int y)
        {
            this.x = x;
            this.y = y;
            height = maxheight;
            hitcounter = 0;
            width = maxwidth;
        }

        //méthode pour dessiner le tas d'os
        public void Render(BufferedGraphics drawingSpace)
        {

            drawingSpace.Graphics.DrawImage(Resources.bone_pile, x, y, width, height);
        }

        //méthode pour savoir comment réagir quand le tas d'os est touché par un projectile
        public void hit(bool player)
        {
            //augmente le nombre de coups recu et diminue sa taille
            hitcounter++;
            height -= 10;
            width -= 10;
            //si touché 5x ou plus, il se casse
            if (hitcounter >= 5)
            {   
                //si c'est le joueur qui l'as cassé, il perd 200 de score
                if (player)
                {
                    GameSpace.Score -= 200;
                }
              
            }
        }
    }
}
