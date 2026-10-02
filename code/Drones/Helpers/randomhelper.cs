
using Drones.Model;

namespace Drones.Helpers
{
    internal static class randomhelper
    {
        private static Random random = new Random();
        public static skeleton rndskeleton(List <skeleton> i)
        {
            int x = random.Next(i.Count);
            return i.ElementAt(x);
        }
        public static demon rnddemon(List<demon> i)
        {
            int x = random.Next(i.Count);
            return i.ElementAt(x);
        }
    }
}
