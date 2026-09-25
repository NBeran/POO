using Drones.Helpers;

namespace Drones
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            // Démarrage
            Application.Run(new AirSpace(new Drone(Config.AIRSPACEWIDTH / 2, Config.AIRSPACEHEIGHT / 2, "Joe")));
        }
    }
}