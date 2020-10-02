using CafeData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CafeUI.UI
{
    public class MenuManager
    {
        private readonly MenuItemRepository _menuItemRepository = new MenuItemRepository();

        private readonly IConsole _console;

        private bool _keepRunning = true;

        public MenuManager()
        {

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
                Console.Clear();

                Console.WriteLine("Welcome to the Komodo Cafe!\n" +
                    "Please select an action from the action list below\n" +
                    "(1) Get all Menu Items\n" +
                    "(2) Find Menu Item By Meal Number\n" +
                    "(3) Add Menu Item\n" +
                    "(4) Remove Menu Item\n" +
                    "(5) Exit\n");

                string response = Console.ReadLine();

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
                            AddItem();
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

        private void AddItem()
        {
            Console.WriteLine("\n--------------------------------------------------\n" + 
                "\tAdd Menu Item.\n" +
                "--------------------------------------------------\n" +
                "Enter the Meal number (Must be number):\n");

            var input = Console.ReadLine();
            if (int.TryParse(input, out int mealNumber))
            {
                MenuItem foundItem = _menuItemRepository.GetMenuItemByMealNumber(mealNumber);
                DisplayItemDetails(foundItem);
            }
            else
            {
                Console.WriteLine("Invalid input!!! You need to enter a valid Meal Number\n" +
                "Press any key to continue...");
                Console.ReadKey();
                GetMenuItemByItemNumber();
            }
        }

        private void RemoveItem()
        {
            Console.WriteLine("\n--------------------------------------------------\n" +
                "\tRemove Menu Item.\n" +
                "--------------------------------------------------\n" +
                "Enter the Meal number (Must be number):\n");

            var input = Console.ReadLine();

            if(int.TryParse(input, out int itemNumber))
            {
                MenuItem foundItem = _menuItemRepository.GetMenuItemByMealNumber(itemNumber);

                if(foundItem != null)
                {
                    var deleted = _menuItemRepository.RemoveMenuItem(foundItem);

                    if (deleted)
                    {
                        Console.WriteLine("Menu item removed successfully!\n");
                    }
                    else
                    {
                        Console.WriteLine("Could not remove the menu item");
                    }
                }
                else
                {
                    Console.WriteLine("Menu item not found.\nYou might want to check the Full list first and then come back here.");
                }

                    ReturnOrQuit();
            }
            else
            {
                Console.WriteLine("Invalid input!!! You need to enter a valid Meal Number\n" +
                "Press any key to continue...");
                Console.ReadKey();
                RemoveItem();
            }
        }

        private void InvalidInputPrompt()
        {
            Console.WriteLine("Invalid input!!! You need to choose between options 1, 2, 3, 4 or 5\n" +
                "Press any key to continue...");
            Console.ReadKey();
            RunMainMenu();
        }

        private void GetMenuItemByItemNumber()
        {
            Console.WriteLine("\tView Menu Item details.\n" +
                "----------------------------------------\n" +
                "Enter the Meal number (Must be number):\n");

            var input = Console.ReadLine();
            if(int.TryParse(input, out int mealNumber))
            {
                MenuItem foundItem = _menuItemRepository.GetMenuItemByMealNumber(mealNumber);
                DisplayItemDetails(foundItem);
            }
            else
            {
                Console.WriteLine("Invalid input!!! You need to enter a valid Meal Number\n" +
                "Press any key to continue...");
                Console.ReadKey();
                GetMenuItemByItemNumber();
            }

            ReturnOrQuit();
        }


        private void DisplayMenuItemsList()
        {
            string header = String.Format("{0,-10}{1,-20}{2,10:C}",
                    "Meal #", "Meal Name", "Price");

            var some = "meal".ToLower();

            Console.WriteLine(header);
            
            Console.WriteLine("--------------------------------------------------");
            foreach (var item in _menuItemRepository.GetAllMenuItems())
            {
                Console.WriteLine(String.Format("{0,-10}{1,-20}{2,10:C}",
                    item.MealNumber, item.MealName, item.Price));
            }
            Console.WriteLine("--------------------------------------------------\n");

            ReturnOrQuit();
        }

        private void ReturnOrQuit()
        {
            Console.WriteLine("\nEnter any key to return to the Main Menu or (Q) to Exit the program\n");

            var input = Console.ReadLine();
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
            Console.WriteLine("\nThanks you for visiting the Komodo Cafe'\n Please visit us again soon.\n Have a great day!\n");
            Thread.Sleep(3000);
        }

        private void DisplayItemDetails(MenuItem menuItem)
        {
            Console.WriteLine($"\n     Meal #: {menuItem.MealNumber}\n" +
                $"       Name: {menuItem.MealName}\n" +
                $"Description: {menuItem.Description}\n");

            if(menuItem.Ingredients.Count > 0)
            {
                Console.WriteLine($"-----------------Ingredients------------------");

                foreach(var ingredient in menuItem.Ingredients)
                {
                    Console.WriteLine($"\t{ingredient}");
                }
            }
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


    }
}
