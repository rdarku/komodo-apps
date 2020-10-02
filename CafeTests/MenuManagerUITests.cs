using System;
using System.Collections.Generic;
using CafeUI;
using CafeUI.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CafeTests
{
    [TestClass]
    public class MenuManagerUITests
    {
        [TestMethod]
        public void AddMenuItem_ShouldReturnTrue()
        {
            var commandList = new List<string> { "3", "10", "White Chocolate Mocha", "a sweet, creamy white chocolate latte", "3/4 cup milk (whole or low-fat)", "x", "10.91", "q" };

            var console = new FakeConsole(commandList);
            var program = new MenuManager(console);
            program.Run();

            Console.WriteLine(console.Output);

            Assert.IsTrue(console.Output.Contains("Menu Item Added Successfully!!!"));

        }

        [TestMethod]
        public void RemoveMenuItem_ShouldReturnTrue()
        {
            var commandList = new List<string> { "4", "3", "q" };

            var console = new FakeConsole(commandList);
            var program = new MenuManager(console);
            program.Run();

            Console.WriteLine(console.Output);

            Assert.IsTrue(console.Output.Contains("Menu item removed successfully!"));
        }

        [TestMethod]
        public void GetMenuItemByMealNumber_ShouldReturnCorrectData()
        {
            var commandList = new List<string> { "2", "3", "q" };

            var console = new FakeConsole(commandList);
            var program = new MenuManager(console);
            program.Run();

            Console.WriteLine(console.Output);

            Assert.IsTrue(console.Output.Contains("3 fluid ounces milk (or more)"));
        }

        [TestMethod]
        public void GetAllMenuItems_ShouldReturnCorrectData()
        {
            List<string> commandList = new List<string> { "1","x", "5" };
            FakeConsole console = new FakeConsole(commandList);
            MenuManager program = new MenuManager(console);
            program.Run();

            Console.WriteLine(console.Output);

            Assert.IsTrue(console.Output.Contains("Pumpkin Spice Latte"));
        }
    }
}
