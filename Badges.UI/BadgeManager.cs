using Badges.Data;
using KomodoApps;
using System.Collections.Generic;
using System.Text;

namespace Badges.UI
{
    public class BadgeManager : ConsoleManager
    {
        private BadgeRepository _badgeRepository = new BadgeRepository();

        public BadgeManager(IConsole console)
            : base(console, "Komodo Key Bagde Manager") {}
        
        public override void SetBanner()
        {
            _banner = @"                                _           ___           _                                                       
  /\ /\___  _ __ ___   ___   __| | ___     / __\ __ _  __| | __ _  ___    /\/\   __ _ _ __   __ _  __ _  ___ _ __ 
 / //_/ _ \| '_ ` _ \ / _ \ / _` |/ _ \   /__\/// _` |/ _` |/ _` |/ _ \  /    \ / _` | '_ \ / _` |/ _` |/ _ \ '__|
/ __ \ (_) | | | | | | (_) | (_| | (_) | / \/  \ (_| | (_| | (_| |  __/ / /\/\ \ (_| | | | | (_| | (_| |  __/ |   
\/  \/\___/|_| |_| |_|\___/ \__,_|\___/  \_____/\__,_|\__,_|\__, |\___| \/    \/\__,_|_| |_|\__,_|\__, |\___|_|   
                                                            |___/                                 |___/           
";
        }

        public override void RunMainMenu()
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

        public override void SeedData()
        {
            _badgeRepository.AddBadge(
                new Badge(
                    1,
                    new List<string>() { "A1", "A2" }
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

        public override void SetActionList()
        {
            _actions.Add(1, "Add a Badge");
            _actions.Add(2, "Edit a Badge");
            _actions.Add(3, "List all Badges");
            _actions.Add(4, "Exit");
        }

        private void EditBadge()
        {
            PrintBanner();

            _console.WriteLine("Edit Badge\n" +
                "================\n\n" + 
                "What is the badge number to update? ");

            if(int.TryParse(_console.ReadLine(), out int badgeID))
            {
                var foundBadge = _badgeRepository.GetBadgeByID(badgeID);

                if(foundBadge == null)
                {
                    _console.WriteLine($"\nA badge with Badge ID {badgeID} could not be found.\nPlease check the Badge ID and try again later.\n");
                    ReturnOrQuit();
                }
                else
                {
                    PrintBadgeDetails(foundBadge);
                    _console.WriteLine("\nWhat would you like to do?\n" +
                        "1. Add a door\n" +
                        "2. Remove a door\n" +
                        "3. Remove all doors\n");

                    if(int.TryParse(_console.ReadLine(), out int action))
                    {
                        switch (action)
                        {
                            case 1:
                                AddDoorToBadge(foundBadge);
                                break;
                            case 2:
                                RemoveDoorFromBadge(foundBadge);
                                break;
                            case 3:
                                RemoveAllDoorsFromBadge(foundBadge);
                                break;
                            default:
                                InvalidEditOption();
                                break;
                        }
                    }
                    else
                    {
                        InvalidEditOption();
                    }
                }
            }
            else
            {
                _console.WriteLine("\nInvalid input!!!\nPlease enter a valid Badge ID [Hint: must be a number]\n" +
                    "To try again with a valid input, enter [C] or any other key to abort.\n");

                var choice = _console.ReadLine();
                if (choice.ToLower() == "c")
                {
                    EditBadge();
                }
                else
                {
                    ReturnOrQuit();
                }
            }
        }

        private void AddDoorToBadge(Badge badge)
        {
            PrintBadgeDetails(badge);
            _console.WriteLine("\nList a door that it needs access to :");

            string input = _console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                _console.WriteLine("Invalid Input!!! Please try again.\nPress any key to continue.");
                AddDoorToBadge(badge);
            }
            else
            {
                bool added = _badgeRepository.AddDoorToBadge(badge.BadgeID, input);
                if (added)
                {
                    _console.WriteLine("\nDoor added to Badge Successfully!.");
                }
                else
                {
                    _console.WriteLine("\nCould not add door to Badge.\nPlease try agaon later.\n");
                }
                PrintBadgeDetails(badge);

                _console.WriteLine("\nWould you like to add another door? (y/n) :");
                string answer = _console.ReadLine().ToLower();
                if (answer == "y" || answer == "yes")
                {
                    AddDoorToBadge(badge);
                }
                else
                {
                    ReturnOrQuit();
                }
            }
        }

        private void RemoveAllDoorsFromBadge(Badge foundBadge)
        {
            if (foundBadge.Doors.Count > 0)
            {
                PrintBadgeDetails(foundBadge);
                bool removed = _badgeRepository.RemoveAllDoorsFromBadge(foundBadge.BadgeID);
                if (removed)
                {
                    _console.WriteLine("\nDoors removed from Badge Successfully!.");
                }
                else
                {
                    _console.WriteLine("\nCould not remove doors from Badge.\nPlease try agaon later.\n");
                }
                PrintBadgeDetails(foundBadge);
                ReturnOrQuit();
                
            }
            else
            {
                _console.WriteLine("This badge has no doors assigned to it.\nPress any key to continue.");
                EditBadge();
            }
        }

        private void RemoveDoorFromBadge(Badge badge)
        {
            if(badge.Doors.Count > 0)
            {
                PrintBadgeDetails(badge);
                _console.WriteLine("Which door would you like to remove? ");

                string input = _console.ReadLine();
                if (string.IsNullOrWhiteSpace(input)){
                    _console.WriteLine("Invalid Input!!! Please try again.\nPress any key to continue.");
                    RemoveDoorFromBadge(badge);
                }
                else
                {
                    bool removed = _badgeRepository.RemoveDoorFromBadge(badge.BadgeID, input);
                    if (removed)
                    {
                        _console.WriteLine("\nDoor removed from Badge Successfully!.");
                    }
                    else
                    {
                        _console.WriteLine("\nCould not remove door from Badge.\nPlease try agaon later.\n");
                    }
                    PrintBadgeDetails(badge);

                    if(badge.Doors.Count > 0)
                    {
                        _console.WriteLine("\nWould you like to remove another door? (y/n) :");
                        string answer = _console.ReadLine().ToLower();
                        if (answer == "y" || answer == "yes")
                        {
                            RemoveDoorFromBadge(badge);
                        }
                        else
                        {
                            ReturnOrQuit();
                        }
                    }
                    else
                    {
                        ReturnOrQuit();
                    }
                }
            }
            else
            {
                _console.WriteLine("This badge has no doors assigned to it.\nPress any key to continue.");
                EditBadge();
            }

        }

        private void AddBadge()
        {
            Badge newBadge = new Badge();
            PrintBanner();

            _console.WriteLine("Add New Badge\n" +
                "================\n\n");

            SetBadgeID(ref newBadge);
            SetBadgeDoors(ref newBadge);

            if (_badgeRepository.AddBadge(newBadge))
            {
                _console.WriteLine("\nBadge Added Successfully!.\n");
            }
            else
            {
                _console.WriteLine("\nCould not Add New Badge.\nPlease try again later.\n");
            }

            ReturnOrQuit();
        }

        private void SetBadgeDoors(ref Badge badge)
        {
            _console.WriteLine("\nList a door that it needs access to :");

            string input = _console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                badge.Doors.Add(input);

                _console.WriteLine("\n Any other doors (y/n)? :");

                string answer = _console.ReadLine().ToLower();

                if (answer == "y" || answer == "yes")
                {
                    SetBadgeDoors(ref badge);
                }
            }
            else
            {
                _console.WriteLine("\nInvalid input! Please enter a valid Door Name.\n" +
                    "To try again with a valid input, enter [C] or any other key to abort.\n");
                string response = _console.ReadLine();
                if (response.ToLower() == "c")
                {
                    SetBadgeDoors(ref badge);
                }
                else
                {
                    ReturnOrQuit();
                }
            }
        }

        private void SetBadgeID(ref Badge badge)
        {
            _console.WriteLine("\nWhat is the number on the badge? : ");

            string input = _console.ReadLine();

            if (int.TryParse(input, out int badgeID))
            {
                badge.BadgeID  = badgeID;
            }
            else
            {
                _console.WriteLine("\nInvalid input! The Badge ID must be a number\n" +
                    "To try again with a valid input, enter [C] or any other key to abort.\n");
                string response = _console.ReadLine();
                if (response.ToLower() == "c")
                {
                    SetBadgeID(ref badge);
                }
                else
                {
                    ReturnOrQuit();
                }
            }
        }

        private void ListAllBadges()
        {
            PrintBanner();

            string printOutput = "Badges List\n" +
                "================\n\n" +
                string.Format("{0,-10}{1,-100}\n",
                    "Badge #",
                    "Door Access");

            foreach (var badge in _badgeRepository.GetAllBadges())
            {
                printOutput += string.Format("{0,-10}{1,-100}\n",
                    badge.Key,
                    GetFormattedStringFromList(badge.Value));
            }

            _console.WriteLine(printOutput);
            ReturnOrQuit();
        }

        private void PrintBadgeDetails(Badge badge)
        {
            PrintBanner();

            string printOutput = (badge.Doors.Count > 0)
                ? $"Badge with ID  {badge.BadgeID} has access to door {GetFormattedStringFromList(badge.Doors)}.\n"
                : $"Badge with ID  {badge.BadgeID} has no doors assigned to it.\n";
            
            _console.WriteLine(printOutput);
        }

        private string GetFormattedStringFromList(List<string> doorsList)
        {
            if(doorsList.Count < 1)
            {
                return "";
            }

            StringBuilder doors = new StringBuilder();
            doors.Append("+");

            doorsList.ForEach((string door) => doors.AppendFormat(", {0}", door));

            return doors.ToString().Replace("+, ", "");
        }

        private void InvalidEditOption()
        {
            _console.WriteLine($"Invalid input!!! You need to choose between options 1 or 2\n" +
               "To try again with a valid input, enter [C] or any other key to abort.\n");

            var choice = _console.ReadLine();
            if (choice.ToLower() == "c")
            {
                EditBadge();
            }
            else
            {
                ReturnOrQuit();
            }
        }
    }
}
