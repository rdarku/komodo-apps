using System.Collections.Generic;

namespace CafeData
{
    public class MenuItem
    {
        private int _mealNumber;

        public int MealNumber {
            get { return _mealNumber; }
            set { _mealNumber = value; }
        }

        public string MealName { get; set; }

        public string Description { get; set; }

        public List<string> Ingredients { get; set; }

        public decimal Price { get; set; }

        public MenuItem() { }

        public MenuItem(int mealNumber, string mealName, string description, List<string> ingredients, decimal price)
        {
            MealNumber = mealNumber;
            MealName = mealName;
            Description = description;
            Ingredients = ingredients;
            Price = price;
        }
    }
}
