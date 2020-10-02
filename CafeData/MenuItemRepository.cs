using System.Collections.Generic;

namespace CafeData
{
    public class MenuItemRepository
    {
        protected readonly List<MenuItem> _menu = new List<MenuItem>();

        public bool AddMenuItem(MenuItem menuItem)
        {
            int countBeforAdd = _menu.Count;
            _menu.Add(menuItem);
            return _menu.Count > countBeforAdd;
        }

        public List<MenuItem> GetAllMenuItems()
        {
            return _menu;
        }

        public MenuItem GetMenuItemByMealNumber(int mealNumber)
        {
            foreach(var mealItem in _menu)
            {
                if(mealItem.MealNumber == mealNumber)
                {
                    return mealItem;
                }
            }

            return null;
        }

        public bool UpdateMenuItem(int mealNumber, MenuItem menuItem)
        {
            var menuItemToUpdate = GetMenuItemByMealNumber(mealNumber);

            if(menuItemToUpdate != null)
            {
                menuItemToUpdate.Description = menuItem.Description;
                menuItemToUpdate.Ingredients = menuItem.Ingredients;
                menuItemToUpdate.MealName = menuItem.MealName;
                menuItemToUpdate.Price = menuItem.Price;
                menuItemToUpdate.MealNumber = menuItem.MealNumber;
                return true;
            }

            return false;
        }

        public bool RemoveMenuItem(MenuItem menuItem)
        {
            return _menu.Remove(menuItem);
        }
    }
}
