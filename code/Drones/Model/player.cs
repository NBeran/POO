using Drones.Properties;
using player.Helpers;
using player.Model;
using System.Runtime.Intrinsics.Arm;

namespace player
{
    // Cette partie de la classe Drone définit ce qu'est un drone par un modèle numérique
    public class Drone
    {
        public string name;                           // Un nom
        public int x;                                 // Position en X depuis la gauche de l'espace aérien
        public int y;                                 // Position en Y depuis le haut de l'espace aérien
        public int speed_x;
        public int speed_y;                             // Déplacement horizontal
        public int live = 3;
        public int playerheight = 75;
        public int speed = 10;
        private int cooldawn = 0;

        // Déplacement vertical
        private Random _alea = new Random();

        // Constructeur
        public Drone(int x, int y, string name)
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
            x = x + (droite ? speed : 0);
            x = x - (gauche ? speed : 0);
            y = y + (bas ? speed : 0);
            y = y - (haut ? speed : 0);
            verifiyborder();

        }

        // Choisit une nouvelle vitesse aléatoirement


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

            drawingSpace.Graphics.DrawImage(Resources.player, x, y, playerheight, playerheight);
        }

        // De manière textuelle
        public override string ToString()
        {
            return $"{name}";
        }



        public void verifiyborder()
        {
            if (x < 0) x = 0;
            if (y < 0) y = 0;
            if (x > Config.AIRSPACEWIDTH - playerheight) x = Config.AIRSPACEWIDTH - playerheight;
            if (y > Config.AIRSPACEHEIGHT - playerheight) y = Config.AIRSPACEHEIGHT - playerheight;
        }
    }
}
