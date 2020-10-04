using KomodoApps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimsUI
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
