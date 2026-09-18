using Drones.Helpers;
using Drones.Properties;

namespace Drones
{
    // Cette partie de la classe Drone définit ce qu'est un drone par un modèle numérique
    public class Drone
    {
        public int charge;                            // La charge actuelle de la batterie
        public string name;                           // Un nom
        public int x;                                 // Position en X depuis la gauche de l'espace aérien
        public int y;                                 // Position en Y depuis le haut de l'espace aérien
        public int speed_x;                           // Déplacement horizontal
        public int speed_y;
        public int live = 3;
        public int playerheight = 50;
        public int speed = 20;
        // Déplacement vertical
        private Random _alea = new Random();

        // Constructeur
        public Drone(int x, int y, string name)
        {
            Random alea = new Random();
            this.x = x;
            this.y = y;
            this.name = name;
            charge = alea.Next(1000); // La charge initiale de la batterie est choisie aléatoirement
            ChangeDirection();
        }

        // Cette méthode calcule le nouvel état dans lequel le drone se trouve après
        // que 'interval' millisecondes se sont écoulées
        public void Update(int interval)
        {
            x += speed_x;
            y += speed_y;
            charge--;
        }

        // Choisit une nouvelle vitesse aléatoirement
        public void ChangeDirection()
        {
            if (AirSpace.bas && y < AirSpace.HEIGHT - playerheight - speed)
            {
                speed_y = speed;
            }
            else if (AirSpace.haut && y >= 0)
            {
                speed_y = -speed;
            }
            else
            {
                speed_y = 0;
            }
            if (AirSpace.gauche && x >= 0)
            {
                speed_x = -speed;
            }
            else if (AirSpace.droite && x < AirSpace.WIDTH - playerheight)
            {
                speed_x = speed;
            }
            else
            {
                speed_x = 0;
            }
        }

        /// //////////////////////////////////////////////////////////////////////////////
        //  
        //  Ce qui suit appartient à la vue, pas au modèle.
        //  Il aurait été préférable de séparer la déclaration de la classe Drone en deux,
        //  Nous regroupons tout ici pour simplifier
        //  
        /// //////////////////////////////////////////////////////////////////////////////

        private Pen droneBrush = new Pen(new SolidBrush(Color.Purple), 3);

        // De manière graphique
        public void Render(BufferedGraphics drawingSpace)
        {
            drawingSpace.Graphics.DrawImage(Resources.drone, x, y, 50, 50);
        }

        // De manière textuelle
        public override string ToString()
        {
            return $"{name} ({((int)((double)charge / 1000 * 100)).ToString()}%)";
        }


    }
}
