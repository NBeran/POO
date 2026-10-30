using player.Helpers;
using player.Model;
using shoot_them_up.Properties;


namespace player
{
    // La classe GameSpace représente le territoire au dessus duquel les drones peuvent voler
    // Il d'agit d'un formulaire (une fenêtre) qui montre une vue 2D depuis en dessus
    // Il n'y a donc pas de notion d'altitude qui intervient

    public partial class GameSpace : Form
    {
        //variables
        private int skeletoncooldawn = 0;
        private int demoncooldown = 0;
        private int cooldawn = 0;
        private int wavecooldawn = 0;
        private int demoninterval = 350;
        private int dashpourcent = 0;
        public static bool right = false;
        public static bool left = false;
        public static bool up = false;
        public static bool down = false;
        public static bool shooting = false;
        private bool gamejustlunch = true;
        private bool seekeybin = false;
        private int olddemon = 0;
        private static int score = 0;
        private static skeleton skeletonshooting;
        private static demon demonshooting;
        private bool gameover = false;
        private int wavecount = 0;

        //liste des objets
        public List<PlayerAtk> atk = new List<PlayerAtk>();
        public List<PlayerAtk> atkdelete = new List<PlayerAtk>();

        public List<skeleton> skeletonwave = new List<skeleton>();
        public List<skeleton> skeletondelete = new List<skeleton>();
        public List<skeletonatk> skeletonatks = new List<skeletonatk>();
        public List<skeletonatk> skeletonatksdelete = new List<skeletonatk>();

        public List<demon> demonwave = new List<demon>();
        public List<demon> demondelete = new List<demon>();
        public List<demonatk> demonatks = new List<demonatk>();
        public List<demonatk> demonatkdelete = new List<demonatk>();

        static public List<bone_pile> defense = new List<bone_pile>();
        public List<bone_pile> defenseadelete = new List<bone_pile>();
        // La flotte est l'ensemble des drones qui évoluent dans notre espace aérien
        private Player _player;

        BufferedGraphicsContext currentContext;
        BufferedGraphics airspace;

        //getter setter pour les variables qui doivent être accessibles depuis l'extérieur de la classe
        internal List<skeleton> Skeletonwave { get => skeletonwave; set => skeletonwave = value; }
        public static int Score { get => score; set => score = value; }
        internal static List<bone_pile> Defense { get => defense; set => defense = value; }
        public int Wavecooldawn { get => wavecooldawn; set => wavecooldawn = value; }
        public int Cooldawn { get => cooldawn; set => cooldawn = value; }
        public int Demoncooldown { get => demoncooldown; set => demoncooldown = value; }
        public int Skeletoncooldawn { get => skeletoncooldawn; set => skeletoncooldawn = value; }
        public static skeleton Skeletonshooting { get => skeletonshooting; set => skeletonshooting = value; }
        public static demon Demonshooting { get => demonshooting; set => demonshooting = value; }





        // Initialisation de l'espace aérien avec un certain nombre de drones
        public GameSpace(Player player)
        {
            InitializeComponent();
            ClientSize = new Size(Config.AIRSPACEWIDTH, Config.AIRSPACEHEIGHT);

            // Gets a reference to the current BufferedGraphicsContext
            currentContext = BufferedGraphicsManager.Current;
            // Creates a BufferedGraphics instance associated with this form, and with
            // dimensions the same size as the drawing surface of the form.
            airspace = currentContext.Allocate(this.CreateGraphics(), this.DisplayRectangle);


            this._player = player;

            //créatoin des defense et des premiuers ennemis
            for (int i = 0; i <= Config.AIRSPACEWIDTH; i += 300)
            {
                defense.Add(new bone_pile(i, Config.limitheight - bone_pile.maxheight));
            }
            newwave();

        }

        // Affichage de la situation actuelle
        public void Render()
        {
            //affiche l'écran d'accueil si le jeu vient de se lancer
            if (gamejustlunch)
            {

                airspace.Graphics.Clear(Color.Orange);
                airspace.Graphics.DrawImage(Resources.Lobby, 0, 0, Config.AIRSPACEWIDTH, Config.AIRSPACEHEIGHT);
                airspace.Render();

            }
            else if (seekeybin)
            {
                airspace.Graphics.Clear(Color.Orange);
                airspace.Graphics.DrawImage(Resources.keybind, 0, 0, Config.AIRSPACEWIDTH, Config.AIRSPACEHEIGHT);
                airspace.Render();
            }
            //affiche l'écran de game over si le joueur est mort
            else if (gameover)
            {
                airspace.Graphics.Clear(Color.Orange);
                airspace.Graphics.DrawImage(Resources.backgroud, 0, 0, Config.AIRSPACEWIDTH, Config.AIRSPACEHEIGHT);
                airspace.Graphics.DrawString("Vous êtes morts", TextHelpers.StartFont, TextHelpers.writingBrush, Config.AIRSPACEWIDTH / 3 + 35, Config.AIRSPACEHEIGHT / 4);
                airspace.Graphics.DrawString("score : " + score.ToString(), TextHelpers.StartFont, TextHelpers.writingBrush, Config.AIRSPACEWIDTH / 3 + 100, Config.AIRSPACEHEIGHT / 2);
                airspace.Graphics.DrawString("Appuyez sur espace pour réessayer", TextHelpers.StartFont, TextHelpers.writingBrush, Config.AIRSPACEWIDTH / 5 + 50, Config.AIRSPACEHEIGHT / 2 + 100);


                airspace.Render();
            }
            //sinon affiche le jeu normallement 
            else
            {

                //affiche le fond, le score et la vie du joueur
                airspace.Graphics.Clear(Color.IndianRed);
                airspace.Graphics.DrawImage(Resources.backgroud, 0, 0, Config.AIRSPACEWIDTH, Config.AIRSPACEHEIGHT);
                airspace.Graphics.DrawString("score : " + score.ToString(), TextHelpers.drawFont, TextHelpers.writingBrush, 15, 15);
                airspace.Graphics.DrawString("vague : " + wavecount.ToString(), TextHelpers.drawFont, TextHelpers.writingBrush, 15, 65);
                //airspace.Graphics.DrawString("vague : " + (Config.AIRSPACEWIDTH - monsterinterval) / 50, TextHelpers.drawFont, TextHelpers.writingBrush, 15, 30);
                dashpourcent = _player.Dashcooldown;
                if (dashpourcent >= 100)
                {
                    airspace.Graphics.DrawString("dash : PRET !", TextHelpers.drawFont, TextHelpers.writingBrush, 15, Config.AIRSPACEHEIGHT - 75);
                }
                else
                {
                    airspace.Graphics.DrawString("dash : " + _player.Dashcooldown.ToString() + " %", TextHelpers.drawFont, TextHelpers.writingBrush, 15, Config.AIRSPACEHEIGHT - 75);
                }
                airspace.Graphics.DrawString("vie : " + _player.live.ToString(), TextHelpers.drawFont, TextHelpers.writingBrush, 15, Config.AIRSPACEHEIGHT - 45);
                _player.Render(airspace);

                //affiche toutes les attaques du joueur
                foreach (PlayerAtk atk in atk)
                {
                    atk.Render(airspace);
                }

                //affiche touts les squelettes
                foreach (skeleton skeleton in skeletonwave)
                {
                    skeleton.Render(airspace);
                }

                //affiche toutes les attaques des squelettes
                foreach (skeletonatk atk in skeletonatks)
                {
                    atk.Render(airspace);
                }

                //affiche tous les démons
                foreach (demon demon in demonwave)
                {
                    demon.Render(airspace);
                }

                //affiche toutes les attaques des démons
                foreach (demonatk atk in demonatks)
                {
                    atk.Render(airspace);
                }

                //affiche toutes les defenses
                foreach (bone_pile bone in defense)
                {
                    bone.Render(airspace);
                }
                airspace.Render();
            }
        }

        // Calcul du nouvel état après que 'interval' millisecondes se sont écoulées
        public void Update(int interval)
        {
            //attend un peu puis lance une nouvelle vague si il n'y a plus d'ennemis
            if (demonwave.Count <= 0 && skeletonwave.Count <= 0)
            {
                wavecooldawn++;
                if (wavecooldawn > 20)
                {
                    wavecooldawn = 0;
                    newwave();
                }
            }
            //augmente les différents cooldown du tir du joueur et des ennemis
            cooldawn++;
            skeletoncooldawn += skeletonwave.Count;
            demoncooldown += demonwave.Count;
            //coisis un squelette aléatoire et le fait tirer si le cooldown est écoulé
            if (skeletoncooldawn >= 30 && skeletonwave.Count >= 1)
            {
                skeletoncooldawn -= 30;
                skeletonshooting = randomhelper.rndskeleton(Skeletonwave);
                skeletonatk atk = new skeletonatk(skeletonshooting.X, skeletonshooting.Y);
                skeletonatks.Add(atk);
            }
            //coisis un démon aléatoire et le fait tirer si le cooldown est écoulé
            if (demoncooldown >= 35 && demonwave.Count >= 1)
            {
                demoncooldown -= 35;
                demonshooting = randomhelper.rnddemon(demonwave);
                demonatk atk = new demonatk(demonshooting.X, demonshooting.Y);
                demonatks.Add(atk);
            }
            //calcul la nouvelle position du joueur
            _player.Update(interval, up, down, left, right);
            //calcule la nouvelle position de toutes les attaques du joueur
            foreach (PlayerAtk a in atk)
            {
                a.update(interval);
            }

            //calcul la nouvelle position de tous les squelettes
            foreach (skeleton skeleton in skeletonwave)
            {


                skeleton.update(interval);
                //tue le joueur si il n'as plus de defense et que un squelette est trop proche
                if (skeleton.Y + skeleton.Height >= Config.limitheight && GameSpace.Defense.Count <= 0)
                {
                    _player.live = 0;
                    gameover = true;
                }

            }
            //calcul la nouvelle position de toutes les attaques des squelettes
            foreach (skeletonatk skeletonatk in skeletonatks)
            {

                skeletonatk.update(interval);
            }
            //calcul la nouvelle position de tous les démons
            foreach (demon demon in demonwave)
            {


                demon.update(interval);
                //tue le joueur si il n'as plus de defense et que un démon est trop proche
                if (demon.Y + skeleton.Height >= Config.limitheight && GameSpace.Defense.Count <= 0)
                {
                    _player.live = 0;
                    gameover = true;
                }
            }
            //calcul la nouvelle position de toutes les attaques des démons
            foreach (demonatk demonatk in demonatks)
            {
                demonatk.update(interval);
            }
            //verifie toutes les collisions et supprimes les objets qui doivent l'être
            checkhit();
            removeobj();
        }

        // Méthode appelée à chaque frame
        private void NewFrame(object sender, EventArgs e)
        {
            if (!gamejustlunch && !gameover && !seekeybin)
            {
                this.Update(ticker.Interval);
            }
            this.Render();
        }

        private void AirSpace_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    //lance une partie si le joueur appuie sur espace et que le jeu vient de se lancer
                    if (gamejustlunch || seekeybin)
                    {
                        gamejustlunch = false;
                        seekeybin = false;
                    }
                    //reset le jeu si le joueur appuie sur espace et que le jeu est terminé
                    else if (gameover)
                    {
                        gameover = false;
                        _player.live = 3;
                        score = 0;
                        _player.x = Config.AIRSPACEWIDTH / 2;
                        _player.y = (Config.AIRSPACEHEIGHT - Config.limitheight) / 2;
                        wavecount = 0;
                        demoninterval = 350;
                        skeletonatks.Clear();
                        demonatks.Clear();
                        atk.Clear();
                        for (int i = 0; i <= Config.AIRSPACEWIDTH; i += 300)
                        {
                            defense.Add(new bone_pile(i, Config.limitheight - bone_pile.maxheight));
                        }
                        foreach (skeleton s in skeletonwave)
                        {
                            s.Speed = 2;
                        }
                        foreach (demon d in demonwave)
                        {
                            d.Speed = 2;
                        }
                        ClearWave();
                        newwave();
                    }
                    //tire si le joueur appuie sur espace et que le jeu est en cours
                    else
                    {
                        shoot();
                    }

                    break;
                //change de direction en fonction des touches appuyées
                case Keys.Up:
                    up = true;
                    break;
                case Keys.Down:
                    down = true;
                    break;
                case Keys.Right:
                    right = true;
                    break;
                case Keys.Left:
                    left = true;
                    break;


                case Keys.E:

                    if (gamejustlunch)
                    {
                        seekeybin = true;
                        gamejustlunch = false;
                    }
                    else if (seekeybin)
                    {
                        seekeybin = false;
                        gamejustlunch = true;
                    }
                    else
                    {
                        _player.dash();
                    }

                    break;


            }
        }
        private void AirSpace_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                //arette de bouger en fonction des touches relachées
                case Keys.Up:
                    up = false;
                    break;
                case Keys.Down:
                    down = false;
                    break;
                case Keys.Right:
                    right = false;
                    break;
                case Keys.Left:
                    left = false;
                    break;
                case Keys.E:
                    _player.dash();
                    break;

            }
        }
        public void shoot()
        {
            //verifie que le cooldown du tir est écoulé pui fait tirer le joueur wt reset le cooldown
            if (cooldawn >= 6)
            {
                atk.Add(new PlayerAtk(_player.x, _player.y));
                cooldawn = 0;
            }
        }

        //verifie toutes les collisions entre les différents objets
        public void checkhit()
        {
            //verifie toutes les collision en rapport avec le joueur
            foreach (PlayerAtk p in atk)
            {
                if (p.Y + PlayerAtk.Height < 0)
                {
                    atkdelete.Add(p);
                }


                foreach (skeleton s in skeletonwave)
                {
                    if (p.X + PlayerAtk.Width >= s.X && p.X <= s.X + skeleton.Height && p.Y + PlayerAtk.Height >= s.Y && p.Y <= s.Y + skeleton.Height)
                    {
                        score += skeleton.Score;
                        skeletondelete.Add(s);
                        atkdelete.Add(p);
                    }
                }
                foreach (demon d in demonwave)
                {
                    if (p.X + PlayerAtk.Width >= d.X && p.X <= d.X + skeleton.Height && p.Y + PlayerAtk.Height >= d.Y && p.Y <= d.Y + skeleton.Height)
                    {
                        d.Hp--;
                        atkdelete.Add(p);
                        if (d.Hp <= 0)
                        {
                            score += demon.Score;
                            demondelete.Add(d);
                        }

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
                foreach (demonatk d in demonatks)
                {
                    if (p.X + PlayerAtk.Width >= d.X && p.X <= d.X + demonatk.Height && p.Y + PlayerAtk.Height >= d.Y && p.Y <= d.Y + demonatk.Height)
                    {
                        demonatkdelete.Add(d);
                        atkdelete.Add(p);

                    }
                }
                foreach (bone_pile b in defense)
                {
                    if (p.X + PlayerAtk.Width >= b.x && p.X <= b.x + b.width && p.Y + PlayerAtk.Height >= b.y && p.Y <= b.y + b.height)
                    {


                        b.hit(true);

                        if (b.Hitcounter >= 5)
                        {
                            defenseadelete.Add(b);
                        }
                        atkdelete.Add(p);
                    }
                }
            }
            //verifie toutes les collision en rapport avec le squelette
            foreach (skeletonatk s in skeletonatks)
            {
                if (s.Y + skeletonatk.Height > Config.AIRSPACEHEIGHT)
                {
                    skeletonatksdelete.Add(s);
                }
                if (_player.x + skeletonatk.Width >= s.X && _player.x <= s.X + skeletonatk.Height && _player.y + PlayerAtk.Height >= s.Y && _player.y <= s.Y + skeletonatk.Height)
                {
                    skeletonatksdelete.Add(s);
                    if (_player.live >= 1)
                    {
                        _player.live -= 1;
                    }
                    if (_player.live == 0)
                    {

                        gameover = true;
                    }
                }
                foreach (bone_pile b in defense)
                {
                    if (s.X + skeletonatk.Width >= b.x && s.X <= b.x + b.width && s.Y + skeletonatk.Height >= b.y && s.Y <= b.y + b.height)
                    {


                        b.hit(false);

                        if (b.Hitcounter >= 5)
                        {
                            defenseadelete.Add(b);
                        }
                        skeletonatksdelete.Add(s);
                    }
                }
            }
            //verifie toutes les collision en rapport avec le demon
            foreach (demonatk d in demonatks)
            {
                if (d.Y + demonatk.Height > Config.AIRSPACEHEIGHT)
                {
                    demonatkdelete.Add(d);
                }
                if (_player.x + demonatk.Width >= d.X && _player.x <= d.X + demonatk.Height && _player.y + PlayerAtk.Height >= d.Y && _player.y <= d.Y + demonatk.Height)
                {
                    demonatkdelete.Add(d);
                    if (_player.live >= 1)
                    {
                        _player.live -= 1;
                    }
                    if (_player.live == 0)
                    {

                        gameover = true;
                    }
                }
                foreach (bone_pile b in defense)
                {
                    if (d.X + demonatk.Width >= b.x && d.X <= b.x + b.width && d.Y + demonatk.Height >= b.y && d.Y <= b.y + b.height)
                    {


                        b.hit(false);

                        if (b.Hitcounter >= 5)
                        {
                            defenseadelete.Add(b);
                        }
                        demonatkdelete.Add(d);
                    }
                }
            }
        }


        //supprime tous les objets qui doivent l'être
        public void removeobj()
        {
            foreach (skeleton a in skeletondelete)
            {
                skeletonwave.Remove(a);
            }
            foreach (demon a in demondelete)
            {
                demonwave.Remove(a);
            }
            foreach (skeletonatk s in skeletonatksdelete)
            {
                skeletonatks.Remove(s);
            }
            foreach (PlayerAtk a in atkdelete)
            {
                atk.Remove(a);
            }
            foreach (demonatk s in demonatkdelete)
            {
                demonatks.Remove(s);
            }
            foreach (bone_pile b in defenseadelete)
            {
                defense.Remove(b);
            }
            //reset les listes de suppression 
            skeletondelete.Clear();
            demondelete.Clear();
            skeletonatksdelete.Clear();
            atkdelete.Clear();
            demonatkdelete.Clear();
            defenseadelete.Clear();
        }

        public void newwave()
        {
            olddemon = 0;
            //diminue l'intervale entre les monstre, ce qui augmente le nombre de monstre
            wavecount++;
            if (wavecount % 2 == 0)
            {
                foreach (skeleton s in skeletonwave)
                {
                    s.Speed += 3;
                }
                foreach (demon d in demonwave)
                {
                    d.Speed += 3;
                }
            }
            demoninterval -= 50;
            //regenere touts les nouveaux monstres
            for (int i = 100; i <= Config.AIRSPACEWIDTH; i += demoninterval)
            {
                demonwave.Add(new demon(i, 100));
                skeletonwave.Add(new skeleton((int)((olddemon + i) / 2), 0));
                olddemon = i;
            }
            //verifie que aucun n'as été créé en dehors de l'écran
            isinscreen();
        }

        //verifie que tous les objets sont encore dans l'écran 
        public void isinscreen()
        {
            foreach (skeleton s in skeletonwave)
            {
                if (s.X < 0 || s.X > Config.AIRSPACEWIDTH - skeleton.Height || s.Y < 0 || s.Y > Config.AIRSPACEHEIGHT - skeleton.Height)
                {
                    skeletondelete.Add(s);
                }
            }
            foreach (demon d in demonwave)
            {
                if (d.X < 0 || d.X > Config.AIRSPACEWIDTH - demon.Height || d.Y < 0 || d.Y > Config.AIRSPACEHEIGHT - demon.Height)
                {
                    demondelete.Add(d);
                }
            }
            foreach (demonatk d in demonatks)
            {
                if (d.X < 0 || d.X > Config.AIRSPACEWIDTH - demonatk.Width || d.Y < 0 || d.Y > Config.AIRSPACEHEIGHT - demonatk.Height)
                {
                    demonatkdelete.Add(d);
                }
            }
            foreach (skeletonatk s in skeletonatks)
            {
                if (s.X < 0 || s.X > Config.AIRSPACEWIDTH - skeletonatk.Width || s.Y < 0 || s.Y > Config.AIRSPACEHEIGHT - skeletonatk.Height)
                {
                    skeletonatksdelete.Add(s);
                }
            }
            foreach (PlayerAtk p in atk)
            {

                if (p.X < 0 || p.X > Config.AIRSPACEWIDTH - PlayerAtk.Width || p.Y < 0 || p.Y > Config.AIRSPACEHEIGHT - PlayerAtk.Height)
                {
                    atkdelete.Add(p);

                }
            }
            foreach (bone_pile b in defense)
            {
                if (b.x < 0 || b.x > Config.AIRSPACEWIDTH - bone_pile.maxwidth || b.y < 0 || b.y > Config.AIRSPACEHEIGHT - bone_pile.maxheight)
                {
                    defenseadelete.Add(b);
                }
            }
            removeobj();
        }

        //supprime tous les ennemis de l'écran
        public void ClearWave()
        {

            skeletonwave.Clear();
            demonwave.Clear();
        }
    }
}