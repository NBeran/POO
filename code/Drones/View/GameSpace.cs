using player.Helpers;
using player.Model;

namespace player
{
    // La classe GameSpace représente le territoire au dessus duquel les drones peuvent voler
    // Il s'agit d'un formulaire (une fenêtre) qui montre une vue 2D depuis en dessus
    // Il n'y a donc pas de notion d'altitude qui intervient

    public partial class GameSpace : Form
    {
        private int cooldawn = 0;
        public static bool droite = false;
        public static bool gauche = false;
        public static bool haut = false;
        public static bool bas = false;
        List<PlayerAtk> attaques = new List<PlayerAtk>();
        List<PlayerAtk> aSupprimer = new List<PlayerAtk>();
        // La flotte est l'ensemble des drones qui évoluent dans notre espace aérien
        private Drone _player;

        BufferedGraphicsContext currentContext;
        BufferedGraphics airspace;


        // Initialisation de l'espace aérien avec un certain nombre de drones
        public GameSpace(Drone player)
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
            foreach (PlayerAtk atk in attaques)
            {

                atk.Render(airspace);
            }
            airspace.Render();
        }

        // Calcul du nouvel état après que 'interval' millisecondes se sont écoulées
        private void Update(int interval)
        {
            cooldawn++;
            _player.Update(interval, haut, bas, gauche, droite);
            foreach (PlayerAtk a in attaques)
            {
                try
                {
                    a.update(interval);
                }
                catch
                {
                    aSupprimer.Add(a);
                }
            }
            foreach (PlayerAtk a in aSupprimer)
            {
                attaques.Remove(a);
            }
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
                case Keys.Space:
                       shoot();
                    break;
                case Keys.W:
                    haut = true;
                    break;
                case Keys.S:
                    bas = true;
                    break;
                case Keys.D:
                    droite = true;
                    break;
                case Keys.A:
                    gauche = true;
                    break;


            }
        }
        private void AirSpace_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    haut = false;
                    break;
                case Keys.S:
                    bas = false;
                    break;
                case Keys.D:
                    droite = false;
                    break;
                case Keys.A:
                    gauche = false;
                    break;

            }
        }
        public void shoot()
        {
            if (cooldawn >= 6)
            {
                Console.WriteLine("appuyé");
                attaques.Add(new PlayerAtk(_player.x, _player.y));
                cooldawn = 0;
            }
        }
    }

}