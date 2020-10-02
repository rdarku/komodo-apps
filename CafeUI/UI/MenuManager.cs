using CafeData;
using System;
using System.Collections.Generic;
using System.Threading;

namespace CafeUI.UI
{
    public class MenuManager
    {
        private readonly MenuItemRepository _menuItemRepository = new MenuItemRepository();

        private readonly IConsole _console;

        private bool _keepRunning = true;

        public MenuManager(IConsole console)
        {
            _console = console;
        }

        public void Run()
        {
            SeedContent();
            RunMainMenu();
        }

        private void RunMainMenu()
        {
            while (_keepRunning)
            {
                PrintBanner();
                _console.WriteLine("Welcome to the Komodo Cafe!\n" +
                    "Please select an action from the action list below\n" +
                    "(1) Get all Menu Items\n" +
                    "(2) Find Menu Item By Meal Number\n" +
                    "(3) Add Menu Item\n" +
                    "(4) Remove Menu Item\n" +
                    "(5) Exit\n");

                string response = _console.ReadLine();

                if(int.TryParse(response, out int choice)){
                    switch (choice)
                    {
                        case 1:
                            DisplayMenuItemsList();
                            break;
                        case 2:
                            GetMenuItemByItemNumber();
                            break;
                        case 3:
                            AddMenuItem();
                            break;
                        case 4:
                            RemoveItem();
                            break;
                        case 5:
                            ExitApp();
                            break;
                        default:
                            InvalidInputPrompt();
                            break;
                    }
                }
                else
                {
                    InvalidInputPrompt();
                }
            }
        }

        private void AddMenuItem()
        {
            MenuItem newItem = new MenuItem();

            _console.WriteLine("\n--------------------------------------------------\n" + 
                "\tAdd Menu Item.\n" +
                "--------------------------------------------------\n");

            GetNewItemNumber(ref newItem);
            GetNewItemName(ref newItem);
            GetNewItemDescription(ref newItem);
            GetNewItemIngredients(ref newItem);
            GetNewItemPrice(ref newItem);

            var added = _menuItemRepository.AddMenuItem(newItem);

            if (added)
            {
                _console.WriteLine("\nMenu Item Added Successfully!!!\n");
            }
            else
            {
                _console.WriteLine("\nFailed to add Menu Item\n");
            }

            ReturnOrQuit();
        }

        private void GetNewItemNumber(ref MenuItem menuItem)
        {
            _console.WriteLine("Enter the Meal Number (Must be number) :\n");

            var input = _console.ReadLine();
            if (int.TryParse(input, out int mealNumber))
            {
                MenuItem foundItem = _menuItemRepository.GetMenuItemByMealNumber(mealNumber);
                if (foundItem != null)
                {
                    _console.WriteLine("Attention! An item with thus number already exists\n" +
                        "Please refer to the full menu list to confirm the item numbers.\n");

                    ReturnOrQuit();
                }
                else
                {
                    menuItem.MealNumber = mealNumber;
                }
            }
            else
            {
                _console.WriteLine("Invalid input!!! Meal Number has to be a whole number\n" +
                    "To try again with a valid input, enter [C] or any other key to abort.\n");
                var choice = _console.ReadLine();

                if (choice.ToLower() == "c")
                {
                    GetNewItemNumber(ref menuItem);
                }
                else
                {
                    ReturnOrQuit();
                }

            }
        }

        private void GetNewItemName(ref MenuItem menuItem)
        {
            _console.WriteLine("Enter the Meal Name :\n");

            var input = _console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                menuItem.MealName = input;
            }
            else
            {
                _console.WriteLine("Invalid input!!! Please type a valid name\n" +
                    "To try again with a valid input, enter [C] or any other key to abort.\n");
                var choice = _console.ReadLine();

                if (choice.ToLower() == "c")
                {
                    GetNewItemName(ref menuItem);
                }
                else
                {
                    ReturnOrQuit();
                }

            }
        }

        private void GetNewItemDescription(ref MenuItem menuItem)
        {
            _console.WriteLine("Enter the Meal Description (optional - Hit [Enter] to skip):\n");

            var input = _console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                menuItem.Description = input;
            }
        }

        private void GetNewItemIngredients(ref MenuItem menuItem)
        {
            _console.WriteLine("Add Meal Ingredients\n" +
                "----------------------\n" +
                $"{menuItem.Ingredients.Count} ingredients added so far.\n");
            _console.WriteLine("Enter a new Meal Ingredient (optional - Hit [X] to skip):\n");

            var input = _console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                if (input.ToLower() != "x")
                {
                    menuItem.Ingredients.Add(input);
                    _console.WriteLine("\n ingredient added successfully\n");
                    GetNewItemIngredients(ref menuItem);
                }
            }
            else
            {
                _console.WriteLine("Invalid input!!! Please type a valid ingredient\n" +
                    "To try again with a valid input, enter [C] or any other key to abort.\n");
                var choice = _console.ReadLine();

                if (choice.ToLower() == "c")
                {
                    GetNewItemPrice(ref menuItem);
                }
                else
                {
                    ReturnOrQuit();
                }
            }
        }

        private void GetNewItemPrice(ref MenuItem menuItem)
        {
            _console.WriteLine("\nEnter the Meal Price :\n");

            var input = _console.ReadLine();
            if (decimal.TryParse(input, out decimal price))
            {
                menuItem.Price = price;
            }
            else
            {
                _console.WriteLine("Invalid input!!! Please type a valid amount\n" +
                    "To try again with a valid input, enter [C] or any other key to abort.\n");
                var choice = _console.ReadLine();

                if (choice.ToLower() == "c")
                {
                    GetNewItemPrice(ref menuItem);
                }
                else
                {
                    ReturnOrQuit();
                }
            }
        }

        private void RemoveItem()
        {
            _console.WriteLine("\n--------------------------------------------------\n" +
                "\tRemove Menu Item.\n" +
                "--------------------------------------------------\n" +
                "Enter the Meal number (Must be number):\n");

            var input = _console.ReadLine();

            if(int.TryParse(input, out int itemNumber))
            {
                MenuItem foundItem = _menuItemRepository.GetMenuItemByMealNumber(itemNumber);

                if(foundItem != null)
                {
                    var deleted = _menuItemRepository.RemoveMenuItem(foundItem);

                    if (deleted)
                    {
                        _console.WriteLine("Menu item removed successfully!\n");
                    }
                    else
                    {
                        _console.WriteLine("Could not remove the menu item\n");
                    }
                }
                else
                {
                    _console.WriteLine("Menu item not found.\nYou might want to check the Full list first and then come back here.\n");
                }

                    ReturnOrQuit();
            }
            else
            {
                _console.WriteLine("Invalid input!!! You need to enter a valid Meal Number\n" +
                "To try again with a valid input, enter [C] or any other key to abort.\n");
                var choice = _console.ReadLine();
                if(choice.ToLower() == "c")
                {
                    RemoveItem();
                }
                else
                {
                    ReturnOrQuit();
                }
            }
        }

        private void InvalidInputPrompt()
        {
            _console.WriteLine("Invalid input!!! You need to choose between options 1, 2, 3, 4 or 5\n" +
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

        private void GetMenuItemByItemNumber()
        {
            _console.WriteLine("\tView Menu Item details.\n" +
                "----------------------------------------\n" +
                "Enter the Meal number (Must be number):\n");

            var input = _console.ReadLine();
            if(int.TryParse(input, out int mealNumber))
            {
                MenuItem foundItem = _menuItemRepository.GetMenuItemByMealNumber(mealNumber);
                DisplayItemDetails(foundItem);
            }
            else
            {
                _console.WriteLine("Invalid input!!! You need to enter a valid Meal Number\n" +
                    "To try again with a valid input, enter [C] or any other key to abort.\n");
                var choice = _console.ReadLine();
                if (choice.ToLower() == "c")
                {
                    GetMenuItemByItemNumber();
                }
                else
                {
                    ReturnOrQuit();
                }
                _console.ReadKey();
            }

            ReturnOrQuit();
        }

        private void DisplayMenuItemsList()
        {
            string header = String.Format("{0,-10}{1,-20}{2,10:C}",
                    "Meal #", "Meal Name", "Price");

            var some = "meal".ToLower();

            _console.WriteLine(header);
            
            _console.WriteLine("--------------------------------------------------");
            foreach (var item in _menuItemRepository.GetAllMenuItems())
            {
                _console.WriteLine(String.Format("{0,-10}{1,-20}{2,10:C}",
                    item.MealNumber, item.MealName, item.Price));
            }
            _console.WriteLine("--------------------------------------------------\n");

            ReturnOrQuit();
        }

        private void DisplayItemDetails(MenuItem menuItem)
        {
            _console.WriteLine($"\n     Meal #: {menuItem.MealNumber}\n" +
                $"       Name: {menuItem.MealName}\n" +
                $"Description: {menuItem.Description}\n");

            if(menuItem.Ingredients.Count > 0)
            {
                _console.WriteLine($"-----------------Ingredients------------------");

                foreach(var ingredient in menuItem.Ingredients)
                {
                    _console.WriteLine($"\t{ingredient}");
                }
            }
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
            _console.WriteLine("\nThanks you for visiting the Komodo Cafe'\n Please visit us again soon.\n Have a great day!\n");
            Thread.Sleep(3000);
        }

        private void SeedContent()
        {
            _menuItemRepository.AddMenuItem(
                new MenuItem(
                    1,
                    "Turkish Coffee",
                    "Authentic Turkish Coffee",
                    new List<string> {
                        "1 cup water (cold)",
                        "1 tablespoon extra finely ground coffee (powder consistency)",
                        "1/8 teaspoon ground cardamom (or 1 cardamom pod, crushed)",
                        "Optional: 1 teaspoon sugar (or more, to taste)"
                    },
                    10.5m
                )
            );
            _menuItemRepository.AddMenuItem(
                new MenuItem(
                    2,
                    "Pumpkin Spice Latte",
                    "Pumpkin spice latte is the fall drink we love (or sometimes just love to hate).",
                    new List<string> {
                        "1 cup milk",
                        "2 tablespoons unsweetened pumpkin puree.",
                        "1 tablespoon sugar, or to taste.",
                        "1 teaspoon ground cinnamon.",
                        "1/2 teaspoon ground ginger.",
                        "1/4 teaspoon ground nutmeg.",
                        "1/8 teaspoon ground cloves.",
                        "1/2 teaspoon vanilla",
                        "1/2 cup brewed espresso (or very strongly brewed coffee).",
                        "Sweetened whipped cream, for topping"
                    },
                    12.5m
                )
            );
            _menuItemRepository.AddMenuItem(
                new MenuItem(
                    3,
                    "Cafe Late",
                    "An Italian-style cafe latte",
                    new List<string> {
                        "1 tablespoon coffee",
                        "1 fluid ounce water",
                        "Optional: 1 1/2 fluid ounces (or 1 shot) of flavored simple syrup",
                        "3 fluid ounces milk (or more)"
                    },
                    8.5m
                )
            );
        }

        private void PrintBanner()
        {
            _console.Clear();
            _console.WriteLine(@"                                _           ___       __      
  /\ /\___  _ __ ___   ___   __| | ___     / __\__ _ / _| ___ 
 / //_/ _ \| '_ ` _ \ / _ \ / _` |/ _ \   / /  / _` | |_ / _ \
/ __ \ (_) | | | | | | (_) | (_| | (_) | / /__| (_| |  _|  __/
\/  \/\___/|_| |_| |_|\___/ \__,_|\___/  \____/\__,_|_|  \___|
                                                              
");
        }

    }
}
