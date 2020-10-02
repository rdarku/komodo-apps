using CafeUI.UI;
using System;

namespace CafeUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Komodo Apps - Cafe Manager";
            MenuManager menuManager = new MenuManager(new RealConsole());
            menuManager.Run();
        }
    }
}
