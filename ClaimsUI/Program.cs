using KomodoApps;
using System;

namespace Claims.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Komodo Apps - Claims Manager";
            IConsole console = new RealConsole();
            ClaimsManager claimsManager = new ClaimsManager(console);
            claimsManager.Run();
        }
    }
}
