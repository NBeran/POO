using Drones.Helpers;
using Drones.Model;
using player.Helpers;
using player.Model;
using System.Collections.Generic;
using System.Drawing.Drawing2D;

namespace player
{
    // La classe GameSpace représente le territoire au dessus duquel les drones peuvent voler
    // Il d'agit d'un formulaire (une fenêtre) qui montre une vue 2D depuis en dessus
    // Il n'y a donc pas de notion d'altitude qui intervient

    public partial class GameSpace : Form
    {
        private int skeletoncooldawn = 0;
        private int demoncooldown = 0;
        private int cooldawn = 0;
        private int wavecooldawn = 0;
        private int monsterinterval = 500;
        public static bool droite = false;
        public static bool gauche = false;
        public static bool haut = false;
        public static bool bas = false;
        public static bool tire = false;
        private static int score = 0;
        private static skeleton skeletonshooting;
        private static demon demonshooting;
        private bool gameover = false;
        List<PlayerAtk> attaques = new List<PlayerAtk>();
        List<PlayerAtk> atkdelete = new List<PlayerAtk>();

        List<skeleton> skeletonwave = new List<skeleton>();
        List<skeleton> skeletondelete = new List<skeleton>();
        List<skeletonatk> skeletonatks = new List<skeletonatk>();
        List<skeletonatk> skeletonatksdelete = new List<skeletonatk>();

        List<demon> demonwave = new List<demon>();
        List<demon> demondelete = new List<demon>();
        List<demonatk> demonatks = new List<demonatk>();
        List<demonatk> demonatkdelete = new List<demonatk>();

        static List<bone_pile> defense = new List<bone_pile>();
        List<bone_pile> defenseadelete = new List<bone_pile>();
        // La flotte est l'ensemble des drones qui évoluent dans notre espace aérien
        private Drone _player;

        BufferedGraphicsContext currentContext;
        BufferedGraphics airspace;

        internal List<skeleton> Skeletonwave { get => skeletonwave; set => skeletonwave = value; }
        public static int Score { get => score; set => score = value; }
        internal static List<bone_pile> Defense { get => defense; set => defense = value; }




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
            for (int i = 0; i <= Config.AIRSPACEWIDTH; i += monsterinterval)
            {
                demonwave.Add(new demon(i, 100));
            }
            for (int i = 0; i <= Config.AIRSPACEWIDTH; i += monsterinterval)
            {
                skeletonwave.Add(new skeleton(i, 0));
            }
            for (int i = 0; i <= Config.AIRSPACEWIDTH; i += 300)
            {
                defense.Add(new bone_pile(i, Config.limitheight - bone_pile.maxheight));
            }

        }

        // Affichage de la situation actuelle
        public void Render()
        {
            if (gameover)
            {

                airspace.Graphics.Clear(Color.Orange);
                airspace.Graphics.DrawString("Vous êtes morts", TextHelpers.DieFont, TextHelpers.writingBrush, Config.AIRSPACEWIDTH/5, Config.AIRSPACEHEIGHT/2);
                airspace.Graphics.DrawString("score : " + score.ToString(), TextHelpers.DieFont, TextHelpers.writingBrush, Config.AIRSPACEWIDTH / 5, Config.AIRSPACEHEIGHT / 2 + 50);
                airspace.Graphics.DrawString("Appuyez sur espace pour réessayer", TextHelpers.DieFont, TextHelpers.writingBrush, Config.AIRSPACEWIDTH / 5, Config.AIRSPACEHEIGHT / 2 + 100);


                airspace.Render();
            }
            else
            {


                airspace.Graphics.Clear(Color.IndianRed);
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
                foreach (demon demon in demonwave)
                {
                    demon.Render(airspace);
                }
                foreach (demonatk atk in demonatks)
                {
                    atk.Render(airspace);
                }
                foreach (bone_pile bone in defense)
                {
                    bone.Render(airspace);
                }
                airspace.Render();
            }
        }

        // Calcul du nouvel état après que 'interval' millisecondes se sont écoulées
        private void Update(int interval)
        {
            if (demonwave.Count <= 0 && skeletonwave.Count <= 0) 
            {
                wavecooldawn++;
                if (wavecooldawn > 20)
                {
                    wavecooldawn = 0;
                    newwave();
                }
            }
            cooldawn++;
            skeletoncooldawn += skeletonwave.Count;
            demoncooldown += demonwave.Count;
            if (skeletoncooldawn >= 30 && skeletonwave.Count >= 1)
            {
                skeletoncooldawn -= 30;
                skeletonshooting = randomhelper.rndskeleton(Skeletonwave);
                skeletonatk atk = new skeletonatk(skeletonshooting.X, skeletonshooting.Y);
                skeletonatks.Add(atk);
            }
            if (demoncooldown >= 35 && demonwave.Count >= 1)
            {
                demoncooldown -= 35;
                demonshooting = randomhelper.rnddemon(demonwave);
                demonatk atk = new demonatk(demonshooting.X, demonshooting.Y);
                demonatks.Add(atk);
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

            foreach (skeleton skeleton in skeletonwave)
            {
                try
                {
                    skeleton.update(interval);
                }
                catch
                {
                    _player.live = 0;
                }

            }
            foreach (skeletonatk skeletonatk in skeletonatks)
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

            foreach (demon demon in demonwave)
            {
                try
                {
                    demon.update(interval);
                }
                catch
                {
                    _player.live = 0;
                }
            }
            foreach (demonatk demonatk in demonatks)
            {
                try
                {
                    demonatk.update(interval);
                }
                catch
                {
                    demonatkdelete.Add(demonatk);
                }
            }

            checkhit();
            removeobj();
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
                    if (gameover)
                    {
                        gameover = false;
                        _player.live = 3;
                        _player.x = Config.AIRSPACEWIDTH / 2;
                        _player.y = (Config.AIRSPACEHEIGHT - Config.limitheight) / 2;
                        monsterinterval = 550;
                        foreach (PlayerAtk a in attaques)
                        {
                            atkdelete.Add(a);
                        }
                        foreach (skeleton s in skeletonwave)
                        {
                            skeletondelete.Add(s);
                        }
                        foreach(demon d in demonwave)
                        {
                            demondelete.Add(d);
                        }
                        foreach(demonatk d in demonatks)
                        {
                            demonatkdelete.Add(d);
                        }
                        foreach(skeletonatk d in skeletonatks)
                        {
                            skeletonatksdelete.Add(d);
                        }
                        foreach(bone_pile b in defense)
                        {
                            defenseadelete.Add(b);
                        }
                        for (int i = 0; i <= Config.AIRSPACEWIDTH; i += 300)
                        {
                            defense.Add(new bone_pile(i, Config.limitheight - bone_pile.maxheight));
                        }
                        removeobj();
                        newwave();
                    }
                    else
                    {
                        shoot();
                    }

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
            foreach (PlayerAtk p in attaques)
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
                        try
                        {
                            b.hit(true);
                        }
                        catch
                        {
                            defenseadelete.Add(b);
                        }
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
                    if (_player.live == 0)
                    {

                        gameover = true;
                    }
                }
                foreach (bone_pile b in defense)
                {
                    if (s.X + skeletonatk.Width >= b.x && s.X <= b.x + b.width && s.Y + skeletonatk.Height >= b.y && s.Y <= b.y + b.height)
                    {
                        try
                        {
                            b.hit(false);
                        }
                        catch
                        {
                            defenseadelete.Add(b);
                        }
                        skeletonatksdelete.Add(s);
                    }
                }
            }
            foreach (demonatk d in demonatks)
            {
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
                        try
                        {
                            b.hit(false);
                        }
                        catch
                        {
                            defenseadelete.Add(b);
                        }
                        demonatkdelete.Add(d);
                    }
                }
            }
        }


        public  void removeobj()
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
                attaques.Remove(a);
            }
            foreach (demonatk s in demonatkdelete)
            {
                demonatks.Remove(s);
            }
            foreach (bone_pile b in defenseadelete)
            {
                defense.Remove(b);
            }
        }

        public void newwave()
        {
            monsterinterval -= 50;
            for (int i = 0; i <= Config.AIRSPACEWIDTH; i += monsterinterval)
            {
                demonwave.Add(new demon(i, 100));
            }
            for (int i = 0; i <= Config.AIRSPACEWIDTH; i += monsterinterval)
            {
                skeletonwave.Add(new skeleton(i, 0));
            }
        }
    }
}