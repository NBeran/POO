using player.Model;

using player.Helpers;
using player.Model;
using System.Runtime.Intrinsics.Arm;
using static System.Formats.Asn1.AsnWriter;
using shoot_them_up.Properties;

namespace player
{
    // Cette partie de la classe Drone définit ce qu'est un drone par un modèle numérique
    public class Player
    {
        //variables
        public string name;                           // Un nom
        public int x;                                 // Position en X depuis la gauche de l'espace aérien
        public int y;                                 // Position en Y depuis le haut de l'espace aérien                        
        public int live = 3;
        private int playerheight = 85;
        private int dashcooldown = 0;
        public int speed = 20;
        private int cooldawn = 0;
        private int dashduration = 0;
        private int dashdurationmax = 3;

        //getter setter pour les variables qui doivent être accessibles depuis l'extérieur de la classe
        public int Playerheight { get => playerheight; set => playerheight = value; }
        public int Dashcooldown { get => dashcooldown; private set => dashcooldown = value; }

        // Constructeur
        public Player(int x, int y, string name)
        {
            this.x = x;
            this.y = y;
            this.name = name;
        }

        // Cette méthode calcule le nouvel état dans lequel le drone se trouve après
        // que 'interval' millisecondes se sont écoulées
        public void Update(int interval, bool haut, bool bas, bool gauche, bool droite)
        {
            cooldawn++;

            if (dashduration > 0)
            {
                
                dashduration--;
                if (dashduration == 0)
                    speed = 20;   
            }
            else if (dashcooldown < 100)
            {
                dashcooldown++;             
            }

            x = x + (droite ? speed : 0);
            x = x - (gauche ? speed : 0);
            y = y + (bas ? speed : 0);
            y = y - (haut ? speed : 0);
            verifiyborder();

        }



        // De manière graphique
        public void Render(BufferedGraphics drawingSpace)
        {

            drawingSpace.Graphics.DrawImage(Resources.player, x, y, playerheight, playerheight);
        }

        // De manière textuelle
        public override string ToString()
        {
            return $"{name}";
        }


        //méthode qui vérifie si le drone est dans les limites de l'espace aérien
        public void verifiyborder()
        {
            if (x < 0) x = 0;
            if (y < Config.limitheight - playerheight) y = Config.limitheight - playerheight;
            if (x > Config.AIRSPACEWIDTH - playerheight) x = Config.AIRSPACEWIDTH - playerheight;
            if (y > Config.AIRSPACEHEIGHT - playerheight) y = Config.AIRSPACEHEIGHT - playerheight;
        }
        public void dash()
        {
            if (dashcooldown >= 100 && dashduration == 0)
            {
                speed = 100;
                dashcooldown = 0;
                dashduration = dashdurationmax;
            }
        }

    }
}
