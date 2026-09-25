using Drones.Helpers;
using Drones.Model;

namespace Drones
{
    // La classe AirSpace représente le territoire au dessus duquel les drones peuvent voler
    // Il s'agit d'un formulaire (une fenêtre) qui montre une vue 2D depuis en dessus
    // Il n'y a donc pas de notion d'altitude qui intervient

    public partial class AirSpace : Form
    {

        public static bool droite = false;
        public static bool gauche = false;
        public static bool haut = false;
        public static bool bas = false;
        public static bool shoot = false;
        
        // La flotte est l'ensemble des drones qui évoluent dans notre espace aérien
        private Drone _player;

        BufferedGraphicsContext currentContext;
        BufferedGraphics airspace;


        // Initialisation de l'espace aérien avec un certain nombre de drones
        public AirSpace(Drone player)
        {
            InitializeComponent();
            ClientSize = new Size(Config.AIRSPACEWIDTH, Config.AIRSPACEHEIGHT);

            // Gets a reference to the current BufferedGraphicsContext
            currentContext = BufferedGraphicsManager.Current;
            // Creates a BufferedGraphics instance associated with this form, and with
            // dimensions the same size as the drawing surface of the form.
            airspace = currentContext.Allocate(this.CreateGraphics(), this.DisplayRectangle);
            this._player = player;
        }

        // Affichage de la situation actuelle
        private void Render()
        {
            airspace.Graphics.Clear(Color.AliceBlue);

            _player.Render(airspace);
            
            airspace.Render();
        }

        // Calcul du nouvel état après que 'interval' millisecondes se sont écoulées
        private void Update(int interval)
        {
            _player.Update(interval);
        }

        // Méthode appelée à chaque frame
        private void NewFrame(object sender, EventArgs e)
        {
            this.Update(ticker.Interval);
            this.Render();
        }

        private void AirSpace_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    haut = true;
                    _player.ChangeDirection();
                    break;
                case Keys.S:
                    bas = true;
                    _player.ChangeDirection();
                    break;
                case Keys.D:
                    droite = true;
                    _player.ChangeDirection();
                    break;
                case Keys.A:
                    gauche = true;
                    _player.ChangeDirection();
                    break;
                case Keys.Space:
                    shoot = true;
                    _player.shoot();
                    break;

            }
        }
        private void AirSpace_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    haut = false;
                    _player.ChangeDirection();
                    break;
                case Keys.S:
                    bas = false;
                    _player.ChangeDirection();
                    break;
                case Keys.D:
                    droite = false;
                    _player.ChangeDirection();
                    break;
                case Keys.A:
                    gauche = false;
                    _player.ChangeDirection();
                    break;
                case Keys.Space:
                    shoot = false;
                    _player.shoot();
                    break;
            }
        }
    }

}