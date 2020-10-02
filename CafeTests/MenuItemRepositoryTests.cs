using CafeData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CafeTests
{
    [TestClass]
    public class MenuItemRepositoryTests
    {
        private MenuItemRepository _repository;
        private MenuItem _menuItem;

        [TestInitialize]
        public void Arrange()
        {
            _menuItem = new MenuItem(
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
                );

            _repository = new MenuItemRepository();

            _repository.AddMenuItem( _menuItem);
            _repository.AddMenuItem(
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
            _repository.AddMenuItem(
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

        [TestMethod]
        public void AddMenuItem_ShouldReturnTrue()
        {
            var newMenuItem = new MenuItem();
            bool addedSuccessfully = _repository.AddMenuItem(newMenuItem);
            Assert.IsTrue(addedSuccessfully);
        }

        [TestMethod]
        public void RemoveMenuItem_ShouldReturnTrue()
        {
            MenuItem foundMenuItem = _repository.GetMenuItemByMealNumber(1);
            bool removedSuccessfully = _repository.RemoveMenuItem(foundMenuItem);
            Assert.IsTrue(removedSuccessfully);
        }

        [TestMethod]
        public void GetMenuItemByMealNumber_ShouldReturnCorrectData()
        {
            MenuItem foundmenuItem = _repository.GetMenuItemByMealNumber(1);

            Assert.AreEqual(foundmenuItem, _menuItem);
        }

        [TestMethod]
        public void GetAllMenuItems_ShouldReturnCorrectData()
        {
            MenuItem newItem = new MenuItem();
            newItem.MealName = "Quarantine Coffe";

            _repository.AddMenuItem(newItem);
            var fullMenuList = _repository.GetAllMenuItems();

            Assert.IsTrue(fullMenuList.Contains(newItem));
        }

        [TestMethod]
        public void UpdateMenuItem_shouldReturnTrue()
        {
            var firstItem = _repository.GetMenuItemByMealNumber(1);
            firstItem.Price = 12.56m;

            bool updated = _repository.UpdateMenuItem(1, firstItem);

            Assert.IsTrue(updated);
        }
    }
}
