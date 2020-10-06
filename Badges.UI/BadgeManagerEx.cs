using Badges.Data;
using KomodoApps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Badges.UI
{
    public class BadgeManagerEx
    {
        private bool _keepRunning = true;

        private readonly IConsole _console;

        private BadgeRepository _badgeRepository = new BadgeRepository();

        protected Dictionary<int, string> _actions = new Dictionary<int, string>();

        public BadgeManagerEx(IConsole console)
        {
            _console = console;
        }

        public void Run()
        {
            SeedData();
            SetActionList();
            RunMainMenu();
        }

        private void SetActionList()
        {
            _actions.Add(1, "Add a Badge");
            _actions.Add(2, "Edit a Badge");
            _actions.Add(3, "List all Badges");
            _actions.Add(4, "Exit");
        }

        private void RunMainMenu()
        {
            while (_keepRunning)
            {
                PrintBanner();
                PrintMainMenu();

                string input = _console.ReadLine();

                switch (input.ToLower())
                {
                    case "1":
                        AddBadge();
                        break;
                    case "2":
                        EditBadge();
                        break;
                    case "3":
                        ListAllBadges();
                        break;
                    case "4":
                        ExitApp();
                        break;
                    default:
                        InvalidInputPrompt();
                        break;
                }
            }
        }


        private void AddBadge()
        {
            throw new NotImplementedException();
        }

        private void EditBadge()
        {
            throw new NotImplementedException();
        }

        private void ListAllBadges()
        {

        }

        private void PrintMainMenu()
        {
            _console.WriteLine("Main menu\n" +
                    "================\n" +
                    "Please select an action from the action list below");
            foreach (var action in _actions)
            {
                Console.WriteLine($"{action.Key}. {action.Value}");
            }
        }

        private void PrintBanner()
        {
            _console.Clear();
            _console.WriteLine(@"                                _           ___           _                                                       
  /\ /\___  _ __ ___   ___   __| | ___     / __\ __ _  __| | __ _  ___    /\/\   __ _ _ __   __ _  __ _  ___ _ __ 
 / //_/ _ \| '_ ` _ \ / _ \ / _` |/ _ \   /__\/// _` |/ _` |/ _` |/ _ \  /    \ / _` | '_ \ / _` |/ _` |/ _ \ '__|
/ __ \ (_) | | | | | | (_) | (_| | (_) | / \/  \ (_| | (_| | (_| |  __/ / /\/\ \ (_| | | | | (_| | (_| |  __/ |   
\/  \/\___/|_| |_| |_|\___/ \__,_|\___/  \_____/\__,_|\__,_|\__, |\___| \/    \/\__,_|_| |_|\__,_|\__, |\___|_|   
                                                            |___/                                 |___/           
");
        }

        private void ReturnOrQuit()
        {
            _console.WriteLine("\nHit [Enter] to return to the Main Menu or enter [Q] to Exit the program\n");

            var input = _console.ReadLine();
            switch (input.ToLower())
            {
                case "q":
                    ExitApp();
                    break;
                default:
                    RunMainMenu();
                    break;
            }
        }

        private void ExitApp()
        {
            _keepRunning = false;
            _console.WriteLine("\nThanks you for using the Komodo Claims Manager\n Have a great day!\n");
            Thread.Sleep(3000);
        }

        private void InvalidInputPrompt()
        {
            _console.WriteLine("Invalid input!!! You need to choose between options 1, 2, 3 or 4\n" +
               "To try again with a valid input, enter [C] or any other key to abort.\n");
            var choice = _console.ReadLine();
            if (choice.ToLower() == "c")
            {
                RunMainMenu();
            }
            else
            {
                ReturnOrQuit();
            }
        }

        private void SeedData()
        {
            _badgeRepository.AddBadge(
                new Badge(
                    1,
                    new List<string>() { "A1","A2"}
                )
            );

            _badgeRepository.AddBadge(
                new Badge(
                    2,
                    new List<string>() { "B1", "B2" }
                )
            );

            _badgeRepository.AddBadge(
                new Badge(
                    3,
                    new List<string>() { "C1", "C2" }
                )
            );
        }
    }
}
