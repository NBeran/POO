
using player.Model;

namespace player.Helpers
{
    public static class randomhelper
    {
        //variable random pour générer les futurs aléatoires
        private static Random random = new Random();

        //méthode pour choisir un squelette aléatoire dans une liste
        public static skeleton rndskeleton(List <skeleton> i)
        {
            int x = random.Next(i.Count);
            return i.ElementAt(x);
        }

        //méthode pour choisir un démon aléatoire dans une liste
        public static demon rnddemon(List<demon> i)
        {
            int x = random.Next(i.Count);
            return i.ElementAt(x);
        }
    }
}
