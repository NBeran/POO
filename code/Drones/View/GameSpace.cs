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
        private int monsterinterval = 350;
        public static bool right = false;
        public static bool left = false;
        public static bool up = false;
        public static bool down = false;
        public static bool shooting = false;
        private bool gamejustlunch = true;
        private static int score = 0;
        private static skeleton skeletonshooting;
        private static demon demonshooting;
        private bool gameover = false;
        List<PlayerAtk> atk = new List<PlayerAtk>();
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
            for (int i = 0; i <= Config.AIRSPACEWIDTH; i += 300)
            {
                defense.Add(new bone_pile(i, Config.limitheight - bone_pile.maxheight));
            }
            newwave();

        }

        // Affichage de la situation actuelle
        public void Render()
        {
            if (gamejustlunch)
            {
                airspace.Graphics.Clear(Color.Orange);
                airspace.Graphics.DrawString("Projet shoot them up", TextHelpers.StartFont, TextHelpers.writingBrush, Config.AIRSPACEWIDTH / 3, Config.AIRSPACEHEIGHT / 2 - Config.AIRSPACEHEIGHT/3);
                airspace.Graphics.DrawString("Noé Béran", TextHelpers.StartFont, TextHelpers.writingBrush, Config.AIRSPACEWIDTH /3 + 100, Config.AIRSPACEHEIGHT / 2);
                airspace.Graphics.DrawString("cliquer sur espace pour commencer", TextHelpers.StartFont, TextHelpers.writingBrush, Config.AIRSPACEWIDTH / 5 + 50, Config.AIRSPACEHEIGHT / 2 + 100);
                airspace.Render();
                
            }
            
            else if (gameover)
            {

                airspace.Graphics.Clear(Color.Orange);
                airspace.Graphics.DrawString("Vous êtes morts", TextHelpers.StartFont, TextHelpers.writingBrush, Config.AIRSPACEWIDTH / 3 + 35, Config.AIRSPACEHEIGHT/4);
                airspace.Graphics.DrawString("score : " + score.ToString(), TextHelpers.StartFont, TextHelpers.writingBrush, Config.AIRSPACEWIDTH / 3 + 100, Config.AIRSPACEHEIGHT / 2);
                airspace.Graphics.DrawString("Appuyez sur espace pour réessayer", TextHelpers.StartFont, TextHelpers.writingBrush, Config.AIRSPACEWIDTH / 5 + 50, Config.AIRSPACEHEIGHT / 2 + 100);


                airspace.Render();
            }
            else
            {


                airspace.Graphics.Clear(Color.IndianRed);
                airspace.Graphics.DrawString("score : " + score.ToString(), TextHelpers.drawFont, TextHelpers.writingBrush, 15, 15);
                airspace.Graphics.DrawString("vie : " + _player.live.ToString(), TextHelpers.drawFont, TextHelpers.writingBrush, 15, Config.AIRSPACEHEIGHT - 45);
                _player.Render(airspace);

                foreach (PlayerAtk atk in atk)
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
            _player.Update(interval, up, down, left, right);
            foreach (PlayerAtk a in atk)
            {
                    a.update(interval);
            }

            foreach (skeleton skeleton in skeletonwave)
            {
                
                
                    skeleton.update(interval);
                
                if (skeleton.Y + skeleton.Height >= Config.limitheight && GameSpace.Defense.Count <= 0)
                {
                    _player.live = 0;
                    gameover = true;
                }

            }
            foreach (skeletonatk skeletonatk in skeletonatks)
            {

                    skeletonatk.update(interval);         
            }

            foreach (demon demon in demonwave)
            {
                
                
                    demon.update(interval);
                
                 if (demon.Y + skeleton.Height >= Config.limitheight && GameSpace.Defense.Count <= 0)
                {
                    _player.live = 0;
                    gameover = true;
                }
            }
            foreach (demonatk demonatk in demonatks)
            {
                    demonatk.update(interval);      
            }

            checkhit();
            removeobj();
        }

        // Méthode appelée à chaque frame
        private void NewFrame(object sender, EventArgs e)
        {
            if (!gamejustlunch && !gameover)
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
                    if (gamejustlunch)
                    {
                        gamejustlunch = false;
                    }
                    else if (gameover)
                    {
                        gameover = false;
                        _player.live = 3;
                        score = 0;
                        _player.x = Config.AIRSPACEWIDTH / 2;
                        _player.y = (Config.AIRSPACEHEIGHT - Config.limitheight) / 2;
                        monsterinterval = 350;
                        foreach (PlayerAtk a in atk)
                        {
                            atkdelete.Add(a);
                        }
                        foreach (skeleton s in skeletonwave)
                        {
                            skeletondelete.Add(s);
                        }
                        foreach (demon d in demonwave)
                        {
                            demondelete.Add(d);
                        }
                        foreach (demonatk d in demonatks)
                        {
                            demonatkdelete.Add(d);
                        }
                        foreach (skeletonatk d in skeletonatks)
                        {
                            skeletonatksdelete.Add(d);
                        }
                        foreach (bone_pile b in defense)
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


            }
        }
        private void AirSpace_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
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

            }
        }
        public void shoot()
        {
            if (cooldawn >= 6)
            {
                atk.Add(new PlayerAtk(_player.x, _player.y));
                cooldawn = 0;
            }
        }
        public void checkhit()
        {
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
            isinscreen();
        }
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
    }
}