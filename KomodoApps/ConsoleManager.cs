using System;
using System.Collections.Generic;
using System.Threading;

namespace KomodoApps
{
    public abstract class ConsoleManager
    {
        protected string _appName;

        protected bool _keepRunning = true;

        protected readonly IConsole _console;

        protected Dictionary<int, string> _actions = new Dictionary<int, string>();

        protected string _banner;

        public ConsoleManager(IConsole console, string appName)
        {
            _appName = appName;
            _console = console;
        }

        public virtual void Run()
        {
            SeedData();
            SetActionList();
            SetBanner();
            RunMainMenu();
        }

        public abstract void SeedData();

        public abstract void SetBanner();

        public abstract void RunMainMenu();

        public abstract void SetActionList();

        protected void PrintBanner()
        {
            _console.Clear();
            _console.WriteLine(_banner);
        }

        protected void PrintMainMenu()
        {
            _console.WriteLine("Main menu\n" +
                    "================\n" +
                    "Please select an action from the action list below");
            foreach (var action in _actions)
            {
                Console.WriteLine($"{action.Key}. {action.Value}");
            }
        }

        protected void InvalidInputPrompt()
        {
            string optionsList = "";

            for (int i=1; i <= _actions.Count; i++) {
                if (i == _actions.Count)
                    optionsList += $" or {i}";
                else
                    optionsList += $" {i},";
            }

            _console.WriteLine($"Invalid input!!! You need to choose between options {optionsList}\n" +
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

        protected void ReturnOrQuit()
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

        protected void ExitApp()
        {
            _keepRunning = false;
            _console.WriteLine($"\nThanks you for using the {_appName}\n Have a great day!\n");
            Thread.Sleep(3000);
        }

    }
}
