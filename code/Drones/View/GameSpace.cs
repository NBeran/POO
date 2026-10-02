using Drones.Helpers;
using Drones.Model;
using player.Helpers;
using player.Model;

namespace player
{
    // La classe GameSpace représente le territoire au dessus duquel les drones peuvent voler
    // Il s'agit d'un formulaire (une fenêtre) qui montre une vue 2D depuis en dessus
    // Il n'y a donc pas de notion d'altitude qui intervient

    public partial class GameSpace : Form
    {
        private int skeletoncooldawn = 0;
        private int cooldawn = 0;
        public static bool droite = false;
        public static bool gauche = false;
        public static bool haut = false;
        public static bool bas = false;
        private static int score = 0;
        private static skeleton shooting;
        List<PlayerAtk> attaques = new List<PlayerAtk>();
        List<PlayerAtk> atkdelete = new List<PlayerAtk>();
        List<skeleton> skeletonwave = new List<skeleton>();
        List<skeleton> skeletondelete = new List<skeleton>();
        List<skeletonatk> skeletonatks = new List<skeletonatk>();
        List<skeletonatk> skeletonatksdelete = new List<skeletonatk>();
        // La flotte est l'ensemble des drones qui évoluent dans notre espace aérien
        private Drone _player;

        BufferedGraphicsContext currentContext;
        BufferedGraphics airspace;

        internal List<skeleton> Skeletonwave { get => skeletonwave; set => skeletonwave = value; }


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
            skeletonwave.AddRange(new[] { new skeleton(0, 50), new skeleton(100, 50), new skeleton(200, 50), new skeleton(300, 50), new skeleton(400, 50) });
        }

        // Affichage de la situation actuelle
        private void Render()
        {
            airspace.Graphics.Clear(Color.AliceBlue);
            
            airspace.Graphics.DrawString("score : " + score.ToString(), TextHelpers.drawFont, TextHelpers.writingBrush, 5, 5);
            airspace.Graphics.DrawString("vie : " + _player.live.ToString(), TextHelpers.drawFont, TextHelpers.writingBrush, 5, Config.AIRSPACEHEIGHT - 30);
            _player.Render(airspace);

            foreach (PlayerAtk atk in attaques)
            {
                atk.Render(airspace);
            }
            foreach (skeleton skeleton in skeletonwave)
            {
                skeleton.Render(airspace);
            }
            foreach (skeletonatk atk in skeletonatks)
            {
                atk.Render(airspace);
            }
            airspace.Render();
        }

        // Calcul du nouvel état après que 'interval' millisecondes se sont écoulées
        private void Update(int interval)
        {
            cooldawn++;
            skeletoncooldawn++;
            if (skeletoncooldawn >= 20 && skeletonwave.Count >= 1)
            {
                skeletoncooldawn = 0;
                shooting = randomhelper.rndskeleton (Skeletonwave);
                skeletonatk atk = new skeletonatk(shooting.X, shooting.Y);
                skeletonatks.Add(atk);
            }
            _player.Update(interval, haut, bas, gauche, droite);
            foreach (PlayerAtk a in attaques)
            {
                try
                {
                    a.update(interval);
                }
                catch
                {
                    atkdelete.Add(a);
                }
            }
            foreach (PlayerAtk a in atkdelete)
            {
                attaques.Remove(a);
            }
            foreach(skeleton skeleton in skeletonwave)
            {
                skeleton.update(interval);
            }
            foreach(skeletonatk skeletonatk in skeletonatks)
            {
                try
                {
                    skeletonatk.update(interval);
                }
                catch
                {
                    skeletonatksdelete.Add(skeletonatk);
                }
            }
            foreach (skeletonatk s in skeletonatksdelete)
            {
                skeletonatks.Remove(s);
            }
            checkhit();
            foreach (skeleton a in skeletondelete)
            {
                skeletonwave.Remove(a);
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
                case Keys.Up:
                    haut = true;
                    break;
                case Keys.Down:
                    bas = true;
                    break;
                case Keys.Right:
                    droite = true;
                    break;
                case Keys.Left:
                    gauche = true;
                    break;


            }
        }
        private void AirSpace_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    haut = false;
                    break;
                case Keys.Down:
                    bas = false;
                    break;
                case Keys.Right:
                    droite = false;
                    break;
                case Keys.Left:
                    gauche = false;
                    break;

            }
        }
        public void shoot()
        {
            if (cooldawn >= 6)
            {
                attaques.Add(new PlayerAtk(_player.x, _player.y));
                cooldawn = 0;
            }
        }
        public void checkhit()
        {
            foreach(PlayerAtk p in attaques)
            {
                foreach (skeleton s in skeletonwave)
                {
                    if (p.X + PlayerAtk.Width >= s.X && p.X <= s.X + skeleton.Height && p.Y + PlayerAtk.Height >= s.Y && p.Y <= s.Y + skeleton.Height)
                    {
                        score += skeleton.Score;
                        skeletondelete.Add(s);
                        atkdelete.Add(p);
                    }
                }
                foreach (skeletonatk s in skeletonatks)
                {
                    if (p.X + PlayerAtk.Width >= s.X && p.X <= s.X + skeletonatk.Height && p.Y + PlayerAtk.Height >= s.Y && p.Y <= s.Y + skeletonatk.Height)
                    {
                        skeletonatksdelete.Add(s);
                        atkdelete.Add(p);

                    }
                }
            }
            foreach (skeletonatk s in skeletonatks)
            {
                if (_player.x + skeletonatk.Width >= s.X && _player.x <= s.X + skeletonatk.Height && _player.y + PlayerAtk.Height >= s.Y && _player.y <= s.Y + skeletonatk.Height)
                {
                    skeletonatksdelete.Add(s);
                    if (_player.live >= 1)
                    {
                        _player.live -= 1;
                    }
                }
            }
        }
    }

}