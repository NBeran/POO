namespace player.Helpers
{
    // Outils pour écrire du texte dans un environnement graphique
    internal static class TextHelpers
    {
        //toutes les fonts utilisées dans le jeu
        public static Font drawFont = new Font("Impact", 20);
        public static Font DieFont = new Font("Arial", 35);
        public static Font StartFont = new Font("Impact", 35, FontStyle.Bold);
        //toutes les couleurs utilisées dans le jeu
        public static SolidBrush writingBrush = new SolidBrush(Color.Black);
    }
}
