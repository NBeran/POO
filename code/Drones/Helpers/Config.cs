namespace player.Helpers
{
    static public class Config
    {
        public static readonly int AIRSPACEWIDTH = 1500;        // largeur of the airspace
        public static readonly int AIRSPACEHEIGHT = 1000;      // hauteur of the airspace
        public static readonly int limitheight = AIRSPACEHEIGHT - (AIRSPACEHEIGHT / 5);       // limite de hauteur pour le joueur
    }
}
