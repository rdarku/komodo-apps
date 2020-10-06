using KomodoApps;
using System;

namespace Badges.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Komodo Apps - Badge Manager";
            IConsole console = new RealConsole();
            BadgeManager manager = new BadgeManager(console);
            manager.Run();
        }
    }
}
