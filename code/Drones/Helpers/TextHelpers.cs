namespace player.Helpers
{
    // Outils pour écrire du texte dans un environnement graphique
    internal static class TextHelpers
    {
        public static Font drawFont = new Font("Impact", 20);
        public static Font DieFont = new Font("Arial", 35);
        public static Font StartFont = new Font("Impact", 35, FontStyle.Bold);
        public static SolidBrush writingBrush = new SolidBrush(Color.Black);
    }
}
